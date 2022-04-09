using ExchangeSystem.Helpers;
using System.Linq;

namespace Exchange.System.Helpers
{
    public static class QueryParser
    {
        public static string GetAction(string query)
        {
            Ex.ThrowIfEmptyOrNull(query);
            string action;
            var queryChapters = query.Split("/");
            if (queryChapters.Length > 1)
                action = queryChapters[1];
            else
                action = queryChapters[0];
            return action;
        }

        public static string GetController(string query)
        {
            Ex.ThrowIfEmptyOrNull(query);
            var chapters = query.Split("/");
            string controller = chapters.FirstOrDefault();
            return controller ?? string.Empty;
        }
    }
}
