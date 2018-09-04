using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PicSortLibrary;
using System.IO;

namespace PicSortConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() == 3)
            {
                string source = args[0];
                string dest = args[1];
                string searchPattern = args[2];

                FileInfo[] fi = PicSortLibrary.PicSortLibrary.ListAllPictures(source, searchPattern, true);
                System.Console.WriteLine(string.Format("{0} fichiers {1} dans {2}", fi.Count(), searchPattern, source));
                int i = 0;
                foreach (FileInfo f in fi)
                {
                    PicSortLibrary.PicSortLibrary.MovePicture(f, dest);
                    System.Console.WriteLine(string.Format("{0}/{1} - {2} a été traité", ++i, fi.Count(), f.Name));
                }
            }
            else
            { 
                System.Console.WriteLine("PicSortConsole.exe source destination searchPattern");
                System.Console.WriteLine("   source : Represents the fully qualified path of the source directory");
                System.Console.WriteLine("   destination : Represents the fully qualified path of the destination directory");
                System.Console.WriteLine("   searchPattern : Contains a combination of valid literal path and wildcard (* and ?) characters");
            }
        }
    }
}
