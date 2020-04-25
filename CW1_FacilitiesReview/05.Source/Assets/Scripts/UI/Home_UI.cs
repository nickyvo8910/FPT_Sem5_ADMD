using UnityEngine;
using System.Collections;

public class Home_UI : MonoBehaviour
{

    public GameObject lbCity;
    public GameObject btnAddNew;
    public GameObject btnTopStationByFaclilities;
    public GameObject btnFind;
    public string Location = "";

    private Location_DAO location;
    private DataTable resuilt;

	// Use this for initialization
	void Start () {
    
        location = new Location_DAO();
	    resuilt = location.getAllLocations();
	    for (int i = 0; i < resuilt.Rows.Count; i++)
	    {
	        lbCity.GetComponent<UIPopupList>().items.Add(resuilt[i]["Name"].ToString());
	    }
	    EventDelegate.Add(lbCity.GetComponent<UIPopupList>().onChange,
                          () =>
                              {
                                  Debug.Log(lbCity.GetComponent<UIPopupList>().value);
                                  Location = lbCity.GetComponent<UIPopupList>().value;
                              });
	}
}
