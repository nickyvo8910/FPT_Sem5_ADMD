using UnityEngine;
using System.Collections;

public class Location_DAO  {

    private SqliteDatabase db;
    private static string dbName = "fReviewDB";

    public Location_DAO()
    {
        db = new SqliteDatabase(dbName);
    }
	
    public int getLocationIDbyName(string loName)
    {
        string query =
           @"select ID from tblLocations where Name = '"+loName+"';";
        return int.Parse(db.ExecuteQuery(query).Rows[0]["ID"].ToString());
    }

    public DataTable getAllLocations()
    {
        string query = "select Name from tblLocations;";
        return db.ExecuteQuery(query);
    }
}
