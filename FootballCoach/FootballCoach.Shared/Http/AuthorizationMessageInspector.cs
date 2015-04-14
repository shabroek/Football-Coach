using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using log4net;
using System.Linq;
using System.ServiceModel.Channels;

namespace Isah.Core.Http
{
    public class AuthorizationMessageInspector : IDispatchMessageInspector
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private struct MessageInpectionData
        {
            public bool Authenticated;
        }

        private String _userName = "IsahUser";

        public AuthorizationMessageInspector()
        {

        }

        private struct CorrelationState
        {
            public MessageInpectionData data;
            public String requestid;
        }

        [ExcludeFromCodeCoverage]
        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            String requestid = (new Random()).Next(10000, 99999).ToString(); //DateTime.Now.Ticks.ToString();

            log4net.ThreadContext.Properties["RequestId"] = requestid;
            log4net.ThreadContext.Properties["UserId"] = _userName;
            
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string address = endpoint.Address;

            _logger.Info("Request Received From : " + address +" To : "+ request.Headers.To);
            var state = new CorrelationState();
            state.requestid = requestid;
            var headers = ((HttpRequestMessageProperty)request.Properties["httpRequest"]).Headers;
            state.data = new MessageInpectionData { Authenticated = false };

            var userAgent = headers["User-Agent"];
            if (headers["Authorization"] != null)
            {
                var authentication = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(headers["Authorization"].Replace("Basic", "").Trim()));
                _userName = authentication.Split(':').First();
                if (!String.IsNullOrEmpty(_userName)) state.data.Authenticated = true;
            }
            instanceContext.Extensions.Add(new IsahUserExtension { UserName = _userName, RequestId = requestid, Authenticated = state.data.Authenticated });
            
            return state;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {

            if (reply.IsFault)
            {
                _logger.Error("Request Error : " + reply.ToString());
            }

            _logger.Info("Request Handled.");


            // add authorization : WWW-Authenticate: Basic realm="insert realm"

            //if (!((CorrelationState)correlationState).data.Authenticated)
            //{
            //    if (reply.Properties["httpResponse"] != null)
            //    {
            //        ((HttpResponseMessageProperty) reply.Properties["httpResponse"]).Headers.Add("WWW-Authenticate", "Basic realm=\"Isah\"");
            //    }
            //    ((HttpResponseMessageProperty)reply.Properties["httpResponse"]).StatusCode = HttpStatusCode.Unauthorized;
            //    ((HttpResponseMessageProperty)reply.Properties["httpResponse"]).SuppressEntityBody = true;
            //}
        }
    }

    [ExcludeFromCodeCoverage]
    public class IsahUserExtension : IExtension<InstanceContext>
    {

        public String UserName;
        public String RequestId;
        public bool Authenticated;

        public void Attach(InstanceContext owner)
        {

        }

        public void Detach(InstanceContext owner)
        {

        }
    }


}