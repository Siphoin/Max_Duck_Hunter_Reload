using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BaseEngine.Interfaces
{
    public interface IInteractorObject
    {
        void Load(ContentManager content);
        void Start();
        void OnDestroy();
    }
}
