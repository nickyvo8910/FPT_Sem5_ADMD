package com.example.log6;

import java.sql.Date;
import java.text.SimpleDateFormat;
import java.util.Vector;
import java.util.logging.Formatter;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.util.Log;
import android.widget.Toast;

public class DataBaseAdapter {

	
    static final String DATABASE_NAME = "Reviewdb";
    static final int DATABASE_VERSION = 2;

    

    public  SQLiteDatabase db;
    
    private final Context context;
    
    private DataBaseHelper dbHelper;
    public DataBaseAdapter(Context _context) 
    {
        context = _context;
        dbHelper = new DataBaseHelper(context, DATABASE_NAME, null, DATABASE_VERSION);
       
    }
    public void open() throws SQLException 
    {
        db = dbHelper.getWritableDatabase();
    }
    public void close() 
    {
        db.close();
    }

    public  SQLiteDatabase getDatabaseInstance()
    {
        return db;
    }
    

    public boolean insertDetail(String IDSta,String NameSta,String location)
    {
    	
    	Cursor cursor=db.query("DetailStation", null, " ID=?", new String[]{IDSta}, null, null, null);
        if(cursor.getCount()<1) // UserName Not Exist
        {
        	ContentValues newValues = new ContentValues();
            // Assign values for each row.
    		
            newValues.put("ID", IDSta);
            newValues.put("NameSta",NameSta);
            newValues.put("Type","00");
            newValues.put("Location",location);
            
            	db.insert("DetailStation", null, newValues);
            
           // insert facilities 
            
     	
           // update facilities
            return true;
    }else{
    	return false;
    }
       
        	
    }
    public boolean insertfacilities()
    {
    	Cursor cursor=db.query("DetailFacilities", null, null, null, null, null, null);;
        if(cursor.getCount()<1) // UserName Not Exist
        {
       ContentValues newValues = new ContentValues();
        // Assign values for each row.
       String[] a=new String[]{"Toilets","Wifi","Lifts"};
		for(int i=0;i<3;i++){
        newValues.put("NameFac", a[i].toString());
        
        	db.insert("DetailFacilities", null, newValues);
		}
        }
        return true;
    }
    
    public boolean insertReview(String ID,String Date,float Rating,String Publish,String NameFac)
    {
       ContentValues newValues = new ContentValues();
		
        
        newValues.put("Date",Date);
        newValues.put("Rating",Rating);
        newValues.put("Publish",Publish);
        newValues.put("IDSta", ID);
        newValues.put("NameFac", NameFac);
        


//     
        	db.insert("DetailReview", null, newValues);
//  
        
        return true;
    }
    public int deleteEntry(String UserName)
    {
      
        String where="USERNAME=?";
        int numberOFEntriesDeleted= db.delete("LOGIN", where, new String[]{UserName}) ;
      
        return numberOFEntriesDeleted;
    }    
    
    public int deleteIdStation(String idSta)
    {
    	
    	int number1= db.delete("FacSta", "IDSta=?", new String[]{idSta}) ;
        int number2= db.delete("DetailReview", "IDSta=?", new String[]{idSta}) ;
        int number3= db.delete("DetailStation", "ID=?", new String[]{idSta}) ;
        
        return number3;
    }  
    
