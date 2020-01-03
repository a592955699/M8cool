using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;

namespace InfoSnifferForm
{
    public class WebUpload
    {
        string _uploadUrl = "";
        int _maxSize = 200;
        string[] _allowExtensions = new string[] { ".gif", ".jpg", ".bmp", ".png", ".zip", ".rar" };
        string _watermarkUrl = "";

        #region 属性
        public string UploadUrl
        {
            get
            {
                return _uploadUrl;
            }
            set
            {
                _uploadUrl = value;
            }
        }

        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath
        {
            get;
            set;
        }

        /// <summary>
        /// 是否改名
        /// </summary>
        public bool Rename
        {
            get;
            set;
        }

        /// <summary>
        /// 是否复盖
        /// </summary>
        public bool Overwrite
        {
            get;
            set;
        }

        /// <summary>
        /// 文件
        /// </summary>
        public UploadFile[] Files
        {
            get;
            set;
        }

        /// <summary>
        /// 允许上传的扩展名，默认gif,jpg,bmp,png,zip,rar
        /// </summary>
        public string[] AllowExtensions
        {
            get
            {
                return _allowExtensions;
            }
            set
            {
                _allowExtensions = value;
            }
        }

        /// <summary>
        /// 最大可以上传的文件大小（K），默认200K
        /// </summary>
        public int MaxSize
        {
            get
            {
                return _maxSize;
            }
            set
            {
                _maxSize = value;
            }
        }

        /// <summary>
        /// 水印图片
        /// </summary>
        public string WatermarkUrl
        {
            get { return _watermarkUrl; }
            set { _watermarkUrl = value; }
        }

        /// <summary>
        /// 返回的Url
        /// </summary>
        public string[] Urls
        {
            get;
            set;
        }

        /// <summary>
        /// 错误类型
        /// </summary>
        public UploadErrorType ErrorType
        {
            get;
            set;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get;
            set;
        }
        #endregion

        public WebUpload() { }

        public WebUpload(UploadFile[] files, string savePath, bool rename, bool overwrite)
        {
            Files = files;
            SavePath = savePath;
            Rename = rename;
            Overwrite = overwrite;
        }

        public WebUpload(UploadFile[] files, string savePath, bool rename, bool overwrite, string[] allowExtensions)
        {
            Files = files;
            SavePath = savePath;
            Rename = rename;
            Overwrite = overwrite;
            AllowExtensions = allowExtensions;
        }

        /// <summary>
        /// 验证文件是否可以上传
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            StringBuilder sbType = new StringBuilder();
            StringBuilder sbSize = new StringBuilder();
            List<string> exts = new List<string>(this.AllowExtensions);
            int fileCount = Files.Length;
            for (int i = 0; i < fileCount; i++)
            {
                UploadFile file = Files[i];
                if (!exts.Contains(file.Extension.ToLower()))
                {
                    sbType.Append(file.FileName);
                    if (i < fileCount - 1)
                    {
                        sbType.AppendLine();
                    }
                }
                if (file.Stream.Length / 1024 > this.MaxSize)
                {
                    sbSize.Append(file.FileName);
                    if (i < fileCount - 1)
                    {
                        sbSize.AppendLine();
                    }
                }
            }
            if (sbType.Length > 0)
            {
                this.ErrorType = UploadErrorType.FileTypeError;
                this.ErrorMessage = sbType.ToString();
                return false;
            }
            else if (sbSize.Length > 0)
            {
                this.ErrorType = UploadErrorType.SizeError;
                this.ErrorMessage = sbSize.ToString();
                return false;
            }

            return true;
        }

