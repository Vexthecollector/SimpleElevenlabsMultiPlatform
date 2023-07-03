namespace SimpleElevenlabsAndroid;
using CommunityToolkit.Maui.Views;


public partial class Settings : ContentPage
{
	Utils utils = new Utils();
	public Settings()
	{
		InitializeComponent();
	}

	public async void setKey(object sender,EventArgs e)
	{
        if (await utils.Initialize(Apikey.Text))
        {
            if(await DisplayAlert("Store Key?","Would you like to store the API Key for future use?","Yes","No")) utils.Store_API_Key(Apikey.Text);
        }
        else
        {
            //var popup = new WarningPopup("The key you entered is invalid");
            //this.ShowPopup(popup);
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