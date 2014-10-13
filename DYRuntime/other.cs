using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace DYRuntime
{
    internal class other
    {
        static other mo;
        internal static other GetOther()
        {
            if (mo == null)
            {
                mo = new other();
            }
            return mo;
        }

        internal void createKeySub(string hkmlKey, string name, string val)
        {
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey mykey = hkml.OpenSubKey(hkmlKey, true);
            mykey.SetValue(name, val);
        }

        internal string GetRegistData(string hkmlKey, string name)
        {
            string registData;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey autoRun = hkml.OpenSubKey(hkmlKey, true);
            registData = autoRun.GetValue(name).ToString();
            return registData;
        }

        internal void DeleteRegist(string hkmlKey, string name)
        {
            string[] subkeyNames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey autoRun = hkml.OpenSubKey(hkmlKey, true);
            subkeyNames = autoRun.GetValueNames();
            foreach (string keyName in subkeyNames)
            {
                if (keyName == name)
                {
                    autoRun.DeleteValue(name);
                }
            }
        }

        internal bool IsRegeditExit(string hkmlKey, string name)
        {
            bool _exit = false;
            try
            {
                string[] subkeyNames;
                RegistryKey hkml = Registry.LocalMachine;
                RegistryKey autoRun = hkml.OpenSubKey(hkmlKey, true);
                subkeyNames = autoRun.GetValueNames();
                foreach (string keyName in subkeyNames)
                {
                    if (keyName == name)
                    {
                        _exit = true;
                        return _exit;
                    }
                }
            }
            catch
            { }
            return _exit;
        }

    }
}
