﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

namespace Tower_Defense
{
    class UpgradeManager : DynamicObject
    {
        //LilyPath
        private DrawBatch myDrawBatch;

        //Upgrade
        private SpriteFont my8bitFont;
        private Button[] myUpgradeOptions;
        private Button mySellButton;
        private Button myTowerIcon;
        private Tower mySelectedTower;
        private Vector2[] myUpgradeOptionsOffset;
        private Vector2
            myShowPosition,
            myHidePosition,
            mySellButtonOffset,
            myTowerIconOffset;
        private int[] myUpgradePrice;
        private string[] myUpgradeName;
        private int
            mySelectedUpgrade,
            mySellPrice;

        public Tower SelectedTower
        {
            set => mySelectedTower = value;
        }

        public UpgradeManager(Vector2 aPosition, Point aSize, float aSpeed, GraphicsDevice aDevice, Vector2 aOffset) : base(aPosition, aSize, aSpeed)
        {
            this.myDrawBatch = new DrawBatch(aDevice);

            this.myUpgradeOptions = new Button[]
            {
                new Button(aPosition, new Point(32), UpgradeAttackRate, 3, "", 0.0f, (2.0f / 3.0f), 0.70f),
                new Button(aPosition, new Point(32), UpgradeRange, 3, "", 0.0f, (2.0f / 3.0f), 0.70f),
                new Button(aPosition, new Point(32), UpgradePower, 3, "", 0.0f, (2.0f / 3.0f), 0.70f),
                new Button(aPosition, new Point(32), UpgradeNumberOfTargets, 3, "", 0.0f, (2.0f / 3.0f), 0.70f),
            };
            this.mySellButton = new Button(aPosition, new Point(96, 36), SellTower, 1, "SELL", 0.5f, (3.0f / 4.0f), (3.1f / 4.0f));
            this.myTowerIcon = new Button(aPosition, new Point(118, 92), null, -1, string.Empty, 0.0f, (1.0f / 3.0f), (1.0f / 3.0f));

            this.myHidePosition = aPosition - aOffset;
            this.myShowPosition = aPosition - aSize.ToVector2();

            this.mySellButtonOffset = new Vector2(aSize.X - 118, aSize.Y - 44); //Values from sprite
            this.myTowerIconOffset = new Vector2(25, 51); //Values from sprite

            this.myUpgradeOptionsOffset = new Vector2[myUpgradeOptions.Length];
            this.myUpgradePrice = new int[myUpgradeOptions.Length];
            this.myUpgradeName = new string[myUpgradeOptions.Length];

            for (int i = 0; i < myUpgradeOptions.Length; i++)
            {
                myUpgradeOptionsOffset[i] = new Vector2(158, 42 + (34 * i)); //Values are from upgrade menu offsets

                switch (i)
                {
                    case 0:
                        myUpgradeName[i] = "Attack Rate";
                        break;
                    case 1:
                        myUpgradeName[i] = "Range";
                        break;
                    case 2:
                        myUpgradeName[i] = "Power";
                        break;
                    case 3:
                        myUpgradeName[i] = "Targets";
                        break;
                }
            }
        }

