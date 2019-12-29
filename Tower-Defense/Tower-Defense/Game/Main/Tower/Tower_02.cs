using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Tower_02 : Tower
    {
        public Tower_02(Vector2 aPosition, Point aSize) : base(aPosition, aSize)
        {
            this.Properties.Name = TowerProperties.Tower_02.Name;

            this.myProperties.FireSpeed = TowerProperties.Tower_02.FireSpeed;
            this.myProperties.Range = TowerProperties.Tower_02.Range;
            this.myProperties.Damage = TowerProperties.Tower_02.Damage;
            this.myProperties.NumberOfTargets = TowerProperties.Tower_02.NumberOfTargets;

            this.myProperties.FireSpeed_Price = TowerProperties.Tower_02.FireSpeed_Price;
            this.myProperties.Range_Price = TowerProperties.Tower_02.Range_Price;
            this.myProperties.Damage_Price = TowerProperties.Tower_02.Damage_Price;
            this.myProperties.NumberOfTargets_Price = TowerProperties.Tower_02.NumberOfTargets_Price;

            this.myProperties.TowerLevelsMax = new int[]
            {
                TowerProperties.Tower_02.FireSpeed_Level_Max,
                TowerProperties.Tower_02.Range_Level_Max,
                TowerProperties.Tower_02.Damage_Level_Max,
                TowerProperties.Tower_02.NumberOfTargets_Level_Max
            };

            this.myProperties.FireSpeedDelay = myProperties.FireSpeed;
        }

        public override void LoadContent()
        {
            base.SetTexture(this.GetType().Name);
        }
    }
}
