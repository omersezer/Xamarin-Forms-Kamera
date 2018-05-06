using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Camera
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            btnTakeVideo.Clicked += BtnTakeVideo_Clicked;
		}

        private async void  BtnTakeVideo_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                DisplayAlert("Hata", "Maalesef kamera şu an da müsait değil", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
            {
                Quality = Plugin.Media.Abstractions.VideoQuality.High,
                Directory = "SampleVideo",
                Name = "video.mp4"
            });

            if (file == null)
            {
                return;
            }

            file.Dispose();
        }

        private async void btnTakePhoto_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                DisplayAlert("HATA", "Maalesef bir sorunla karşılaştık", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "photo.jpg",

            });
            if (file == null)
            {
                return;
            }
            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });

        }
        private async void btnPickPhoto_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                DisplayAlert("HATA", "Maalesef telefonunuz fotoğraf seçmeyi desteklemiyor", "OK");

            }

            // Seçtiğimiz fotoğrafı file değişkenine veriyorum
            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
            {
                return;
            }

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });

        }
        private async void btnPickVideo_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                DisplayAlert("Hata", "Video Seçimi Yapamazsınız!", "Tamam");
                return;
            }

            var file = await CrossMedia.Current.PickVideoAsync();

            if (file == null)
            {
                return;
            }

            file.Dispose();
        }
    }
}
