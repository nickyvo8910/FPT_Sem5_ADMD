using System;
using System.IO;
using UnityEngine;
using System.Collections;


public class AppManager : MonoBehaviour
{
    /// <summary>
    /// all form on app
    /// </summary>
    public GameObject[] form;

    private Photos_DAO pPhotosDao;
    private DataTable resuilt;
    private bool[] regeventform;
    /*
    /// note
    /// 0 - new station
    /// 1 - Home
    /// 2 - Find detail
    /// 3 - Top station
    /// 4 - Add new
    /// 5 - Find resuilt
    /// 6 - Photo detail
    */

    void Start()
    {
        pPhotosDao = new Photos_DAO();
        regeventform = new bool[6];
        for (int i = 0; i < regeventform.Length; i++)
        {
            regeventform[i] = false;
        }

        if (!regeventform[1])
        {
            //reg event form home
            EventDelegate.Add(form[1].GetComponent<Home_UI>().btnAddNew.GetComponent<UIButton>().onClick, Home_Click_AddNew);
            EventDelegate.Add(form[1].GetComponent<Home_UI>().btnFind.GetComponent<UIButton>().onClick, Home_Click_Find);
            EventDelegate.Add(form[1].GetComponent<Home_UI>().btnTopStationByFaclilities.GetComponent<UIButton>().onClick,
                              Home_Click_TopStationByFacilities);
        }
        resuilt = pPhotosDao.GetAllPicture();
        
        WWW www;
        for (int i = 0; i < resuilt.Rows.Count; i++)
        {
            if (!File.Exists(Application.persistentDataPath + "/" + resuilt[i]["PhotoFile"]))
            {
                www = new WWW(Application.streamingAssetsPath + "/Pictures/" + resuilt[i]["PhotoFile"]);
                // Wait for download to complete - not pretty at all but easy hack for now 
                // and it would not take long since the data is on the local device.
                while (!www.isDone) { ;}

                if (String.IsNullOrEmpty(www.error))
                {
                    System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + resuilt[i]["PhotoFile"], www.bytes);
                }
            }
        }
    }

    private void Home_Click_AddNew()
    {
        AppContext.Current.Next = 0;
        AppContext.Current.Pre = 1;


        form[AppContext.Current.Next].SetActive(true);
        form[AppContext.Current.Next].GetComponentInChildren<NewStation>().Innit();
        form[AppContext.Current.Pre].SetActive(false);
        
    }
    public void Back_To_Home()
    {
        AppContext.Current.Next = 1;
        AppContext.Current.Pre = 0;

        form[AppContext.Current.Next].SetActive(true);
        form[AppContext.Current.Pre].SetActive(false);
    }

    private void Home_Click_Find()
    {
        AppContext.Current.Next = 5;
        AppContext.Current.Pre = 1;
        form[AppContext.Current.Next].SetActive(true); Debug.Log(form[AppContext.Current.Pre].GetComponent<Home_UI>().Location);
        form[AppContext.Current.Next].GetComponent<FindResult_UI>().ShowResult(form[AppContext.Current.Pre].GetComponent<Home_UI>().lbCity.GetComponent<UIPopupList>().value);
        form[AppContext.Current.Pre].SetActive(false);
        EventDelegate.Add(
            form[AppContext.Current.Next].GetComponent<FindResult_UI>().ButtonBack.GetComponent<UIButton>().onClick,
            () =>
                {
                    form[AppContext.Current.Next].GetComponent<FindResult_UI>().DesTroyResuiltItem();
                    form[AppContext.Current.Next].SetActive(false);
                    form[AppContext.Current.Pre].SetActive(true);
                });

    }

    public void Home_Click_TopStationByFacilities()
    {
        AppContext.Current.Pre = 1;
        AppContext.Current.Next = 3;

        form[AppContext.Current.Next].SetActive(true);
        form[AppContext.Current.Pre].SetActive(false);

        //reg event form Top Station
        if (!regeventform[3])
        {
            EventDelegate.Add(form[3].GetComponent<TopStations>().ButtonBack.GetComponent<UIButton>().onClick,
                          TopStation_Facilities);
        }
    }

    public void TopStation_Facilities()
    {
        form[AppContext.Current.Next].SetActive(false);
        form[AppContext.Current.Pre].SetActive(true);
    }

    public void Top_Station_ClickDetail(int _stationid)
    {
        AppContext.Current.Pre = 3;
        AppContext.Current.Next = 2;
        form[AppContext.Current.Next].SetActive(true);
        form[AppContext.Current.Next].GetComponent<StationFindDetail_UI>().DisplayDetail(_stationid);
        form[AppContext.Current.Pre].SetActive(false);     
    }

}
