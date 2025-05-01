using System.Text;

namespace Quiron.Extensions
{
    public static class ExceptionExtension
    {
        public static string AggregateMessage(this Exception ex)
        {
            var sbExcept = new StringBuilder();
            sbExcept.AppendLine(ex.Message);
            var inner = (ex.InnerException is not null);
            var innerEx = ex.InnerException;

            while (inner)
            {
                sbExcept.AppendLine($"   ---->>>> {innerEx?.Message} ");
                inner = (innerEx?.InnerException is not null);
                innerEx = innerEx?.InnerException;
            }

            return sbExcept.ToString();
        }
    }
}