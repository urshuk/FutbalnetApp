using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace FutbalnetApp.Controls
{
	public class ListDataTemplateSelector : DataTemplateSelector
	{
		public DataTemplate First { get; set; }
		public DataTemplate Alternative { get; set; }

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			return ((IEnumerable<object>)((CollectionView)container).ItemsSource).ToList().IndexOf(item) % 2 == 0 ? First : Alternative;
		}
	}
}
