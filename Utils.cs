using ElevenLabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleElevenlabsMultiPlatform
{
    public class Utils
    {
        public async Task<Boolean> Initialize(string apikey)
        {
            try
            {

                Manager.Configs.Api = new ElevenLabsClient(apikey);
                await get_Voices();
                await get_Current_User();
                await get_Current_Models();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async void Store_API_Key(string apikey)
        {
            await SecureStorage.Default.SetAsync("xi-api-key", apikey);
            //var myFile = File.Create($"{FileSystem.AppDataDirectory}.apikey");
            //myFile.Close();
            //File.WriteAllText($"{FileSystem.AppDataDirectory}.apikey", apikey);
        }

        public async Task<Boolean> get_Voices()
        {
            Manager.Configs.AllVoices = (List<ElevenLabs.Voices.Voice>)await Manager.Configs.Api.VoicesEndpoint.GetAllVoicesAsync();
            Console.WriteLine(Manager.Configs.AllVoices);
            List<EasyVoice> voices = new List<EasyVoice>();
            foreach(var voice in Manager.Configs.AllVoices)
            {
                voices.Add(new EasyVoice(voice.Id,voice.Name));
            }
            Manager.Configs.EasyVoices = voices;
            return true;
        }



        public async Task get_Current_User()
        {
            Manager.Configs.UserInfo = await Manager.Configs.Api.UserEndpoint.GetUserInfoAsync();
            return;
        }
        
        public async Task get_Current_Models()
        {
            Manager.Configs.Models = await Manager.Configs.Api.ModelsEndpoint.GetModelsAsync();
            return;
        }
    }
}
