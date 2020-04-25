using UnityEngine;
using System.Collections;

public class Photos
{

    private int _stationId;
    private string _photoFile;

    public int StationId
    {
        get { return _stationId; }
        set { _stationId = value; }
    }

    public string PhotoFile
    {
        get { return _photoFile; }
        set { _photoFile = value; }
    }
}
