using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Distance.Droid.CustomRender;
using Distance.Interface;
[assembly: Xamarin.Forms.Dependency(typeof(ExitAppAndroid))]
namespace Distance.Droid.CustomRender
{

    public class ExitAppAndroid : IExit
    {
        public void CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
    
}