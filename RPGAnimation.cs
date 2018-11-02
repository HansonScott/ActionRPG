using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RPG
{
    public enum AnimationType
    {
        Miss,
        Hit_Physical,
    }

    public class RPGAnimation: RPGObject
    {
        public AnimationType AnimType;
        public TimeSpan Duration;
        public DateTime Start;

        public RPGAnimation(AnimationType t) : this(t, new TimeSpan(0, 0, 0, 0, 250)) { }
        public RPGAnimation(AnimationType t, TimeSpan duration)
        {
            this.AnimType = t;
            this.Duration = duration;
            Start = DateTime.Now;
        }

        public override void DrawSelf(Graphics g)
        {
            new RPGDraw().DrawAnimation(g, this);
        }
    }
}
