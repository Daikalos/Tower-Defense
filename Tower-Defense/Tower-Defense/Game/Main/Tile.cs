﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Tile : StaticObject
    {
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
            aSpriteBatch.Draw(myTexture, myDestRect, null, myColor);
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
                    this.myColor = new Color(200, 200, 240);
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
                    break;
                default:
                    SetTexture(myTerrainType + "_Tile_" + Extensions.NumberFormat(myTileForm));
                    break;
            }
        }
        public void SetTextureEditor()
        {
            switch (myTileType)
            {
                case '#':
                    SetTexture(myTerrainType + "_Tile_" + Extensions.NumberFormat(myTileForm));
                    break;
                default:
                    SetTexture(myTerrainType + "_Tile_" + Extensions.NumberFormat(myTileForm));
                    break;
            }
        }
    }
}
