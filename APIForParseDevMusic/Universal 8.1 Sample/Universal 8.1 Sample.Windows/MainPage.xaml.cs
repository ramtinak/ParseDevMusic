/* 

Based on music, mp3 searcher sites
Made for http://api.win-nevis.com
api.win-nevis.com may change

Download from http://www.win-nevis.com


Parse Dev Studio

http://www.ParseDev.com

*/
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Linq;

// usinge morede niyaz
using APIForParseDevMusic;


namespace Universal_8._1_Sample
{
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;
        public SiteList Sites;
        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
            Loaded += MainPage_Loaded;
        }
        async private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Sites == null || Sites != null && !Sites.Any())
            {
                pb.Visibility = Visibility.Visible;
                var list = await WebGard.GetSites();

                if (list == null || list != null && !list.Any())
                {
                    string str = "There is a problem that couldn't retrive sites." + Environment.NewLine
                        + "Please check your network and try again.";
                    await new Windows.UI.Popups.MessageDialog(str).ShowAsync();
                }
                else
                {
                    Sites = list;
                    gridView.ItemsSource = Sites;
                    await new Windows.UI.Popups.MessageDialog(string.Format("{0} site(s) added...", list.Count)).ShowAsync();
                }
            }
            else gridView.ItemsSource = Sites;
            pb.Visibility = Visibility.Collapsed;
        }

        private void gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (sender != null &&
                e != null &&
                e.ClickedItem != null &&
                e.ClickedItem.GetType() == typeof(WebSite))
            {
                WebSite site = e.ClickedItem as WebSite;
                ((Frame)Window.Current.Content).Navigate(typeof(SitePage),
                    e.ClickedItem as WebSite);
            }

        }
    }
}
