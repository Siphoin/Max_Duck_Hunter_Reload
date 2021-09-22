using Microsoft.Xna.Framework.Content;

namespace BaseEngine.Models.Audio
{
    public abstract class AudioManagerBase
    {
        protected ContentManager contentManager;

       public abstract void LoadAudioData();
    }
}
