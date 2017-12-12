using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eShopOnContainers.Core.Helpers
{
    public static class ExceptionHelpers
    {
        public static string ToMessage(this WebExceptionStatus status)
        {
            switch (status)
            {
                    case WebExceptionStatus.ConnectFailure:
                    case WebExceptionStatus.MessageLengthLimitExceeded:
                    case WebExceptionStatus.SendFailure:
                        return "We cannot connect to Catalog Services";
                case WebExceptionStatus.Pending:
                case WebExceptionStatus.Success:
                case WebExceptionStatus.RequestCanceled:
                    return "We cannot connect to services in this moment. Try again later.";
                default:
                    return "An error ocurred.";
            }
        }
    }
}
