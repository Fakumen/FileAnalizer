using FileSystemAnalizer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.App
{
    public interface IScanDataTreeNode<out TScanData>
        where TScanData : IScanData
    {
        TScanData ScanData { get; }
    }
}
