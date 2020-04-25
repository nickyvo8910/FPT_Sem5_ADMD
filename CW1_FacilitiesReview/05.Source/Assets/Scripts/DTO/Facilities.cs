using UnityEngine;
using System.Collections;

public class Facilities
{
    private string _id;
    private string _name;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }
}
