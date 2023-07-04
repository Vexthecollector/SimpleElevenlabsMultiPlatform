using ElevenLabs.User;
using ElevenLabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleElevenlabsMultiPlatform
{
    public class Manager
    {
        AppShell shell;
        MainPage mainPage;

        ElevenLabsClient api;
        ElevenLabs.Voices.Voice voice;
        List<ElevenLabs.Voices.Voice> allVoices;
        List<EasyVoice> easyVoices;
        ElevenLabs.Models.Model model;
        IReadOnlyList<ElevenLabs.Models.Model> models;
        List<EasyModel> easyModels;
        UserInfo userInfo;

        private static readonly Manager configs = new Manager();

        static Manager()
        {
        }
        private Manager()
        {
        }

        public static Manager Configs
        {
            get { return configs; }
        }

        public AppShell Shell { get => shell; set => shell = value; }
        public MainPage MainPage { get => mainPage; set => mainPage = value; }


        public ElevenLabsClient Api { get => api; set => api = value; }

        public ElevenLabs.Voices.Voice Voice { get => voice; set => voice = value; }
        public List<EasyVoice> EasyVoices { get => easyVoices; set => easyVoices = value; }
        public List<ElevenLabs.Voices.Voice> AllVoices { get => allVoices; set => allVoices = value; }
        public UserInfo UserInfo { get => userInfo; set => userInfo = value; }
        public IReadOnlyList<ElevenLabs.Models.Model> Models { get => models; set => models = value; }
        public List<EasyModel> EasyModels { get=>easyModels; set => easyModels = value; }
        public ElevenLabs.Models.Model Model { get => model; set => model = value; }
    }
}
