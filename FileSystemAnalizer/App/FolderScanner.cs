using FileSystemAnalizer.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.App
{
    public class FolderScanner : Scanner<string, FolderScannedData>
    {
        public FolderScanner() : base(path => Directory.Exists(path)) { }

        protected override FolderScannedData Scan(string path)
        {
            var result = new FolderScannedData(path);
            result.InspectAll();
            return result;
        }
    }
}
