using BaseEngine.Interfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEngine.Models
{
    public class BackgrounsMusic : IInteractorObject
    {
        private Song _music;
       

        public void OnDestroy()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            MediaPlayer.IsRepeating = true;

            MediaPlayer.Play(_music);           
        }

        public void Load(ContentManager content) => _music = content.Load<Song>($"{Constants.PATH_RESOURCES_MUSIC}music");
    }
}
