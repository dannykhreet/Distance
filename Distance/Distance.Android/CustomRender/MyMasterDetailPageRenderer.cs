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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

using Android.Support.V4.Widget;
using Distance.View;

[assembly: ExportRenderer(typeof(MainMasterDetailPage), typeof(MyMasterDetailPageRenderer))]

namespace Distance.Droid.CustomRender
{
    public class MyMasterDetailPageRenderer : MasterDetailPageRenderer
    {
        public MyMasterDetailPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(VisualElement oldElement, VisualElement newElement)
        {
            base.OnElementChanged(oldElement, newElement);

            var width = Resources.DisplayMetrics.WidthPixels;

            var fieldInfo = GetType().BaseType.GetField("_masterLayout", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var _masterLayout = (ViewGroup)fieldInfo.GetValue(this);
            var lp = new DrawerLayout.LayoutParams(_masterLayout.LayoutParameters);

            MainMasterDetailPage page = (MainMasterDetailPage)newElement;
            lp.Width = (int)(page.WidthRatio * width);

            lp.Gravity = (int)GravityFlags.Left;
            _masterLayout.LayoutParameters = lp;
        }
    }

}