using ElevenLabs;
using ElevenLabs.Models;
using ElevenLabs.TextToSpeech;
using ElevenLabs.Voices;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Plugin.Maui.Audio;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace SimpleElevenlabsMultiPlatform;

public partial class MainPage : ContentPage
{
    Utils utils = new Utils();
    

    public MainPage()
    {
        Manager.Configs.MainPage = this;
        InitializeComponent();
        //Init();
    }

    public void OnLoaded(object sender, EventArgs e)
    {
        Init();
    }

    public async Task<Boolean> Init()
    {
        if (await SecureStorage.Default.GetAsync("xi-api-key") != null)
        {
            try
            {
                await utils.Initialize(await SecureStorage.Default.GetAsync("xi-api-key"));
                await FillVoices();
                await FillModels();
                return true;
            }
            catch
            {

                await DisplayAlert("Malformed API Key", "The API Key wasn't properly stored. You will have to set it again.", "Ok");
                MainPage.LoadSettings();
            }
            //textBox1.Text = api.ElevenLabsAuthentication.ApiKey;
            //LoadDashBoard();
        }
        else
        {
            await DisplayAlert("Missing API Key", "No API Key set yet, please set one in the settings", "Ok");
            //Disable_Buttons();

            MainPage.LoadSettings();
        }
        return false;

    }

    public static async void LoadSettings()
    {
        
        await Shell.Current.GoToAsync("//Settings");
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        try
        {

        Play_Sound(MessageBox.Text, ((EasyVoice)VoiceSelection.SelectedItem).Id);
        MessageBox.Text = "";
        } catch
        {

        }
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        try
        {

        Play_Sound(((Entry)sender).Text, ((EasyVoice)VoiceSelection.SelectedItem).Id);
        ((Entry)sender).Text = "";
        }
        catch { }
    }

    private async void Play_Sound(String text, String voiceID)
    {


        ElevenLabs.Voices.VoiceSettings defaultVoiceSettings = await Manager.Configs.Api.VoicesEndpoint.GetDefaultVoiceSettingsAsync();
        GetSliderValues(ref defaultVoiceSettings);
        HttpClient Client = new HttpClient();
        Client.DefaultRequestHeaders.Add("User-Agent", "ElevenLabs-DotNet");
        Client.DefaultRequestHeaders.Add("xi-api-key", Manager.Configs.Api.ElevenLabsAuthentication.ApiKey);
        var temp = JsonSerializer.Serialize(new TextToSpeechRequest(text, Manager.Configs.Model, defaultVoiceSettings));
        var payload = ToJsonStringContent(temp);
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

    public async Task<Boolean> FillModels()
    {
        ModelSelection.ItemsSource = Manager.Configs.EasyModels; 
        ModelSelection.SelectedIndex = 0;

        return true;
    }

    public void SetSliderValues(VoiceSettings voiceSettings)
    {
        ClaritySlider.Value = voiceSettings.SimilarityBoost * 100f;
        StabilitySlider.Value = voiceSettings.Stability*100f;
        StyleSlider.Value = voiceSettings.Style*100f;
        SpeakerBoost.IsChecked = voiceSettings.SpeakerBoost;
    }

    public void GetSliderValues(ref VoiceSettings voiceSettings)
    {
        voiceSettings.SimilarityBoost = (float)ClaritySlider.Value /100f;
        voiceSettings.Stability = (float)StabilitySlider.Value /100f;
        voiceSettings.Style = (float) StyleSlider.Value /100f;
        voiceSettings.SpeakerBoost = SpeakerBoost.IsChecked; 
    }

    public void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        SetSliderValues(Manager.Configs.AllVoices.Find(voice =>  voice.Id == ((EasyVoice)VoiceSelection.SelectedItem).Id).Settings);
        } catch { }
    }

    public void OnModelChanged(object sender, EventArgs e)
    {
       
        Manager.Configs.Model = Manager.Configs.Models.First(model => model.Id == ((EasyModel)ModelSelection.SelectedItem).Id);
    }


}

