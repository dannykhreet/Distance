using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Distance.iOS.CustomRender;
using Distance.View;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MainMasterDetailPage), typeof(MyMasterDetailPageRenderer))]
namespace Distance.iOS.CustomRender
{
    public class MyMasterDetailPageRenderer : MyPhoneMasterDetailRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);


        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

        }
    }
}