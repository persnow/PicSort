using Microsoft.VisualStudio.TestTools.UnitTesting;
using PicSortLibrary;
using System;
using System.IO;

namespace PicSortTest
{
    [TestClass]
    public class PicSortTest
    {
        [TestMethod]
        public void Test_ListAllPictures()
        {
            FileInfo [] fi = PicSortTools.ListAllPictures(@".\ListAllPictures", "*.jpg", true);
            // There are 3 jpg pictures in the folder
            Assert.IsTrue(fi.Length == 3);

            fi = PicSortTools.ListAllPictures(@".\ListAllPictures", "*.png", true);
            Assert.IsTrue(fi.Length == 2);
        }

        [TestMethod]
        public void Test_MovePicture_DesertJpg_To_2008_03()
        {
            // The file already exists in the destination folder
            FileInfo fiDesert = new FileInfo(@".\MovePicture\Desert.jpg");
            string dateFolder = string.Format("PicSort_{0}_{1}", 2008, "03");
            PicSortTools.MovePicture(fiDesert, @".\ResultsPictures", false);

            Assert.IsFalse(File.Exists(@".\MovePicture\Desert.jpg"));
            Assert.IsTrue(File.Exists(string.Format(@".\ResultsPictures\{0}\Desert.jpg", dateFolder)));
        }

        [TestMethod]
        public void Test_MovePicture_KoalaAndPenguinsJpg_To_2008_02()
        {
            // The file does not exists in the destination folder
            FileInfo fiKoala = new FileInfo(@".\MovePicture\Koala.jpg");
            PicSortTools.MovePicture(fiKoala, @".\ResultsPictures", false);

            Assert.IsFalse(File.Exists(@".\MovePicture\Koala.jpg"));
            var dateFolder = string.Format("PicSort_{0}_{1}", 2008, "02");
            Assert.IsTrue(File.Exists(string.Format(@".\ResultsPictures\{0}\Koala.jpg", dateFolder)));

            FileInfo fiPenguins = new FileInfo(@".\MovePicture\Penguins.jpg");
            PicSortTools.MovePicture(fiPenguins, @".\ResultsPictures", false);

            Assert.IsFalse(File.Exists(@".\MovePicture\Penguins.jpg"));
            Assert.IsTrue(File.Exists(string.Format(@".\ResultsPictures\{0}\Penguins.jpg", dateFolder)));
        }

        [TestMethod]
        public void Test_MovePicture_TigerPng_To_2008_02()
        {
            var dateFolder = string.Format("PicSort_{0}_{1}", 2008, "02");
            var fileName = "Tiger.png";

            FileInfo fiTiger = new FileInfo($@".\MovePicture\{fileName}");
            PicSortTools.MovePicture(fiTiger, @".\ResultsPictures", false);

            Assert.IsFalse(File.Exists($@".\MovePicture\{fileName}"));
            Assert.IsTrue(File.Exists(string.Format(@".\ResultsPictures\{0}\{1}", dateFolder, fileName)));
        }

        [TestMethod]
        public void Test_MovePicture_PngWithoutDateTaken_To_ActualDate()
        {
            var dateFolder = string.Format("PicSort_{0}_{1}", DateTime.Now.Year, DateTime.Now.Month.ToString("D2"));

            string fileName = "TigerWithoutDateTaken.png";
            FileInfo fiTigerWithoutDateTaken = new FileInfo($@".\MovePicture\{fileName}");
            PicSortTools.MovePicture(fiTigerWithoutDateTaken, @".\ResultsPictures", false);

            Assert.IsFalse(File.Exists($@".\MovePicture\{fileName}"));
            Assert.IsTrue(File.Exists(string.Format(@".\ResultsPictures\{0}\{1}", dateFolder, fileName)));
        }
    }
}
