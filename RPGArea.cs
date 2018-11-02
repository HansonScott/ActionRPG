using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Drawing;

namespace RPG
{
    public class RPGArea
    {
        #region Declarations
        public static string DEFAULT_AREA = "000";
        public static int MAX_AREA_OBJECTS = 150;
        public static int MAX_AREA_EFFECTS = 10;
        public static int GRAB_MAX_DISTANCE = RPGCalc.DEFAULT_TOUCH_RANGE;

        public string AREA_PATH = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar
                                    + "Area" + Path.DirectorySeparatorChar;
        private RPGObject[] RPGObjects;
        public RPGEffect[] Effects;
        string ID;
        #endregion

        #region Constructor and Setup
        public RPGArea(string AreaFileName)
        {
            if (AreaFileName == null || AreaFileName.Length < 3)
            {
                AreaFileName = DEFAULT_AREA;
            }

            this.ID = AreaFileName;

            RPGObjects = new RPGObject[MAX_AREA_OBJECTS];
            Effects = new RPGEffect[MAX_AREA_EFFECTS];

            // add player to area
            Session.thisSession.player.Location = new Point(700, 250);
            RPGObjects[GetObjSlot()] = Session.thisSession.player;

            // add other random people.
            for (int i = 0; i < 1; i++)
            {
                Actor a = Actor.CreateRandomRobber();
                a.Location = new Point(20, 50);
                RPGObjects[GetObjSlot()] = a;
            }
            for (int i = 0; i < 3; i++)
            {
                Actor a = Actor.CreateRandomThug();
                a.Location = new Point(20 + 20 * i, 100 + 100 * i);
                RPGObjects[GetObjSlot()] = a;
            }
            for (int i = 0; i < 5; i++)
            {
                RPGObjects[GetObjSlot()] = Actor.CreateRandomArcher();
            }

            // read file and load data into memory
            try
            {
                LoadFile(System.IO.File.ReadAllText(AREA_PATH + ID + ".area"));
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        private void LoadFile(string AreaInfo)
        {
            // parse off info to load area.
        }
        #endregion

        #region Events
        public void DrawArea(Graphics g)
        {
            // draw background
            new RPGDraw().FillBack(this, g);

            // draw any area-related effects

            // draw all game objects
            SortObjects();
            foreach (RPGObject obj in RPGObjects)
            {
                if (obj != null 
                    && obj.DeleteMe == false)
                {
                    obj.DrawSelf(g);
                }
            }
        }
        #endregion

        #region Public methods
        public bool AddObject(RPGObject obj)
        {
            int s = GetObjSlot();
            if (s > -1)
            {
                RPGObjects[s] = obj;
                if (obj.isOfType(typeof(Actor)))
                {
                    (obj as Actor).ResetStats();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void RemoveObject(RPGObject obj)
        {
            for (int i = 0; i < RPGObjects.Length; i++)
            {
                if (RPGObjects[i] != null)
                {
                    if (RPGObjects[i] == obj)
                    {
                        RPGObjects[i] = null;
                        return;
                    }
                }
            }
        }
        public RPGObject[] GetObjects()
        {
            return RPGObjects;
        }
        public RPGObject[] GetObjectsYSorted()
        {
            SortObjects();
            return RPGObjects;
        }
        public RPGObject GetObjectAt(Point point)
        {
            RPGCalc calc = new RPGCalc();

            foreach (RPGObject obj in RPGObjects)
            {
                if(obj != null 
                && calc.ObjectOnPoint(obj, point))
                {
                    return obj;
                }
            }

            // if nothing found...
            return null;
        }
        public ArrayList GetItemsNear(Point point)
        {
            RPGCalc calc = new RPGCalc();
            ArrayList results = new ArrayList();

            foreach (RPGObject obj in RPGObjects)
            {
                if (obj == null) { continue; }
                // if object is not actor
                if (obj.GetType() != typeof(Actor)
                && obj.GetType() != typeof(PlayerCharacter))
                {
                    // and if object is near point
                    if (calc.DistanceBetween(obj, point) < GRAB_MAX_DISTANCE)
                    {
                        results.Add(obj);
                    }
                }
            }

            // if nothing found...
            return results;
        }
        public int GetObjSlot()
        {
            for( int i = 0; i < RPGObjects.Length; i++)
            {
                if(RPGObjects[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region private methods
        private void SortObjects()
        {
            // go through each object, and sort on Y, for drawing.
            Array.Sort(RPGObjects, delegate(RPGObject obj1, RPGObject obj2)
            {
                if (obj1 == null && obj2 == null) 
                { 
                    return 0; 
                }
                else if (obj2 == null)
                {
                    return 1;
                }
                else if (obj1 == null)
                {
                    return -1;
                }

                return obj1.Y.CompareTo(obj2.Y);
            });
        }
        #endregion
    }
}
