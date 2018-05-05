using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace YogaApi.Core.Models
{
    [Serializable]
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception exception) : base(message, exception) { }

        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            base.GetObjectData(info, context);
        }
    }
}
