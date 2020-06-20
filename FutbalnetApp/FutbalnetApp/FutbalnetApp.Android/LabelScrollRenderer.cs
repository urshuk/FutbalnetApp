using Android.Content;
using FutbalnetApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(LabelScrollRenderer))]
namespace FutbalnetApp.Droid
{
    class LabelScrollRenderer : LabelRenderer
	{
        public LabelScrollRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            Control.VerticalScrollBarEnabled = false;
        }
    }
}