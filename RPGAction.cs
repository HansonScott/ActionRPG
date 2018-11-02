using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace RPG
{
    public class ActionQueue
    {
        public enum Priority
        {
            Essential = 0,
            Important = 1,
            Helpful = 2,
            Unimportant = 3,
            Harmful = 4,
            Dangerous = 5
        }
        private ArrayList Actions;
        private RPGObject self;
        public ActionQueue(RPGObject a)
        {
            self = a;
            Actions = new ArrayList();
        }

        public void AddFirst(RPGAction ActionToDoFirst)
        {
            // look through list of actions and add new with index min - 1;
            int min = 1;

            if (Actions.Count > 0)
            {
                min = (Actions[0] as RPGAction).Index;

                foreach (RPGAction a in Actions)
                {
                    min = Math.Min(min, a.Index);
                }
            }
            ActionToDoFirst.Index = min - 1;
            Actions.Add(ActionToDoFirst);

            switch(ActionToDoFirst.type)
            {
                case (RPGAction.ActionType.Attack):
                    {
                        ((Actor)ActionToDoFirst.Source).States.IsAttacking = true;
                        break;
                    }
                case (RPGAction.ActionType.Walk):
                case (RPGAction.ActionType.Get):
                    {
                        if (ActionToDoFirst.Source != null)
                        {
                            if (ActionToDoFirst.Source.isOfType(typeof(Actor)))
                            {
                                ((Actor)ActionToDoFirst.Source).States.IsAttacking = false;
                            }
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }
        public void AddLast(RPGAction ActionToDoLast)
        {
            // look through list of actions and add new with index max + 1;
            int max = 1;
            if (Actions.Count > 0)
            {
                max = (Actions[0] as RPGAction).Index;

                foreach (RPGAction a in Actions)
                {
                    max = Math.Max(max, a.Index);
                }
            }

            ActionToDoLast.Index = max;
            Actions.Add(ActionToDoLast);
        }
        public void Clear()
        {
            // remove all
            Actions.Clear();
        }
        public void ClearAndAddNew(RPGAction newAction)
        {
            Clear();
            AddFirst(newAction);
        }
        public RPGAction PeekNextAction()
        {
            if (Actions == null || Actions.Count == 0)
            {
                return null;
            }
            else
            {
                RPGAction firstAction;
                if (Actions.Count > 0)
                {
                    firstAction = (Actions[0] as RPGAction);

                    foreach (RPGAction a in Actions)
                    {
                        if (a.Index < firstAction.Index)
                        {
                            firstAction = a;
                        }
                    }

                    return firstAction;
                }
                return null;
            }
        }
        public RPGAction GetNextAction()
        {
            RPGAction firstAction = PeekNextAction();
            Actions.Remove(firstAction);
            return firstAction;
        }
        public bool Contains(RPGAction.ActionType t, Actor source, Actor target)
        {
            RPGAction a = new RPGAction(t, target);
            a.Source = source;
            a.target = target;
            return Contains(a);
        }
        public bool Contains(RPGAction action)
        {
            if (self.currentAction != null)
            {
                if (self.currentAction.Source == action.Source
                && self.currentAction.target == action.target
                && self.currentAction.type == action.type)
                {
                    return true;
                }
            }

            foreach (RPGAction a in Actions)
            {
                if (action.target == a.target
                && action.type == a.type
                && action.Source == a.Source)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class RPGAction
    {
        public static int DEFAULT_UPDATE_FREQUENCY = 100;
        public enum ActionType
        {
            Walk,
            Attack,
            Get
        }
        public int UpdateFrequency;
        private DateTime lastUpdate;
        public bool NeedsUpdating;
        public bool Accomplished;
        public ActionType type;
        public Point destination;
        public RPGObject target;
        public RPGObject Source;
        public int Index;

        public RPGAction(ActionType t, RPGObject targetObject)
        {
            type = t;
            NeedsUpdating = true;
            UpdateFrequency = DEFAULT_UPDATE_FREQUENCY;
            lastUpdate = DateTime.Now;
            target = targetObject;
            destination = targetObject.Location;
        }
        public RPGAction(ActionType t, Point targetLocation)
        {
            type = t;
            NeedsUpdating = true;
            UpdateFrequency = DEFAULT_UPDATE_FREQUENCY;
            lastUpdate = DateTime.Now;
            destination = targetLocation;
        }

        public void Check()
        {
            if (DateTime.Now.CompareTo(lastUpdate.AddMilliseconds(UpdateFrequency)) > 0)
            {
                NeedsUpdating = true;
            }
        }
    }
}
