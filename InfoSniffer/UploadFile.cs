using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace InfoSnifferForm
{
    public class UploadFile
    {
        Stream _stream;
        string _fileName;
        string _contentType;
        bool _appendWatermark = true;

        public Stream Stream
        {
            get { return _stream; }
            set { _stream = value; }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public bool AppendWatermark
        {
            get { return _appendWatermark; }
            set { _appendWatermark = value; }
        }

        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        public string FileNameWithoutExtension
        {
            get
            {
                return Path.GetFileNameWithoutExtension(FileName);
            }
        }

        public string Extension
        {
            get
            {
                return Path.GetExtension(FileName);
            }
        }

        public bool IsImage
        {
            get {
                List<string> exts = new List<string>(new string[] { ".gif", ".jpg", ".bmp", ".png" });
                return exts.Contains(this.Extension.ToLower());
            }
        }

        public UploadFile() { }
        public UploadFile(Stream stream, string fileName, string contentType) {
            this.Stream = stream;
            this.FileName = fileName;
            this.ContentType = contentType;
        }
    }
}
