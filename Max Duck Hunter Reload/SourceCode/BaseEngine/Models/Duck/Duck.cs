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
        public float _currentAngleY = 0;

        public event Action<Duck> OnClickPlayer;

        public event Action<Duck> OnExitScreen;

        public event Action<Duck> OnRemove;

        private MouseInput _mouseInput;

        private ContentManager _contentManager;

        private Dictionary<Type, IState<Duck>> _stateMap;

        private IState<Duck> _currentState;

        public DuckDirection Direction { get; private set; }

        public Vector2 CurrentPosition { get; set; } = new Vector2(Constants.SCREEN_WIDTH, Constants.SCREEN_HEIGHT);

        public Duck(MouseInput mouseInput)
        {
            if (mouseInput == null)
            {
                throw new NullReferenceException("mouse input reference is null");
            }

            _mouseInput = mouseInput;


            _stateMap = new Dictionary<Type, IState<Duck>>();
        }


        public void Load(ContentManager content)
        {
            Random random = new Random();

            int indexDirection = random.Next(0, 2);

            Direction = (DuckDirection)indexDirection;

            Debug.WriteLine(Direction);

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

            Random random = new Random();

            GeneratePosition();

        }

        private void GeneratePosition()
        {
            Random randomPositionGenerator = new Random();

            int ScreenX = Constants.SCREEN_WIDTH / 2;

            int ScreenY = Constants.SCREEN_HEIGHT / 12;

            int x = Direction == DuckDirection.Left ? ScreenX + Constants.BORDER_SPAWN_DUCK : ScreenX - Constants.BORDER_SPAWN_DUCK;

            int y = randomPositionGenerator.Next(ScreenY * -1, ScreenY);

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

            

            spriteBatch.Draw(Texture, CurrentPosition, Rectangle, Color.White);
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

        public void SetTextureDeath() => LoadTexture("Img/duck_down", _contentManager);


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

        public void RotateDownAngle() => _currentAngleY = 90;

        public void Dispose() => _mouseInput = null;

        public void OnDestroy() => _mouseInput.OnMouseDown -= CheckMouseClick;
    }
}
