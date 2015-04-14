using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Isah.Core.Logging;
using log4net;

namespace Isah.Core.Http
{
    public class IsahHttpClient : IIsahHttpClient
    {
        // ReSharper disable once InconsistentNaming
        protected readonly HttpClient _client;
        private ILog _logger;
        protected AuthenticationMethod _authenticationMethod;
        private String _authenticationToken;
        private String _userName;

        public IsahHttpClient(Uri serviceUri, AuthenticationMethod authMethod, String authToken, String userName, ILog logger = null)
        {
            var cookies = new CookieContainer();

            if (logger == null) _logger = new NullLogger();
            else _logger = logger;
            //needed for fiddler !
            // ReSharper disable once UnusedVariable
            var handler = new HttpClientHandler
            {
                CookieContainer = cookies,
                UseCookies = true,
                UseDefaultCredentials = false,
                Proxy = new WebProxy("http://localhost:8888", false, new string[] { }),
                UseProxy = true,
            };

            _authenticationMethod = authMethod;
            _authenticationToken = authToken;
            _userName = userName;
            _client = new HttpClient { BaseAddress = serviceUri };
            //_logger.Info(@"HttpClient instantiated , BaseAddress : " + serviceUri);

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
        }

        [ExcludeFromCodeCoverage]
        public virtual async Task<AuthenticationMethod> RequestAuthenticationMethod(string getRequestUri)
        {
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + getRequestUri).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return AuthenticationMethod.None;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized && response.Headers.WwwAuthenticate != null)
            {
                if (response.Headers.WwwAuthenticate.Any(h => h.Scheme == "Basic"))
                {
                    return AuthenticationMethod.Basic;
                }

                if (response.Headers.WwwAuthenticate.Any(h => h.Scheme == "Bearer"))
                {
                    return AuthenticationMethod.Bearer;
                }
            }

