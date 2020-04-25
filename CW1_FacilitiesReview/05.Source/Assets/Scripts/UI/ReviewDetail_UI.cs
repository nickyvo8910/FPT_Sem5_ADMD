using UnityEngine;
using System.Collections;

public class ReviewDetail_UI : MonoBehaviour {
    //
    public GameObject ButtonBack;
    public GameObject ItemReview;
    public GameObject UIGrid;
    private Reviews_DAO reviews;
    private DataTable resuilt;
    private int countRow;
	// Use this for initialization

//	void Start () {
//	        ShowReview(1);
//	}
	
	public void ShowReview(int _stationId)
	{
	    reviews = new Reviews_DAO();
	    if (resuilt != null && resuilt.Rows.Count > 0)
	    {
	        resuilt.Rows.Clear();
            DesTroyResuiltItem();
	    }
	    resuilt = reviews.GetReviewByStationId(_stationId);
	    countRow = resuilt.Rows.Count;
//        Debug.Log("countRow new "+countRow);
        if ( countRow > 0)
        {
            for (int i = 0; i < resuilt.Rows.Count; i++)
            {
                var go = GameObject.Instantiate(ItemReview) as GameObject;
                go.transform.parent = UIGrid.transform;
                go.transform.localScale = new Vector3(1, 1, 1);
                go.transform.localPosition = new Vector3(0, -250*i, 0);
                go.name = "ItemReview" + i;
                go.GetComponent<ItemReview_UI>().SetValues("Review: " + i, resuilt[i]["Name"].ToString(), resuilt[i]["Rating"].ToString(), resuilt[i]["Comment"].ToString());
            }
        }
	}


    public void DesTroyResuiltItem()
    {
//        for (int i = 0; i < countRow; i++)
//        {
//            Destroy(GameObject.Find("ScrollviewBG/UIGrid/ItemReview" + i) as GameObject);
//            Debug.Log("remove game object " + i);
//        }
        
//        int childs = UIGrid.transform.childCount;
//        Debug.Log("childs " + childs);
//            for (int i = childs -1; i >= 0; i--)
//            {
//                GameObject.Destroy(UIGrid.transform.GetChild(i).gameObject);
//            }

        foreach (Transform child in UIGrid.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
