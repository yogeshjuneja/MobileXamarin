using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp
{
   public interface IImageCompress
    {
        byte[] ResizeImage(byte[] imageData, float? width=null, float? height=null);
    }
}
