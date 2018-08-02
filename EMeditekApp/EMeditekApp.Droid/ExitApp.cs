using EMeditekApp.Droid;
using Xamarin.Forms;


[assembly: Dependency(typeof(ExitApp))]
namespace EMeditekApp.Droid
{
  public  class ExitApp :IExitApp
    {
     public   void Exit()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}