        public bool Upload()
        {
            //验证文件
            //if (!this.Validate() || Files.Length == 0)
            if (Files.Length == 0)
            {
                return false;
            }

            string uploadUrl = string.Format("{0}?path={1}&rename={2}&overwrite={3}",
                this.UploadUrl,this.SavePath, this.Rename ? "true" : "false", this.Overwrite ? "true" : "false");

            string boundary = "----WebKitFormBoundaryAALsAAoaAAsgAAaQ";
            HttpWebRequest httpReq;
            httpReq = (HttpWebRequest)WebRequest.Create(uploadUrl);
            httpReq.KeepAlive = false;
            httpReq.Method = "POST";
            httpReq.ContentType = "multipart/form-data; boundary=" + boundary;

            //httpReq.ContentLength = bytes.Length;
            Stream reqStream = httpReq.GetRequestStream();

            Bitmap wateBmp = null;

            StringBuilder sb = new StringBuilder();
            byte[] bytes;
            byte[] buffer;
            int bytesRead;
            int fileCount = this.Files.Length;

            for (int i = 0; i < fileCount; i++)
            {
                UploadFile file = this.Files[i];
                //if (file.Stream.Length < 1)
                //    continue;

                /*如果是图片就加水印
                if (file.IsImage && file.AppendWatermark)
                {

                    if (wateBmp == null)
                        wateBmp = new Bitmap(HttpContext.Current.Server.MapPath(this.WatermarkUrl));

                    MemoryStream ms = new MemoryStream();

                    Bitmap bmp = new Bitmap(file.Stream);
                    if (bmp.Width > wateBmp.Width && bmp.Height > wateBmp.Height)
                    {
                        int x = bmp.Width - wateBmp.Width - 20;
                        int y = bmp.Height - wateBmp.Height - 20;
                        Bitmap bmp1 = QuickWeb.Utility.FileUpload.AppendWatermark(bmp, wateBmp, x, y, 0.4f);
                        if (file.Extension.Equals(".gif", StringComparison.CurrentCultureIgnoreCase))
                        {
                            OctreeQuantizer quantizer = new OctreeQuantizer(255, 8);
                            using (Bitmap quantized = quantizer.Quantize(bmp1))
                            {
                                quantized.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                            }

                        }
                        if (file.Extension.Equals(".jpg", StringComparison.CurrentCultureIgnoreCase))
                        {
                            ImageCodecInfo imageCodecInfo = QuickWeb.Utility.FileUpload.GetEncoderInfo("image/jpeg");
                            EncoderParameters encoderParameters = new EncoderParameters(1);
                            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

                            bmp1.Save(ms, imageCodecInfo, encoderParameters);
                        }
                        if (file.Extension.Equals(".png", StringComparison.CurrentCultureIgnoreCase))
                            bmp1.Save(ms, ImageFormat.Png);
                        if (file.Extension.Equals(".bmp", StringComparison.CurrentCultureIgnoreCase))
                            bmp1.Save(ms, ImageFormat.Bmp);
                    }
                    if (ms.Length > 0)
                        file.Stream = ms;
                }
                */

                sb.Remove(0, sb.Length);
                sb.Append("--");
                sb.Append(boundary);
                sb.AppendLine();
                sb.AppendFormat("Content-Disposition: form-data; name=\"file{0}\"; filename=\"{1}\"", i, Path.GetFileName(file.FileName));
                sb.AppendLine();
                sb.AppendFormat("Content-Type: {0}", file.ContentType);
                sb.AppendLine();
                sb.AppendLine();

                bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
                reqStream.Write(bytes, 0, bytes.Length);

                buffer = new byte[1024];
                bytesRead = 0;

                Stream stream = file.Stream;
                stream.Position = 0;
                while (true)
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;
                    reqStream.Write(buffer, 0, bytesRead);
                }
                if (i < fileCount - 1)
                {
                    bytes = System.Text.Encoding.UTF8.GetBytes(System.Environment.NewLine);
                    reqStream.Write(bytes, 0, bytes.Length);
                }

            }

            sb.Remove(0, sb.Length);
            sb.AppendLine();
            sb.AppendFormat("--{0}--", boundary);
            bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            reqStream.Write(bytes, 0, bytes.Length);


            reqStream.Close();
            HttpWebResponse response = (HttpWebResponse)httpReq.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
            string result = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();

            if (!result.StartsWith("http://"))
                throw new System.Exception(result);
            this.Urls = Regex.Split(result, System.Environment.NewLine);
            return true;
        }
    }
}
