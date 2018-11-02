using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace RPG
{
    public class RPGObject
    {
        /// <summary>
        /// for AI purposes
        /// </summary>
        public enum Action
        {
            Move,
            Attack,
            Talk,
            Get,
            Use
        }

        /// <summary>
        /// for graphical purposes
        /// </summary>
        public enum ActionState
        {
            Standing,
            Walking,
            Attacking,
            Dying
        }

        public enum FacingDirection
        {
            North,
            South,
            East,
            West
        }

        #region Declarations
        private Size size;
        private Point loc;

        private int m_BaseSpeed;
        private int m_CurrentSpeed;
        public RPGEffect[] Effects;

        private int destX;// destination of movement
        private int destY;// destination of movement
        private int vX; // speed of horizontal movement
        private int vY;// speed of vertical movement

        private Color primaryColor = Color.Black;
        private Color secondaryColor = Color.Black;


        private FacingDirection dir;

        public ActionState currentState;
        public RPGAction currentAction;
        public ActionQueue Actions;

        private bool selected;
        public bool DeleteMe;

        private string m_Name;

        private bool m_Obstacle; // stops walking?
        protected string m_desc;

        #endregion

        #region Properties
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public FacingDirection Direction
        {
            get { return dir; }
            set { dir = value; }
        }
        public int Height
        {
            get { return size.Height; }
            set { size.Height = value; }
        }
        public int Width
        {
            get { return size.Width; }
            set { size.Width = value; }
        }
        public Point Location
        {
            get { return loc; }
            set { loc = value; }
        }
        public Size Size
        {
            get { return size; }
            set { size = value; }
        }
        public int X
        {
            get { return loc.X; }
            set { loc.X = value; }
        }
        public int Y
        {
            get { return loc.Y; }
            set { loc.Y = value; }
        }
        public int VX
        {
            get { return vX; }
            set { vX = value; }
        }
        public int VY
        {
            get { return vY; }
            set { vY = value; }
        }
        public Color Color1
        {
            get { return primaryColor; }
            set { primaryColor = value; }
        }
        public Color Color2
        {
            get { return secondaryColor; }
            set { secondaryColor = value; }
        }
        public bool IsSelected
        {
            get { return selected; }
            set { selected = value; }
        }
        public int CurrentSpeed
        {
            get { return m_CurrentSpeed; }
            set { m_CurrentSpeed = value; }
        }
        public int BaseSpeed
        {
            get { return m_BaseSpeed; }
            set { m_BaseSpeed = value; }
        }
        public bool ImpedesWalking
        {
            get { return m_Obstacle; }
            set { m_Obstacle = value; }
        }
        public string Description
        {
            get { return m_desc; }
        }

        #endregion

        #region Constructor
        public RPGObject()
        {
            // start in random place
            size = new Size(25, 40);
            loc = new Point(new RPGCalc().Roll(PanelAction.PANEL_WIDTH - this.Width),
                            new RPGCalc().Roll(PanelAction.PANEL_HEIGHT - this.Height));
            dir = FacingDirection.South;

            // start with random colors
            Color1 = new RPGCalc().RandomColor();
            Color2 = new RPGCalc().RandomColor();

            currentState = ActionState.Standing;
            Actions = new ActionQueue(this);
            Effects = new RPGEffect[RPGEffect.MAX_EFFECTS];
        }
        #endregion

        #region Low level Events
        /// <summary>
        ///  draw static or dynamic images to represent self on the action field (graphics passed in)
        /// </summary>
        /// <param name="g"></param>
        public virtual void DrawSelf(Graphics g)
        {
            new RPGDraw().DrawObject(g, this);
        }
        /// <summary>
        /// Update data for movement, AI, death, etc.
        /// </summary>
        public virtual void UpdateSelf()
        {
            // actions handled in Actor

            // handle non-actor effects?
        }
        #endregion

        #region Middle level Events
        public void CalculateMovement(Point destination)
        {
            this.destX = destination.X;
            this.destY = destination.Y;

            int distanceX = destX - this.X;
            int distanceY = destY - this.Y;

            double angle = Math.Atan((double)distanceY / (double)distanceX);

            this.vX = (int)(Math.Cos(angle)* CurrentSpeed);
            this.vY = (int)(Math.Sin(angle) * CurrentSpeed);

            if (destX < this.X)
            {
                // then we're traveling west
                this.vX = -this.vX;
                this.vY = -this.vY;
            }
        }
        public bool Move()
        {
            #region take a (pretend) step in that direction
            this.X += this.VX;
            this.Y += this.VY;

            #endregion

            #region Figure out if we can take that step(check for collisions)
            if (StepCollidedWithAnything())
            {
                // if not, then undo the step
                this.X -= this.VX;
                this.Y -= this.VY;
                return false;
            }
            else
            {
                return true;
            }
            #endregion
        }
        public void Turn()
        {
            Turn(new Point(this.X + this.VX, this.Y + this.VY));
        }
        public void Turn(Point p)
        {
            if (p.X > this.X + Math.Abs(this.VX)) // E
            {
                if (Math.Abs(this.X - p.X) > Math.Abs(this.Y - p.Y)) // E more than N,S
                {
                    this.Direction = FacingDirection.East;
                }
                else
                {
                    // check N/S
                    if (p.Y > this.Y + Math.Abs(this.VY)) //S
                    {
                        this.Direction = FacingDirection.South;
                    }
                    else if (p.Y < this.Y - Math.Abs(this.VX))
                    {
                        this.Direction = FacingDirection.North;
                    }
                    else
                    {
                        // don't turn, we're on the point.
                    }
                }
            }
            else if (p.X < this.X - Math.Abs(this.VX)) // W
            {
                if (Math.Abs(this.X - p.X) > Math.Abs(this.Y - p.Y)) // W more than N,S
                {
                    this.Direction = FacingDirection.West;
                }
                else
                {
                    // check N/S
                    if (p.Y > this.Y + Math.Abs(this.VY)) //S
                    {
                        this.Direction = FacingDirection.South;
                    }
                    else if (p.Y < this.Y - Math.Abs(this.VY))
                    {
                        this.Direction = FacingDirection.North;
                    }
                    else
                    {
                        // don't turn, we're on the point.
                    }
                }
            }
            else // X values are equal
            {
                // check N/S
                if (p.Y > this.Y + Math.Abs(this.VY)) //S
                {
                    this.Direction = FacingDirection.South;
                }
                else if (p.Y < this.Y - Math.Abs(this.VY))
                {
                    this.Direction = FacingDirection.North;
                }
                else
                {
                    // don't turn, we're on the point.
                }
            }
        }
        public void StopMoving()
        {
            this.destX = this.X;
            this.destY = this.Y;
            this.VX = 0;
            this.VY = 0;
        }
        #endregion

        #region High level Events
        public void Act(Action a, Point loc, RPGObject obj)
        {
            switch (a)
            {
                case(Action.Move):
                    {
                        RPGAction action = new RPGAction(RPGAction.ActionType.Walk, loc);

                        // because we want our feet to go to the location,
                        // adjust the location to be relative to the feet.
                        // Center on object for now.

                        loc.X -= this.Width / 2;
                        loc.Y -= this.Height / 2;

                        action.Source = this;
                        action.destination = loc;

                        this.currentAction = null;
                        Actions.ClearAndAddNew(action);
                        break;
                    }
                case (Action.Attack):
                    {
                        RPGAction action = new RPGAction(RPGAction.ActionType.Attack, obj);
                        action.target = obj;
                        action.Source = this;

                        this.currentAction = null;
                        Actions.ClearAndAddNew(action);
                        break;
                    }
                case (Action.Get):
                    {
                        RPGAction action = new RPGAction(RPGAction.ActionType.Get, obj);

                        action.target = obj;
                        action.Source = this;
                        // adjust the height slightly
                        action.destination = new Point(obj.X, obj.Y - (this.Height / 2));

                        this.currentAction = null;
                        Actions.ClearAndAddNew(action);
                        break;
                    }
                case (Action.Talk):
                    {
                        break;
                    }
                case (Action.Use):
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion

        #region Public methods
        public bool isOfType(Type t)
        {
            if (this.GetType() == t)
            {
                return true;
            }
            else if (this.GetType().BaseType == t)
            {
                return true;
            }
            else if (this.GetType().BaseType.BaseType == t)
            {
                return true;
            }
            else if (this.GetType().BaseType.BaseType.BaseType == t)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddEffect(RPGEffect effect)
        {
            bool result = false;
            for (int i = 0; i < Effects.Length; i++)
            {
                if (Effects[i] == null)
                {
                    Effects[i] = effect;
                    result = true;
                    break;
                }
            }
            // then no open slots
            return result;
        }
        #endregion

        #region Statics
        public static RPGObject GetRandomObject()
        {
            return new RPGObject();
        }
        #endregion

        #region private methods
        private bool StepCollidedWithAnything()
        {
            foreach (RPGObject obj in Session.thisSession.thisArea.GetObjects())
            {
                if (obj != null 
                    && obj != this 
                    && obj.ImpedesWalking)
                {
                    if(new RPGCalc().ObjectsCollide(this, obj))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }

    //    public bool RemoveEffect(RPGEffect e)
    //    {
    //        for (int i = 0; i < effects.Length; i++)
    //        {
    //            if (effects[i] == e)
    //            {
    //                effects[i] = null;
    //                return true;
    //            }
    //        }
    //        // then no match found
    //        return false;
    //    }
    //    public void DisableAllEffects()
    //    {
    //        for (int i = 0; i < effects.Length; i++)
    //        {
    //            if (effects[i] != null)
    //            {
    //                if (effects[i].IsActive)
    //                {
    //                    effects[i].Disable();
    //                }
    //            }
    //        }
    //    }
    //    public void EnableAllEffects()
    //    {
    //        for (int i = 0; i < effects.Length; i++)
    //        {
    //            if (effects[i] != null)
    //            {
    //                if (effects[i].IsActive)
    //                {
    //                    effects[i].Enable();
    //                }
    //            }
    //        }
    //    }
    //    public bool HaveAnyEffects()
    //    {
    //        if (this.effects == null) { return false; }
    //        for (int i = 0; i < effects.Length; i++)
    //        {
    //            if (effects[i] != null)
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }    
}
