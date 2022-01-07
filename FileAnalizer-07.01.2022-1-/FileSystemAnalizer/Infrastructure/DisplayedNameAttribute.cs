using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.Infrastructure
{
    public class DisplayedNameAttribute : Attribute
    {
        public readonly string Name;
        public DisplayedNameAttribute(string name)
            => Name = name;
    }
}
