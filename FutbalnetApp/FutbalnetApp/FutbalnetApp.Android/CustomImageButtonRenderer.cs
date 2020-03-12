using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using FutbalnetApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImageButton), typeof(CustomImageButtonRenderer))]
namespace FutbalnetApp.Droid
{
	class CustomImageButtonRenderer : ImageButtonRenderer
	{
        public CustomImageButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ImageButton> e)
        {
            base.OnElementChanged(e);

            if (!(e.NewElement is ImageButton))
                return;

            this.SetBackgroundColor(Color.Transparent.ToAndroid()); //This is the line to set the transparent background without issues
        }
    }
}