using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class ArtificialIntelligence
    {
        #region Technical Items
        public const int DEFAULT_AI_UPDATE_DELAY = 1000; // ms
        public DateTime LastUpdate;
        public long AIUpdateDelay; // ms between updates
        #endregion

        #region AI Memory
        private Actor self;
        public Actor lastAttacker;
        public Actor lastTarget;
        #endregion

        #region AI Options
        public bool RunToAttackMelee;
        public bool RetaliateOnAttacker;
        //public bool StandGround;
        //public bool KeepRangedDistance;
        //public bool RunFromEnemyWhenHealthBelow50Percent;
        //public bool RunFromEnemyWhenHealthBelow25Percent;
        //public bool RunFromEnemyWhenHealthBelow10Percent;
        #endregion

        #region Constructor
        public ArtificialIntelligence(Actor a)
        {
            this.self = a;

            // adjust delay for reactions/intelligence...?
            this.AIUpdateDelay = DEFAULT_AI_UPDATE_DELAY;

            // set the default options
            RetaliateOnAttacker = true;
            RunToAttackMelee = true;
        }
        #endregion

        public void DecideWhatToDo()
        {
            // this is if nothing is happening and the actor is just standing there...

            // assess the state of 'self' and add new actions.
            LastUpdate = System.DateTime.Now;

            #region if Is Being Attacked
            if (self.States.IsBeingAttacked)
            {
                // make sure our attacker is still alive
                if (lastAttacker == null
                || lastAttacker.DeleteMe == true
                || lastAttacker.States.IsDying)
                {
                    // or we're not being attacked anymore
                    self.States.IsBeingAttacked = false;
                }
                else
                {
                    // if not already attacking target
                    if (self.Actions.Contains(RPGAction.ActionType.Attack, self, lastAttacker) == false)
                    {
                        // make sure our AI wants us to retaliate
                        if (this.RetaliateOnAttacker)
                        {
                            self.Act(RPGObject.Action.Attack, lastAttacker.Location, lastAttacker);
                        }
                    }
                }
            }
            #endregion

            else
            {
                // go through surroundings and stimulous

                RPGObject[] objs = Session.thisSession.thisArea.GetObjects();
                foreach (RPGObject obj in objs)
                {
                    // maybe attack other actors
                    if (obj == null || 
                        !obj.isOfType(typeof(Actor)))
                    {
                        continue;
                    }

                    Actor objActor = obj as Actor;

                    // maybe attack certain alignments
                    Actor.Alignment a = objActor.GetAlignment();
                    if (!ShouldAttackAlignment(objActor))
                    {
                        continue;
                    }

                    // maybe attack if within LOS
                    RPGCalc calc = new RPGCalc();
                    int dist = calc.DistanceBetween(self, objActor);
                    if (dist > self.LOSRange)
                    {
                        continue;
                    }

                    // we've passed all other checks, so attack
                    self.Act(RPGObject.Action.Attack, objActor.Location, obj);
                }
            }
        } // end DecideWhatToDo

        private bool ShouldAttackAlignment(Actor actor)
        {
            Actor.Alignment a = actor.GetAlignment();

            switch (self.GetAlignment())
            {
                // Good
                case (Actor.Alignment.Lawful_Good):
                case (Actor.Alignment.Neutral_Good):
                case (Actor.Alignment.Chaotic_Good):
                    {
                        // good only attacks evil
                        if (a == Actor.Alignment.Lawful_Evil ||
                            a == Actor.Alignment.Neutral_Evil ||
                            a == Actor.Alignment.Chaotic_Evil)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                // Evil
                case (Actor.Alignment.Lawful_Evil):
                case (Actor.Alignment.Neutral_Evil):
                case (Actor.Alignment.Chaotic_Evil):
                    {
                        // evil attacks all but evil
                        if (a == Actor.Alignment.Lawful_Evil ||
                            a == Actor.Alignment.Neutral_Evil ||
                            a == Actor.Alignment.Chaotic_Evil)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                // Neutral
                case (Actor.Alignment.Chaotic_Neutral):
                case (Actor.Alignment.Lawful_Neutral):
                case (Actor.Alignment.True_Neutral):
                    {
                        // Neutral attacks based on other things...

                        // first, for now, space.

                        RPGCalc calc = new RPGCalc();
                        int dist = calc.DistanceBetween(self, actor);
                        if (dist <= (actor.Width * 4)) // personal space
                        {
                            return true;
                        }
                        // Relation to PC?
                        else//if(other issues){}else
                        {
                            return false;
                        }
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public void ActorAttacked(ActorAttackedArgs e)
        {
            if (self.States.IsBeingAttacked == false)
            {
                // then we were not being attacked yet...
                lastAttacker = e.Attacker;
                self.States.IsBeingAttacked = true;
            }
            else
            {
                // we were already being attacked.
                if (lastAttacker == null)
                {
                    lastAttacker = e.Attacker;
                }
                else if(lastAttacker == e.Attacker)
                {
                    // then same attacker, do nothing...
                }
                else
                {
                    // a different attacker, then decide if we should switch...
                }
            }
        }
    }
}
