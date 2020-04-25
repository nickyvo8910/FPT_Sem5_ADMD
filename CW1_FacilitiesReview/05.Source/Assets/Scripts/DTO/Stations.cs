using UnityEngine;
using System.Collections;

public class Stations
{

    private int _id;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    private string _name;
    private int type;

    public int Type
    {
        get { return type; }
        set { type = value; }
    }
    private int _locationId;

    public int LocationId
    {
        get { return _locationId; }
        set { _locationId = value; }
    }
    private float _wifi;

    public float Wifi
    {
        get { return _wifi; }
        set { _wifi = value; }
    }
    private float _trolleys;

    public float Trolleys
    {
        get { return _trolleys; }
        set { _trolleys = value; }
    }
    private float _refreshment;

    public float Refreshment
    {
        get { return _refreshment; }
        set { _refreshment = value; }
    }
    private float _atm;

    public float Atm
    {
        get { return _atm; }
        set { _atm = value; }
    }
    private float _toilet;

    public float Toilet
    {
        get { return _toilet; }
        set { _toilet = value; }
    }
    private float _phone;

    public float Phone
    {
        get { return _phone; }
        set { _phone = value; }
    }
    private float _disabledAccess;

    public float DisabledAccess
    {
        get { return _disabledAccess; }
        set { _disabledAccess = value; }
    }
    private bool _status;

    public bool Status
    {
        get { return _status; }
        set { _status = value; }
    }
   
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

  
}
