﻿/*  Copyright 2012 PerceiveIT Limited
 *  This file is part of the Scryber library.
 *
 *  You can redistribute Scryber and/or modify 
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 * 
 *  Scryber is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 * 
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with Scryber source code in the COPYING.txt file.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using Scryber.PDF.Native;
using Scryber.PDF;

namespace Scryber.Drawing
{
    [PDFParsableValue()]
    public abstract class ImageData : ITypedObject
    {

        //
        // properties
        //

        #region public string SourcePath {get;set;}

        private string _path;
        /// <summary>
        /// Gets the source path this image data was loaded from
        /// </summary>
        public string SourcePath
        {
            get { return _path; }
            protected set { _path = value; }
        }

        #endregion


        #region public int PixelWidth {get;set;}

        private int _w;
        /// <summary>
        /// Gets the total number of pixels in one row of the image
        /// </summary>
        public int PixelWidth
        {
            get { return _w; }
            protected set { _w = value; }
        }

        #endregion

        #region public int PixelHeight {get;set;}

        private int _h;

        public int PixelHeight
        {
            get { return _h; }
            protected set { _h = value; }
        }

        #endregion


        #region  public int BitsPerColor {get;set;}

        private int _bitdepth;

        /// <summary>
        /// Gets or set the number of bits per single color sample
        /// </summary>
        public int BitsPerColor
        {
            get { return _bitdepth; }
            internal protected set { _bitdepth = value; }
        }

        #endregion


        #region public ColorSpace ColorSpace {get;set;}

        private ColorSpace _cs = ColorSpace.RGB;

        public ColorSpace ColorSpace
        {
            get { return _cs; }
            internal protected set { _cs = value; }
        }

        #endregion

        #region public int ColorsPerSample {get; set;}

        private int _colspsample;

        /// <summary>
        /// Gets the numbers of colour values that make up an individual pixel.
        /// </summary>
        public int ColorsPerSample
        {
            get { return _colspsample; }
            protected internal set { _colspsample = value; }
        }

        #endregion

        #region public int HorizontalResolution {get;set;}

        private int _hres;
        /// <summary>
        /// Gets the horizontal resolution of the image (pixels per inch)
        /// </summary>
        public int HorizontalResolution
        {
            get { return _hres; }
            internal protected set { _hres = value; }
        }

        #endregion

        #region public int VerticalResolution {get;set;}

        private int _vres;
        /// <summary>
        /// Gets the vertical resolution of the image (pixels per inch)
        /// </summary>
        public int VerticalResolution
        {
            get { return _vres; }
            internal protected set { _vres = value; }
        }

        #endregion

        #region public PDFUnit DisplayHeight {get;}

        public PDFUnit DisplayHeight
        {
            get { return new PDFUnit(((double)this.PixelHeight) / (double)this.VerticalResolution, PageUnits.Inches); }
        }

        #endregion

        #region public PDFUnit DisplayWidth {get;}

        public PDFUnit DisplayWidth
        {
            get { return new PDFUnit(((double)this.PixelWidth) / (double)this.HorizontalResolution, PageUnits.Inches); }
        }

        #endregion

        #region public string Filter {get;set;}

        private IStreamFilter[] _filters;

        /// <summary>
        /// Gets the stream filter
        /// </summary>
        public IStreamFilter[] Filters
        {
            get { return _filters; }
            set
            {
                _filters = value;
                this.ResetFilterCache();
            }
        }

        #endregion

        #region  public bool HasFilter {get;}

        public bool HasFilter
        {
            get { return null != _filters && _filters.Length > 0; }
        }

        #endregion

        #region public virtual bool IsPrecompressedData {get;}

        /// <summary>
        /// If true then the raw image data is precompressed otherwise it is raw binary pixel data
        /// </summary>
        public virtual bool IsPrecompressedData
        {
            get { return false; }
        }

        #endregion

        private ObjectType _type;

        /// <summary>
        /// Gets the type of object this is
        /// </summary>
        public ObjectType Type
        {
            get { return _type; }
        }

        //
        // ctor(s)
        //

        #region protected PDFImageData(string source, int w, int h) + 1 overload

        /// <summary>
        /// Protected constructor - accepts the source path, width and height (in pixels)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        protected ImageData(string source, int w, int h)
            : this(ObjectTypes.ImageData, source, w, h)
        {
            this._path = source;
            this._w = w;
            this._h = h;
        }

        protected ImageData(ObjectType type, string source, int w, int h)
        {
            this._type = type;
            this._path = source;
            this._w = w;
            this._h = h;
        }

        #endregion

        //
        // rendering

        #region public PDFSize GetSize()

        private const double DefaultResolution = 72.0;

        /// <summary>
        /// Gets the natural size of the image in page units
        /// </summary>
        /// <returns></returns>
        public PDFSize GetSize()
        {
            double hres = (double)this.HorizontalResolution;
            if (hres < 1.0)
                hres = DefaultResolution;
            double vres = (double)this.VerticalResolution;
            if (vres <= 1)
                vres = DefaultResolution;

            double w = ((double)this.PixelWidth) / hres;
            double h = ((double)this.PixelHeight) / vres;

            return new PDFSize(new PDFUnit(w, PageUnits.Inches), new PDFUnit(h, PageUnits.Inches));
        }

        #endregion

        public abstract PDFObjectRef Render(PDFName name, IStreamFilter[] filters,  PDFContextBase context, PDFWriter writer);

        public abstract void ResetFilterCache();

        //
        // static methods
        //

        public static ImageData LoadImageFromURI(string uri, IComponent owner = null)
        {
            //throw new NotSupportedException("Don't use the loading from a remote uri. Use the Document.RegisterRemoteFileRequest");

            using (System.Net.Http.HttpClient wc = new System.Net.Http.HttpClient())
            {
                //wc. = System.Net.CredentialCache.DefaultNetworkCredentials;

                bool compress = false;

                if (owner is IOptimizeComponent)
                    compress = ((IOptimizeComponent)owner).Compress;

                ImageData img;
                byte[] data = wc.GetByteArrayAsync(uri).Result;
                img = InitImageData(uri, data, compress);
                return img;

            }
        }

        public static ImageData LoadImageFromStream(string sourceKey, System.IO.Stream stream, IComponent owner = null)
        {
            using (System.Drawing.Image bmp = System.Drawing.Image.FromStream(stream))
            {
                bool compress = false;

                if (null != owner && owner is IOptimizeComponent)
                    compress = ((IOptimizeComponent)owner).Compress;

                ImageData img;
                img = InitImageData(sourceKey, bmp, compress);
                return img;
            }
        }

        public static ImageData LoadImageFromLocalFile(string path, IComponent owner = null)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            if (fi.Exists == false)
                throw new ArgumentNullException("path", "The file at the path '" + path + "' does not exist.");

            bool compress = false;

            if (null != owner && owner is IOptimizeComponent)
                compress = ((IOptimizeComponent)owner).Compress;

            using (System.Drawing.Image bmp = System.Drawing.Image.FromFile(path))
            {
                ImageData img;
                img = InitImageData(path, bmp, compress);
                return img;
            }
        }

        public static ImageData LoadImageFromUriData(string src, IDocument document, IComponent owner)
        {
            if (null == document) throw new ArgumentNullException("document");
            if (null == owner) throw new ArgumentNullException("owner");

            var dataUri = ParseDataURI(src);

            if (dataUri.encoding != "base64") throw new ArgumentException("src", $"unsupported encoding {dataUri.encoding}; expected base64");
            if (dataUri.mediaType != "image/png") throw new ArgumentException("src", $"unsupported encoding {dataUri.mediaType}; expected image/png");

            var binary = Convert.FromBase64String(dataUri.data);

            using (var ms = new System.IO.MemoryStream(binary))
            {
                return ImageData.LoadImageFromStream(document.GetIncrementID(owner.Type) + "data_png", ms, owner);
            }
        }

        /// <summary>
        /// Creates a new PDFImageData instance from the specified bitmap.
        /// The sourceKey is a unique reference to this image data in the document
        /// </summary>
        /// <param name="sourcekey"></param>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static ImageData LoadImageFromBitmap(string sourcekey, System.Drawing.Bitmap bitmap, bool compress = false)
        {
            if (null == bitmap)
                throw new ArgumentNullException("bitmap");
            ImageData data = InitImageData(sourcekey, bitmap, compress);
            
            return data;
        }


        public static ImageData InitImageData(string uri, byte[] data, bool compress)
        {
            ImageData img;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(data))
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms, false);
                img = InitImageData(uri, bmp, compress);
                bmp.Dispose();
            }
            return img;
        }

        private static ImageData InitImageData(string uri, System.Drawing.Image bmp, bool compress)
        {
            ImageData imgdata;
            bool dispose = false;

            //if this image data is not a bitmap but a metafile (drawing instructions)
            //we need to convert it to a bitmap image.
            if (bmp is System.Drawing.Imaging.Metafile)
            {
                bmp = ConvertMetafileToBitmap(bmp as System.Drawing.Imaging.Metafile);
                dispose = true;
            }
            if(compress)
            {
                bmp = ConvertToJpeg(bmp);
            }
            try
            {
                Imaging.ImageParser parser = Imaging.ImageFormatParser.GetParser(bmp);
                imgdata = parser(uri, bmp);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(String.Format(Errors.CouldNotParseTheImageAtPath, uri, ex.Message), "bmp", ex);
            }
            finally
            {
                if (dispose && null != bmp)
                    bmp.Dispose();
            }

            return imgdata;
        }

        //private static readonly System.Drawing.Imaging.ImageFormat jpegFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
        //private static readonly System.Drawing.Imaging.EncoderParameters jpegParams = new System.Drawing.Imaging.EncoderParameters();

        private static Image ConvertToJpeg(Image bmp)
        {
            //if (bmp.RawFormat.Guid != jpegFormat.Guid)
            //{
            //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //    bmp.Save(ms, jpegFormat);
            //    ms.Flush();
            //    ms.Position = 0;
            //    bmp.Dispose(); //Dispose of this one and use the one with the stream.
            //    return Image.FromStream(ms);

            //}
            //else
            return bmp;
        }

        public static ImageData Parse(string data)
        {
            return Parse(data, false);
        }

        public static ImageData Parse(string data, bool compress)
        {
            if (string.IsNullOrEmpty(data) == false)
            {
                try
                {
                    byte[] binary = System.Convert.FromBase64String(data);
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(binary))
                    {
                        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                        return ImageData.LoadImageFromBitmap(Guid.NewGuid().ToString(), bmp, compress);
                    }
                }
                catch (Exception ex)
                {
                    throw new PDFException("Cannot convert the BASE64 string to image data", ex);
                }
            }
            else
                return null;
        }




        private static System.Drawing.Bitmap ConvertMetafileToBitmap(System.Drawing.Imaging.Metafile emf)
        {
            using (System.IO.MemoryStream bmpStream = new System.IO.MemoryStream())
            {
                using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(emf.Width, emf.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                {
                    bmp.SetResolution(emf.HorizontalResolution, emf.VerticalResolution);
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
                    {
                        g.Clear(System.Drawing.Color.Transparent);
                        g.DrawImage(emf, 0, 0);
                    }
                    bmp.Save(bmpStream, System.Drawing.Imaging.ImageFormat.Png);
                }
                bmpStream.Flush();
                bmpStream.Position = 0;
                System.Drawing.Bitmap final = new System.Drawing.Bitmap(bmpStream);
                return final;
            }


        }

        #region Parse URI data from inline src data:[<MIME-type>][;charset=<encoding>][;base64],<data>

        /**
         * Parse a data uri and return a record 
         * @param uri 
         */
        internal record UriData { public string mediaType; public string parameters; public string encoding; public string data; }

        private static readonly Regex splitter = new Regex(@"^data:([-\w]+\/[-+\w.]+)?((?:;?[\w]+=[-\w]+)*);(base64)?,(.*)");

        


        private static UriData ParseDataURI(string uri)
        {

            var match = splitter.Match(uri);

            return new UriData
            {
                mediaType = match.Groups[1]?.Value,
                parameters = match.Groups[2]?.Value,
                encoding = match.Groups[3]?.Value,
                data = match.Groups[4]?.Value
            };
        }

        

        #endregion
    }
}