using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using GameFramework.game;
using GameFramework.game.utils;

namespace RufusAndTheMagicMushrooms.framework.storage
{
    class StorageManager
    {
        // public static StorageManager() { }
        public static void save(string path, string[] array)
        {
            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (store.FileExists("InTheRoot.txt"))
                    {
                        try
                        {
                            using (StreamWriter sw =
                                  new StreamWriter(store.OpenFile("InTheRoot.txt",
                                      FileMode.Open, FileAccess.Write)))
                            {
                              

                                sw.WriteLine("O QUE ESCREVER");
                                //sw.Flush();
                                sw.Close();
                            }

                        }
                        catch (IsolatedStorageException ex)
                        {
                            Trace.write("store Messag" + ex.Message.ToString());
                        }
                    }
                }
            }
            catch (IsolatedStorageException ex)
            {
                Trace.write("store Messag" + ex.Message.ToString());
            }
        }
        // public static  string[2](string path)
        //{
        //string[2] array = new string[2];
        //return array;
        //}
    }
}
