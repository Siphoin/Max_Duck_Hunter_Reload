using BaseEngine.Interfaces;
using System;

namespace BaseEngine.Models.DuckModel.States
{
    public abstract class DuckStateBase : IState<Duck>
    {
        protected Duck Owner { get; private set; }

        public virtual void Enter()
        {
            throw new NotImplementedException();
        }

        public virtual void Exit()
        {
            throw new NotImplementedException();
        }

        public virtual void SetOwner(Duck owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("duck reference argument is null");
            }

            Owner = owner;
        }

        public virtual void Update()
        {
            throw new NotImplementedException();
        }

        protected void LogOfState (string message) => System.Diagnostics.Debug.WriteLine($"duck {message}");
    }
}
