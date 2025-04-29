#if __IOS__
using Microsoft.Maui.Controls.PlatformConfiguration;
using UIKit;
#endif

namespace WalletTracker.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();

        //WalletPieChart.Options = new
        //{
        //    fontSize = 10,
        //    fontName = "Raleway-Regular",
        //    chartArea = new
        //    {
        //        width = "90%",
        //        height = "70%"
        //    },
        //    legend = new
        //    {
        //        position = "right",
        //        textStyle = new { fontSize = 10 },

        //    },
        //    tooltip = new
        //    {
        //        isHtml = true,
        //        showColorCode = true
        //    },
        //    animation = new
        //    {
        //        duration = 1000,
        //        easing = "out",
        //        startup = true
        //    }, 
        //    pieHole = 0.35,
        //};
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

//          #if __IOS__
//             var window = this.GetParentWindow()?.Handler?.PlatformView as UIWindow;
// 		if (window is not null)
// 		{
// 			var topPadding = window?.SafeAreaInsets.Top ?? 0;

// 			var statusBar = new UIView(new CoreGraphics.CGRect(0, 0, UIScreen.MainScreen.Bounds.Size.Width, topPadding))
// 			{
// 				BackgroundColor = UIColor.Green,
// 			};
			
// 			var view = this.Handler?.PlatformView as UIView;
// 			if (view is not null)
// 			{
// 				view?.AddSubview(statusBar);
// 			}
// 		}
// #endif
    }

}
