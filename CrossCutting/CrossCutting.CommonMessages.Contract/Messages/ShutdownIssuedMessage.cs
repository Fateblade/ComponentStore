using System;

namespace Fateblade.Components.CrossCutting.CommonMessages.Contract.Messages
{
    public class ShutdownIssuedMessage
    {
        public Type Issuer { get; set; }
        public string Reason { get; set; }
    }
}
