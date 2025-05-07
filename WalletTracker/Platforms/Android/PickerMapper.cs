using System;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using AndroidX.ConstraintLayout.Core.Widgets;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using WalletTracker.Controls;

namespace WalletTracker.Platforms.Android;

public static class PickerMapper
{
    public static async Task Map(IElementHandler handler, IElement view)
    {
        if (view is BorderedPicker picker)
        {
            var nativePicker = (PickerHandler)handler;

            var gradientDrawable = new GradientDrawable();

            gradientDrawable.SetCornerRadius((int)ContextExtensions.ToPixels(handler.MauiContext.Context, picker.CornerRadius));
            gradientDrawable.SetStroke((int)ContextExtensions.ToPixels(handler.MauiContext.Context, picker.BorderWidth), picker.BorderColor.ToAndroid());
            gradientDrawable.SetColor(picker.BackgroundColor.ToAndroid());
            // gradientDrawable.SetSize(
            //     (int)ContextExtensions.ToPixels(handler.MauiContext.Context, picker.FontSize),
            //     (int)ContextExtensions.ToPixels(handler.MauiContext.Context, picker.FontSize)
            // );

            nativePicker.PlatformView.Background = gradientDrawable;

            nativePicker.PlatformView.SetPadding(
                (int)ContextExtensions.ToPixels(handler.MauiContext.Context, picker.Padding.Left),
                (int)ContextExtensions.ToPixels(handler.MauiContext.Context, picker.Padding.Top / 2),
                (int)ContextExtensions.ToPixels(handler.MauiContext.Context, picker.Padding.Right),
                (int)ContextExtensions.ToPixels(handler.MauiContext.Context, picker.Padding.Bottom / 2)
            );

            nativePicker.PlatformView.SetTextSize(ComplexUnitType.Px, (int)ContextExtensions.ToPixels(handler.MauiContext.Context, picker.FontSize));
            nativePicker.PlatformView.SetTextColor(picker.TextColor.ToAndroid());
            nativePicker.PlatformView.SetTypeface(Typeface.Create(picker.FontFamily, TypefaceStyle.Normal), TypefaceStyle.Normal);

            nativePicker.PlatformView.SetHint(picker, picker.Placeholder);
            nativePicker.PlatformView.SetHintTextColor(picker.PlaceholderColor.ToAndroid());

            if(picker.Image != null)
            {
                var icon = await ConvertImageSourceToBitmapAsync(picker.Image);
                icon = Bitmap.CreateScaledBitmap(icon, (int)ContextExtensions.ToPixels(handler.MauiContext.Context, 25), (int)ContextExtensions.ToPixels(handler.MauiContext.Context, 25), true);
                nativePicker.PlatformView.SetCompoundDrawablesWithIntrinsicBounds(null, null, new BitmapDrawable(handler.MauiContext.Context.Resources, icon), null);
            }

            nativePicker.PlatformView.Enabled = picker.Enabled;
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
