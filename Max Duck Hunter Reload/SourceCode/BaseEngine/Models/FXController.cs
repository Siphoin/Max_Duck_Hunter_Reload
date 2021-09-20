using BaseEngine.Interfaces;
using BaseEngine.Models;
using BaseEngine.Models.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEngine.Models
{
  public  class FXController : IInteractorObject
    {
        private AudioFXManager _audioFXManager;

        private DuckSpawner _duckSpawner;
        public FXController(AudioFXManager audioFXManager, DuckSpawner duckSpawner)
        {
            if (audioFXManager == null)
            {
                throw new NullReferenceException("audio fx manager reference is null");
            }

            if (duckSpawner == null)
            {
                throw new NullReferenceException("duck spawner reference is null");
            }

            _audioFXManager = audioFXManager;

            _duckSpawner = duckSpawner;
        }

        public void Load(ContentManager content)
        {
           // nothing logic
        }

        public void OnDestroy()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            _duckSpawner.OnDuckKill += PlayFXKillDuck;
        }

        private void PlayFXKillDuck()
        {
            _audioFXManager.GetSound("shoot").Play();
        }
    }
}
