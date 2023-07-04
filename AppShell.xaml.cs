namespace SimpleElevenlabsMultiPlatform;

public partial class AppShell : Shell
{
	public AppShell()
	{
		Manager.Configs.Shell = this;
		InitializeComponent();
	}
}
