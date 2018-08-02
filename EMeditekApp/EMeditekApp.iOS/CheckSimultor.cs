using EMeditekApp.iOS;
using ObjCRuntime;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(CheckSimultor))]
namespace EMeditekApp.iOS
{
    class CheckSimultor :ICheckSimulator
    {
        public bool Check()
        {
            if (Runtime.Arch == Arch.SIMULATOR)
                return true;
            return false;

        }
    }
}
