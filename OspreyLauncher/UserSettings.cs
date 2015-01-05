using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace OspreyLauncher
{
    public class UserSettings
    {
        static UserSettings instance = null;
        Hashtable settings = new Hashtable();
        
        private UserSettings()
        {
            FileStream file = new FileStream("settings.conf", FileMode.Open, FileAccess.ReadWrite);
            StreamReader readMap = new StreamReader(file);
            BinaryFormatter bf = new BinaryFormatter();
            settings = (Hashtable)bf.Deserialize(readMap.BaseStream);
        }

        public static UserSettings GetInstance()
        {
            if (instance == null)
            {
                instance = new UserSettings();
            }
            return instance;
        }

        public bool getBooleanSetting(string key)
        {
            return (bool)settings[key];
        }
    }
}