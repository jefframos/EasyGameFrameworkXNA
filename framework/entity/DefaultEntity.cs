using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameFramework.game.graphics;
using Microsoft.Xna.Framework.Content;
using GameFramework.game.utils;
using GameFramework.game.layer;
using GameFramework.framework.primitives;
using GameFramework.game.FX;

namespace GameFramework.game.entity
{

    class DefaultEntity : Drawable
    {
        public Vector2 position;
        public Vector2 velocity;
        public float gravity;
        public double idRandom { get; set; }
        public int range { get; set; }
        protected int limitBaseY;
        private float jumpForce;
        public Texture2D texture { get; set; }
        public Dictionary<string, Rectangle> spriteSheetPositions { get; set; }
        public SpriteSheet spriteSheet { get; set; }
        public Boolean dead { get; set; }
        public Boolean isInitialized { get; set; }
        public Layer parentLayer { get; set; }
        public Vector2 scale { get; set; }
        public SpriteEffects spriteEffects { get; set; }
        public Vector2 centerPosition;
        public Boolean collidable { get; set; }
        public Boolean deadly { get; set; }
        public float rotation { get; set; }
        public string effect { get; set; }
        protected ContentManager content;
        protected bool inFloor;
        protected float bounce;
        protected Color color;
        //public AbstractBehaviour behaviour { get; set; }
        protected Rectangle bounds;
        public DefaultEntity()
        {
            isInitialized = false;
            dead = false;
            gravity = 0f;
            limitBaseY = 1000;
            jumpForce = 10;
            position = new Vector2(10, 10);
            velocity = new Vector2(0, gravity);
            scale = new Vector2(1, 1);
            spriteEffects = SpriteEffects.None;
            centerPosition = new Vector2();
            collidable = true;
            deadly = false;
            bounce = 0.2f;
            color = Color.White;
            rotation = 0;
            bounds = new Rectangle(0, 0, 800, 480);
        }
        /**
         * Avisa a entidade em quais objetos ela está colidindo
         */
        public virtual void collide(LinkedList<DefaultEntity> inCollideList)
        {
        }
        public virtual DefaultFX getDieEffect()
        {
            return null;
        }
        override public void update(GameTime gameTime)
        {
            if (spriteSheet != null)
                spriteSheet.update();

            if (position.Y - velocity.Y >= limitBaseY && velocity.Y >= 0)
            {
                floorCollide();
            }
            else
                velocity.Y += gravity;

            position.Y += velocity.Y;
            position.X += velocity.X;
        }
        protected virtual void floorCollide()
        {
            inFloor = true;
            position.Y = limitBaseY;
            velocity.Y = 0;
        }
        public virtual void kill()
        {
            dead = true;
        }
        public void jump()
        {
            velocity.Y -= jumpForce;
        }

        public override void init(ContentManager Content)
        {
            content = Content;
            isInitialized = true;
        }
        override public void dispose()
        {
            texture.Dispose();
        }
        /**
         * Verifica se a entidade está nos bounds da tela e já altera as velocidades para não sair
         */
        protected bool apllyBoundsCollide(bool forceFeedback)
        {
            bool bCollide = false;
            if (velocity.X > 0 && position.X > (bounds.X + bounds.Width) - centerPosition.X * 2 + velocity.X)
            {
                if(forceFeedback)
                    velocity.X = -Math.Abs(velocity.X * bounce);
                else
                    velocity.X = 0;
                bCollide = true;
            }
            else if (velocity.X < 0 && position.X < bounds.X + velocity.X)
            {
                if (forceFeedback)
                    velocity.X = Math.Abs(velocity.X * bounce);
                else
                    velocity.X = 0;
                bCollide = true;
            }
            if (velocity.Y > 0 && position.Y > (bounds.Y + bounds.Height) - centerPosition.Y * 2 + velocity.Y)
            {
                if (forceFeedback)
                    velocity.Y = -Math.Abs(velocity.Y * bounce);
                else
                    velocity.Y = 0;
                bCollide = true;
            }
            else if (velocity.Y < 0 && position.Y < bounds.Y + velocity.Y)
            {
                if (forceFeedback)
                    velocity.Y = Math.Abs(velocity.Y * bounce);
                else
                    velocity.Y = 0;
                bCollide = true;
            }
            return bCollide;

        }
        override public void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            if (texture != null && !texture.IsDisposed)
            {
                if (spriteSheet != null)
                {
                    if (spriteSheet.currentAnimation != null)
                        spriteBatch.Draw(texture, position, spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(spriteSheet.currentAnimation.currentID)], color, rotation, new Vector2(), scale, spriteEffects, 0f);
                    else
                        spriteBatch.Draw(texture, position, spriteSheet[spriteSheet.currentFrame], color, rotation, new Vector2(), scale, spriteEffects, 0f);
                }
                else
                {
                    spriteBatch.Draw(texture, position, null, color, rotation, new Vector2(), scale, spriteEffects, 0f);
                }
            }

        }
    }
}
