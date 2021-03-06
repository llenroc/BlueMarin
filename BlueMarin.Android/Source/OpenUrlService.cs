﻿using System;
using Android.App;
using Android.Content;
using Android.Util;

namespace BlueMarin.Android
{
	public class OpenUrlService : IOpenUrlService
	{
		readonly Context mContext;

		public OpenUrlService (Context context)
		{
			this.mContext = context;		
		}

		public OpenUrlService ()
		{		
			this.mContext = Application.Context;	
		}

		public void OpenUrl (string url, string mime = null, bool extra = false)
		{
			if (!url.Contains (":/")) {
				url = "http://" + url;
			}

			var uri = global::Android.Net.Uri.Parse (url);
			Intent intent = new Intent (Intent.ActionView);
			if (mime != null)
				intent.SetDataAndType (uri, mime);
			else 
				intent.SetData (uri);

			intent.AddFlags (ActivityFlags.NewTask);

			if (extra){
				Intent intent2 = new Intent (Intent.ActionView);
				intent2.SetData (uri);
				Intent[] intentArray =  { intent2 }; 
				intent = Intent.CreateChooser (intent, "Open");
				intent.PutExtra(Intent.ExtraInitialIntents, intentArray);
			}

			try {
				mContext.StartActivity (intent);
			} catch (Exception ex) {
				try {
					Console.WriteLine(ex.Message);
					Intent intent3 = new Intent (Intent.ActionView);
					intent3.SetFlags(ActivityFlags.NewTask);
					mContext.StartActivity (intent3);
				} catch (Exception) {
					Log.Error ("cannot open link ", ex.Message);
				}
			} 
		}
	}
}
