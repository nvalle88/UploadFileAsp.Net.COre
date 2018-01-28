using System;
using System.Collections.Generic;
using System.Text;

namespace UploadFile.Utils
{
   public class DataFile
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public byte[] FileData { get; set; }
    }
}
