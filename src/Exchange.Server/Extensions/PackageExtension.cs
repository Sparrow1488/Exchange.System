using Exchange.System.Packages.Default;

namespace Exchange.Server.Extensions
{
    public static class PackageExtension
    {
        public static T As<T>(this IPackage package)
            where T : IPackage 
                => (T)package;
    }
}
