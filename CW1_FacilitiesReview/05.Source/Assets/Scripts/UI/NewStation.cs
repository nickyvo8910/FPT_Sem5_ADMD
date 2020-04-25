using System;
using UnityEngine;
using System.Collections;

public class NewStation : MonoBehaviour
{

    public GameObject TxtStation;
    public GameObject TxtStationName;
    public GameObject CbUnderground;
    public GameObject CbRail;
    public GameObject CbDlr;
    public GameObject LbLocation;
    public GameObject CbWifi;
    public GameObject CbToilets;
    public GameObject CbTrolleys;
    public GameObject CbRefesh;
    public GameObject CbAtm;
    public GameObject CbPhone;
    public GameObject BtnAddStation;

    public GameObject BtnHome;
    public GameObject BtnStations;
    public GameObject BtnAbout;
    public GameObject PopupPanel;
    public GameObject BtnBack;
    public GameObject BtnConfirm;

    public string Location = "";

    private Stations crrStation;
    private Location_DAO locationDAO;
    private Stations_DAO stationDAO;
    private DataTable resuilt;



    void Start()
    {
        
    }

    // Use this for initialization
  
    public void Innit()
    {

        Debug.Log("Start Innit");
        crrStation = new Stations();
        TxtStation = GameObject.Find("txtStationID/Label");
        TxtStationName = GameObject.Find("txtStationName/Label");

        CbUnderground = GameObject.Find("Underground");
        CbRail = GameObject.Find("Rail");
        CbDlr = GameObject.Find("DLR");
        LbLocation = GameObject.Find("lbLocation");

        CbWifi = GameObject.Find("Wifi");
        CbToilets = GameObject.Find("Toilets");
        CbTrolleys = GameObject.Find("Trolleys");
        CbRefesh = GameObject.Find("Refesh");
        CbAtm = GameObject.Find("ATM");
        CbPhone = GameObject.Find("Phone");
        BtnAddStation = GameObject.Find("btnAddStation");


        Debug.Log("DAO Innit");
        stationDAO = new Stations_DAO();
        locationDAO = new Location_DAO();

        Debug.Log("Data Innit");
        TxtStation.GetComponent<UILabel>().text = (stationDAO.getMaxStaID() + 1).ToString();


        Debug.Log("Fill Data Innit");
        resuilt = locationDAO.getAllLocations();
        for (int i = 0; i < resuilt.Rows.Count; i++)
        {
            LbLocation.GetComponent<UIPopupList>().items.Add(resuilt[i]["Name"].ToString());
        }

        Debug.Log("Event Innit");
        EventDelegate.Add(LbLocation.GetComponent<UIPopupList>().onChange,
                         () =>
                         {
                             Debug.Log(LbLocation.GetComponent<UIPopupList>().value);
                             Location = LbLocation.GetComponent<UIPopupList>().value;
                         });

        EventDelegate.Add(BtnAddStation.GetComponent<UIButton>().onClick, BtnClickAddNewStation);

        Debug.Log("End Innit");
    }

    private void BtnClickAddNewStation()
    {       

        Debug.Log("Start Confirm");
        //Visible Popup
        if (PopupPanel.active)
        {
            PopupPanel.SetActive(false);
        }else
            PopupPanel.SetActive(true);
        //Disable Controls
        setControlsState(false);
        //set Button Events
        EventDelegate.Add(BtnBack.GetComponent<UIButton>().onClick, () => { PopupPanel.SetActive(false); setControlsState(true); });
        EventDelegate.Add(BtnConfirm.GetComponent<UIButton>().onClick, () => { insertStation(); PopupPanel.SetActive(false); setControlsState(true); resetAllControls(); gameObject.GetComponentInParent<AppManager>().Back_To_Home(); });
       
        
    }

