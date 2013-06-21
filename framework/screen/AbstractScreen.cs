using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using GameFramework.framework.primitives;
using Microsoft.Xna.Framework.Input.Touch;
using GameFramework.framework.GUI.button;
using GameFramework.game.layer;

namespace GameFramework.game.screen
{
    class AbstractScreen
    {
        public string screenName;
        protected SpriteBatch screenBatch;
        protected ContentManager content;
        public ScreenManager screenManager { get; set; }
        public LinkedList<object> childs;
        protected TouchCollection touches;
        protected Action outCallback;
        public AbstractScreen(string name, SpriteBatch batch, ContentManager content)
        {
            childs = new LinkedList<object>();
            screenName = name;
            this.content = content;
            screenBatch = batch;
        }

        public virtual void build()
        {
            destroy();
            screenManager.currentScreen = this;
            if (screenBatch != null && screenBatch.IsDisposed)
                screenBatch = new SpriteBatch(screenBatch.GraphicsDevice);
        }
        public virtual void init(ContentManager content)
        {
            this.content = content;
        }
        public virtual void addChild(Object child)
        {
            childs.AddLast(child);
        }
        public virtual void transitionIn()
        {
            build();
        }
        public virtual void transitionOut(Action callback)
        {
            outCallback = callback;
            callback();
        }
        public virtual void destroy()
        {
            dispose();
            screenBatch.Dispose();       
        }
        public virtual void update(GameTime gameTime)
        {
            updateChilds(gameTime);
            touches = TouchPanel.GetState();
        }
        public virtual void dispose()
        {
            while (childs.Count > 0)
                childs.RemoveFirst();
            childs = new LinkedList<object>();
        }
        public virtual void draw()
        {
            screenBatch.Begin();
            drawChilds(screenBatch);
            screenBatch.End();
        }
        /**
         * atualiza os filhos
         */
        public virtual void updateChilds(GameTime gametime)
        {
            if (childs != null && childs.Count > 0)
            {
                for (int i = 0; i < childs.Count; i++)// object temp in childs)
                {
                    if (childs.ElementAt(i) is Drawable)
                    {
                        if (childs.ElementAt(i) is SimpleButton)
                        {
                            SimpleButton tempBt = (SimpleButton)childs.ElementAt(i);
                            tempBt.updateButton(gametime, touches);
                        }
                        else
                        {
                            Drawable tempDr = (Drawable)childs.ElementAt(i);
                            tempDr.update(gametime);
                        }
                    }
                    if (childs.ElementAt(i) is Layer)
                    {
                        Layer tempLr = (Layer)childs.ElementAt(i);
                        tempLr.update(gametime);
                    }
                }
            }
        }
        /**
         * desenha os filhos
         */
        public virtual void drawChilds(SpriteBatch spriteBach)
        {
            if (childs != null)
                foreach (object temp in childs)
                {
                    if (temp is Drawable)
                    {
                        Drawable tempDr = (Drawable)temp;
                        tempDr.draw(spriteBach);
                    }
                    else if (temp is Layer)
                    {
                        Layer tempLr = (Layer)temp;
                        tempLr.draw(spriteBach);
                    }
                }
        }
    }
}
