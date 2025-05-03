using CoreGraphics;
using Foundation;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using WalletTracker.Controls;

namespace WalletTracker.Platforms.iOS;

    public static class EntryMapper
    {
        public static void Map(IElementHandler handler, IElement view)
        {
           if(view is BorderedEntry entry)
           {
                var nativeEntry = (EntryHandler)handler;
                var control = nativeEntry.PlatformView;

                if(control != null)
                {
                    control.LeftView = new UIView(new CGRect(0, 0, entry.Padding.Left, entry.Padding.Top));
                    control.LeftViewMode = UITextFieldViewMode.Always;
                    control.RightView = new UIView(new CGRect(0, 0, entry.Padding.Right, entry.Padding.Bottom));
                    control.RightViewMode = UITextFieldViewMode.Always;

                    control.Layer.BorderColor = entry.BorderColor.ToCGColor();
                    control.Layer.BorderWidth = entry.BorderWidth;
                    control.Layer.CornerRadius = entry.CornerRadius;

                    control.BackgroundColor = entry.BackgroundColor.ToPlatform();
                    control.Font = UIFont.FromName(entry.FontFamily, (nfloat)entry.FontSize) ?? UIFont.SystemFontOfSize((nfloat)entry.FontSize);
                    control.TextColor = entry.TextColor.ToPlatform();
                    control.TintColor = entry.CursorColor.ToPlatform();
                    control.AttributedPlaceholder = new NSAttributedString(entry.Placeholder ?? string.Empty, new UIStringAttributes { ForegroundColor = entry.PlaceholderColor.ToPlatform(), Font = UIFont.SystemFontOfSize((nfloat)entry.PlaceholderFontSize) });
                }
            }
        }
    }
