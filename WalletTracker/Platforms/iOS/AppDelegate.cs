using Foundation;
using UIKit;

namespace WalletTracker;

[Register(nameof(AppDelegate))]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    // public override bool WillFinishLaunching(UIApplication application, NSDictionary launchOptions)
    // {
    // 	// UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
    //     // if (statusBar != null && statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
    //     // {
    //     //     statusBar.BackgroundColor =UIColor.Yellow;
    //     // }

    //     return base.WillFinishLaunching(application, launchOptions);
    // }

    public override bool WillFinishLaunching(UIApplication application, NSDictionary launchOptions)
    {
        return base.WillFinishLaunching(application, launchOptions);

        
    }
}
