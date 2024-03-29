﻿using System.IO;
using System.Windows.Media.Imaging;

namespace benais_jWPF_Medecin.View.Converters
{
    public static class ImageToByteArray
    {
        public static byte[] Convert(BitmapImage image)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
                return data;
            }
        }
    }
}
