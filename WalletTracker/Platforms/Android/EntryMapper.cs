using Android.Graphics.Drawables;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;
using WalletTracker.Controls;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using Java.Time.Format;
using Android.Util;
using Android.Graphics;

namespace WalletTracker.Platforms.Android;

public static class EntryMapper
{
    public static void Map(IElementHandler handler, IElement view)
    {
        if(view is BorderedEntry entry)
        {
            var nativeEntry = (EntryHandler)handler;

            var gradientDrawable = new GradientDrawable();

            gradientDrawable.SetCornerRadius((int)ContextExtensions.ToPixels(handler.MauiContext.Context, entry.CornerRadius));
            gradientDrawable.SetStroke((int)ContextExtensions.ToPixels(handler.MauiContext.Context, entry.BorderWidth), entry.BorderColor.ToAndroid());
            gradientDrawable.SetColor(entry.BackgroundColor.ToAndroid());

            nativeEntry.PlatformView.Background = gradientDrawable;

            nativeEntry.PlatformView.SetPadding(
                (int)ContextExtensions.ToPixels(handler.MauiContext.Context, entry.Padding.Left),
                (int)ContextExtensions.ToPixels(handler.MauiContext.Context, entry.Padding.Top),
                (int)ContextExtensions.ToPixels(handler.MauiContext.Context, entry.Padding.Right),
                (int)ContextExtensions.ToPixels(handler.MauiContext.Context, entry.Padding.Bottom)
            );

            // var textStyle = TextStyle.Values(new TextStyle()
            // {
            //     FontSize = entry.FontSize,
            //     FontFamily = entry.FontFamily,
            //     FontStyle = entry.FontAttributes.HasFlag(FontAttributes.Italic) ? FontStyle.Italic : FontStyle.Normal,
            //     FontWeight = entry.FontAttributes.HasFlag(FontAttributes.Bold) ? FontWeight.Bold : FontWeight.Normal
            // });

            nativeEntry.PlatformView.SetTextSize(ComplexUnitType.Sp, (float)entry.FontSize);
            //nativeEntry.PlatformView.Typeface = (Typeface)entry.FontFamily;
            // nativeEntry.PlatformView.SetTextColor(entry.TextColor.ToAndroid());
            // nativeEntry.PlatformView.SetHintTextColor(entry.PlaceholderColor.ToAndroid());
            nativeEntry.PlatformView.UpdatePlaceholder(entry);

            nativeEntry.PlatformView.TextCursorDrawable.SetTint(entry.CursorColor.ToAndroid());
        }
    }
}