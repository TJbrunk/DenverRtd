using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RtdData;
using System.IO;

namespace RtdScheduleApp.Droid
{
    [Activity(Label = "RtdScheduleApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CopySqlDb();
            LoadApplication(new App());
        }

        private void CopySqlDb()
        {
            if(!File.Exists(RtdDbContext.DbPath))
            {
                var dbFile = Path.GetFileName(RtdDbContext.DbPath);
                using (var br = new BinaryReader(Application.Context.Assets.Open(dbFile)))
                {
                    using (var bw = new BinaryWriter(new FileStream(RtdDbContext.DbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, length);
                        }
                    }
                }
            }
        }
    }
}