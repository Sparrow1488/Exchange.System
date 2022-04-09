using Exchange.System.Packages;

namespace Exchange.System.Extensions
{
    public static class CastExtension
    {
        public static T As<T>(this Request request)
            where T : Request
                => (T)request;
    }
}
