/* 

Based on music, mp3 searcher sites
Made for http://api.win-nevis.com
api.win-nevis.com may change

Download from http://www.win-nevis.com


Parse Dev Studio

http://www.ParseDev.com

*/

using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// usinge morede niyaz 
using APIForParseDevMusic;


namespace UWP_Sample
{
    public sealed partial class DownloadLinkPage : Page
    {
        WebSite site;
        string siteName = "";
        WebGard webGard = new WebGard();
        public DownloadLinkPage()
        {
            this.InitializeComponent();

            webGard.OnProgress += WebGard_OnProgress;
            webGard.OnResult += WebGard_OnResult;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            object[] obj = e.Parameter as object[];
            site = obj[0] as WebSite;
            siteName = site.Name;
            Music music = obj[1] as Music;
            if (!string.IsNullOrEmpty(siteName) && music != null)
            {
                if (site.IsSearcher == null)
                {
                    string createUrl = string.Format(Constants.DownloadLinkAddress,
                        siteName.ToLower(), music.Url);
                    webGard.Begin(createUrl);
                    if (!string.IsNullOrEmpty(music.ImageUrl))
                    {
                        var image = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(music.ImageUrl));
                        img.Source = image;
                    }
                }
                else
                {
                    LV.Items.Add("Searcher site has download link.");
                    LV.Items.Add("No need to get them!");
                }

            }
        }

        async private void WebGard_OnResult(object sender, Results result)
        {
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;
            await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (result != null && result.ShowType == ShowType.DataRetrieved && !string.IsNullOrEmpty(result.Source))
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(result.Source);
                    MemoryStream stream = new MemoryStream(byteArray);

                    DataContractJsonSerializer jsonSerializer;
                    // dataye site e iranmusic kami fargh dare
                    // iranmusic ham link haye download ro migire
                    // ham posthaye mortabet ba link e download
                    // yani ahang haye 
                    if (siteName.ToLower() == "iranmusic")
                    {
                        jsonSerializer = new DataContractJsonSerializer(typeof(DownloadsList));

                        var TMusicList = (DownloadsList)jsonSerializer.ReadObject(stream);

                        LV.ItemsSource = TMusicList.Downloads;
                        // get relative posts
                        // LV.ItemsSource = TMusicList.Relative;
                    }
                    else
                    {
                        // baghie site ha nemitonan mese IranMusic ham linkhaye dl
                        // ham posthaye marbot ro begiran
                        // faghat mitonan link hay download ro begiran
                        jsonSerializer = new DataContractJsonSerializer(typeof(DownloadedList));

                        var TMusicList = (DownloadedList)jsonSerializer.ReadObject(stream);

                        LV.ItemsSource = TMusicList;
                    }
                }
            });
        }

        private async void WebGard_OnProgress(object sender, Progresses progress)
        {
            if (progress != null)
            {
                var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;
                await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    switch (progress.ShowType)
                    {
                        case ShowType.RetrievingData:
                            pb.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            break;
                        case ShowType.DataRetrieved:
                        case ShowType.Filter:
                        case ShowType.Null:
                        case ShowType.Error:
                            pb.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                            break;
                    }
                });
            }
        }

        private async void LV_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (sender != null && e != null && e.ClickedItem != null)
            {
                if (e.ClickedItem.GetType() == typeof(DownloadLink))
                {
                    var downloadLink = e.ClickedItem as DownloadLink;
                    await new MessageDialog(downloadLink.Url).ShowAsync();
                }
            }
        }
    }
}
