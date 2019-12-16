﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Tile : StaticObject
    {
        private Texture2D myGroundTexture;
        private List<Tile> myHistory; //Used for pathfinding
        private Color myColor;
        private bool myIsObstacle;
        private char myTileType;
        private int myTileForm;
        private string myTerrainType;

        public List<Tile> History
        {
            get => myHistory;
            set => myHistory = value;
        }

        public bool IsObstacle
        {
            get => myIsObstacle;
            set => myIsObstacle = value;
        }
        public char TileType
        {
            get => myTileType;
            set => myTileType = value;
        }

        public int TileForm
        {
            get => myTileForm;
            set => myTileForm = value;
        }

        public Vector2 GetCenter()
        {
            return new Rectangle(DestRect.X - (int)myOrigin.X, DestRect.Y - (int)myOrigin.Y, mySize.X, mySize.Y).Center.ToVector2();
        }

        public Tile(Vector2 aPosition, Point aSize, char aTileType, string aTerrainType) : base(aPosition, aSize)
        {
            this.myTileType = aTileType;
            this.myTerrainType = aTerrainType;

            DefineTileProperties();
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            if (myGroundTexture == null)
            {
                aSpriteBatch.Draw(myTexture, myDestRect, mySourceRect, myColor, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            }
            else
            {
                aSpriteBatch.Draw(myGroundTexture, myDestRect, mySourceRect, myColor, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                //Obstacle texture is drawn within depth class
            }
        }

        public void DrawEditor(SpriteBatch aSpriteBatch)
        {
            if (myGroundTexture == null)
            {
                aSpriteBatch.Draw(myTexture, myDestRect, mySourceRect, myColor);
            }
            else
            {
                aSpriteBatch.Draw(myGroundTexture, myDestRect, mySourceRect, myColor);
                aSpriteBatch.Draw(myTexture, myDestRect, mySourceRect, Color.White);
            }
        }

        public void DefineTileProperties()
        {
            switch (myTileType)
            {
                case '#':
                    this.myIsObstacle = true;
                    this.myColor = new Color(240, 200, 200);
                    this.myTileForm = StaticRandom.RandomNumber(6, 10);
                    break;
                case '/':
                    this.myIsObstacle = false;
                    this.myColor = new Color(150, 150, 240);
                    this.myTileForm = StaticRandom.RandomNumber(4, 6);
                    break;
                default:
                    this.myIsObstacle = false;
                    this.myColor = new Color(255, 255, 255);
                    this.myTileForm = StaticRandom.RandomNumber(0, 4);
                    break;
            }
        }

        public void SetTexture()
        {
            switch (myTileType)
            {
                case '#':
                    SetTexture(myTerrainType + "_Tile_" + Extensions.NumberFormat(myTileForm));
                    myGroundTexture = ResourceManager.RequestTexture(myTerrainType + "_Tile_00");
                    break;
                default:
                    SetTexture(myTerrainType + "_Tile_" + Extensions.NumberFormat(myTileForm));
                    myGroundTexture = null;
                    break;
            }
        }
        public void SetTextureEditor()
        {
            switch (myTileType)
            {
                case '#':
                    SetTexture(myTerrainType + "_Tile_" + Extensions.NumberFormat(myTileForm));
                    myGroundTexture = ResourceManager.RequestTexture(myTerrainType + "_Tile_00");
                    break;
                default:
                    SetTexture(myTerrainType + "_Tile_" + Extensions.NumberFormat(myTileForm));
                    myGroundTexture = null;
                    break;
            }
        }
    }
}
