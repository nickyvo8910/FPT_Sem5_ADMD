using UnityEngine;
using System.Collections;

public class Facilities_DAO  {

    private SqliteDatabase db;
    private static string dbName = "fReviewDB";

    public Facilities_DAO()
    {
        db = new SqliteDatabase(dbName);
    }

    public int GetFacilitiesIDByName(string _facilitiesName)
    {
        string query = @"select ID from tblFacilities where Name = '" + _facilitiesName + "';";
        return int.Parse(db.ExecuteQuery(query)[0]["ID"].ToString());
    }
}
