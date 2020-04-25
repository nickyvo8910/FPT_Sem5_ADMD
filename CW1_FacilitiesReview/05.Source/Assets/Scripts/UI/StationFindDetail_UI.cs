using System.IO;
using UnityEngine;
using System.Collections;

public class StationFindDetail_UI : MonoBehaviour
{
    //
    public GameObject ButtonBack;
    public GameObject StationName;
    public GameObject StationImage;
    public GameObject StationCity;
    public GameObject Wifi;
    public GameObject Toilet;
    public GameObject Trolleys;
    public GameObject Refeshment;
    public GameObject Atm;
    public GameObject DisabledAccess;
    public GameObject ButtonCountPhotos;
    public GameObject ButtonCountReview;


    public GameObject buttonAddPhotos;
    public GameObject buttonWriteReview;


    public GameObject PopupCapture;
    public GameObject WcCamera;
    public GameObject buttonCapture;
    public GameObject buttonCancel;
    
    
    // panel detail
    public GameObject ReviewDetail;
    public GameObject buttonBackDetail;

    //panel Review
    public GameObject Addreview;
    
    //photos panel
    public GameObject PhotosDetail;

    // global varible
    public int stationId;

    //
    private Stations_DAO stationsDao;
    private Reviews_DAO reviewsDao;
    private Photos_DAO photosDao;
    private Photos pPhotos;
    private DataTable resuilt;
    private WebCamTexture webCamTexture;

    
    private string ConvertPoint(string _point)
    {
        switch (_point)
        {
            case "1":
                return "*";
                break;
            case "2":
                return "**";
                break;
            case "3":
                return "***";
                break;
            case "4":
                return "****";
                break;
            case "5":
                return "*****";
                break;
            default:
                return "";
        }
    }

    public void DisplayDetail(int _stationid)
    {
        stationsDao = new Stations_DAO();
        reviewsDao = new Reviews_DAO();
        photosDao = new Photos_DAO();
        pPhotos = new Photos();
        stationId = _stationid;

        EventDelegate.Add(buttonWriteReview.GetComponent<UIButton>().onClick,
                          () =>
                              {
                                  Addreview.SetActive(true);
                                  Addreview.GetComponent<AddReview_UI>().Init(_stationid);
                                  gameObject.SetActive(false);
                              });

        EventDelegate.Add(ButtonCountPhotos.GetComponent<UIButton>().onClick,
                          () =>
                              {
                                  PhotosDetail.SetActive(true);
//                                  PhotosDetail.GetComponent<PhotoDetail_UI>().DesTroyResuiltItem();
                                  PhotosDetail.GetComponent<PhotoDetail_UI>().Setphotos(_stationid);
                                  gameObject.SetActive(false);
                              });

        EventDelegate.Add(ButtonBack.GetComponent<UIButton>().onClick, Clickback);
        EventDelegate.Add(buttonAddPhotos.GetComponent<UIButton>().onClick, ClickAddPhotos);
        EventDelegate.Add(ButtonCountReview.GetComponent<UIButton>().onClick, () => { 
            ReviewDetail.SetActive(true);
            ReviewDetail.GetComponent<ReviewDetail_UI>().ShowReview(_stationid);
            EventDelegate.Add(
                buttonBackDetail.GetComponent<UIButton>().onClick,
                () =>
                {
                    ReviewDetail.SetActive(false);
                });
        });


        resuilt = stationsDao.GetStationById(_stationid);
        StationName.GetComponent<UILabel>().text = resuilt[0]["Name"].ToString();
        StationCity.GetComponent<UILabel>().text = resuilt[0]["LocationName"].ToString();
        Wifi.GetComponent<UILabel>().text = ConvertPoint(resuilt[0]["Wifi"].ToString());
        Toilet.GetComponent<UILabel>().text = ConvertPoint(resuilt[0]["Toilet"].ToString());
        Trolleys.GetComponent<UILabel>().text = ConvertPoint(resuilt[0]["Trolley"].ToString());
        Refeshment.GetComponent<UILabel>().text = ConvertPoint(resuilt[0]["Refreshment"].ToString());
        Atm.GetComponent<UILabel>().text = ConvertPoint(resuilt[0]["ATM"].ToString());
        DisabledAccess.GetComponent<UILabel>().text = ConvertPoint(resuilt[0]["DisabledAccess"].ToString());

        //

        ButtonCountReview.GetComponentInChildren<UILabel>().text = reviewsDao.CoutNumReview(_stationid) + " Review";
        ButtonCountPhotos.GetComponentInChildren<UILabel>().text = photosDao.CoutNumPhotos(_stationid) + " Photos";
    }

    private void Clickback()
    {
        gameObject.GetComponentInParent<AppManager>().Home_Click_TopStationByFacilities();
        gameObject.SetActive(false);
    }

    private void ClickAddPhotos()
    {
        Debug.Log("init WcCamera");
        PopupCapture.SetActive(true);
        SetStateButton(false);
        EventDelegate.Add(buttonCapture.GetComponent<UIButton>().onClick,
                          ()
                          =>
                              {
                                  Texture2D texture2D = new Texture2D(webCamTexture.width, webCamTexture.height);
                                  texture2D.SetPixels(webCamTexture.GetPixels());
                                  texture2D.Apply();
                                  webCamTexture.Stop();
                                  File.WriteAllBytes(Application.persistentDataPath + "/" + photosDao.GetAllPicture().Rows.Count.ToString() + ".jpg", texture2D.EncodeToJPG());
                                  pPhotos.StationId = stationId;
                                  pPhotos.PhotoFile = photosDao.GetAllPicture().Rows.Count.ToString() + ".jpg";
                                  bool temp = photosDao.InsertPhoto(pPhotos);
                                  Debug.Log("capture " + temp);
                                  SetStateButton(true);
                                  PopupCapture.SetActive(false);
                                  webCamTexture.Stop();
                              }
            );

        EventDelegate.Add(buttonCancel.GetComponent<UIButton>().onClick, () =>
                                                                             {
                                                                                 SetStateButton(true);
                                                                                 webCamTexture.Stop();
                                                                                 PopupCapture.SetActive(false);
                                                                             });
        initCamera(1);
    }

    private void initCamera(int position)
    {
        var deviceName = WebCamTexture.devices[position].name;

        webCamTexture = new WebCamTexture(deviceName, 720, 1280, 30);

        webCamTexture.Play();

        WcCamera.GetComponent<UITexture>().mainTexture = webCamTexture;

    }

    private void SetStateButton(bool state)
    {
        ButtonBack.GetComponent<UIButton>().enabled = state;
        ButtonCountPhotos.GetComponent<UIButton>().enabled = state;
        ButtonCountReview.GetComponent<UIButton>().enabled = state;
        buttonWriteReview.GetComponent<UIButton>().enabled = state;
        buttonAddPhotos.GetComponent<UIButton>().enabled = state;
    }
}
