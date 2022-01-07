using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.Domain
{
    public static class DomainEnumerableExtensions
    {
        public static IEnumerable<TInfo> Filter<TInfo>(this IEnumerable<TInfo> fileSystemInfos, bool skipSystemEntries)
            where TInfo : FileSystemInfo
        {
            foreach (var info in fileSystemInfos)
            {
                if (!skipSystemEntries || !info.Attributes.HasFlag(FileAttributes.System))
                    yield return info;
            }
        }

        public static IEnumerable<T> SafeEnumerate<T>(this IEnumerable<T> collection)
        {
            foreach (var e in collection)
            {
                try
                {
                    var eTest = e;
                }
                catch
                {
                    continue;
                }
                yield return e;
            }
        }
    }
}
