using System.Collections.Generic;
using System.IO;
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
using Xamarin.Media;
using Console = System.Console;
using File = Java.IO.File;
using Uri = Android.Net.Uri;

namespace XamarinSample
{
    [Activity(Label = "TakePhotoActivity")]
    public class TakePhotoActivity : Activity
    {
        private string FilePath { get; set; }
        private DrawView drawView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.image);

            drawView = FindViewById<DrawView>(Resource.Id.myDrawView);

            FindViewById<Button>(Resource.Id.saveImageBtn)
                .Click += delegate { SaveImage(); };

            var picker = new MediaPicker(this);
            if (!picker.IsCameraAvailable)
                Console.WriteLine("No camera!");
            else
            {
                var intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions
                {
                    Name = "test.jpg",
                    Directory = null
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
            FilePath = path;
            var file = new File(path);
            var uri = Uri.FromFile(file);
            drawView.SetImageURI(uri);
        }

        protected void SaveImage()
        {
            drawView.DrawingCacheEnabled = true;
            var bmp = drawView.DrawingCache;

            var root = Environment.ExternalStorageDirectory;

            var filePath = (root.AbsolutePath + "/saved.jpg");
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                bmp.Compress(Bitmap.CompressFormat.Jpeg, 100, fileStream);
                fileStream.Flush();
            }
        }
    }
}