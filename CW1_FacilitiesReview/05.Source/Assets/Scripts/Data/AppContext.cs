using UnityEngine;
using System.Collections;

public class AppContext {

    private static AppContext instance;

    public static AppContext Current
    {
        get { return instance ?? (instance = new AppContext()); }
    }

    public static string Datapath
    {
        get { return datapath; }
        set { datapath = value; }
    }

    public string Location
    {
        get { return location; }
        set { location = value; }
    }

    public int Pre
    {
        get { return pre; }
        set { pre = value; }
    }

    public int Next
    {
        get { return next; }
        set { next = value; }
    }

    ////////////////   type public or private varible ///////////

    private static string datapath = Application.persistentDataPath;

    private string location = "";

    private int pre;

    private int next;

}
