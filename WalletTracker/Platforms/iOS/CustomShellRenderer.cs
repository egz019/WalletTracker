using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using UIKit;

namespace WalletTracker.Platforms.iOS;

public class CustomShellRenderer : ShellRenderer
{
    protected override IShellItemRenderer CreateShellItemRenderer(ShellItem item)
    {
        var renderer = base.CreateShellItemRenderer(item);

        if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad && UIDevice.CurrentDevice.CheckSystemVersion(18, 0) && renderer is ShellItemRenderer shellItemRenderer)
            shellItemRenderer.TraitOverrides.HorizontalSizeClass = UIUserInterfaceSizeClass.Compact;

        return renderer;
    }
}
