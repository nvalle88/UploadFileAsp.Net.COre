using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UploadFile.Utils;
using UploadFile.Helpes;
using Microsoft.AspNetCore.Mvc;

namespace UploadFile.Services
{
    public class UploadFileService
    {
        /// <summary>
        /// Method to upload files to the web server Asp.Net.Core
        /// </summary>
        /// <param name="files">File of type List <IFormFile> files</param>
        /// <param name="folderNameServer">Destination folder on the web server</param>
        /// <param name="webRootPath">Path where the application is hosted</param>
        /// <returns></returns>
        public static async Task<bool> UploadFiles(List<IFormFile> files,string folderName, string webRootPath)
        {
            var targetDirectory = Path.Combine(webRootPath, folderName);
            if (! System.IO.File.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            foreach (var file in files)
            {
                var dataFile =FileHelper.GetDataFile(file);
                await UploadFile(dataFile, folderName, webRootPath);
            }
            
            return true;
        }

        private static async Task<bool> UploadFile(DataFile file, string folderServer, string WebRootPath)
        {

            try
            {
                var stream = new MemoryStream(file.FileData);
                var a = string.Format("{0}/{1}", folderServer, file.Name);
                var targetDirectory = Path.Combine(WebRootPath, a);

                using (var fileStream = new FileStream(targetDirectory, FileMode.Create, FileAccess.Write))
                {
                    await stream.CopyToAsync(fileStream);
                }

                return true;

            }
            catch (Exception)
            {
                return false;

            }
        }

        public static string DeleteFile(string fileFullName, string folderServer, string webRootPath)
        {
            try
            {
                var a = string.Format("{0}/{1}", folderServer,fileFullName);
                var targetDirectory = Path.Combine(webRootPath, a);
                if (System.IO.File.Exists(targetDirectory))
                {
                    System.IO.File.Delete(targetDirectory);
                    return Constants.True;
                }

                return Constants.NotExistDirectoryServer;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        public static DataFile GetFile(string fileFullName, string folderServer, string webRootPath)
        {
            var a = string.Format("{0}/{1}", folderServer, fileFullName);
            var targetDirectory = Path.Combine(webRootPath, a);

            if (File.Exists(targetDirectory))
            {
                return  FileHelper.GetDataFile(targetDirectory);

            }

            return null;
        }

        public static FileResult Dowland(string fileFullName, string folderServer, string webRootPath)
        {
            var a = string.Format("{0}/{1}", folderServer, fileFullName);
            var targetDirectory = Path.Combine(webRootPath, a);

            if (File.Exists(targetDirectory))
            {
                var file=  FileHelper.GetDataFile(targetDirectory);

                var fileName = string.Format("{0}", $"{ file.Name}");
                string mime = MimeKit.MimeTypes.GetMimeType(fileName);
                var ba = new FileController();
                return ba.File(file.FileData, mime, fileName);
            }

            return null;
        }

    }
}
