using UnityEngine;
using System.Collections;

public class Photos_DAO {

	private SqliteDatabase db;
    private static string dbName = "fReviewDB";

    public Photos_DAO()
    {
        db = new SqliteDatabase(dbName);
    }

    public int CoutNumPhotos(int _stationid)
    {
        string query = @"select PhotoFile from tblPhotos where StationID = " + _stationid + " ;";
        return db.ExecuteQuery(query).Rows.Count;
    }

    public DataTable GetAllPicture()
    {
        string query = @"select tblPhotos.PhotoFile from tblPhotos;";
        return db.ExecuteQuery(query);
    }

    public bool InsertPhoto(Photos photos)
    {
        string query = string.Format(@"insert into tblPhotos(StationID, PhotoFile) values ({0},'{1}');", photos.StationId, photos.PhotoFile);

        return db.ExecuteNonQuery(query);
    }

    public DataTable GetAllPhotosByStationID(int _stationId)
    {
        string query = @"select PhotoFile from tblPhotos where StationID = " + _stationId + " ;";
        return db.ExecuteQuery(query);
    }
}
