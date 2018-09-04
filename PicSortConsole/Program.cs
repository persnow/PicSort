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
            string source = args[0];
            string dest = args[1];

            string searchPattern = "*.jpg";

            FileInfo [] fi = PicSortLibrary.PicSortLibrary.ListAllPictures(source, searchPattern, true);
            System.Console.WriteLine(string.Format("{0} fichiers {1} dans {2}", fi.Count(), searchPattern, source));
            int i = 0;
            foreach (FileInfo f in fi)
            {
                PicSortLibrary.PicSortLibrary.MovePicture(f, dest);
                System.Console.WriteLine(string.Format("{0}/{1} - {2} a été traité", ++i, fi.Count(), f.Name));
            }
        }
    }
}
