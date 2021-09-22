using BaseEngine.Interfaces;
using BaseEngine.Models.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEngine.Models
{
    public class BackgrounsMusic : IInteractorObject
    {
        private int _currentIndexSong;

        private SongManager _songManager;

        private Song[] _songs;


        public BackgrounsMusic (SongManager songManager)
        {
            if (songManager == null)
            {
                throw new ArgumentNullException("song manager refrence is null");
            }

            _songManager = songManager;

            _currentIndexSong = 0;
        }
       

        public void OnDestroy()
        {
            MediaPlayer.MediaStateChanged -= CheckStateMediaPlayer;
        }

        public void Start()
        {

            _songs = _songManager.GetSongs();

            MediaPlayer.MediaStateChanged += CheckStateMediaPlayer;

            MediaPlayer.Volume = 0.5f;

            PlaySong(new Random().Next(0, _songs.Length));
        }

        private void CheckStateMediaPlayer(object sender, EventArgs e)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                _currentIndexSong = MathHelper.Clamp(_currentIndexSong + 1, 0, _songs.Length - 1);
                PlaySong(_currentIndexSong);
            }
        }

        public void Load(ContentManager content)
        {
            // nothing logic
        }

        private void PlaySong(int index) => MediaPlayer.Play(_songs[index]);
    }
}
