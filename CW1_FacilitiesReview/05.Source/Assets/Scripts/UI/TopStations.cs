using UnityEngine;
using System.Collections;

public class TopStations : MonoBehaviour
{

    public GameObject[] Item;

    public GameObject ButtonBack;

    public GameObject[] btnDetail;

    public string stationname = "";

    private UIPopupList lbFacilities;

    private Stations_DAO _stations;

    private DataTable Resuilt;
    // Use this for initialization
	void Start ()
	{
        _stations = new Stations_DAO();
	    lbFacilities = GameObject.Find("lbFacility").GetComponent<UIPopupList>();
	    EventDelegate.Add(lbFacilities.onChange, UpdateResuilt);
        EventDelegate.Add(btnDetail[0].GetComponent<UIButton>().onClick, clickbutton1);
        EventDelegate.Add(btnDetail[1].GetComponent<UIButton>().onClick, clickbutton2);
        EventDelegate.Add(btnDetail[2].GetComponent<UIButton>().onClick, clickbutton3);
        EventDelegate.Add(btnDetail[3].GetComponent<UIButton>().onClick, clickbutton4);
        EventDelegate.Add(btnDetail[4].GetComponent<UIButton>().onClick, clickbutton5);
	}

    private void clickbutton1()
    {
//        Debug.Log(Item[0].GetComponent<ResuiltTopStation>().StationName.GetComponent<UILabel>().text);
        gameObject.GetComponentInParent<AppManager>()
                  .Top_Station_ClickDetail(
                      Item[0].GetComponent<ResuiltTopStation>().stationId);
//        stationname = Item[0].GetComponent<ResuiltTopStation>().StationName.GetComponent<UILabel>().text;
    }
    private void clickbutton2()
    {
        gameObject.GetComponentInParent<AppManager>()
                  .Top_Station_ClickDetail(
                      Item[1].GetComponent<ResuiltTopStation>().stationId);
    }
    private void clickbutton3()
    {
        gameObject.GetComponentInParent<AppManager>()
                  .Top_Station_ClickDetail(
                      Item[2].GetComponent<ResuiltTopStation>().stationId);
    }
    private void clickbutton4()
    {
        gameObject.GetComponentInParent<AppManager>()
                  .Top_Station_ClickDetail(
                      Item[3].GetComponent<ResuiltTopStation>().stationId);
    }
    private void clickbutton5()
    {
        gameObject.GetComponentInParent<AppManager>()
                  .Top_Station_ClickDetail(
                      Item[4].GetComponent<ResuiltTopStation>().stationId);
    }

    private void UpdateResuilt()
	{
	    string rate = "";
	    Debug.Log(lbFacilities.value);
	    Resuilt = _stations.GetTopFacilities(lbFacilities.value);
	    for (int i = 0; i  < Resuilt.Rows.Count; i++)
	    {
            switch (Resuilt[i][lbFacilities.value.ToString()].ToString())
            {
                case "1":
                    rate = "Rate: *";
                    break;
                case "2":
                    rate = "Rate: **";
                    break;
                case "3":
                    rate = "Rate: ***";
                    break;
                case "4":
                    rate = "Rate: ****";
                    break;
                case "5":
                    rate = "Rate: *****";
                    break;
            }
            Item[i].GetComponent<ResuiltTopStation>().SetValue(Resuilt[i]["Name"].ToString(), rate, int.Parse(Resuilt[i]["ID"].ToString()));
	    }
	}
}
