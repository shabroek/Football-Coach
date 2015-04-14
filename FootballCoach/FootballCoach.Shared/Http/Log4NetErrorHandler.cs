using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using System.Security.Authentication;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using log4net;

namespace Isah.Core.Http
{
    [ExcludeFromCodeCoverage]
    public class Log4NetErrorHandler : IErrorHandler
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool HandleError(Exception error)
        {
            _logger.Error("An unexpected has occurred.", error);
            return false; // Exception has to pass the stack further
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is AuthenticationException)
            {
                fault = Message.CreateMessage(version, "none");
                fault.Properties.Add("httpResponse", new HttpResponseMessageProperty());
                if (fault.Properties["httpResponse"] != null)
                {
                    ((HttpResponseMessageProperty)fault.Properties["httpResponse"]).Headers.Add("WWW-Authenticate", "Basic realm=\"Isah\"");
                }
                ((HttpResponseMessageProperty)fault.Properties["httpResponse"]).StatusCode = HttpStatusCode.Unauthorized;
                ((HttpResponseMessageProperty)fault.Properties["httpResponse"]).SuppressEntityBody = true;
            }
            else if (!(error is WebFaultException) && !(error is WebFaultException<string>))
            {
                var ex = new WebFaultException<String>(error.ToString(), HttpStatusCode.InternalServerError);
                MessageFault flt = ex.CreateMessageFault();
                fault = Message.CreateMessage(version, flt, "");
            }
        }
    }
}
