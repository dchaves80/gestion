package com.dchalarmas.monitoreo;

import java.io.Console;
import java.sql.Date;
import java.util.logging.LogManager;
import java.util.logging.Logger;

import android.R.string;
import android.content.BroadcastReceiver;
import android.content.ContentValues;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.net.Uri;
import android.os.Debug;
import android.util.Log;
import android.widget.Toast;

public class recibidor extends BroadcastReceiver {

	@Override
	public void onReceive(Context context, Intent intent) {
		// TODO Auto-generated method stub
		final android.telephony.SmsManager S = android.telephony.SmsManager.getDefault();
		if (S!=null)
		{
			
			
				Uri mSmsinboxQueryUri = Uri.parse("content://sms/inbox");
				Cursor cursor1 = context.getContentResolver().query(mSmsinboxQueryUri,null, null, null, null);
				
				int columns = cursor1.getColumnCount();
				for (int a=0;a<columns;a++)
				{
					Log.d("Android", cursor1.getColumnName(a));
					
				}
				int address = cursor1.getColumnIndex("address");
				int date_sent = cursor1.getColumnIndex("date_sent");
				int _id = cursor1.getColumnIndex("_id");
				int READ = cursor1.getColumnIndex("read");
				
				while (cursor1.moveToNext())
				{
					
					if (cursor1.getInt(READ)==0)
					{
					
					Date D = new Date(Long.parseLong(cursor1.getString(date_sent)));
					Log.d("Android", cursor1.getString(address) + "(" + D.toString() + ")");
					ContentValues values = new ContentValues();
                    values.put("read", true);
                    String Id = cursor1.getString(_id);
					context.getContentResolver().update(mSmsinboxQueryUri, values, "_id=" + Id , null);
					}
					
				}
				
			Toast.makeText(context,"Mensaje recibido...", Toast.LENGTH_LONG).show();
			
			
				
		}
			
		}
		

	}


