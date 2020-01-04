using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

namespace Tower_Defense
{
    class ShopManager : DynamicObject
    {
        //LilyPath
        private DrawBatch myDrawBatch;

        //Shop
        private SpriteFont my8bitFont;
        private Button[] myBuyOptions;
        private Tile myCurrentTile;
        private Vector2[] myBuyOptionsOffset;
        private Vector2 
            myShowPosition,
            myHidePosition;
        private int[] myBuyPrice;
        private int 
            mySelectedTower,
            myHoveredTower;

        public ShopManager(Vector2 aPosition, Point aSize, float aSpeed, GraphicsDevice aDevice, Vector2 aOffset) : base(aPosition, aSize, aSpeed)
        {
            this.myDrawBatch = new DrawBatch(aDevice);

            this.myBuyOptions = new Button[]
            {
                new Button(aPosition, new Point(118, 92), null, -1, string.Empty, 0.0f, (1.0f / 3.0f), 0.36f),
                new Button(aPosition, new Point(118, 92), null, -1, string.Empty, 0.0f, (1.0f / 3.0f), 0.36f),
                new Button(aPosition, new Point(118, 92), null, -1, string.Empty, 0.0f, (1.0f / 3.0f), 0.36f),
            };

            this.myHidePosition = aPosition - aOffset;
            this.myShowPosition = aPosition - aSize.ToVector2();

            this.myBuyOptionsOffset = new Vector2[myBuyOptions.Length];
            this.myBuyPrice = new int[myBuyOptions.Length];
            this.mySelectedTower = -1;
            this.myHoveredTower = -1;

            for (int i = 0; i < myBuyOptions.Length; i++)
            {
                myBuyOptionsOffset[i] = new Vector2(50 + (137 * (i % 2)), 24 + (147 * (i / 2))); //Values are from shop menu buy option offsets

                switch (i) 
                {
                    case 0:
                        myBuyPrice[i] = TowerProperties.Tower_00.Price;
                        break;
                    case 1:
                        myBuyPrice[i] = TowerProperties.Tower_01.Price;
                        break;
                    case 2:
                        myBuyPrice[i] = TowerProperties.Tower_02.Price;
                        break;
                }
            }
        }

        public void Update(GameTime aGameTime, GameWindow aWindow)
        {
            MenuSlide(aGameTime);
            PlaceTower();

            UpdateButtons(aWindow);
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            DrawTowerRange();

            aSpriteBatch.Draw(myTexture, Camera.TopLeftCorner + myPosition / Camera.Zoom,
                SourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f / Camera.Zoom, SpriteEffects.None, 0.0f);

            Array.ForEach(myBuyOptions, o => o.Draw(aSpriteBatch));

            for (int i = 0; i < myBuyPrice.Length; i++)
            {
                StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "$" + myBuyPrice[i].ToString(),
                    new Vector2(myBuyOptions[i].Position.X + 2, myBuyOptions[i].Position.Y + 105), Color.MediumSeaGreen, 0.5f);
            }

            if (mySelectedTower >= 0 && mySelectedTower < myBuyOptions.Length)
            {
                aSpriteBatch.Draw(myBuyOptions[mySelectedTower].Texture, Camera.ViewToWorld(KeyMouseReader.MousePos),
                    null, Color.White, 0.0f, Vector2.Zero, 0.25f / Camera.Zoom, SpriteEffects.None, 0.0f);
            }

            DrawTowerStats(aSpriteBatch);
        }

