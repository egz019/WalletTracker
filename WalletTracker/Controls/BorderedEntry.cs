using System.ComponentModel.Design.Serialization;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace WalletTracker.Controls;

public class BorderedEntry : Microsoft.Maui.Controls.Entry
{
    public BorderedEntry()
    {
        BackgroundColor = Colors.Transparent;
    }

    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(BorderedEntry), Colors.Black);
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(BorderedEntry), 1);
    public int BorderWidth
    {
        get => (int)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    public static readonly BindableProperty CursorColorProperty = BindableProperty.Create(nameof(CursorColor), typeof(Color), typeof(BorderedEntry), Colors.Black, propertyChanged: OnCursorColorChanged);
    public Color CursorColor
    {
        get => (Color)GetValue(CursorColorProperty);
        set => SetValue(CursorColorProperty, value);
    }

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(BorderedEntry), 0);
    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(BorderedEntry), new Thickness(8));
    public Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }

    public static readonly new BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(BorderedEntry), Colors.Gray);
    public new Color PlaceholderColor
    {
        get => (Color)GetValue(PlaceholderColorProperty);
        set => SetValue(PlaceholderColorProperty, value);
    }

    public static readonly BindableProperty PlaceholderFontSizeProperty = BindableProperty.Create(nameof(PlaceholderFontSize), typeof(double), typeof(BorderedEntry), 14.0);
    public double PlaceholderFontSize
    {
        get => (double)GetValue(PlaceholderFontSizeProperty);
        set => SetValue(PlaceholderFontSizeProperty, value);
    }

    public static readonly BindableProperty PlaceholderFontFamilyProperty = BindableProperty.Create(nameof(PlaceholderFontFamily), typeof(string), typeof(BorderedEntry), "RalewayRegular");
    public string PlaceholderFontFamily
    {
        get => (string)GetValue(PlaceholderFontFamilyProperty);
        set => SetValue(PlaceholderFontFamilyProperty, value);
    }

    public static readonly BindableProperty PlaceholderFontAttributesProperty = BindableProperty.Create(nameof(PlaceholderFontAttributes), typeof(FontAttributes), typeof(BorderedEntry), FontAttributes.None);
    public FontAttributes PlaceholderFontAttributes
    {
        get => (FontAttributes)GetValue(PlaceholderFontAttributesProperty);
        set => SetValue(PlaceholderFontAttributesProperty, value);
    }

    public static readonly BindableProperty HasAdornmentProperty = BindableProperty.Create(nameof(HasAdornment), typeof(bool), typeof(BorderedEntry), true);
    public bool HasAdornment
    {
        get => (bool)GetValue(HasAdornmentProperty);
        set => SetValue(HasAdornmentProperty, value);
    }

    private static void OnCursorColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is BorderedEntry entry)
        {
            var color = (Color)newValue;
            entry.On<iOS>().SetCursorColor(color);
        }
    }
}