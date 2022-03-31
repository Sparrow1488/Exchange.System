using Exchange.System.Packages;

namespace Exchange.Server.Extensions
{
    public static class PackageExtension
    {
        public static T As<T>(this Package package)
            where T : Package 
                => (T)package;
    }
}
