using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp
{
 public   interface IOpenPfd
    {
        string OpenPdfFile(byte[] _pdfbytes);
    }
}
