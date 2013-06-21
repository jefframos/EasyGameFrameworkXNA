using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameFramework.game.entity;

namespace GameFramework.game.FX
{
    class DefaultFX : DefaultEntity         
    {
        public DefaultFX(float x, float y)
        {
            position.X = (float)x;
            position.Y = (float)y;
        }
        public override void init(ContentManager Content)
        {
           
        }
        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }
        public override void kill()
        {
            dead = true;
        }       
    }
}
