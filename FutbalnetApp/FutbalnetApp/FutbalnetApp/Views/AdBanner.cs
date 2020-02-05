using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FutbalnetApp.Views
{
    public class AdBanner : View
    {
        public enum Sizes { Standardbanner, LargeBanner, MediumRectangle, FullBanner, Leaderboard, SmartBannerPortrait }
        public Sizes Size { get; set; }
        public AdBanner()
        {

            BackgroundColor = (Color)Application.Current.Resources["BackgroundColor"];
        }
    }
}
