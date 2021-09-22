using BaseEngine.Interfaces;
using Exception;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BaseEngine.Models.Audio
{
    public class AudioFXManager : AudioManagerBase
    {
        private SoundEffectContainer _container;

        private Dictionary<string, SoundEffect> _soundsList;

        public AudioFXManager (ContentManager contentManager)
        {
            if (contentManager == null)
            {
                throw new NullReferenceException("content manager refrence is null");
            }
            _container = new SoundEffectContainer();

            _soundsList = new Dictionary<string, SoundEffect>();

            this.contentManager = contentManager;
        }

        public override void LoadAudioData()
        {
            try
            {
                string data = File.ReadAllText(Constants.PATH_MANIFEST_FX_AUDIO);
                _container = JsonConvert.DeserializeObject<SoundEffectContainer>(data);

                for (int i = 0; i < _container.Sounds.Length; i++)
                {
                    string id = _container.Sounds[i];

                    SoundEffect soundEffect = contentManager.Load<SoundEffect>($"{Constants.PATH_RESOURCES_FX_AUDIO}{id}");

                    _soundsList.Add(id, soundEffect);
                }
            }
            catch
            {
                throw new FileNotFoundException("file config audio fx not found or not valid");
            }

        }

        public SoundEffect GetSound(string id)
        {
            if (!_soundsList.ContainsKey(id))
            {
                throw new SoundEffectNotFoundException($"sound effect (id {id}) not found on list sounds of audio manager");
            }

            return _soundsList[id];
        }
    }
}
