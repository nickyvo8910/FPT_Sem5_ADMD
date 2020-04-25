using UnityEngine;
using System.Collections;

public class PhotoDetail_UI : MonoBehaviour
{


    public GameObject btnBack;
    public GameObject ItemPhotos;
    public GameObject UIgrid;
    private int staionID;
    private DataTable resuilt;
    private Photos_DAO photosDao;
    private int countRow = 0;
//    void Start()
//    {
//        
//
//        Setphotos(1);
//    }

    void BackFindDetail()
    {
        gameObject.GetComponentInParent<AppManager>().form[2].SetActive(true);
        gameObject.SetActive(false);
    }

    public void Setphotos(int _stationid)
    {

        photosDao = new Photos_DAO();
        EventDelegate.Add(btnBack.GetComponent<UIButton>().onClick, BackFindDetail);

        staionID = _stationid;
        
        if (resuilt != null && resuilt.Rows.Count > 0)
        {
            resuilt.Rows.Clear();
            DesTroyResuiltItem();
        }

        resuilt = photosDao.GetAllPhotosByStationID(staionID);
        countRow = resuilt.Rows.Count;

        if (countRow > 0)
        {
            for (int i = 0; i < countRow; i++)
            {
                var go = GameObject.Instantiate(ItemPhotos) as GameObject;
                go.transform.parent = UIgrid.transform;
                go.transform.localScale = new Vector3(1, 1, 1);
                go.transform.localPosition = new Vector3(0, -250*i, 0);
                go.name = "ResultPhotos" + i;


                Texture2D texture2D = new Texture2D(100, 100);
                texture2D.LoadImage(
                    System.IO.File.ReadAllBytes(Application.persistentDataPath + "/" + resuilt[i]["PhotoFile"]));

                go.GetComponent<UITexture>().mainTexture = texture2D;
            }
        }

    }

    public void DesTroyResuiltItem()
    {
        foreach (Transform child in UIgrid.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
