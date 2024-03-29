﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FileManager.Modules.Converters
{
	public class BooleanToVisibilityConverter:IValueConverter
	{
		public object Convert(object value, System.Type targetType, object parameter, string language)
		{
			if (value is Boolean && (bool)value)
			{
				return Visibility.Visible;
			}

			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, System.Type targetType, object parameter, string language)
		{
			if (value is Visibility && (Visibility)value == Visibility.Visible)
			{
				return true;
			}

			return false;
		}
	}
}
