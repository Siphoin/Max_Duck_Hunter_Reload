using BaseEngine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace BaseEngine.Models
{
   public class DuckSpawner : IInteractorObject
    {
        public event Action OnDuckKill;

        private MouseInput _mouseInput;

        private BaseEngine _baseEngine;

        private Timer _timer;


        public DuckSpawner (BaseEngine baseEngine, MouseInput mouseInput)
        {
            if (mouseInput == null)
            {
                throw new NullReferenceException("mouse input reference is null");
            }

            if (baseEngine == null)
            {
                throw new NullReferenceException("base engine reference is null");
            }

            _mouseInput = mouseInput;

            _baseEngine = baseEngine;
        }

        public void Load(ContentManager content)
        {
        }

        public void OnDestroy()
        {
           
        }

        public void Start()
        {
            _timer = new Timer();

            _timer.Elapsed += SpawnDuck;

            _timer.AutoReset = true;

            _timer.Interval = 3000;

            _timer.Start();
        }

        private void SpawnDuck(object sender, ElapsedEventArgs e)
        {
            Duck newDuck = new Duck(_mouseInput);

            newDuck.OnClickPlayer += CallEventIncrementScore;

            newDuck.OnExitScreen += RemoveDuck;

            newDuck.OnRemove += RemoveDuck;

            _baseEngine.RegisterInteractorObject(newDuck);
        }

        private void RemoveDuck(Duck duck)
        {
            using (duck)
            {
              duck.OnClickPlayer -= RemoveDuck;

              duck.OnExitScreen -= RemoveDuck;

              duck.OnRemove += RemoveDuck;

              _baseEngine.RemoveInteractorObject(duck);
            }

            
        }

        private void CallEventIncrementScore(Duck duck) => OnDuckKill?.Invoke();
    }
}
