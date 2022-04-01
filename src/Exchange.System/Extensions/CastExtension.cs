using Exchange.System.Packages;
using System;

namespace Exchange.System.Extensions
{
    public static class CastExtension
    {
        public static T As<T>(this Package package)
            where T : Package 
                => (T)package;

        public static T As<T>(this Request request)
            where T : Request
                => (T)request;
    }
}
