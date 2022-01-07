using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.Infrastructure
{
    public interface ILazyDataLoader<TData>
    {
        bool IsDataReady { get; }
        event Action<TData> DataReady;
    }

    public class ObjectNotReadyException : Exception
    {
        public ObjectNotReadyException() : this("Object was not ready for use.") { }

        public ObjectNotReadyException(string message) : base(message)
        {

        }
    }
}
