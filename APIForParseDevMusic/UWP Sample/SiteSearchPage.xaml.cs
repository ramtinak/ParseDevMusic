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

// usinge morede niyaz
using APIForParseDevMusic;

namespace UWP_Sample
{
    public sealed partial class SiteSearchPage : Page
    {
        WebGard webGard = new WebGard();
        bool bbb = false;
        int page = 1;
        public SiteSearchPage()
        {
            this.InitializeComponent();
            webGard.OnProgress += WebGard_OnProgress;
            webGard.OnResult += WebGard_OnResult;
            Loaded += SiteSearchPage_Loaded;
        }

        private void SiteSearchPage_Loaded(object sender, RoutedEventArgs e)
        {
            comboSearch.ItemsSource = MainPage.Current.Sites;
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

                    if ((comboSearch.SelectedItem as WebSite).IsSearcher == null)
                    {
                        var jsonSerializer = new DataContractJsonSerializer(typeof(TMusicList));

                        var TMusicList = (TMusicList)jsonSerializer.ReadObject(stream);

                        LV.ItemsSource = TMusicList;
                    }
                    else
                    {
                        var jsonSerializer = new DataContractJsonSerializer(typeof(SearchList));

                        var searchList = (SearchList)jsonSerializer.ReadObject(stream);

                        LV.ItemsSource = searchList;
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
                    WebSite webSite = comboSearch.SelectedItem as WebSite;
                    string SearchWord = txtSearch.Text;
                    page++;
                    string createAddress;
                    if (webSite.IsSearcher == null)
                        createAddress = string.Format(Constants.SearchAddressWithPage,
                            webSite.Name.ToLower(), SearchWord, page);
                    else
                        createAddress = string.Format(Constants.SearcherInOneAddressWithPage,
                    webSite.Name.ToLower(), SearchWord, page);

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
                    WebSite webSite = comboSearch.SelectedItem as WebSite;

                    ((Frame)Window.Current.Content).Navigate(typeof(DownloadLinkPage),
                   new object[] { webSite.Name, e.ClickedItem as Music });
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (comboSearch.SelectedIndex == -1)
                return;
            else if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                page = 1;
                WebSite webSite = comboSearch.SelectedItem as WebSite;
                string SearchWord = txtSearch.Text;
                string createAddress;
                System.Diagnostics.Debug.WriteLine(webSite.IsSearcher);
                if (webSite.IsSearcher == null)
                    createAddress = string.Format(Constants.SearchAddress,
                        webSite.Name.ToLower(), SearchWord);
                else
                    createAddress = string.Format(Constants.SearcherInOneAddress,
                webSite.Name.ToLower(), SearchWord);
                
                webGard.Begin(createAddress);
                bbb = true;
            }
            else
                txtSearch.Focus(FocusState.Keyboard);
        }
    }
}