        private void DrawTowerRange()
        {
            myDrawBatch.Begin(DrawSortMode.Deferred, BlendState.AlphaBlend,
                SamplerState.AnisotropicClamp, null, null, null, Camera.TranslationMatrix);

            if (myCurrentTile != null)
            {
                switch (mySelectedTower)
                {
                    case 0: //Tower_00
                        myDrawBatch.DrawEllipse(Pen.Black, new Rectangle(
                            (int)(myCurrentTile.GetCenter().X - (TowerProperties.Tower_00.Range / 2)),
                            (int)(myCurrentTile.GetCenter().Y - (TowerProperties.Tower_00.Range / 4)),
                            (int)(TowerProperties.Tower_00.Range),
                            (int)(TowerProperties.Tower_00.Range / 2)));
                        break;
                    case 1: //Tower_01
                        myDrawBatch.DrawEllipse(Pen.Black, new Rectangle(
                            (int)(myCurrentTile.GetCenter().X - (TowerProperties.Tower_01.Range / 2)),
                            (int)(myCurrentTile.GetCenter().Y - (TowerProperties.Tower_01.Range / 4)),
                            (int)(TowerProperties.Tower_01.Range),
                            (int)(TowerProperties.Tower_01.Range / 2)));
                        break;
                    case 2: //Tower_02
                        myDrawBatch.DrawEllipse(Pen.Black, new Rectangle(
                            (int)(myCurrentTile.GetCenter().X - (TowerProperties.Tower_02.Range / 2)),
                            (int)(myCurrentTile.GetCenter().Y - (TowerProperties.Tower_02.Range / 4)),
                            (int)(TowerProperties.Tower_02.Range),
                            (int)(TowerProperties.Tower_02.Range / 2)));
                        break;
                }
            }

            myDrawBatch.End();
        }
        private void DrawTowerStats(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.End(); //For drawbatch to draw ontop of spritebatch, spritebatch must end and drawbatch begin

            myDrawBatch.Begin();

            if (myHoveredTower >= 0 && myHoveredTower < myBuyOptions.Length)
            {
                myDrawBatch.DrawRectangle(Pen.DarkGray,
                    new Vector2(myBuyOptions[myHoveredTower].Position.X - 138, myBuyOptions[myHoveredTower].Position.Y),
                    myBuyOptions[myHoveredTower].Size.X + 21,
                    myBuyOptions[myHoveredTower].Size.Y + 1);
                myDrawBatch.FillRectangle(Brush.Gray,
                    new Vector2(myBuyOptions[myHoveredTower].Position.X - 138, myBuyOptions[myHoveredTower].Position.Y),
                    myBuyOptions[myHoveredTower].Size.X + 20,
                    myBuyOptions[myHoveredTower].Size.Y);
            }

            myDrawBatch.End();

            aSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                SamplerState.AnisotropicClamp, null, null, null, Camera.TranslationMatrix);

