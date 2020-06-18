using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using FutbalnetApp.Controls;
using FutbalnetApp.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SegmentView), typeof(SegmentViewRenderer_iOS))]
namespace FutbalnetApp.iOS
{
	class SegmentViewRenderer_iOS : ViewRenderer<SegmentView, UISegmentedControl>
	{
		UISegmentedControl control;


		protected override void OnElementChanged(ElementChangedEventArgs<SegmentView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement == null)
				return;

			if (Control == null)
			{
				control = new UISegmentedControl();

				for (int i = 0; i < Element.Children.Count; i++)
				{
					var segment = Element.Children.ElementAt(i);
					if (segment.Image != null)
					{
						//var img = NativeImage.FromFile(((FileImageSource)segment.Image).File);
						//img = img.Scale(new CoreGraphics.CGSize(17, 17)); // Apple docs
						//_control.InsertSegment(img, i, false);
					}
					else
						control.InsertSegment(segment.Title, i, false);
				}

				control.ClipsToBounds = true;
				control.TintColor = Element.TintColor.ToUIColor();
				control.SelectedSegment = Element.SelectedIndex;
				control.BackgroundColor = Element.BackgroundColor.ToUIColor();
				control.Layer.MasksToBounds = true;
				if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
				{
					control.SelectedSegmentTintColor = Element.TintColor.ToUIColor();
					control.SetTitleTextAttributes(new UITextAttributes { TextColor = Element.TextColor.ToUIColor() }, UIControlState.Normal);
					control.SetTitleTextAttributes(new UITextAttributes { TextColor = Element.SelectedTextColor.ToUIColor() }, UIControlState.Selected); 
				}

				control.Layer.BorderColor = Element.IsBorderColorSet()
					? Element.BorderColor.ToCGColor()
					: Element.TintColor.ToCGColor();

				if (Element.IsCornerRadiusSet())
				{
					control.Layer.CornerRadius = Element.CornerRadius;
					control.Layer.BorderWidth = (nfloat)Element.BorderWidth;
				}

				SetSelectedTextColor();
				SetNativeControl(control);
			}

			if (e.OldElement != null && control != null)
				control.ValueChanged -= OnSelectedIndexChanged;

			if (e.NewElement != null)
				control.ValueChanged += OnSelectedIndexChanged;
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (control == null || Element == null) return;

			switch (e.PropertyName)
			{
				case "SelectedIndex":
					control.SelectedSegment = Element.SelectedIndex;
					SetSelectedTextColor();
					break;
				case "IsEnabled":
					control.Enabled = Element.IsEnabled;
					control.TintColor = Element.IsEnabled ? Element.TintColor.ToUIColor() : Color.Gray.ToUIColor();// Element.UnselectedTintColor.ToUIColor();
					break;
			}
		}

		void SetSelectedTextColor()
		{
			// Don't change the color on iOS 13
			if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
				return;

			var attr = new UITextAttributes
			{
				TextColor = Element.IsEnabled ? Color.White.ToUIColor() : Element.TintColor.ToUIColor()
			};
			control.SetTitleTextAttributes(attr, Element.IsEnabled ? UIControlState.Selected : UIControlState.Normal);
		}

		void OnSelectedIndexChanged(object sender, System.EventArgs e)
		{
			Element.SelectedIndex = (int)control.SelectedSegment;
		}

		protected override void Dispose(bool disposing)
		{
			control.ValueChanged -= OnSelectedIndexChanged;
			control.Dispose();
			base.Dispose(disposing);
		}
	}
}