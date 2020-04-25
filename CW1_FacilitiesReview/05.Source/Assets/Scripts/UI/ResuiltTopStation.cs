using UnityEngine;
using System.Collections;

public class ResuiltTopStation : MonoBehaviour
{
    public GameObject StationName;
    public GameObject Rate;
    public GameObject ButtonDetail;

    public int stationId;
    // Use this for initialization
	void Start ()
	{
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetValue(string _stationName, string _rate, int _stationid)
    {
        StationName.GetComponent<UILabel>().text = _stationName;
        Rate.GetComponent<UILabel>().text = _rate;
        stationId = _stationid;
    }

}
