/* 

Based on music, mp3 searcher sites
Made for http://api.win-nevis.com
api.win-nevis.com may change

Download from http://www.win-nevis.com


Parse Dev Studio

http://www.ParseDev.com

*/
namespace APIForParseDevMusic
{
    /// <summary>
    /// Address haye morede niyaz baraye estefade kardan az API Parse Dev Studio
    /// Created by http://www.ParseDev.com
    /// Our forum: http://www.Win-Nevis.com
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Daryafte liste kamele site ha
        /// </summary>
        public const string SitesAddress = "http://api.win-nevis.com/sites.aspx";
        /// <summary>
        /// Daryafte yek site
        /// </summary>
        public const string Address = "http://api.win-nevis.com/index.aspx?siteAddress={0}";
        /// <summary>
        /// Daryaft yek page az ye site
        /// </summary>
        public const string AddressWithPage = "http://api.win-nevis.com/index.aspx?siteAddress={0}&p={1}";
        /// <summary>
        /// Justju kardan dar yek site
        /// </summary>
        public const string SearchAddress = "http://api.win-nevis.com/index.aspx?siteAddress={0}&s={1}";
        /// <summary>
        /// Daryafte yek page az yek justju
        /// </summary>
        public const string SearchAddressWithPage = "http://api.win-nevis.com/index.aspx?siteAddress={0}&s={1}&p={2}";


        /// <summary>
        /// Justju kardan dar tamamie site haye searche music
        /// </summary>
        public const string SearcherAddress = "http://api.win-nevis.com/searcher.aspx?s={0}";
        /// <summary>
        /// Daryafte yek page az tamamie site haye searche music
        /// </summary>
        public const string SearcherAddressWithPage = "http://api.win-nevis.com/searcher.aspx?s={0}&p={1}";
        /// <summary>
        /// Justju kardan dar tamamie site haye searche music
        /// </summary>
        public const string SearcherInOneAddress = "http://api.win-nevis.com/searcher.aspx?siteAddress={0}&s={1}";
        /// <summary>
        /// Justju kardan dar tamamie site haye searche music
        /// </summary>
        public const string SearcherInOneAddressWithPage = "http://api.win-nevis.com/searcher.aspx?siteAddress={0}&s={1}&p={2}";



        /// <summary>
        /// Daryafte linke download
        /// </summary>
        public const string DownloadLinkAddress = "http://api.win-nevis.com/index.aspx?siteAddress={0}&getDownloadLink={1}";

    }
}
