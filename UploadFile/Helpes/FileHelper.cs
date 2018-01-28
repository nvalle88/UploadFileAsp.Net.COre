using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UploadFile.Utils;

namespace UploadFile.Helpes
{

    public static class FileHelper
    {
        public static DataFile GetDataFile(IFormFile file)
        {

            if (file != null)
            {
                return new DataFile
                {
                    Name = FileHelper.GetName(file)
                                ,
                    FileData = FileHelper.GetDataByte(file)
                                ,
                    Extension = FileHelper.GetExtension(file)
                };
            }

            return null;
        }

        public static DataFile GetDataFile(string path)
        {

            if (path != null)
            {
                return new DataFile
                {
                    Name = FileHelper.GetName(path)
                                ,
                    FileData = FileHelper.GetDataByte(path)
                                ,
                    Extension = FileHelper.GetExtension(path)
                };
            }

            return null;
        }

        private static string GetExtension(IFormFile file)
        {
            return Path.GetExtension(file.FileName);
        }

        private static string GetExtension(string fileFullName)
        {
            return Path.GetExtension(fileFullName);
        }

        private static string GetName(IFormFile file)
        {
            return Path.GetFileName(file.FileName);
        }

        private static string GetName(string fileFullName)
        {
            return Path.GetFileName(fileFullName);
        }

        private static byte[] GetDataByte(string path)
        {
            var file = new FileStream(path, FileMode.Open);
            byte[] data;
            using (var br = new BinaryReader(file))
                data = br.ReadBytes((int)file.Length);
            return data;
        }

        private static byte[] GetDataByte(IFormFile file)
        {
            byte[] data;
            using (var br = new BinaryReader(file.OpenReadStream()))
                data = br.ReadBytes((int)file.OpenReadStream().Length);
            return data;
        }
    }
}
