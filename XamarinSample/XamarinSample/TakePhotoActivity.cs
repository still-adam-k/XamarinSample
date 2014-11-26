using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Xamarin.Media;
using Console = System.Console;
using Uri = Android.Net.Uri;


namespace XamarinSample
{
    [Activity(Label = "TakePhotoActivity")]
    public class TakePhotoActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.image);
            var picker = new MediaPicker(this);
            if (!picker.IsCameraAvailable)
                Console.WriteLine("No camera!");
            else
            {
                var intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions
                {
                    Name = "test.jpg",
                    Directory = "PCN"
                });
                StartActivityForResult(intent, 0);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            // User canceled
            if (resultCode == Result.Canceled)
                return;

            data.GetMediaFileExtraAsync(this).ContinueWith(t =>
            {
               DisplayImage(t.Result.Path);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void DisplayImage(string path)
        {
            var imageControl = FindViewById<ImageView>(Resource.Id.imageTaken);
            imageControl.SetImageURI(Uri.FromFile( new File(path)));
        }
    }
}