package com.example.log5;



import android.support.v7.app.ActionBarActivity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class MainActivity extends ActionBarActivity {
	
	EditText text1,text2;
	int number;
	float total;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		Button btn=(Button) findViewById(R.id.button1);
		
		btn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				boolean c=check();
				if(c==true){
				calculator();
				Toast t= Toast.makeText(getApplicationContext(), String.valueOf(total)+" $", Toast.LENGTH_SHORT);
				t.show();
				}
			}
		});
	}
	
	public boolean check(){
		text1=(EditText) findViewById(R.id.editText1);
		text2=(EditText) findViewById(R.id.editText2);
		if(text1.getText().toString().equals("")){
			text1.setError("not Blank");
			return false;
			
		}if(text2.getText().toString().equals("")){
			text2.setError("not Blank");
			return false;
		}
		
		return true;
	}
	
	public void calculator(){
		text1=(EditText) findViewById(R.id.editText1);
		text2=(EditText) findViewById(R.id.editText2);
		
		if (Integer.parseInt(text2.getText().toString()) > 35){
			number=(Integer.parseInt(text2.getText().toString()))-35;
			total=(number * ((Float.parseFloat(text1.getText().toString())+((Float.parseFloat(text1.getText().toString())/2))))+
					((35 * (Float.parseFloat(text1.getText().toString())))));
		}else{
			total=(Integer.parseInt(text2.getText().toString()))*(Float.parseFloat(text1.getText().toString()));
		}
		
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
