using Microsoft.Xna.Framework.Audio;
using Newtonsoft.Json;
using System;

namespace BaseEngine.Models.Audio
{
    [Serializable]
  public  class SoundEffectContainer
    {
        [JsonRequired]
        public string[] Sounds { get; private set; }

        public SoundEffectContainer ()
        {
            Sounds = new string[0];
        }
    }
}
