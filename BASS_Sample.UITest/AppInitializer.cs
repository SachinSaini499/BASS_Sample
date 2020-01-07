using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace BASS_Sample.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.InstalledApp("com.companyname.bass_sample").StartApp();
            }
            return ConfigureApp.iOS.StartApp();
        }
    }
}