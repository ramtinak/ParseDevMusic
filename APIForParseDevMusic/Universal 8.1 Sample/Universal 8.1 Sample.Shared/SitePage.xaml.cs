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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// usinge morede niyaz
using APIForParseDevMusic;

namespace Universal_8._1_Sample
{

    public sealed partial class SitePage : Page
    {
        string siteName = "";
        WebSite site;
        WebGard webGard = new WebGard();
        bool bbb = false;
        int page = 1;
        public SitePage()
        {
            this.InitializeComponent();
            webGard.OnProgress += WebGard_OnProgress;
            webGard.OnResult += WebGard_OnResult;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            site = e.Parameter as WebSite;
            siteName = site.Name;
            txtSiteName.Text = siteName;
            if (!string.IsNullOrEmpty(siteName))
            {
                string createAddress;
                if (site.IsSearcher != null)
                    return;
                else
                    createAddress = string.Format(Constants.Address, siteName.ToLower());

                webGard.Begin(createAddress);
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

                    var jsonSerializer = new DataContractJsonSerializer(typeof(TMusicList));
                     
                    var TMusicList = (TMusicList)jsonSerializer.ReadObject(stream);

                    LV.ItemsSource = TMusicList;
                    pb.Visibility = Visibility.Collapsed;
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
                            bbb = true;
                            //pb.Text = Convert.ToString(ShowType.RetrievingData);
                            pb.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            break;

                        case ShowType.Null:
                        case ShowType.DataRetrieved:
                        case ShowType.Filter:
                        case ShowType.Error:
                            bbb = false;
                            //pb.Text = Convert.ToString(ShowType.Error);
                            pb.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                            break;
                    }
                });
            }
        }


        private void sc_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            if (sc.VerticalOffset > 0 && sc.ScrollableHeight > 150)
            {
                if (sc.VerticalOffset >= sc.ScrollableHeight - 140 && bbb == false)
                {
                    page++;
                    string createAddress = string.Format(Constants.AddressWithPage,
                        siteName.ToLower(), page);

                    webGard.Begin(createAddress);
                    bbb = true;
                }
            }
        }

        private void LV_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (sender != null && e != null && e.ClickedItem != null)
            {
                if (e.ClickedItem.GetType() == typeof(Music))
                {
                    ((Frame)Window.Current.Content).Navigate(typeof(DownloadLinkPage),
                   new object[] { site, e.ClickedItem as Music });
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(SiteSearchPage));
        }
    }
}
