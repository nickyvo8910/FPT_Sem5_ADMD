using UnityEngine;
using System.Collections;

public class Reviews_DAO {

    private SqliteDatabase db;
    private static string dbName = "fReviewDB";

    public Reviews_DAO()
    {
        db = new SqliteDatabase(dbName);
    }

    public int CoutNumReview(int _stationid)
    {
        string query =
            @"select tblStations.ID, tblStations.name, tblReviews.Rating from tblStations, tblReviews 
                        where tblStations.ID = tblReviews.StationID and tblStations.ID = " + _stationid + " ;";
        return db.ExecuteQuery(query).Rows.Count;
    }

    public DataTable GetReviewByStationId(int _id)
    {
        string query =
            @"select tblReviews.StationID, tblFacilities.Name, tblReviews.Rating, tblReviews.Comment 
            from tblReviews, tblFacilities 
            where tblFacilities.ID = tblReviews.FacilityID and tblReviews.StationID = " +_id+" ;";
        return db.ExecuteQuery(query);
    }

    public bool InsertReview(Reviews reviews)
    {
        string query = string.Format(@"insert into tblReviews(StationID, FacilityID, RevDate, Rating, Comment) 
                                        values ({0}, {1}, '{2}', {3}, '{4}') ;", reviews.Stationid, reviews.Facilitiyid, reviews.Date,reviews.Rating, reviews.Comment);
        if (db.ExecuteNonQuery(query))
            return true;
        return false;
    }
}
