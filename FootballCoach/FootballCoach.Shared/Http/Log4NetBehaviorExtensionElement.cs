using System;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel.Configuration;

namespace Isah.Core.Http
{
    [ExcludeFromCodeCoverage]
    public class Log4NetBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(Log4NetServiceBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new Log4NetServiceBehavior();
        }
    }
}