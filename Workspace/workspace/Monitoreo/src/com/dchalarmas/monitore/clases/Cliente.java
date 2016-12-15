package com.dchalarmas.monitore.clases;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.StringWriter;
import java.net.*;

public class Cliente {
public static class Connection
	{
	
	public static Socket _Socket;
	public static SocketAddress _SocketAddress;
	public static InputStream _InputStream;
	public static OutputStream _Outputstream;
	
	
	private static boolean IsAlive()
	{
		if (
				_Socket!=null &&
				_Socket.isConnected()==true &&
				_Socket.isClosed()==false
				)		
		{
			return true;
			
		} else 
		{
			return false;
			
		}
		
	}
	
	
	private static String getStringFromInputStream(InputStream is) {

		BufferedReader br = null;
		StringBuilder sb = new StringBuilder();

		String line;
		try {

			br = new BufferedReader(new InputStreamReader(is));
			while ((line = br.readLine()) != null) {
				sb.append(line);
			}

		} catch (IOException e) {
			e.printStackTrace();
		} finally {
			if (br != null) {
				try {
					br.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}

		
		return sb.toString();

	}
	
	public static String Recibir()
	{
		if (IsAlive()==true)
		{
			return getStringFromInputStream(_InputStream);
					
		} else 
		{
			return "";
			
		}
		
	}
	
	public static boolean Enviar(String _sendString)
	{
		if (IsAlive())
		{
			try {
				_Outputstream.write(_sendString.getBytes());
				_Outputstream.flush();
				return true;
			} catch (IOException e) {
				// TODO Auto-generated catch block
				
				e.printStackTrace();
				return false;
			}
			
		} else 
		{
			return false;
			
		}
	}
	
		public static boolean Connect(String IP, int PORT)
		{
					try {
						
						_Socket = new Socket(IP, PORT);
						_InputStream = _Socket.getInputStream();
						_Outputstream = _Socket.getOutputStream();
						return true;
						
					} catch (IOException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
						return false;
						
					}	
		}
		
	}
}