            if (myHoveredTower >= 0 && myHoveredTower < myBuyOptions.Length)
            {
                switch (myHoveredTower)
                {
                    case 0:
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "AR:  " + TowerProperties.Tower_00.AttackRate,
                            new Vector2(myBuyOptions[myHoveredTower].Position.X - 136, myBuyOptions[myHoveredTower].Position.Y + 10), new Color(180, 180, 180), 0.5f);
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "RNG: " + TowerProperties.Tower_00.Range,
                            new Vector2(myBuyOptions[myHoveredTower].Position.X - 136, myBuyOptions[myHoveredTower].Position.Y + 34), new Color(180, 180, 180), 0.5f);
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "PWR: " + TowerProperties.Tower_00.Power,
                            new Vector2(myBuyOptions[myHoveredTower].Position.X - 136, myBuyOptions[myHoveredTower].Position.Y + 58), new Color(180, 180, 180), 0.5f);
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "TRG: " + TowerProperties.Tower_00.NumberOfTargets,
                            new Vector2(myBuyOptions[myHoveredTower].Position.X - 136, myBuyOptions[myHoveredTower].Position.Y + 82), new Color(180, 180, 180), 0.5f);
                        break;
                    case 1:
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "AR:  " + TowerProperties.Tower_01.AttackRate,
                            new Vector2(myBuyOptions[myHoveredTower].Position.X - 136, myBuyOptions[myHoveredTower].Position.Y + 10), new Color(180, 180, 180), 0.5f);
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "RNG: " + TowerProperties.Tower_01.Range,
                            new Vector2(myBuyOptions[myHoveredTower].Position.X - 136, myBuyOptions[myHoveredTower].Position.Y + 34), new Color(180, 180, 180), 0.5f);
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "PWR: " + TowerProperties.Tower_01.Power,
                            new Vector2(myBuyOptions[myHoveredTower].Position.X - 136, myBuyOptions[myHoveredTower].Position.Y + 58), new Color(180, 180, 180), 0.5f);
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "TRG: " + TowerProperties.Tower_01.AttackRate,
                            new Vector2(myBuyOptions[myHoveredTower].Position.X - 136, myBuyOptions[myHoveredTower].Position.Y + 82), new Color(180, 180, 180), 0.5f);
                        break;
                    case 2:
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "AR:  " + TowerProperties.Tower_02.AttackRate,
                            new Vector2(myBuyOptions[myHoveredTower].Position.X - 136, myBuyOptions[myHoveredTower].Position.Y + 10), new Color(180, 180, 180), 0.5f);
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "RNG: " + TowerProperties.Tower_02.Range,
                            new Vector2(myBuyOptions[myHoveredTower].Position.X - 136, myBuyOptions[myHoveredTower].Position.Y + 34), new Color(180, 180, 180), 0.5f);
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "PWR: " + TowerProperties.Tower_02.Power,
                            new Vector2(myBuyOptions[myHoveredTower].Position.X - 136, myBuyOptions[myHoveredTower].Position.Y + 58), new Color(180, 180, 180), 0.5f);
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "TRG: " + TowerProperties.Tower_02.NumberOfTargets,
                            new Vector2(myBuyOptions[myHoveredTower].Position.X - 136, myBuyOptions[myHoveredTower].Position.Y + 82), new Color(180, 180, 180), 0.5f);
                        break;
                }
            }
        }

        private void MenuSlide(GameTime aGameTime)
        {
            if (KeyMouseReader.MousePos.X > myPosition.X)
            {
                if (myPosition.X - myCurrentSpeed > myShowPosition.X)
                {
                    myCurrentSpeed = mySpeed * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
                    myPosition.X -= myCurrentSpeed;
                }
                else
                {
                    myPosition.X = myShowPosition.X;
                }
            }
            if (KeyMouseReader.MousePos.X < myPosition.X)
            {
                if (myPosition.X + myCurrentSpeed < myHidePosition.X)
                {
                    myCurrentSpeed = mySpeed * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
                    myPosition.X += myCurrentSpeed;
                }
                else
                {
                    myPosition.X = myHidePosition.X;
                }
            }
        }
        private void PlaceTower()
        {
            if (mySelectedTower != -1)
            {
                Tuple<Tile, bool> tempTile = Level.TileAtPos(Camera.ViewToWorld(KeyMouseReader.MousePos));

                if (tempTile.Item2)
                {
                    if (!tempTile.Item1.IsObstacle && UserInterface.IsMouseOutside())
                    {
                        myCurrentTile = tempTile.Item1;
                        if (KeyMouseReader.LeftClick() && GameInfo.Money >= myBuyPrice[mySelectedTower])
                        {
                            TowerManager.AddTower(mySelectedTower, tempTile.Item1.GetCenter());

                            tempTile.Item1.IsObstacle = true;
                            GameInfo.Money -= myBuyPrice[mySelectedTower];

                            mySelectedTower = -1;
                            myCurrentTile = null;
                        }
                    }
                    else
                    {
                        myCurrentTile = null;
                    }
                }
                else
                {
                    myCurrentTile = null;
                }

                if (KeyMouseReader.RightClick())
                {
                    mySelectedTower = -1;
                    myCurrentTile = null;
                }
            }
        }
        private void UpdateButtons(GameWindow aWindow)
        {
            bool tempIsHovered = false;
            for (int i = 0; i < myBuyOptions.Length; i++)
            {
                myBuyOptions[i].Update(aWindow);
                myBuyOptions[i].Position = myPosition + myBuyOptionsOffset[i];

                if (myBuyOptions[i].IsClicked())
                {
                    mySelectedTower = i;
                }
                if (myBuyOptions[i].IsHold())
                {
                    tempIsHovered = true;
                    myHoveredTower = i;
                }
            }
            if (!tempIsHovered)
            {
                myHoveredTower = -1;
            }
        }

        public void LoadContent()
        {
            my8bitFont = ResourceManager.RequestFont("8-bit");
            base.SetTexture("Shop_Menu");

            for (int i = 0; i < myBuyOptions.Length; i++)
            {
                myBuyOptions[i].SetTexture("Tower_" + Extensions.NumberFormat(i) + "_Icon");
            }
        }
    }
}
