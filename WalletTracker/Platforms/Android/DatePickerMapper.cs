using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using WalletTracker.Controls;

namespace WalletTracker.Platforms.Android;

public static class DatePickerMapper
{
    public static async Task Map(IElementHandler handler, IElement view)
    {
        if (view is BorderedDatePicker datePicker)
        {
            var nativeDatePicker = (DatePickerHandler)handler;

            var gradientDrawable = new GradientDrawable();

            gradientDrawable.SetCornerRadius((int)ContextExtensions.ToPixels(handler.MauiContext.Context, datePicker.CornerRadius));
            gradientDrawable.SetStroke((int)ContextExtensions.ToPixels(handler.MauiContext.Context, datePicker.BorderWidth), datePicker.BorderColor.ToAndroid());
            gradientDrawable.SetColor(datePicker.BackgroundColor.ToAndroid());

            nativeDatePicker.PlatformView.Background = gradientDrawable;

            nativeDatePicker.PlatformView.SetPadding(
                (int)ContextExtensions.ToPixels(handler.MauiContext.Context, datePicker.Padding.Left),
                (int)ContextExtensions.ToPixels(handler.MauiContext.Context, datePicker.Padding.Top / 2),
                (int)ContextExtensions.ToPixels(handler.MauiContext.Context, datePicker.Padding.Right),
                (int)ContextExtensions.ToPixels(handler.MauiContext.Context, datePicker.Padding.Bottom / 2)
            );

            if(datePicker.Image != null)
            {
                var icon = await ConvertImageSourceToBitmapAsync(datePicker.Image);
                nativeDatePicker.PlatformView.SetCompoundDrawablesWithIntrinsicBounds(null, null, new BitmapDrawable(handler.MauiContext.Context.Resources, icon), null);
            }

            nativeDatePicker.PlatformView.Enabled = datePicker.Enabled;
        }
    }

    private static async Task<Bitmap> ConvertImageSourceToBitmapAsync(ImageSource imageSource)
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
            
            var imageSourceServiceResult = await provider.GetPlatformImageAsync(imageSource, mauiContext);
            var icon = imageSourceServiceResult?.Value;

            if (icon == null)
                return null;

            Bitmap bitmap;
            if (icon is BitmapDrawable bitmapDrawable)
            {
                bitmap = bitmapDrawable.Bitmap;
                //bitmap = Bitmap.CreateBitmap(bitmap, 0, 0, icon.IntrinsicWidth, icon.IntrinsicHeight);
            }
            else
            {
                bitmap = Bitmap.CreateBitmap(icon.IntrinsicWidth, icon.IntrinsicHeight, Bitmap.Config.Argb8888);
                Canvas canvas = new Canvas(bitmap);
                icon.SetBounds(0, 0, canvas.Width, canvas.Height);
                icon.Draw(canvas);
            }
            return bitmap;
        }
}
