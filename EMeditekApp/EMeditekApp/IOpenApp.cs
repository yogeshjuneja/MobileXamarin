using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp
{
    public interface IOpenApp
    {
        void OpenAApplication(string PackageName, string IOS = null, string AndroidUrl = null);
         
    }
}
