using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Reviews
{

    private string _id;
    private int _stationid;
    private int _facilitiyid;
    private string _date;
    private int _rating;
    private string _comment;

    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public int Stationid
    {
        get { return _stationid; }
        set { _stationid = value; }
    }

    public int Facilitiyid
    {
        get { return _facilitiyid; }
        set { _facilitiyid = value; }
    }

    public string Date
    {
        get { return _date; }
        set { _date = value; }
    }

    public int Rating
    {
        get { return _rating; }
        set { _rating = value; }
    }

    public string Comment
    {
        get { return _comment; }
        set { _comment = value; }
    }
}
