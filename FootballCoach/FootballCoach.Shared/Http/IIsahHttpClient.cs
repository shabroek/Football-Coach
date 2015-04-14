using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Isah.Core.Http
{
    public interface IIsahHttpClient : IDisposable
    {
        Task<Stream> GetStreamAsync(string requestUri);
        Task<T> GetAsync<T>(string requestUri);
        Task<string> PostAsync<T>(string requestUri, T content);
        Task<string> PostAsyncHttpContent(string requestUri, HttpContent content);
        Task<string> PutAsync<T>(string requestUri, T content);
        Task<string> DeleteAsync(string requestUri);
        Uri GetServiceUri();
        Task<AuthenticationMethod> RequestAuthenticationMethod(string getRequestUri);
        bool IsTokenExpired();
    }
}
