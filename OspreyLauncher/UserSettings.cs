using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace OspreyLauncher
{
    public class UserSettings
    {
        static UserSettings instance = null;
        Hashtable settings = new Hashtable();

        private UserSettings()
        {
            bool haveHadChance = false;
            string filePath = GetFilePath();
            while(!SettingsExist())
            {
                string configurationMakerPath = "OspreyLauncherConfigurationMaker.exe";
                if (!File.Exists(configurationMakerPath) || haveHadChance) // can't give them a chance or they have already had a chance to configure.
                    throw new SettingsNotFoundException();
                else
                {
                    // Launch the configuration maker
                    ProcessStartInfo processInfo = new ProcessStartInfo();
                    processInfo.FileName = configurationMakerPath;
                    Process configurationMaker = Process.Start(processInfo);
                    configurationMaker.WaitForExit();
                    haveHadChance = true;
                }
            }
            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            StreamReader readMap = new StreamReader(file);
            BinaryFormatter bf = new BinaryFormatter();
            settings = (Hashtable)bf.Deserialize(readMap.BaseStream);
        }

        private bool SettingsExist()
        {
            return File.Exists(GetFilePath());            
        }
        
        private string GetFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"OspreyLauncher.conf");
        }

        public string GetCEFSettingsPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"OspreyLauncherCEFCache");
        }

        public static UserSettings GetInstance()
        {
            if (instance == null)
            {
                instance = new UserSettings();
            }
            return instance;
        }

        public bool isx86System()
        {
            return getBooleanSetting("x86system");
        }

        public bool isInDebugMode()
        {
            return getBooleanSetting("debugMode");
        }

        public int getControlPort()
        {
            return getIntegerSetting("controlPort");
        }

        public string getFrontendUrl()
        {
            return getStringSetting("frontendURL");
        }

        private string getStringSetting(string key)
        {
            return (string)settings[key];
        }

        private bool getBooleanSetting(string key)
        {
            return (bool)settings[key];
        }

        private int getIntegerSetting(string key)
        {
            return (int)settings[key];
        }
    }
}