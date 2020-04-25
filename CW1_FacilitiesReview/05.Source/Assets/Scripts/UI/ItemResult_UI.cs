using UnityEngine;
using System.Collections;

public class ItemResult_UI : MonoBehaviour
{
    public GameObject StationName;
    public GameObject Facilities;
    public GameObject Picture;


    
//    void Start()
//    {
//        Texture2D texture2D = new Texture2D(100,100);
//        texture2D.LoadImage(System.IO.File.ReadAllBytes(Application.persistentDataPath + "/picture.png"));
//        Picture.GetComponent<UITexture>().mainTexture = texture2D;
//    }

    public void SetResult(string _stationname, string _linkPic, int _wifi, int _toilet, int _trolley, int _refeshment,
                          int _atm, int _disableacces)
    {
        StationName.GetComponent<UILabel>().text = _stationname;

        Texture2D texture2D = new Texture2D(100, 100);
        texture2D.LoadImage(System.IO.File.ReadAllBytes(Application.persistentDataPath + "/" + _linkPic));
        Picture.GetComponent<UITexture>().mainTexture = texture2D;

        if(_wifi == 0)
            Facilities.GetComponent<Facilities_UI>().Facilities[0].SetActive(false);
        
        if(_toilet == 0)
            Facilities.GetComponent<Facilities_UI>().Facilities[1].SetActive(false);

        if (_trolley == 0)
            Facilities.GetComponent<Facilities_UI>().Facilities[2].SetActive(false);

        if (_refeshment == 0)
            Facilities.GetComponent<Facilities_UI>().Facilities[3].SetActive(false);

        if (_atm == 0)
            Facilities.GetComponent<Facilities_UI>().Facilities[4].SetActive(false);

        if (_disableacces == 0)
            Facilities.GetComponent<Facilities_UI>().Facilities[5].SetActive(false);

    }

    public void SetStationName(string _name, string _linkPic, int _wifi)
    {
        StationName.GetComponent<UILabel>().text = _name;
        Texture2D texture2D = new Texture2D(100, 100);
        texture2D.LoadImage(System.IO.File.ReadAllBytes(Application.persistentDataPath + "/" + _linkPic));
        Picture.GetComponent<UITexture>().mainTexture = texture2D;

        if(_wifi == 0)
            Facilities.GetComponent<Facilities_UI>().Facilities[0].SetActive(false);
    }
    
}
