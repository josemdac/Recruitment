using Aspose.Imaging;
using Aspose.Imaging.FileFormats.Jpeg;
using Aspose.Imaging.ImageOptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RenovaHrEmploymentAPI.Helpers
{
    class ImageHelper
    {
        public static byte[] CompressImage(byte[] B64Data, double w, double h)
        {
            var tempFilePath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".jpg";
            var tempResultPath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".jpg";
            File.WriteAllBytes(tempFilePath, B64Data);
            //return B64Data;
            var image = Image.Load(tempFilePath);

            var options = new JpegOptions();
            options.CompressionType = JpegCompressionMode.Progressive;
            options.ResolutionSettings = new ResolutionSetting
            {
                VerticalResolution = h,
                HorizontalResolution = w
            };
            options.Quality = 10;
            image.Save(tempResultPath, options);
            var data = File.ReadAllBytes(tempResultPath);
            
            //File.Delete(tempFilePath);
            File.Delete(tempResultPath);
            return data;



        }
    }
}
