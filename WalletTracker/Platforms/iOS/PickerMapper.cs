using System;
using CoreGraphics;
using DryIoc.ImTools;
using Foundation;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using WalletTracker.Controls;

namespace WalletTracker.Platforms.iOS;

public static class PickerMapper
{
    public static async Task Map(IElementHandler handler, IElement view)
    {
        try 
        {
        if(view is BorderedPicker picker)
        {
            var nativePicker = (PickerHandler)handler;
            var control = nativePicker.PlatformView;

            if (control != null)
            {
                control.Layer.BorderColor = picker.BorderColor.ToCGColor();
                control.Layer.BorderWidth = picker.BorderWidth;
                control.Layer.CornerRadius = picker.CornerRadius;
                control.BackgroundColor = picker.BackgroundColor.ToPlatform();
                control.Font = UIFont.FromName(picker.FontFamily, (nfloat)picker.FontSize) ?? UIFont.SystemFontOfSize((nfloat)picker.FontSize);
                control.TextColor = picker.TextColor.ToPlatform();
                control.AttributedPlaceholder = new NSAttributedString(picker.Placeholder ?? string.Empty, new UIStringAttributes { ForegroundColor = picker.PlaceholderColor.ToPlatform(), Font = UIFont.SystemFontOfSize((nfloat)picker.PlaceholderFontSize) });
        
                control.Frame = new CGRect(control.Frame.X, control.Frame.Y, picker.Width, picker.Height);

                control.LeftView = new UIView(new CGRect(0, 0, (nfloat)picker.Padding.Left, (nfloat)picker.Padding.Top));
                control.LeftViewMode = UITextFieldViewMode.Always;

                var downArrow = await ConvertImageSourceToUIImageAsync(picker.Image);
                var imageView = new UIImageView(downArrow);
                imageView.Frame = new CGRect(0, 0, 25, 25);
                imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

                var container = new UIView(new CGRect(0, 0, imageView.Frame.Width + (nfloat)picker.Padding.Left, 20));
                container.AddSubview(imageView);

				control.RightView = container;
				control.RightViewMode = UITextFieldViewMode.Always;
                control.RightView.UserInteractionEnabled = true;
                control.RightView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    control.BecomeFirstResponder();
                    control.SendActionForControlEvents(UIControlEvent.TouchUpInside);
                }));
                //control.RightView.ContentMode = UIViewContentMode.ScaleAspectFit;
                // control.RightView.Frame = new CoreGraphics.CGRect(0, 0, downArrow.Size.Width, downArrow.Size.Height);

                control.Enabled = picker.Enabled;
            }
        }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in PickerMapper: {ex.Message}");
        }
    }

    private static async Task<UIImage> ConvertImageSourceToUIImageAsync(ImageSource imageSource)
        {
            if (imageSource == null)
                return null;

            var mauiContext = Application.Current.Handler.MauiContext;

            IImageSourceService provider = imageSource switch
            {
                FileImageSource => new FileImageSourceService(),
                StreamImageSource => new StreamImageSourceService(),
                UriImageSource => new UriImageSourceService(),
                _ => null
            };

            if (provider == null)
                return null;
            
            
            var imageSourceServiceResult = await provider.GetImageAsync(imageSource, 1, default);
            //await provider.GetPlatformImageAsync(imageSource, mauiContext);
            var image = imageSourceServiceResult?.Value;

            if (image == null)
                return null;

            return image;
        }
}