    public int deleteNote(String id)
    {
       
        int number1= db.delete("DetailReview", "IDSta=?", new String[]{id}) ;
       
        return number1;
    }  
    public String getSinlgeEntry(String IDSta)
    {
        Cursor cursor=db.query("DetailStation", null, " ID=?", new String[]{IDSta}, null, null, null);
        if(cursor.getCount()<1) // UserName Not Exist
        {
            cursor.close();
            return "NOT EXIST";
        }
        cursor.moveToFirst();
        String password= cursor.getString(cursor.getColumnIndex("PASSWORD"));
        cursor.close();
        return password;                
    }
    
    
    public Vector SearchStation(String NamePet)
    {Vector<String> e1=new Vector<String>();
    Vector<String> e2=new Vector<String>();
    Vector<String> e3=new Vector<String>();
    Vector<Vector> vec=new Vector<Vector>();
    
    	Cursor c = db.rawQuery("SELECT * FROM DetailStation", null); 

    	while(c.moveToNext()){
    		
    		e1.add(c.getString(c.getColumnIndex("NameSta")));
    		e2.add(c.getString(c.getColumnIndex("Type")));
    		e3.add(c.getString(c.getColumnIndex("ID")));
    	}
    
    	vec.add(e1);
    	vec.add(e2);
    	vec.add(e3);
    	return vec;
    }
    public String SearchPhone(String idpet){
    
    	Cursor c = db.rawQuery("SELECT * FROM DetailStation WHERE ID="+idpet , null); 

    	c.moveToNext();
    		
    		String s= c.getString(c.getColumnIndex("Phone"));
    		
    	
  
    	return s;
    }
    public Vector SearchNoteStation(String IDSta)
    {Vector<String> e1=new Vector<String>();
    Vector<String> e2=new Vector<String>();
    Vector<String> e3=new Vector<String>();
    Vector<Vector> vec=new Vector<Vector>();
    
    	Cursor c = db.rawQuery("SELECT * FROM DetailReview WHERE IDSta='"+IDSta+"' ORDER BY ID DESC", null); 

    	while(c.moveToNext()){
    		
    		e1.add(c.getString(c.getColumnIndex("Publish")));
    		e2.add(c.getString(c.getColumnIndex("NameFac"))+"--------"+c.getString(c.getColumnIndex("Date")));
    		e3.add(String.valueOf(c.getFloat(c.getColumnIndex("Rating"))));
    	}
  
    	vec.add(e1);
    	vec.add(e2);
    	vec.add(e3);
    	return vec;
    }
    
    public Vector StationDetail(String ID)
    {Vector vec=new Vector();
    
    	Cursor c = db.rawQuery("SELECT * FROM DetailStation Where ID='"+ID+"'", null); 

    	while(c.moveToNext()){
    		
    		vec.add(c.getString(c.getColumnIndex("ID")));
    		vec.add(c.getString(c.getColumnIndex("NameSta")));
    		vec.add(c.getString(c.getColumnIndex("Type")));
    		vec.add(c.getString(c.getColumnIndex("Location")));
    	}
    	
    	return vec;
    }
    public Vector FacilitiesDetail(String ID)
    {Vector vec1=new Vector();
    Vector vec2=new Vector();
    Vector vec3=new Vector();
    Vector<Vector> vec=new Vector<Vector>();
    
    	Cursor c = db.rawQuery("SELECT * FROM FacSta Where IDSta='"+ID+"' and Status=1", null); 

    	while(c.moveToNext()){
    		vec1.add(c.getString(c.getColumnIndex("Status")));
    		vec3.add(c.getString(c.getColumnIndex("IDFac")));
    	}
    	for(int i=0;i<vec3.size();i++){
    		Cursor c1 = db.rawQuery("SELECT * FROM DetailFacilities  Where ID="+vec3.get(i).toString(), null); 

        	while(c1.moveToNext()){
        		vec2.add(c1.getString(c1.getColumnIndex("NameFac")));
        	}

    	}
    	vec.add(vec1);
    	vec.add(vec2);
    	return vec;
    }
    
    
    public boolean UpdateDetail(String IDSta,String NameSta,String type ,String location,Vector veFac)
    {
    	// update detail
       ContentValues newValues = new ContentValues();
 
       newValues.put("NameSta",NameSta);
       newValues.put("Type",type);
       newValues.put("Location",location);

        	db.update("DetailStation", newValues, "ID = ?",new String[]{IDSta});
       // update facilities
        	newValues = new ContentValues();
        	 
            newValues.put("Status",false);

             	db.update("FacSta", newValues, "IDSta = ?",new String[]{IDSta});
             	
             	newValues = new ContentValues();
             	newValues.put("Status",false);
             	db.update("FacSta", newValues, "IDSta = ?",new String[]{IDSta});
             	
             	 for(int i=0;i<veFac.size();i++){
               	  newValues = new ContentValues();
                    newValues.put("Status",true);
                     	db.update("FacSta", newValues, "IDSta = ? and IDFac = ?",new String[]{IDSta,veFac.get(i).toString()}); 
               } 	
        	
        return true;
    }
    public void  updateEntry(String userName,String password,String qualification,String specialization,String registrationNumber,String cellNumber,String mailId)
    {
 
        ContentValues updatedValues = new ContentValues();
 
        updatedValues.put("USERNAME", userName);
        updatedValues.put("PASSWORD",password);


        String where="USERNAME = ?";
        db.update("LOGIN",updatedValues, where, new String[]{userName});               
    }        
    

}
