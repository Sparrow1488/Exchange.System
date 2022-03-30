using Exchange.System.Packages.Primitives;

namespace Exchange.Server.Extensions
{
    public static class PackageExtension
    {
        public static T As<T>(this IPackage package)
            where T : IPackage 
                => (T)package;
    }
}