    private void insertStation()
    {
        try
        {

            locationDAO = new Location_DAO();
            stationDAO = new Stations_DAO();
            crrStation = new Stations();
            crrStation.Id = int.Parse(TxtStation.GetComponent<UILabel>().text);
            crrStation.Name = TxtStationName.GetComponent<UILabel>().text;

            if (CbUnderground.GetComponent<UIToggle>().value)
            {
                crrStation.Type = 1;
            }
            if (CbRail.GetComponent<UIToggle>().value)
            {
                crrStation.Type = 2;
            }
            if (CbDlr.GetComponent<UIToggle>().value)
            {
                crrStation.Type = 3;
            }

            crrStation.LocationId = locationDAO.getLocationIDbyName(Location);

            if (CbWifi.GetComponent<UIToggle>().value)
            {
                crrStation.Wifi = 1;
            }
            if (CbToilets.GetComponent<UIToggle>().value)
            {
                crrStation.Toilet = 1;
            }
            if (CbTrolleys.GetComponent<UIToggle>().value)
            {
                crrStation.Trolleys = 1;
            }
            if (CbRefesh.GetComponent<UIToggle>().value)
            {
                crrStation.Refreshment = 1;
            }
            if (CbAtm.GetComponent<UIToggle>().value)
            {
                crrStation.Atm = 1;
            }
            if (CbPhone.GetComponent<UIToggle>().value)
            {
                crrStation.Phone = 1;
            }
            //Assumps that has disabled access
            crrStation.DisabledAccess = 1;

            //Open to confirmation

            //Check PK
            if (crrStation.Id > stationDAO.getMaxStaID())
            {

                //Insert to DB
                if (stationDAO.InsertStation(crrStation))
                {
                    //Log Success
                    Debug.ClearDeveloperConsole();
                    Debug.Log(crrStation.Id);
                    Debug.Log(crrStation.Name);
                    Debug.Log(crrStation.Type);
                    Debug.Log(crrStation.LocationId);
                    Debug.Log(crrStation.Wifi);
                    Debug.Log(crrStation.Toilet);
                    Debug.Log(crrStation.Trolleys);
                    Debug.Log(crrStation.Refreshment);
                    Debug.Log(crrStation.Atm);
                    Debug.Log(crrStation.Phone);
                    Debug.Log("Insert Success");
                    //Redirect To Home
                }
                else
                {
                    Debug.ClearDeveloperConsole();
                    Debug.Log(crrStation.Id);
                    Debug.Log(crrStation.Name);
                    Debug.Log(crrStation.Type);
                    Debug.Log(crrStation.LocationId);
                    Debug.Log(crrStation.Wifi);
                    Debug.Log(crrStation.Toilet);
                    Debug.Log(crrStation.Trolleys);
                    Debug.Log(crrStation.Refreshment);
                    Debug.Log(crrStation.Atm);
                    Debug.Log(crrStation.Phone);
                    Debug.Log("Insert Failed");

                }
            }
            else
            {
                Debug.Log("PK Issue");

            }

        }
        catch (Exception ex)
        {
            Debug.ClearDeveloperConsole();
            Debug.Log("Exception");

        }
    }

    private void setControlsState(bool stt)
    {
        BtnAddStation.GetComponent<UIButton>().enabled = stt;

//        TxtStationName.GetComponent<UILabel>().enabled = stt;
        TxtStationName.GetComponentInParent<UIInput>().enabled = stt;

        CbUnderground.GetComponent<UIToggle>().enabled = stt;
        CbRail.GetComponent<UIToggle>().enabled = stt;
        CbDlr.GetComponent<UIToggle>().enabled = stt;

        LbLocation.GetComponent<UIPopupList>().enabled = stt;

        CbWifi.GetComponent<UIToggle>().enabled = stt;
        CbToilets.GetComponent<UIToggle>().enabled = stt;
        CbTrolleys.GetComponent<UIToggle>().enabled = stt;
        CbRefesh.GetComponent<UIToggle>().enabled = stt;
        CbAtm.GetComponent<UIToggle>().enabled = stt;
        CbPhone.GetComponent<UIToggle>().enabled = stt;
    }

    private void resetAllControls(){
        TxtStation.GetComponent<UILabel>().text = "";
        TxtStationName.GetComponent<UILabel>().text = "";

        //        Debug.Log(TxtStation.GetComponent<UILabel>().text);
        CbUnderground.GetComponent<UIToggle>().value = false;
        CbRail.GetComponent<UIToggle>().value = false;
        CbDlr.GetComponent<UIToggle>().value = false;

        LbLocation.GetComponent<UIPopupList>().value = "City";

        CbWifi.GetComponent<UIToggle>().value = false;
        CbToilets.GetComponent<UIToggle>().value = false;
        CbTrolleys.GetComponent<UIToggle>().value = false;
        CbRefesh.GetComponent<UIToggle>().value = false;
        CbAtm.GetComponent<UIToggle>().value = false;
        CbPhone.GetComponent<UIToggle>().value = false;

    }
}