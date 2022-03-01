using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = null;
    private static object o = new object();
    private Dictionary<string, MonoBehaviour> dic = new Dictionary<string, MonoBehaviour>();

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (o)
                {
                    if (instance == null)
                    {
                        instance = new UIManager();
                    }
                }
            }
            return instance;
        }
    }

    public void RegistUI(string name, MonoBehaviour ui)
    {
        if (dic.ContainsKey(name))
        {
            return;
        }
        else
        {
            dic.Add(name, ui);
        }
    }

    public MonoBehaviour GetUI(string name)
    {
        if (dic.TryGetValue(name, out MonoBehaviour r))
        {
            return r;
        }
        return default;
    }
}
