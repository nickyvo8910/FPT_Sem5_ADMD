using UnityEngine;
using System.Collections;

public class FindResult_UI : MonoBehaviour
{
    //

    public GameObject UIgrid, ButtonBack;
    public GameObject ItemResuilt;

    public string Location = "";

    private Stations_DAO stations;
    private DataTable resuilt;
    private int countRow;

	public void ShowResult(string _location)
	{
        Debug.Log(_location);
        stations = new Stations_DAO();
	    resuilt = stations.GetStationByLocation(_location);
	    countRow = resuilt.Rows.Count;

        if (resuilt.Rows.Count > 0)
        {
            for (int i = 0; i < resuilt.Rows.Count; i++)
            {
                var go = GameObject.Instantiate(ItemResuilt) as GameObject;
                go.transform.parent = UIgrid.transform;
                go.transform.localScale = new Vector3(1, 1, 1);
                go.transform.localPosition = new Vector3(0, -250 * i, 0);
                go.name = "Result" + i;
                  go.GetComponent<ItemResult_UI>()
                  .SetResult(resuilt[i]["Name"].ToString(), resuilt[i]["PhotoFile"].ToString(), int.Parse(resuilt[i]["Wifi"].ToString()),
                            int.Parse(resuilt[i]["Toilet"].ToString()), int.Parse(resuilt[i]["Trolley"].ToString()), int.Parse(resuilt[i]["Refreshment"].ToString()), int.Parse(resuilt[i]["ATM"].ToString()), int.Parse(resuilt[i]["DisabledAccess"].ToString()));
//                Debug.Log("item "+i);
            }
        }
	}

    public void DesTroyResuiltItem()
    {
//        Debug.Log("item count: " + countRow);
        for (int i = 0; i < countRow; i++)
        {
            Destroy(GameObject.Find("ScrollviewBG/UIGrid/Result"+i));
//            Debug.Log("destroy item: " + GameObject.Find("ScrollviewBG/UIGrid/Result" + i).name);
        }
    }
}
