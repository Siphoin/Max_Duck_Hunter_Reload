using Newtonsoft.Json;
using System;

namespace BaseEngine.Models.Audio
{
    [Serializable]
   public class SongContainer
    {
        [JsonRequired]
        public string[] Songs { get; private set; }

        public SongContainer ()
        {
            Songs = new string[0];
        }
    }
}
