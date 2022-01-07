using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.Infrastructure
{
    public interface ITree<out TTree, out TLeave>
        where TTree : ITree<TTree, TLeave>
        where TLeave : ITreeLeave
    {
        IEnumerable<TTree> Trees { get; }
        IEnumerable<TLeave> Leaves { get; }
    }

    public interface ITreeLeave
    {

    }
}
