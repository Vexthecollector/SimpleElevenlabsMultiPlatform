namespace SimpleElevenlabsAndroid;


public partial class Settings : ContentPage
{
	Utils utils = new Utils();
	public Settings()
	{
		InitializeComponent();
	}

	public async void setKey(object sender,EventArgs e)
	{
        ActivityIndicator indicator = new ActivityIndicator{ IsRunning = true , Color=Colors.Purple};
        if (await utils.Initialize(Apikey.Text))
        {
            indicator.IsRunning = false;
            if(await DisplayAlert("Store Key?","Would you like to store the API Key for future use?","Yes","No")) utils.Store_API_Key(Apikey.Text);
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
        else
        {
            indicator.IsRunning = false;
            //var popup = new WarningPopup("The key you entered is invalid");
            //this.ShowPopup(popup);
            await DisplayAlert("Wrong Key", "The key you entered was invalid.","Ok");
        }

    }

    private void Try_Get_Api_Key()
    {
        try
        {
            Apikey.Text = Manager.Configs.Api.ElevenLabsAuthentication.ApiKey;
        }
        catch (Exception ex) { 
        
            Console.WriteLine(ex.ToString());
        }
    }
}