using System;
using CoreGraphics;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using WalletTracker.Controls;

namespace WalletTracker.Platforms.iOS;

public static class DatePickerMapper
{
    public static async Task Map(IElementHandler handler, IElement view)
    {
        if (view is BorderedDatePicker datePicker)
        {
            var nativeDatePicker = (DatePickerHandler)handler;
            var control = nativeDatePicker.PlatformView;
            
            control.Layer.CornerRadius = (float)datePicker.CornerRadius;
            control.Layer.BorderWidth = (float)datePicker.BorderWidth;
            control.Layer.BorderColor = datePicker.BorderColor.ToCGColor();

            control.BackgroundColor = datePicker.BackgroundColor.ToPlatform();
            control.Font = UIFont.FromName(datePicker.FontFamily, (nfloat)datePicker.FontSize) ?? UIFont.SystemFontOfSize((nfloat)datePicker.FontSize);
            control.TextColor = datePicker.TextColor.ToPlatform();

            //  control.Frame = new CGRect(control.Frame.X, control.Frame.Y, datePicker.Width, datePicker.Height);

            control.LeftView = new UIView(new CGRect(0, 0, (nfloat)datePicker.Padding.Left, (nfloat)datePicker.Padding.Top));
            control.LeftViewMode = UITextFieldViewMode.Always;

            var calendarIcon = await ConvertImageSourceToUIImageAsync(datePicker.Image);
            var imageView = new UIImageView(calendarIcon);
            imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            imageView.Frame = new CGRect(0, 0, 25, 25);
            imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

            var container = new UIView(new CGRect(0, 0, imageView.Frame.Width + (nfloat)datePicker.Padding.Left, 20));
            container.AddSubview(imageView);
            
            // control.RightView = imageView;
            // control.RightViewMode = UITextFieldViewMode.Always;
            // control.RightView.ContentMode = UIViewContentMode.ScaleAspectFit;
            // control.RightAnchor.ConstraintEqualTo(control.RightView.RightAnchor).Active = true;

            control.RightView = container;
            control.RightViewMode = UITextFieldViewMode.Always;
            control.RightView.UserInteractionEnabled = true;
            control.RightView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                control.BecomeFirstResponder();
                control.SendActionForControlEvents(UIControlEvent.TouchUpInside);
            }));
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
