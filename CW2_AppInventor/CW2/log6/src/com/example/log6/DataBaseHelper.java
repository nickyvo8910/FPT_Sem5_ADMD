package com.example.log6;

import com.example.log6.DataBaseAdapter;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteDatabase.CursorFactory;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

public class DataBaseHelper extends SQLiteOpenHelper
{
	private static final String create_detail = "create table "+"DetailStation"+
            "( " +"ID"+" text primary key ,"+ "NameSta text"
     		+ ",Type text,Location text)";
	private static final String create_detail_Facility = "create table "+"DetailFacilities"+
            "( " +"ID"+" integer primary key autoincrement,NameFac text)";
	private static final String create_detail_Fac_Sta = "create table "+"FacSta"+
            "( " +"ID"+" integer primary key autoincrement,IDFac integer,IDSta text,Status boolean,FOREIGN KEY(IDFac) REFERENCES DetailFacilities(ID), " +
            														"FOREIGN KEY(IDSta) REFERENCES DetailStation(ID))";
	private static final String create_detail_Review = "create table "+"DetailReview"+
            "( " +"ID"+" integer primary key autoincrement,Date Date,Rating float,Publish text,IDSta text,NameFac text, FOREIGN KEY(IDSta) REFERENCES DetailStation(ID)" +
            		" )";
            		

	
	
    public DataBaseHelper(Context context, String name,CursorFactory factory, int version) 
    {
               super(context, name, factory, version);
    }
	// Called when no database exists in disk and the helper class needs
    // to create a new one.
    @Override
    public void onCreate(SQLiteDatabase _db) 
    {
            _db.execSQL(create_detail);
            _db.execSQL(create_detail_Facility);
            _db.execSQL(create_detail_Fac_Sta);
            _db.execSQL(create_detail_Review);

    }
    // Called when there is a database version mismatch meaning that the version
    // of the database on disk needs to be upgraded to the current version.
    @Override
    public void onUpgrade(SQLiteDatabase _db, int _oldVersion, int _newVersion) 
    {
            // Log the version upgrade.
            Log.w("TaskDBAdapter", "Upgrading from version " +_oldVersion + " to " +_newVersion + ", which will destroy all old data");

            // Upgrade the existing database to conform to the new version. Multiple
            // previous versions can be handled by comparing _oldVersion and _newVersion
            // values.
            // The simplest case is to drop the old table and create a new one.
            _db.execSQL("DROP TABLE IF EXISTS " + "TEMPLATE");
            // Create a new one.
            onCreate(_db);
    }

}

