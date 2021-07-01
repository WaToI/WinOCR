using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using Windows.Storage.Streams;

namespace WinOCR {
    public class WinOCR {
        OcrEngine ocr => OcrEngine.TryCreateFromUserProfileLanguages ();

        public async Task<OcrResult> GetOcrResultAsync (string path) {
            return await ocr.RecognizeAsync (await GetSoftwareBitmapAsync (path));
        }

        public async Task<string> ReadToEndAsync (string path) {
            var res = await ocr.RecognizeAsync (await GetSoftwareBitmapAsync (path));
            return res.Text;
        }

        public IEnumerable<string> ReadLine (string path) {
            var res = ocr.RecognizeAsync (GetSoftwareBitmapAsync (path).Result).AsTask ().Result;
            foreach (var i in res.Lines) {
                yield return i.Text;
            }
        }

        public async IAsyncEnumerable<string> ReadLineAsync (string path) {
            var res = await ocr.RecognizeAsync (await GetSoftwareBitmapAsync (path));
            foreach (var i in res.Lines) {
                yield return i.Text;
            }
        }

        public async Task<SoftwareBitmap> GetSoftwareBitmapAsync (string path) {
            IRandomAccessStream stream = null;
            if (string.IsNullOrEmpty (path)) {
                stream = CaptureScreenMS ().AsRandomAccessStream ();
            } else {
                var fi = new FileInfo (path);
                var appFolder = await StorageFolder.GetFolderFromPathAsync (fi.Directory.FullName);
                var sfi = await appFolder.GetFileAsync (fi.Name);
                stream = await sfi.OpenAsync (FileAccessMode.Read);
            }
            var decoder = await BitmapDecoder.CreateAsync (stream);
            return await decoder.GetSoftwareBitmapAsync ();
        }

        public Bitmap CaptureScreenBMP (string savePath = "") {
            //var scrs = System.Windows.Forms.Screen.AllScreens;
            //var w = scrs.Sum (s => s.Bounds.Width);
            //var h = scrs.Max (s => s.Bounds.Height);
            var w = (int) System.Windows.SystemParameters.VirtualScreenWidth;
            var h = (int) System.Windows.SystemParameters.VirtualScreenHeight;
            var bitmap = new Bitmap (w, h);
            using (var g = Graphics.FromImage (bitmap)) {
                g.CopyFromScreen (0, 0, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
            }
            if (!string.IsNullOrEmpty (savePath)) {
                //var tmpPath = $"{Path.GetTempFileName()}.png";
                bitmap.Save (savePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            return bitmap;
        }

        public MemoryStream CaptureScreenMS () {
            var bitmap = CaptureScreenBMP ();
            var ms = new MemoryStream ();
            bitmap.Save (ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms;
        }

    } //class OCR
} //namespace