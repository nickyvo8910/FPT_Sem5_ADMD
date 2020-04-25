package com.example.log6;

import com.example.log6.DataBaseAdapter;

import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class MainActivity extends ActionBarActivity {
	private DataBaseAdapter  DBA;
	EditText text1,text2,text3;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		DBA =new DataBaseAdapter(this);
		DBA.open();
		
		Button btn=(Button) findViewById(R.id.btn_nextEnter);
		
		btn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				boolean c=check();
				if(c==true){
					boolean e= DBA.insertDetail(text1.getText().toString(), text2.getText().toString(), text3.getText().toString());
					if(e==true){
						Toast.makeText(getApplicationContext(), "Inserted", Toast.LENGTH_SHORT).show();	
					}else{
						Toast.makeText(getApplicationContext(), "ID has inserted", Toast.LENGTH_SHORT).show();
					}
				}
			}
		});
		
	}
	
	public boolean check(){
		text1=(EditText) findViewById(R.id.txtIDSta);
		text2=(EditText) findViewById(R.id.txtNameSta);
		text3=(EditText) findViewById(R.id.txtLocation);
		if(text1.getText().toString().equals("")){
			text1.setError("not Blank");
			return false;
			
		}
		
		return true;
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
}