            //error
            _logger.Error(@"RequestAuthenticationMethod unsuccessful, URL : " + _client.BaseAddress + getRequestUri + " Status code : " + response.StatusCode);
            throw new WebFaultException<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
        }

        [ExcludeFromCodeCoverage]
        public virtual async Task<T> GetAsync<T>(string requestUri)
        {
            requestUri = requestUri.FormatUrl();

            HttpResponseMessage response = null;

            switch (_authenticationMethod)
            {

                default:
                case AuthenticationMethod.None:
                    response = await _client.GetAsync(_client.BaseAddress + requestUri).ConfigureAwait(false);
                    break;

                case AuthenticationMethod.Basic:
                    var encoder = new UTF8Encoding();
                    var data = encoder.GetBytes(_userName + @":none");
                    _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Basic " + Convert.ToBase64String(data));
                    response = await _client.GetAsync(_client.BaseAddress + requestUri).ConfigureAwait(false);
                    break;

                case AuthenticationMethod.Bearer:
                    //get auth token !!!!
                    //var authclient = new AuthenticationHttpClient(new Uri(@"http://localhost/AuthenticationService/"), _logger);
                    //var token = authclient.GetAsync<String>("users/0001/authenticationtoken/sfctoken");
                    _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _authenticationToken);
                    response = await _client.GetAsync(_client.BaseAddress + requestUri).ConfigureAwait(false);
                    break;
            }

            if (response.IsSuccessStatusCode)
            {
                _logger.Debug(@"GetAsync<" + typeof(T) + "> successful, URL : " + _client.BaseAddress + requestUri);
                return await response.Content.ReadAsAsync<T>();
            }
            else
            {
                //error
                var result = await response.Content.ReadAsStringAsync();
                _logger.Error(@"GetAsync<" + typeof(T) + "> unsuccessful, URL : " + _client.BaseAddress + requestUri + " Status code : " + response.StatusCode);
                if (String.IsNullOrEmpty(result))
                    throw new WebFaultException(response.StatusCode);
                throw new WebFaultException<string>(result, response.StatusCode);
            }

        }

        [ExcludeFromCodeCoverage]
        public virtual async Task<Stream> GetStreamAsync(string requestUri)
        {
            requestUri = requestUri.FormatUrl();

            HttpResponseMessage response = null;

            switch (_authenticationMethod)
            {
                default:
                case AuthenticationMethod.None:
                    response = await _client.GetAsync(_client.BaseAddress + requestUri).ConfigureAwait(false);
                    break;

                case AuthenticationMethod.Basic:
                    var encoder = new UTF8Encoding();
                    var data = encoder.GetBytes(_userName + ":none");
                    _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Basic " + Convert.ToBase64String(data));
                    response = await _client.GetAsync(_client.BaseAddress + requestUri).ConfigureAwait(false);
                    break;

                case AuthenticationMethod.Bearer:
                    _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _authenticationToken);
                    response = await _client.GetAsync(_client.BaseAddress + requestUri).ConfigureAwait(false);
                    break;

            }

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                _logger.Debug(@"GetStreamAsync successful, URL : " + _client.BaseAddress + requestUri);
                return result;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                _logger.Error(@"GetStreamAsync unsuccessful, URL : " + _client.BaseAddress + requestUri + " Status code : " + response.StatusCode);
                if (String.IsNullOrEmpty(result))
                    throw new WebFaultException(response.StatusCode);
                throw new WebFaultException<string>(result, response.StatusCode);
            }
        }

        [ExcludeFromCodeCoverage]
        public virtual async Task<string> PostAsync<T>(string requestUri, T content)
        {
            requestUri = requestUri.FormatUrl();

            HttpResponseMessage response = null;

            switch (_authenticationMethod)
            {
                default:
                case AuthenticationMethod.None:
                    response = await _client.PostAsXmlAsync(_client.BaseAddress + requestUri, content).ConfigureAwait(false);
                    break;

                case AuthenticationMethod.Basic:
                    var encoder = new UTF8Encoding();
                    var data = encoder.GetBytes(_userName + ":none");
                    _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Basic " + Convert.ToBase64String(data));
                    response = await _client.PostAsXmlAsync(_client.BaseAddress + requestUri, content).ConfigureAwait(false);
                    break;

                case AuthenticationMethod.Bearer:
                    _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _authenticationToken);
                    response = await _client.PostAsXmlAsync(_client.BaseAddress + requestUri, content).ConfigureAwait(false);
                    break;

            }

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                _logger.Debug(@"PostAsync<" + typeof(T) + "> successful, URL : " + _client.BaseAddress + requestUri + ", CONTENT : " + content ?? "null");
                return result;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                _logger.Error(@"PostAsync<" + typeof(T) + "> unsuccessful, URL : " + _client.BaseAddress + requestUri + " Status code : " + response.StatusCode + ", CONTENT : " + content ?? "null");
                if (String.IsNullOrEmpty(result))
                    throw new WebFaultException(response.StatusCode);
                throw new WebFaultException<string>(result, response.StatusCode);
            }
        }

        [ExcludeFromCodeCoverage]
        public virtual async Task<string> PostAsyncHttpContent(string requestUri, HttpContent content)
        {
            requestUri = requestUri.FormatUrl();

            HttpResponseMessage response = null;

            switch (_authenticationMethod)
            {
                default:
                case AuthenticationMethod.None:
                    response = await _client.PostAsync(_client.BaseAddress + requestUri, content).ConfigureAwait(false);
                    break;

                case AuthenticationMethod.Basic:
                    var encoder = new UTF8Encoding();
                    var data = encoder.GetBytes(_userName + ":none");
                    _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Basic " + Convert.ToBase64String(data));
                    response = await _client.PostAsync(_client.BaseAddress + requestUri, content).ConfigureAwait(false);
                    break;

                case AuthenticationMethod.Bearer:
                    _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _authenticationToken);
                    response = await _client.PostAsync(_client.BaseAddress + requestUri, content).ConfigureAwait(false);
                    break;

            }

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                _logger.Debug(@"PostAsync<" + typeof(HttpContent) + "> successful, URL : " + _client.BaseAddress + requestUri + ", CONTENT : " + content ?? "null");
                return result;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                _logger.Error(@"PostAsync<" + typeof(HttpContent) + "> unsuccessful, URL : " + _client.BaseAddress + requestUri + " Status code : " + response.StatusCode + ", CONTENT : " + content ?? "null");
                if (String.IsNullOrEmpty(result))
                    throw new WebFaultException(response.StatusCode);
                throw new WebFaultException<string>(result, response.StatusCode);
            }
        }

        [ExcludeFromCodeCoverage]
        public virtual async Task<string> PutAsync<T>(string requestUri, T content)
        {
            requestUri = requestUri.FormatUrl();

            HttpResponseMessage response = null;

            switch (_authenticationMethod)
            {
                default:
                case AuthenticationMethod.None:
                    response = await _client.PutAsXmlAsync(_client.BaseAddress + requestUri, content).ConfigureAwait(false);
                    break;

                case AuthenticationMethod.Basic:
                    var encoder = new UTF8Encoding();
                    var data = encoder.GetBytes(_userName + ":none");
                    _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Basic " + Convert.ToBase64String(data));
                    response = await _client.PutAsXmlAsync(_client.BaseAddress + requestUri, content).ConfigureAwait(false);
                    break;

                case AuthenticationMethod.Bearer:
                    _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _authenticationToken);
                    response = await _client.PutAsXmlAsync(_client.BaseAddress + requestUri, content).ConfigureAwait(false);
                    break;

            }

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                _logger.Debug(@"PutAsync<" + typeof(T) + "> successful, URL : " + _client.BaseAddress + requestUri + ", CONTENT : " + content ?? "null");
                return result;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                _logger.Error(@"PutAsync<" + typeof(T) + "> unsuccessful, URL : " + _client.BaseAddress + requestUri + " Status code : " + response.StatusCode + ", CONTENT : " + content ?? "null");
                if (String.IsNullOrEmpty(result))
                    throw new WebFaultException(response.StatusCode);
                throw new WebFaultException<string>(result, response.StatusCode);
            }
        }

        [ExcludeFromCodeCoverage]
        public virtual async Task<string> DeleteAsync(string requestUri)
        {
            requestUri = requestUri.FormatUrl();

            HttpResponseMessage response = null;

            switch (_authenticationMethod)
            {
                default:
                case AuthenticationMethod.None:
                    response = await _client.DeleteAsync(_client.BaseAddress + requestUri).ConfigureAwait(false);
                    break;

                case AuthenticationMethod.Basic:
                    var encoder = new UTF8Encoding();
                    var data = encoder.GetBytes(_userName + ":none");
                    _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Basic " + Convert.ToBase64String(data));
                    response = await _client.DeleteAsync(_client.BaseAddress + requestUri).ConfigureAwait(false);
                    break;

                case AuthenticationMethod.Bearer:
                    _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + _authenticationToken);
                    response = await _client.DeleteAsync(_client.BaseAddress + requestUri).ConfigureAwait(false);
                    break;

            }

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                _logger.Debug(@"DeleteAsync successful, URL : " + _client.BaseAddress + requestUri);
                return result;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                _logger.Error(@"DeleteAsync unsuccessful, URL : " + _client.BaseAddress + requestUri + " Status code : " + response.StatusCode);
                if (String.IsNullOrEmpty(result))
                    throw new WebFaultException(response.StatusCode);
                throw new WebFaultException<string>(result, response.StatusCode);
            }

        }

        public Uri GetServiceUri()
        {
            return _client.BaseAddress;
        }

        public bool IsTokenExpired()
        {
            if (!String.IsNullOrWhiteSpace(_authenticationToken))
            {
                //check token expiration
                var tokenPayload = _authenticationToken.Split('.')[1];
                if (tokenPayload.Length % 4 != 0) tokenPayload += new string('=', 4 - tokenPayload.Length % 4); //padding for base64
                var jsonToken = Json.Decode(Encoding.UTF8.GetString(Convert.FromBase64String(tokenPayload)));

                //get expiry time
                // Unix timestamp is seconds past epoch
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                DateTime expiryDateTime = dtDateTime.AddSeconds(jsonToken.exp).ToLocalTime();

                if ((expiryDateTime - new TimeSpan(0, 0, 10, 0)) < DateTime.Now)
                {
                    //expires in 10 minutes
                    return true;
                }
            }
            return false;

        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
