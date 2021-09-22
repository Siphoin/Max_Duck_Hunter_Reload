using BaseEngine.Extensions;
using BaseEngine.Interfaces;
using BaseEngine.Models.DuckModel.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BaseEngine.Models
{
    public class Duck : DrawableObjectBase, IDrawableObject, IUpdateLogicObject, IDisposable
    {



        public event Action<Duck> OnClickPlayer;

        public event Action<Duck> OnExitScreen;

        public event Action<Duck> OnRemove;

        private MouseInput _mouseInput;

        private ContentManager _contentManager;

        private Dictionary<Type, IState<Duck>> _stateMap;

        private IState<Duck> _currentState;

        public float Angle { get; private set; }

        public DuckDirection Direction { get; private set; }

        public Color CurrentColor { get; private set; }

        public Vector2 CurrentPosition { get; set; }

        public Duck(MouseInput mouseInput)
        {
            if (mouseInput == null)
            {
                throw new NullReferenceException("mouse input reference is null");
            }

            Angle = 0;

            CurrentColor = Color.White;

            CurrentPosition = new Vector2(Constants.SCREEN_WIDTH, Constants.SCREEN_HEIGHT);

            _mouseInput = mouseInput;

            _stateMap = new Dictionary<Type, IState<Duck>>();
        }


        public void Load(ContentManager content)
        {
            Random random = new Random();

            int indexDirection = random.Next(0, 2);

            Direction = (DuckDirection)indexDirection;


            string prefixFileDuck = "_";

            if (Direction == DuckDirection.Right)
            {
                prefixFileDuck += "right";
            }

            else if (Direction == DuckDirection.Left)
            {
                prefixFileDuck += "left";
            }

            _contentManager = content;

            Rectangle = new Rectangle(0, 0, 200, 161);

            LoadTexture($"{Constants.PATH_RESOURCES_IMAGES}duck{prefixFileDuck}", _contentManager);
        }



        public void Start()
        {
            InitializeStates();

            GeneratePosition();

        }

        private void GeneratePosition()
        {
            Random randomPositionGenerator = new Random();

            int ScreenX = Constants.SCREEN_WIDTH / 2;

            int ScreenY = Constants.SCREEN_HEIGHT / 2;

            float x = Direction == DuckDirection.Left ? ScreenX + Constants.BORDER_SPAWN_DUCK : ScreenX - Constants.BORDER_SPAWN_DUCK;

            float y = (float)randomPositionGenerator.NextDouble(ScreenY * -1, ScreenY);

            CurrentPosition = new Vector2(x, y);
        }

        public void Update(GameTime gameTime)
        {
            if (_currentState != null)
            {
                _currentState.Update();
            }
        }



        public void Draw(SpriteBatch spriteBatch)
        {
           if (Texture == null)
            {
                return;
            }

            spriteBatch.Draw(Texture, CurrentPosition, null, CurrentColor, Angle, Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        private void CheckMouseClick(int indexMouse, Vector2 mousePosition)
        {
            if (indexMouse == 0)
            {
            float distance = Vector2.Distance(CurrentPosition, mousePosition);

                if (distance < Constants.RADUIS_SHOOT_DUCK)
                {
                    OnClickPlayer?.Invoke(this);

                    SetState(GetState<DuckStateDeath>());
                }
            }

        }

        #region States
        private void InitializeStates()
        {
            DuckStateFly stateFly = new DuckStateFly();

            DuckStateDeath stateDeath = new DuckStateDeath();

            AddState<DuckStateFly>(stateFly);

            AddState<DuckStateDeath>(stateDeath);

            SetStateByDefault();
        }


        protected void AddState<T>(IState<Duck> state)
        {
            state.SetOwner(this);
            _stateMap[typeof(T)] = state;
        }

        protected void SetState(IState<Duck> state)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = state;

            _currentState.Enter();
        }

        protected IState<Duck> GetState<T>() where T : IState<Duck>
        {
            var type = typeof(T);
            return _stateMap[type];
        }

        protected void SetStateByDefault() => SetState(GetState<DuckStateFly>());

        protected void SetStateDeath() => SetState(GetState<DuckStateDeath>());

        #endregion

        public void CallEventExitDuckOnScreen() => OnExitScreen?.Invoke(this);

        public void CallEventRemove() => OnRemove?.Invoke(this);

        public void SubcribeMouseDownEvent() => _mouseInput.OnMouseDown += CheckMouseClick;

        public void UncribeMouseDownEvent() => _mouseInput.OnMouseDown -= CheckMouseClick;

        public void SetAlphaColorIntensity(byte value) => CurrentColor = new Color(value, value, value, value);

        public void SetAngleRotation(float value) => Angle = value;

        public void Dispose() => _mouseInput = null;

        public void OnDestroy() => _mouseInput.OnMouseDown -= CheckMouseClick;
    }
}
