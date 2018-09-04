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
            // There are 3 pictures in the folder
            Assert.IsTrue(fi.Length == 3);
        }

        [TestMethod]
        public void Test_MovePicture()
        {
            // Because the file copied in the test folder have the current date as modification date
            string dateFolder = string.Format("PicSort_{0}_{1}", DateTime.Now.Year, DateTime.Now.Month.ToString("D2"));

            FileInfo fiDesert = new FileInfo(@".\MovePicture\Desert.jpg");
            PicSortTools.MovePicture(fiDesert, @".\ResultsPictures");

            Assert.IsFalse(File.Exists(@".\MovePicture\Desert.jpg"));
            Assert.IsTrue(File.Exists(string.Format(@".\ResultsPictures\{0}\Desert.jpg", dateFolder)));

            FileInfo fiKoala = new FileInfo(@".\MovePicture\Koala.jpg");
            PicSortTools.MovePicture(fiKoala, @".\ResultsPictures");

            Assert.IsFalse(File.Exists(@".\MovePicture\Koala.jpg"));
            Assert.IsTrue(File.Exists(string.Format(@".\ResultsPictures\{0}\Koala.jpg", dateFolder)));

            FileInfo fiPenguins = new FileInfo(@".\MovePicture\Penguins.jpg");
            PicSortTools.MovePicture(fiPenguins, @".\ResultsPictures");

            Assert.IsFalse(File.Exists(@".\MovePicture\Penguins.jpg"));
            Assert.IsTrue(File.Exists(string.Format(@".\ResultsPictures\{0}\Penguins.jpg", dateFolder)));
        }
    }
}
