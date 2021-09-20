using BaseEngine;
using BaseEngine.Interfaces;
using BaseEngine.Models;
using BaseEngine.Models.Audio;
using Exception;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BaseEngine
{
    public class BaseEngine : Game
    {
        private const string NAME_FOLBER_RESOURCES = "Resources";


        private List<IInteractorObject> _interactorObjects;

        public event Action<int, Vector2> OnMouseDown;

        private GraphicsDeviceManager _graphics;

        private Background _background;

        private DuckSpawner _duckSpawner;

        private BackgrounsMusic _backgrounsMusic;

        private AudioFXManager _audioFXManager;

        private FXController _fXController;

        private SpriteBatch _spriteBatch;

        private MouseInput _mouseInput;

        public BaseEngine()
        {
            _mouseInput = new MouseInput(this);

            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = Constants.SCREEN_WIDTH;

            _graphics.PreferredBackBufferHeight = Constants.SCREEN_HEIGHT;

            Content.RootDirectory = NAME_FOLBER_RESOURCES;
           
            IsMouseVisible = true;

            Window.KeyDown += Quit;

            _interactorObjects = new List<IInteractorObject>();
        }


        private void Quit(object sender, InputKeyEventArgs key)
        {
            if (key.Key == Keys.Escape)
            {
                Exit();
            }
        }

        protected override void Initialize()
        {
            _background = new Background();

            _duckSpawner = new DuckSpawner(this, _mouseInput);

            _backgrounsMusic = new BackgrounsMusic();

            _audioFXManager = new AudioFXManager(Content);

            _fXController = new FXController(_audioFXManager, _duckSpawner);

            RegisterInteractorObject(_background);

            RegisterInteractorObject(_duckSpawner);

            RegisterInteractorObject(_backgrounsMusic);

            RegisterInteractorObject(_fXController);

            _audioFXManager.LoadSounds();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < _interactorObjects.Count; i++)
            {
                _interactorObjects[i].Load(Content);
                
            }
        }

        protected override void Update(GameTime gameTime)
        {
            UpdateLogic(gameTime);

            CheckMouseDown();

            base.Update(gameTime);
        }

        private void CheckMouseDown()
        {
            MouseState mouseState = Mouse.GetState();

            Vector2 mousePosition = mouseState.Position.ToVector2();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                OnMouseDown?.Invoke(0, mousePosition);
            }


            if (mouseState.RightButton == ButtonState.Pressed)
            {
                OnMouseDown?.Invoke(1, mousePosition);
            }
        }

        private void UpdateLogic(GameTime gameTime)
        {
            for (int i = 0; i < _interactorObjects.Count; i++)
            {
                if (_interactorObjects[i] is IUpdateLogicObject)
                {
                    IUpdateLogicObject logicObject = (IUpdateLogicObject)_interactorObjects[i];

                    logicObject.Update(gameTime);
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin();

            DrawObjects(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawObjects(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _interactorObjects.Count; i++)
            {
                if (_interactorObjects[i] is IDrawableObject)
                {
                    IDrawableObject drawObject = (IDrawableObject)_interactorObjects[i];

                    drawObject.Draw(spriteBatch);
                }
            }
        }

        public void RegisterInteractorObject (IInteractorObject @object)
        {
            if (@object == null)
            {
                throw new NullReferenceException("interactor object is null");
            }

            if (_interactorObjects.Contains(@object))
            {
                throw new InteractionObjectContainsOnMemoryException("interactor object registered on engine");
            }

            _interactorObjects.Add(@object);

            @object.Load(Content);

            @object.Start();
        }

        public void RemoveInteractorObject (IInteractorObject @object)
        {
            if (@object == null)
            {
                throw new NullReferenceException("interactor object is null");
            }

            if (!_interactorObjects.Contains(@object))
            {
                throw new InteractionObjectNotContainsOnMemoryException("interactor object not registered on engine");
            }

            @object.OnDestroy();

            _interactorObjects.Remove(@object);


        }

    }
}
