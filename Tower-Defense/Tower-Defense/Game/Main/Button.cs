﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Button : StaticObject
    {
        private SpriteFont my8bitFont;
        private Rectangle myOffset;
        private OnClick myIsClicked; //Uncertain about naming
        private string myDisplayText;
        private float myTextSize;

        public string DisplayText
        {
            get => myDisplayText;
        }

        public Button(Vector2 aPosition, Point aSize, OnClick aClickFunction, string aDisplayText, float aTextSize) : base(aPosition, aSize)
        {
            this.myIsClicked = aClickFunction;
            this.myDisplayText = aDisplayText;
            this.myTextSize = aTextSize;

            this.myOffset = new Rectangle(
                (int)aPosition.X - (aSize.X / 96),
                (int)aPosition.Y - (aSize.Y / 96),
                aSize.X + (aSize.X / 48),
                aSize.Y + (aSize.Y / 48));
        }

        public override void Update()
        {
            base.Update();

            if (IsClicked())
            {
                myIsClicked?.Invoke();
            }
            if (IsHold())
            {
                DestRect = myOffset;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            Rectangle tempDrawRect = new Rectangle(
                (int)Camera.Position.X + DestRect.X, 
                (int)Camera.Position.Y + DestRect.Y,
                DestRect.Width, DestRect.Height);

            aSpriteBatch.Draw(myTexture, tempDrawRect, SourceRect, Color.White);
            StringManager.DrawStringMid(aSpriteBatch, my8bitFont, myDisplayText, tempDrawRect.Center.ToVector2(), Color.Black, myTextSize);
        }

        public bool IsClicked()
        {
            return
                KeyMouseReader.LeftClick() &&
                BoundingBox.Contains(KeyMouseReader.CurrentMouseState.Position);
        }
        public bool IsHold()
        {
            return BoundingBox.Contains(KeyMouseReader.CurrentMouseState.Position);
        }

        public delegate void OnClick();

        public static void Back(MainGame aGame, GameWindow aWindow)
        {
            aGame.ChangeState(new MenuState(aGame, aWindow));
        }
        public static void Editor(MainGame aGame, GameWindow aWindow)
        {
            aGame.ChangeState(new EditorState(aGame, aWindow));
        }
        public static void Leaderboard(MainGame aGame)
        {
            aGame.ChangeState(new LeaderboardState(aGame));
        }
        public static void Exit(MainGame aGame)
        {
            aGame.Exit();
        }

        public void LoadContent()
        {
            SetTexture("Border");
            my8bitFont = ResourceManager.RequestFont("8-bit");
        }
    }
}
