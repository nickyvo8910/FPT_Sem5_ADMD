using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PopupShareFb_UI : MonoBehaviour
{
    
	
    public void ShareFeed(Reviews reviews)
    {
        if (FB.IsLoggedIn)
        {
            FeedLink =
                "http://demofacilities.tk";
            FeedLinkName =
                "Facilities Reviews";
            FeedLinkCaption = reviews.Comment;
            FeedLinkDescription = "New Facility Review Has Been Post " ;
            PostFbFeed();
        }
    }


    #region FB.Feed() example

    public string FeedToId = "";
    public string FeedLink = "";
    public string FeedLinkName = "";
    public string FeedLinkCaption = "";
    public string FeedLinkDescription = "";
    public string FeedPicture = "";
    public string FeedMediaSource = "";
    public string FeedActionName = "";
    public string FeedActionLink = "";
    public string FeedReference = "";
    public bool IncludeFeedProperties = false;
    private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

    private void PostFbFeed()
    {
        Dictionary<string, string[]> feedProperties = null;
        if (IncludeFeedProperties)
        {
            feedProperties = FeedProperties;
        }
        FB.Feed(
            toId: FeedToId,
            link: FeedLink,
            linkName: FeedLinkName,
            linkCaption: FeedLinkCaption,
            linkDescription: FeedLinkDescription,
            picture: FeedPicture,
            mediaSource: FeedMediaSource,
            actionName: FeedActionName,
            actionLink: FeedActionLink,
            reference: FeedReference,
            properties: feedProperties,
            callback: Callback
        );
    }

    #endregion

    protected void Callback(FBResult result)
    {
        string lastResponse = "";
        //         lastResponseTexture = null;
        // Some platforms return the empty string instead of null.
        if (!String.IsNullOrEmpty(result.Error))
        {
            lastResponse = "Error Response:\n" + result.Error;
        }
        else if (!String.IsNullOrEmpty(result.Text))
        {
            lastResponse = "Success Response:\n" + result.Text;
            gameObject.GetComponentInParent<AddReview_UI>().EnableAllControl(true);
            gameObject.SetActive(false);
        }
        else if (result.Texture != null)
        {
            //            lastResponseTexture = result.Texture;
            lastResponse = "Success Response: texture\n";
        }
        else
        {
            lastResponse = "Empty Response\n";
        }
//        Debug.Log(lastResponse);
    }
}
