﻿using MaterialDesignThemes.Wpf;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfClientCalander
{
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return PackIconKind.Abc;
            switch (value)
            {
                case "Home":
                    return PackIconKind.Home;
                case "Group Admin Page":
                    return PackIconKind.TableAccount;
                case "Manager Settings":
                    return PackIconKind.AccountSupervisorCircle;
                case "My Profile":
                    return PackIconKind.HomeAccount;
                case "Users":
                    return PackIconKind.BadgeAccount;
                case "Groups":
                    return PackIconKind.AccountsGroup;
                case "Admins":
                    return PackIconKind.CalendarAccount;
                case "Events":
                    return PackIconKind.Event;
                default:
                        break;
                }
            return PackIconKind.Abc;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
