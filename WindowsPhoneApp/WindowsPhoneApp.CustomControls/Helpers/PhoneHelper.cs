#if !NETFX_CORE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;

namespace WindowsPhoneApp.CustomControls.Helpers
{
    internal static class PhoneHelper
    {
        public static bool TryGetPhoneApplicationFrame(out PhoneApplicationFrame phoneApplicationFrame)
        {
            phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            return phoneApplicationFrame != null;
        }

        public static bool IsPortrait(this PhoneApplicationFrame phoneApplicationFrame)
        {
            PageOrientation portrait = PageOrientation.Portrait | PageOrientation.PortraitDown | PageOrientation.PortraitUp;
            return (portrait & phoneApplicationFrame.Orientation) == phoneApplicationFrame.Orientation;
        }

        public static bool IsPortrait()
        {
            PhoneApplicationFrame phoneAppFrame = null;
            if (TryGetPhoneApplicationFrame(out phoneAppFrame))
            {
                return phoneAppFrame.IsPortrait();
            }

            return true;
        }

        public static double GetUsefullWidth()
        {
            PhoneApplicationFrame phoneAppFrame = null;
            if (TryGetPhoneApplicationFrame(out phoneAppFrame))
            {
                return phoneAppFrame.GetUsefulWidth();
            }

            return Application.Current.Host.Content.ActualWidth;
        }

        public static double GetUsefulHeight()
        {
            PhoneApplicationFrame phoneAppFrame = null;
            if (TryGetPhoneApplicationFrame(out phoneAppFrame))
            {
                return phoneAppFrame.GetUsefulHeight();
            }

            return Application.Current.Host.Content.ActualHeight;
        }

        public static Size GetUsefulSize(this Size initialSize)
        {
            if (IsPortrait())
                return initialSize;

            return new Size(initialSize.Height, initialSize.Width);
        }

        public static double GetUsefulWidth(this PhoneApplicationFrame phoneApplicationFrame)
        {
            return phoneApplicationFrame.IsPortrait() ? phoneApplicationFrame.ActualWidth : phoneApplicationFrame.ActualHeight;
        }

        public static double GetUsefulHeight(this PhoneApplicationFrame phoneApplicationFrame)
        {
            return IsPortrait(phoneApplicationFrame) ? phoneApplicationFrame.ActualHeight : phoneApplicationFrame.ActualWidth;
        }
    }
}
#endif