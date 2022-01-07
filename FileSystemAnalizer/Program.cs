using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileSystemAnalizer.App;
using FileSystemAnalizer.UI;

namespace FileSystemAnalizer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var path = @"D:\GAMES";
            //var scanner = new FolderScanner();
            //var node = new FolderDataNode(scanner.TryScan(path));
            //var sizeUnits = node.ScanData.Size.BestFittingUnits;
            //Console.WriteLine($"{node.ScanData.Size.GetInUnits(sizeUnits)} {sizeUnits}");
            //Console.ReadLine();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FileAnalizerForm());
        }
    }
}
