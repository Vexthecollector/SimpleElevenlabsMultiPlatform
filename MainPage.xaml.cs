using CommunityToolkit.Maui.Views;
using ElevenLabs;
using ElevenLabs.Models;
using ElevenLabs.TextToSpeech;
using ElevenLabs.Voices;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Plugin.Maui.Audio;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace SimpleElevenlabsAndroid;

public partial class MainPage : ContentPage
{
    Utils utils = new Utils();
            

    public MainPage()
	{
		InitializeComponent();
		Init();
    }



	public async void Init()
	{
        if (File.Exists($"{FileSystem.AppDataDirectory}.apikey"))
        {
            try
            {

                await utils.Initialize(File.ReadAllText($"{FileSystem.AppDataDirectory}.apikey"));
                await FillVoices();

            }
            catch
            {
                await DisplayAlert("Malformed API Key", "The API Key wasn't properly stored. You will have to set it again.", "Ok");
                
            }
            //textBox1.Text = api.ElevenLabsAuthentication.ApiKey;
            //LoadDashBoard();
        }
        else
        {
            await DisplayAlert("Missing API Key","No API Key set yet, please set one in the settings", "Ok"));
            //Disable_Buttons();

            //LoadSettings();
        }
		
    }



    private void OnCounterClicked(object sender, EventArgs e)
	{
		Play_Sound(MessageBox.Text, ((EasyVoice)VoiceSelection.SelectedItem).Id);
		MessageBox.Text = "";
    }

	private void OnEntryCompleted(object sender, EventArgs e)
	{
		Play_Sound(((Entry)sender).Text, ((EasyVoice)VoiceSelection.SelectedItem).Id);
		((Entry)sender).Text = "";
	}

	private async void Play_Sound(String text,String voiceID)
	{
        ElevenLabs.Voices.VoiceSettings defaultVoiceSettings = await Manager.Configs.Api.VoicesEndpoint.GetDefaultVoiceSettingsAsync();
        HttpClient Client = new HttpClient();
        Client.DefaultRequestHeaders.Add("User-Agent", "ElevenLabs-DotNet");
        Client.DefaultRequestHeaders.Add("xi-api-key", Manager.Configs.Api.ElevenLabsAuthentication.ApiKey);
        var temp = JsonSerializer.Serialize(new TextToSpeechRequest(text, Model.MonoLingualV1, defaultVoiceSettings));
		var payload=ToJsonStringContent(temp);
        var response = await Client.PostAsync($"https://api.elevenlabs.io/v1/text-to-speech/{voiceID}", payload);
        var responseStream = await response.Content.ReadAsStreamAsync();
		var audioplayer = AudioManager.Current.CreatePlayer(responseStream);
		audioplayer.Play();
    }

    public StringContent ToJsonStringContent(string json)
	{

            return new StringContent(json, Encoding.UTF8, "application/json");
	}

	public async Task<Boolean> FillVoices()
	{
		VoiceSelection.ItemsSource = Manager.Configs.EasyVoices;
		VoiceSelection.SelectedIndex = 0;
		return true;
    }

}

