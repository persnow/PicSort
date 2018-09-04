using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSortLibrary
{
    public static class PicSortLibrary
    {
        public static FileInfo[] ListAllPictures(string path, string searchPattern, bool recursive)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            return di.GetFiles(searchPattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }

        public static void MovePicture(FileInfo fi, string destination)
        {
            string destinationFolder = destination + "\\PicSort_" + fi.CreationTime.Year + "_" + fi.CreationTime.Month.ToString("D2");
            DirectoryInfo di = new DirectoryInfo(destinationFolder);
            if (!di.Exists)
                di.Create();
            FileInfo newFi = new FileInfo(destinationFolder + "\\" + fi.Name);
            if (newFi.Exists)
            {
                if (newFi.Length == fi.Length)
                {
                    // The file already exists
                    // TODO : Delete or store in garbage folder
                }
                else
                {
                    // TODO : Rename the files
                }
            }
            else
            {
                fi.CopyTo(newFi.FullName);
            }
            fi.Delete();
        }
    }
}
