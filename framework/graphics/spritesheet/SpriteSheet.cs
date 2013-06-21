using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameFramework.game.graphics.spritesheet;
using GameFramework.game.utils;

namespace GameFramework.game.graphics
{
    public class SpriteSheet
    {
        private string[] nameList;
        private LinkedList<AnimationStruct> animations;
        public AnimationStruct currentAnimation { get; set; }
        public Texture2D sheet { get; set; }
        public Dictionary<string, Rectangle> map { get; set; }
        private int timeElapsed;
        public int currentFrame { get; set; }
        public Rectangle this[int index]
        {
            get
            {
                // if nameList has not been set up do it now.
                if (nameList == null) nameList = map.Keys.ToArray();
                return map[nameList[index]];
            }
            private set { }
        }
        public SpriteSheet(Texture2D _sheet, Dictionary<string, Rectangle> _map)
        {
            sheet = _sheet;
            map = _map;
            animations = new LinkedList<AnimationStruct>();
            timeElapsed = 0;
        }
        public void setFrame(int _frame)
        {
            currentAnimation = null;
            currentFrame = _frame;
        }
        /**
         * Adiciona anima��es na lista de anima��es
         */
        public void addAnimation(AnimationStruct _animation)
        {
            animations.AddFirst(_animation);
        }
        /**
         *Da play na anima��o 
         */
        public void play(string label)
        {
            for (int i = 0; i < animations.Count; i++)
            {
                if (animations.ElementAt(i).name == label)
                    currentAnimation = animations.ElementAt(i);
            }
        }
        public void update()
        {
            if (currentAnimation != null)
            {
                timeElapsed++;
                if (timeElapsed > currentAnimation.timeFrame)
                {
                    currentAnimation.currentID++;
                    if (currentAnimation.currentID >= currentAnimation.frames.Count)
                    {
                        if (currentAnimation.repeat)
                            currentAnimation.currentID = 0;
                        else
                            currentAnimation.currentID = currentAnimation.frames.Count - 1;
                        if (currentAnimation.callback != null)
                            currentAnimation.callback();
                    }
                    timeElapsed = 0;
                }
                else
                    timeElapsed++;
            }
            else
            {
            }
        }

    }
}
