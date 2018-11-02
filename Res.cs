#region using
using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;
using System.Media;
#endregion 

namespace RPG
{
    public class Res
    {
        public static bool USE_SOUNDS = false;
        public static string appPath = System.Environment.CurrentDirectory + Path.DirectorySeparatorChar;
        public static string ImagePath = appPath + "Images" + Path.DirectorySeparatorChar;

        #region data members
        private static Hashtable pics = new Hashtable();
        #endregion

        #region static methods
        public static Bitmap getPic(String filename)
        {
            Bitmap result = (Bitmap)pics[filename];
            if (result == null) 
            {
                string fullFilePath = ImagePath + filename;
                if (System.IO.File.Exists(fullFilePath))
                {
                    pics[filename] = new Bitmap(fullFilePath);
                    result = (Bitmap)pics[filename];
                }
                else
                {
                    string blank = ImagePath + "blank.gif";
                    result = (Bitmap)pics[blank];
                }
            }
            return result;
        }
    #endregion

    #region private methods
    #endregion
    } // end class
} // end namespace