using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Distance.Interface;
using Distance.iOS.CustomRender;
using Foundation;
using UIKit;
[assembly: Xamarin.Forms.Dependency(typeof(ExitAppIOS))]

namespace Distance.iOS.CustomRender
{
    public class ExitAppIOS : IExit
    {
        public void CloseApp()
        {
            Thread.CurrentThread.Abort();
        }
    }
    
}