        public void Update(GameTime aGameTime, GameWindow aWindow)
        {
            MenuSlide(aGameTime);
            UpdateButtons(aWindow);

            SetPrices();
            ClickOutside();
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            DrawTowerRange();
            DrawTowerStats(aSpriteBatch);

            aSpriteBatch.Draw(myTexture, Camera.TopLeftCorner + myPosition / Camera.Zoom,
                SourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f / Camera.Zoom, SpriteEffects.None, 0.0f);

            if (mySelectedTower != null)
            {
                if (myTowerIcon.Texture != null)
                {
                    myTowerIcon.Draw(aSpriteBatch);

                    StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, mySelectedTower.Properties.Name, myPosition + myTowerIconOffset +
                        new Vector2(58, 104), Color.LightSlateGray, 0.4f);
                }

                for (int i = 0; i < myUpgradeOptions.Length; i++)
                {
                    if (mySelectedTower.Properties.TowerLevels[i] < mySelectedTower.Properties.TowerLevelsMax[i])
                    {
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, myUpgradeName[i] + ": Lv. " + mySelectedTower.Properties.TowerLevels[i],
                            new Vector2(myUpgradeOptions[i].Position.X + 38, myUpgradeOptions[i].Position.Y + 7), Color.LightSlateGray, 0.4f);
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "$" + myUpgradePrice[i].ToString(),
                            new Vector2(myUpgradeOptions[i].Position.X + 38, myUpgradeOptions[i].Position.Y + 23), Color.MediumSeaGreen, 0.35f);
                    }
                    else
                    {
                        StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, myUpgradeName[i] + ": Lv. Max",
                            new Vector2(myUpgradeOptions[i].Position.X + 38, myUpgradeOptions[i].Position.Y + 7), Color.LightSlateGray, 0.4f);
                    }
                }

                StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "$" + mySellPrice, new Vector2(mySellButton.Position.X, mySellButton.Position.Y - 10), Color.MediumSeaGreen, 0.4f);
                mySellButton.Draw(aSpriteBatch);
            }

            Array.ForEach(myUpgradeOptions, o => o.Draw(aSpriteBatch));
        }

        private void DrawTowerRange()
        {
            if (mySelectedTower != null)
            {
                myDrawBatch.Begin(DrawSortMode.Deferred, BlendState.AlphaBlend,
                    SamplerState.AnisotropicClamp, null, null, null, Camera.TranslationMatrix);

                myDrawBatch.DrawEllipse(Pen.Black, new Rectangle(
                    (int)(mySelectedTower.OffsetPosition.X - (mySelectedTower.Properties.Range / 2)),
                    (int)(mySelectedTower.OffsetPosition.Y - (mySelectedTower.Properties.Range / 4)),
                    (int)(mySelectedTower.Properties.Range),
                    (int)(mySelectedTower.Properties.Range / 2)));

                myDrawBatch.End();
            }
        }
        private void DrawTowerStats(SpriteBatch aSpriteBatch)
        {
            if (mySelectedTower != null)
            {
                myDrawBatch.Begin(DrawSortMode.Deferred, BlendState.AlphaBlend,
                    SamplerState.AnisotropicClamp, null, null, null, Camera.TranslationMatrix);

                myDrawBatch.DrawRectangle(Pen.DarkGray,
                    new Vector2(mySelectedTower.Position.X + mySelectedTower.Size.X, mySelectedTower.Position.Y - (myTowerIcon.Size.Y / 2)),
                    myTowerIcon.Size.X + 20,
                    myTowerIcon.Size.Y);
                myDrawBatch.FillRectangle(Brush.Gray,
                    new Vector2(mySelectedTower.Position.X + mySelectedTower.Size.X, mySelectedTower.Position.Y - (myTowerIcon.Size.Y / 2)),
                    myTowerIcon.Size.X + 20,
                    myTowerIcon.Size.Y);

                myDrawBatch.End();

                StringManager.DrawStringLeft(aSpriteBatch, my8bitFont, "AR:  " + Math.Round(mySelectedTower.Properties.AttackRate, 2),
                    new Vector2(mySelectedTower.Position.X + mySelectedTower.Size.X + 2, mySelectedTower.Position.Y - (myTowerIcon.Size.Y / 2) + 10), new Color(180, 180, 180), 0.5f);
                StringManager.DrawStringLeft(aSpriteBatch, my8bitFont, "RNG: " + mySelectedTower.Properties.Range,
                    new Vector2(mySelectedTower.Position.X + mySelectedTower.Size.X + 2, mySelectedTower.Position.Y - (myTowerIcon.Size.Y / 2) + 34), new Color(180, 180, 180), 0.5f);
                StringManager.DrawStringLeft(aSpriteBatch, my8bitFont, "PWR: " + mySelectedTower.Properties.Power,
                    new Vector2(mySelectedTower.Position.X + mySelectedTower.Size.X + 2, mySelectedTower.Position.Y - (myTowerIcon.Size.Y / 2) + 58), new Color(180, 180, 180), 0.5f);
                StringManager.DrawStringLeft(aSpriteBatch, my8bitFont, "TRG: " + mySelectedTower.Properties.NumberOfTargets,
                    new Vector2(mySelectedTower.Position.X + mySelectedTower.Size.X + 2, mySelectedTower.Position.Y - (myTowerIcon.Size.Y / 2) + 82), new Color(180, 180, 180), 0.5f);
            }
        }

        private void MenuSlide(GameTime aGameTime)
        {
            if (mySelectedTower != null)
            {
                if (myPosition.Y - myCurrentSpeed > myShowPosition.Y)
                {
                    myCurrentSpeed = mySpeed * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
                    myPosition.Y -= myCurrentSpeed;
                }
                else
                {
                    myPosition.Y = myShowPosition.Y;
                }
            }
            if (mySelectedTower == null)
            {
                if (myPosition.Y + myCurrentSpeed < myHidePosition.Y)
                {
                    myCurrentSpeed = mySpeed * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
                    myPosition.Y += myCurrentSpeed;
                }
                else
                {
                    myPosition.Y = myHidePosition.Y;
                }
            }
        }
        private void UpdateButtons(GameWindow aWindow)
        {
            for (int i = 0; i < myUpgradeOptions.Length; i++)
            {
                if (myUpgradeOptions[i].IsClicked())
                {
                    mySelectedUpgrade = i;
                }

                myUpgradeOptions[i].Update(aWindow);
                myUpgradeOptions[i].Position = myPosition + myUpgradeOptionsOffset[i];
            }

            mySellButton.Update(aWindow);
            mySellButton.Position = myPosition + mySellButtonOffset;

            myTowerIcon.Update(aWindow);
            myTowerIcon.Position = myPosition + myTowerIconOffset;

            if (mySelectedTower != null)
            {
                myTowerIcon.SetTexture(mySelectedTower.GetType().Name + "_Icon");
            }
        }

        private void SetPrices()
        {
            if (mySelectedTower != null)
            {
                mySellPrice = (mySelectedTower.Properties.Price / 2);

                for (int i = 0; i < myUpgradeOptions.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            myUpgradePrice[i] = mySelectedTower.Properties.AttackRateLevel * mySelectedTower.Properties.AttackRate_Price;
                            mySellPrice += ((mySelectedTower.Properties.AttackRateLevel - 1) * mySelectedTower.Properties.AttackRate_Price) / 2;
                            break;
                        case 1:
                            myUpgradePrice[i] = mySelectedTower.Properties.RangeLevel * mySelectedTower.Properties.Range_Price;
                            mySellPrice += ((mySelectedTower.Properties.RangeLevel - 1) * mySelectedTower.Properties.Range_Price) / 2;
                            break;
                        case 2:
                            myUpgradePrice[i] = mySelectedTower.Properties.PowerLevel * mySelectedTower.Properties.Power_Price;
                            mySellPrice += ((mySelectedTower.Properties.PowerLevel - 1) * mySelectedTower.Properties.Power_Price) / 2;
                            break;
                        case 3:
                            myUpgradePrice[i] = mySelectedTower.Properties.NumberOfTargetsLevel * mySelectedTower.Properties.NumberOfTargets_Price;
                            mySellPrice += ((mySelectedTower.Properties.NumberOfTargetsLevel - 1) * mySelectedTower.Properties.NumberOfTargets_Price) / 2;
                            break;
                    }
                }

            }
        }
        private void ClickOutside()
        {
            if (mySelectedTower != null)
            {
                if (KeyMouseReader.LeftClick() && UserInterface.IsMouseOutside())
                {
                    mySelectedTower = null;
                }
            }
        }

        public void UpgradeAttackRate(GameWindow aWindow)
        {
            if (mySelectedTower != null && GameInfo.Money >= myUpgradePrice[mySelectedUpgrade])
            {
                if (mySelectedTower.Properties.TowerLevels[mySelectedUpgrade] < mySelectedTower.Properties.TowerLevelsMax[mySelectedUpgrade])
                {
                    GameInfo.Money -= myUpgradePrice[mySelectedUpgrade];

                    mySelectedTower.Properties.AttackRateLevel++;
                    mySelectedTower.Properties.AttackRateDelay *= TowerProperties.Tower_Upgrade.AttackRate_Upgrade;

                    if (mySelectedTower.Properties.AttackRate > mySelectedTower.Properties.AttackRateDelay)
                    {
                        mySelectedTower.Properties.AttackRate = mySelectedTower.Properties.AttackRateDelay;
                    }
                }
            }
        }
        public void UpgradeRange(GameWindow aWindow)
        {
            if (mySelectedTower != null && GameInfo.Money >= myUpgradePrice[mySelectedUpgrade])
            {
                if (mySelectedTower.Properties.TowerLevels[mySelectedUpgrade] < mySelectedTower.Properties.TowerLevelsMax[mySelectedUpgrade])
                {
                    GameInfo.Money -= myUpgradePrice[mySelectedUpgrade];

                    mySelectedTower.Properties.RangeLevel++;
                    mySelectedTower.Properties.Range += TowerProperties.Tower_Upgrade.Range_Upgrade;
                }
            }
        }
        public void UpgradePower(GameWindow aWindow)
        {
            if (mySelectedTower != null && GameInfo.Money >= myUpgradePrice[mySelectedUpgrade])
            {
                if (mySelectedTower.Properties.TowerLevels[mySelectedUpgrade] < mySelectedTower.Properties.TowerLevelsMax[mySelectedUpgrade])
                {
                    GameInfo.Money -= myUpgradePrice[mySelectedUpgrade];

                    mySelectedTower.Properties.PowerLevel++;
                    mySelectedTower.Properties.Power += TowerProperties.Tower_Upgrade.Power_Upgrade;
                }
            }
        }
        public void UpgradeNumberOfTargets(GameWindow aWindow)
        {
            if (mySelectedTower != null && GameInfo.Money >= myUpgradePrice[mySelectedUpgrade])
            {
                if (mySelectedTower.Properties.TowerLevels[mySelectedUpgrade] < mySelectedTower.Properties.TowerLevelsMax[mySelectedUpgrade])
                {
                    GameInfo.Money -= myUpgradePrice[mySelectedUpgrade];

                    mySelectedTower.Properties.NumberOfTargetsLevel++;
                    mySelectedTower.Properties.NumberOfTargets += TowerProperties.Tower_Upgrade.NumberOfTargets_Upgrade;
                }
            }
        }

        public void SellTower(GameWindow aWindow)
        {
            mySelectedTower.IsAlive = false;
            mySelectedTower = null;

            GameInfo.Money += mySellPrice;
        }

        public void LoadContent()
        {
            my8bitFont = ResourceManager.RequestFont("8-bit");
            base.SetTexture("Upgrade_Menu");

            for (int i = 0; i < myUpgradeOptions.Length; i++)
            {
                myUpgradeOptions[i].LoadContent();
            }
            mySellButton.LoadContent();
        }
    }
}
