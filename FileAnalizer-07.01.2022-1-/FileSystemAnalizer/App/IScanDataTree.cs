using FileSystemAnalizer.Domain;
using FileSystemAnalizer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.App
{
    public interface IScanDataTree<TTreeNode>
        where TTreeNode : IScanDataTreeNode<IScanData>
    {
        void AddNode(TTreeNode node);

        void Clear();
    }
}
