using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp
{
   public   interface IImageHelpers
    {
        string SaveFile(string collectionName, byte[] imageByte, string fileName);
        byte[] RotateImage(string path);
        int GetRotation(string filePath);
        string SavePdfFile(string collectionName, byte[] imageByte, string fileName);
       byte[] ResizeImage(byte[] imageData, float? width, float? height);
    }
}
