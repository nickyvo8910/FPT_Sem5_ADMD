using UnityEngine;
using System.Collections;

public class Stations_DAO
{


    private SqliteDatabase db;
    private static string dbName = "fReviewDB";


    public Stations_DAO()
    {
        db = new SqliteDatabase(dbName);
    }

    public DataTable GetTopFacilities(string _facilities)
    {
        string query = "select Name, ID, "+_facilities+" from tblStations order by "+_facilities+" desc limit 5;";
        return db.ExecuteQuery(query);
    }

    public DataTable GetStationByName(string _stationName)
    {
        string query =
            @"select tblStations.Name, tblLocations.Name as 'LocationName', tblStations.Wifi, tblStations.Toilet, tblStations.Trolley, tblStations.Refreshment,
                tblStations.ATM, tblStations.DisabledAccess from tblStations, tblLocations 
                where tblLocations.ID = tblStations.locationID and tblStations.name = '" +_stationName+"';";
        return db.ExecuteQuery(query);
    }

    public DataTable GetStationByLocation(string _location)
    {
        string query =
            @"select tblStations.name, tblPhotos.PhotoFile, 
                tblStations.Wifi, tblStations.Toilet, tblStations.trolley, tblStations.refreshment, 
                tblStations.ATM, tblStations.DisabledAccess from tblStations, tblPhotos, tblLocations 
                where tblPhotos.StationID = tblStations.ID and tblLocations.ID = tblStations.LocationID and tblLocations.Name = '"+_location+"' order by tblStations.ID asc;";
        return db.ExecuteQuery(query);
    }

    public int getMaxStaID(){
        string query =
           @"select Max (ID) as 'Max' from tblStations;";
        return int.Parse(db.ExecuteQuery(query).Rows[0]["Max"].ToString());
    }
    public bool InsertStation(Stations stations)
    {
        if (stations.Id > getMaxStaID())
        {
            string query = string.Format(
                @"insert into tblStations(Name,Type,LocationID,Wifi,Trolley,Refreshment,ATM,Toilet,Phone,DisabledAccess,Status) values('{0}',{1},{2},{3},{4},{5},{6},{7},{8},{9},{10});",stations.Name,stations.Type,stations.LocationId,stations.Wifi,stations.Trolleys,stations.Refreshment,stations.Atm,stations.Toilet,stations.Phone,stations.DisabledAccess,1);
            return db.ExecuteNonQuery(query);
        }
        else
            return false;
    }

    public DataTable GetStationById(int stationId)
    {
        string query =
            @"select tblStations.ID, tblStations.Name, tblLocations.Name as 'LocationName', tblStations.Wifi, tblStations.Toilet, tblStations.Trolley, tblStations.Refreshment,
                tblStations.ATM, tblStations.DisabledAccess from tblStations, tblLocations 
                where tblLocations.ID = tblStations.locationID and tblStations.ID = '" + stationId + "';";
        return db.ExecuteQuery(query);
    }
}
