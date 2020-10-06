﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Team_Rocket_Game.Model.Entities;

namespace Team_Rocket_Game.Model.Factories.BulletFactories
{
    public class MidBossBulletFactory : EnemyBulletAbstractFactory
    {
        public MidBossBulletFactory()
        {
            this.InitJSON("MidBossBullets.json");
            this.json = this.input.ReadToEnd();
        }

        public override Bullet Create(Vector2 origin)
        {
            return base.Create(origin);
        }
    }
}
