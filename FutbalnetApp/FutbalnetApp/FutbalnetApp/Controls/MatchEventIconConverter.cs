using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace FutbalnetApp.Controls
{
	public class MatchEventIconConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch (value)
			{
				case "GOAL": return GetGoalTypeImageSouce(parameter as string);
				case "CARD": return GetCardTypeImageSouce(parameter as string);
				case "NPK": return Device.RuntimePlatform == Device.iOS ? "penaltyMiss.pdf" : "";
				case "SUBST": return Device.RuntimePlatform == Device.iOS ? "substitution.pdf" : "";

				default: return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		private string GetCardTypeImageSouce(string type)
		{
			switch (type)
			{
				case "ZK": return Device.RuntimePlatform == Device.iOS ? "CardIcon.pdf" : "";
				case "CK": return Device.RuntimePlatform == Device.iOS ? "red.pdf" : "";
				case "2ZK": return Device.RuntimePlatform == Device.iOS ? "secondYellow.pdf" : "";

				default: return null;
			}
		}
		private string GetGoalTypeImageSouce(string type)
		{
			switch (type)
			{
				case "PENALTY": return Device.RuntimePlatform == Device.iOS ? "penalty.pdf" : "";
				case "GAME": return Device.RuntimePlatform == Device.iOS ? "goal.pdf" : "";
				case "STANDARD": return Device.RuntimePlatform == Device.iOS ? "freeKick.pdf" : "";

				default: return null;
			}
		}
	}
}
