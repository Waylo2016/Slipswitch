namespace Slipswitch;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        blazorWebView.WebResourceRequested += (sender, e) =>
        {
            if (!IsAppModuleScript(e.Uri))
                return;

            e.Handled = true;
            var packagePath = "wwwroot" + Uri.UnescapeDataString(e.Uri.AbsolutePath);
            e.SetResponse(200, "OK", "text/javascript", FileSystem.OpenAppPackageFileAsync(packagePath)!);
        };
	}
	
	private static bool IsAppModuleScript(Uri uri) =>
		uri.Host == "0.0.0.1"
		&& uri.AbsolutePath.EndsWith(".mjs", StringComparison.OrdinalIgnoreCase);
}
