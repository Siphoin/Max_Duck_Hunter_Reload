using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BaseEngine.Models.Audio
{
    public class SongManager : AudioManagerBase
    {
        private SongContainer _container;

        private Song[] _songsList;
        public SongManager(ContentManager contentManager)
        {
            if (contentManager == null)
            {
                throw new NullReferenceException("content manager refrence is null");
            }

            _container = new SongContainer();

            this.contentManager = contentManager;
        }
        public override void LoadAudioData()
        {
            try {

                string data = File.ReadAllText(Constants.PATH_MANIFEST_SONGS);
                _container = JsonConvert.DeserializeObject<SongContainer>(data);

                _songsList = new Song[_container.Songs.Length];

                for (int i = 0; i < _container.Songs.Length; i++)
                {
                    string id = _container.Songs[i];

                    Song song = contentManager.Load<Song>($"{Constants.PATH_RESOURCES_MUSIC}{id}");

                    _songsList[i] = song;
                }
        }

            catch (System.Exception e)
            {
                throw new FileNotFoundException($"file config audio songs not found or not valid Error: {e.Message}");

            }
        }

        public Song[] GetSongs ()
        {
            Song[] songs = new Song[_songsList.Length];

            _songsList.CopyTo(songs, 0);

            return songs;
        }
}
    }
