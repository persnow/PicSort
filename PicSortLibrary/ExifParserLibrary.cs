using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PicSortLibrary
{
    public static class ExifParserLibrary
    {
        public static DateTime GetDateTaken(string imagePath)
        {
            const string creationTimePrefix = "Creation Time: ";
            DateTime? dateTaken = null;

            try
            {
                var directories = ImageMetadataReader.ReadMetadata(imagePath);
                var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

                if (subIfdDirectory != null)
                {
                    // JPG
                    dateTaken = subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                }
                else
                {
                    // PNG
                    foreach (var directory in directories)
                    {
                        if (directory.Name == "PNG-tEXt")
                        {
                            foreach (var tag in directory.Tags)
                            {
                                if (tag.Name == "Textual Data" && tag.Description.StartsWith(creationTimePrefix))
                                {
                                    if (DateTime.TryParseExact(tag.Description.Replace(creationTimePrefix, ""), "yyyy:MM:dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AssumeLocal, out DateTime result))
                                    {
                                        return result;
                                    }
                                }
                            }
                        }
                    }
                }

                return dateTaken ?? DateTime.MinValue;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime GetDateTakenOrModified(FileInfo imageFileInfo)
        {
            DateTime dt = ExifParserLibrary.GetDateTaken(imageFileInfo.FullName);

            if (dt.Equals(DateTime.MinValue))
            {
                dt = imageFileInfo.CreationTime < imageFileInfo.LastWriteTime ? imageFileInfo.CreationTime : imageFileInfo.LastWriteTime;
            }

            return dt;
        }
    }
}
