using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
//using Facebook;
//using Facebook.MiniJSON;

public class AddReview_UI : MonoBehaviour {

    //

    public GameObject DateReview;
    public GameObject TypeOfFacilities;
    public GameObject Rating;
    public GameObject Comment;
    public GameObject ShareFb;
    public GameObject Save;
    public GameObject buttonBack;
    public GameObject PopupFb;
    public GameObject PopupSave;

    private Reviews reviews;
    private Reviews_DAO reviewsDao;
    private Facilities_DAO facilitiesDao;

    public int stationID;
    // Use this for initialization
//	void Start ()
//	{
//        Debug.Log();
//	}

    public void Init(int _stationID)
    {
        reviews = new Reviews();
        reviewsDao = new Reviews_DAO();
        facilitiesDao = new Facilities_DAO();
        DateReview.GetComponent<UILabel>().text = DateTime.Now.Date.ToString("dd-MM-yyyy");
        reviews.Date = DateReview.GetComponent<UILabel>().text;
        Debug.Log(reviews.Date);
        stationID = _stationID;
        reviews.Stationid = stationID;

        EventDelegate.Add(buttonBack.GetComponent<UIButton>().onClick,
                          () =>
                              {
                                  gameObject.GetComponentInParent<AppManager>().form[2].SetActive(true);
                                  gameObject.SetActive(false);
                              });



        EventDelegate.Add(Rating.GetComponent<UIPopupList>().onChange,
                          () =>
                          {
                              Debug.Log(ConvertStarToPoint(Rating.GetComponent<UIPopupList>().value));
                              reviews.Rating = ConvertStarToPoint(Rating.GetComponent<UIPopupList>().value);
                          });

        EventDelegate.Add(TypeOfFacilities.GetComponent<UIPopupList>().onChange,
                          () =>
                              {
                                  Debug.Log("Facilities ID: " + facilitiesDao.GetFacilitiesIDByName(TypeOfFacilities.GetComponent<UIPopupList>().value) + " | facilities Name: " + TypeOfFacilities.GetComponent<UIPopupList>().value);
                                  reviews.Facilitiyid =
                                      facilitiesDao.GetFacilitiesIDByName(
                                          TypeOfFacilities.GetComponent<UIPopupList>().value);
                              });

        EventDelegate.Add(Save.GetComponent<UIButton>().onClick, 
            () =>
                {
                    reviews.Comment = Comment.GetComponent<UIInput>().value;
                    EnableAllControl(false);
                    PopupSave.SetActive(true);
                    EventDelegate.Add(GameObject.Find("PopUp/btnBack").GetComponent<UIButton>().onClick,
                                      () =>
                                          {
                                              EnableAllControl(true);
                                              PopupSave.SetActive(false);
                                          });
                    EventDelegate.Add(GameObject.Find("PopUp/btnConfirm").GetComponent<UIButton>().onClick,
                                      () =>
                                          {
                                              SaveData();
                                              EnableAllControl(true);
                                              PopupSave.SetActive(false);
                                          });
                }
            );

        EventDelegate.Add(ShareFb.GetComponent<UIButton>().onClick,
                          () =>
                              {
                                  EnableAllControl(false);
                                  reviews.Comment = Comment.GetComponent<UIInput>().value;
                                  FB.Init(() =>
                                              {
                                                  if (!FB.IsLoggedIn)
                                                  {
                                                      FB.Logout();
                                                      FB.Login("public_profile,email,user_friends",
                                                           (FBResult resuit)
                                                           =>
                                                               {
                                                                   PopupFb.SetActive(true);
                                                                   PopupFb.GetComponent<PopupShareFb_UI>().ShareFeed(reviews);
                                                               });
                                                  }
                                                  else
                                                  {
                                                      FB.Login("public_profile,email,user_friends",
                                                           (FBResult resuit)
                                                           =>
                                                           {
                                                               PopupFb.SetActive(true);
                                                               PopupFb.GetComponent<PopupShareFb_UI>().ShareFeed(reviews);
                                                           });
                                                  }
                                              });
                              });
    }

    private void SaveData()
    {
        if (reviewsDao.InsertReview(reviews))
        {
            Debug.Log("Save All");
        }
        else
        {
            Debug.Log("Error");
        }
    }


    private int ConvertStarToPoint(string _star)
    {
        switch (_star)
        {
            case "*":
                return 1;
                break;

            case "**":
                return 2;
                break;

            case "***":
                return 3;
                break;

            case "****":
                return 4;
                break;

            case "*****":
                return 5;
                break;

            default:
                return 1;
        }
    }

    public void EnableAllControl(bool key)
    {
        TypeOfFacilities.GetComponent<BoxCollider>().enabled = key;
        buttonBack.GetComponent<UIButton>().enabled = key;
        Rating.GetComponent<UIPopupList>().enabled = key;
        Comment.GetComponent<UIInput>().enabled = key;
        ShareFb.GetComponent<UIButton>().enabled = key;
        Save.GetComponent<UIButton>().enabled = key;
    }

    public void EnableFinđetail(bool enable)
    {
        
    }

}
