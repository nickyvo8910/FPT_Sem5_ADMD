using UnityEngine;
using System.Collections;

public class ItemReview_UI : MonoBehaviour
{

    public GameObject ReviewID;
    public GameObject Facilities;
    public GameObject Rate;
    public GameObject Comment;

    // Use this for initialization
	void Start () {
	
	}

    public void SetValues(string _reviewID, string _facilitiesName, string _rate, string _comment)
    {
        ReviewID.GetComponent<UILabel>().text = _reviewID;
        Facilities.GetComponent<UILabel>().text = _facilitiesName;
        Rate.GetComponent<UILabel>().text = ConvertPoint(_rate);
        Comment.GetComponent<UILabel>().text = _comment;
    }

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

}
