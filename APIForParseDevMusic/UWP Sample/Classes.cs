/* 

Based on music, mp3 searcher sites
Made for http://api.win-nevis.com
api.win-nevis.com may change

Download from http://www.win-nevis.com


Parse Dev Studio

http://www.ParseDev.com

*/
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;

namespace APIForParseDevMusic
{
    /// <summary>
    /// jsoni ke in class parse mikone
    /// {
    ///    "ComingSoon": false,
    ///    "Date": "23rd Mar 2016",
    ///    "ImageUrl": "http:\/\/www.27farskids.com\/wp-content\/uploads\/2016\/03\/ddd-1-e1458748085612.jpg",
    ///    "Name": "Matin M.T – Daghigheha",
    ///    "Singer": null,
    ///    "Song": null,
    ///    "Type": "music",
    ///    "Url": "http:\/\/www.27farskids.com\/music\/matin-m-t-daghigheha\/",
    ///    "View": "42,178 Views"
    ///  }
    /// </summary>
    public class Music
    {
        private bool comSoon = false;
        public string Type { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }        
        public string View { get; set; }
        public string Date { get; set; }
        public string Url { get; set; }
        public bool ComingSoon { get { return comSoon; } set { comSoon = value; } }
        public string Song { get; set; }
        public string Singer { get; set; }
        [System.Runtime.Serialization.IgnoreDataMember()]
        public BitmapImage BitmapImage
        {
            get
            {
                Uri uriResult;
                bool result = Uri.TryCreate(ImageUrl, UriKind.Absolute, out uriResult);
                if (!result)
                    return null;
                else
                return new BitmapImage(uriResult);
            }
        }
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Singer))
                return Singer + " - " + Song;
            else
                return Name;
        }
    }


    /// <summary>
    /// site haei ke faghat kareshon search hast objecteshon injorie
    /// nokte: momkene bazi az maghadire bazgashti meghdari nadashte bashand
    /// masalan baraye Site zenefarm in maghadir ro barmigardone:
    /// Name, Url, Duration, Size, TrackId
    /// baghie ro Null return mikone
    /// mesal:
    /// {
    ///    "ComingSoon": false,
    ///    "Date": null,
    ///    "ImageUrl": "",
    ///    "Name": "A river runs through it: Iran",
    ///    "Singer": null,
    ///    "Song": null,
    ///    "Type": null,
    ///    "Url": "http:\/\/zenefarm.com\/download\/iran\/164270325",
    ///    "View": null,
    ///    "Author": null,
    ///    "Duration": " 0:12:56",
    ///    "Size": " 11.85 MB",
    ///    "Sound": null,
    ///    "Title": null,
    ///    "TrackId": "164270325"
    ///  }
    /// ama baraye site e mp3pm in ha ro barmigardone:
    /// Author, Title, Url, Sound, Duration
    /// baghie ro Null return mikone
    /// {
    ///    "ComingSoon": false,
    ///    "Date": null,
    ///    "ImageUrl": null,
    ///    "Name": null,
    ///    "Singer": null,
    ///    "Song": null,
    ///    "Type": null,
    ///    "Url": "http:\/\/cs1.mp3.pm\/download\/3381228\/S2VaYW9MTDBlazJYQ294UmdISjM1enVFM1VGdjVpbnVCSE5CSlArN3k1dG9hTlI2TWZwS294OS9GdUt2eVVXNFNoSDkzYmZWK3VnMmRUeVpDLzEwQUhFL2E3VjdBUEFjVmNpQmN1d21xTkkyS0ZzdWVzN1p1bjVyR0cwaFFpRzM\/._ll_l_._l_-_Pla4ut_Nebesa_Iran_(mp3.pm).mp3",
    ///    "View": null,
    ///    "Author": "✔.ιllιlι.ιl [►]",
    ///    "Duration": "05:16",
    ///    "Size": null,
    ///    "Sound": "http:\/\/cs1.mp3.pm\/listen\/3381228\/S2VaYW9MTDBlazJYQ294UmdISjM1enVFM1VGdjVpbnVCSE5CSlArN3k1dG9hTlI2TWZwS294OS9GdUt2eVVXNFNoSDkzYmZWK3VnMmRUeVpDLzEwQUhFL2E3VjdBUEFjVmNpQmN1d21xTkkyS0ZzdWVzN1p1bjVyR0cwaFFpRzM\/._ll_l_._l_-_Pla4ut_Nebesa_Iran_(mp3.pm).mp3",
    ///    "Title": "Pla4ut Nebesa (Iran)",
    ///    "TrackId": null
    ///  }
    /// </summary>
    public class SearchData : Music
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Sound { get; set; }
        public string TrackId { get; set; }
        public string Size { get; set; }
        public string Duration { get; set; }
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Author))
                return Author + " - " + Title;
            else
            return Name.ToString();
        }
    }


    /// <summary>
    /// Listi az Search data ro mide ke mikhaim ounha ro parse konim
    /// </summary>
    public class SearchList : List<SearchData> { }


    /// <summary>
    /// Tafavoti ke tuye SearchList va SearchCollection hast ine ke
    /// age biaim bedone darkhaste search az siti yani:
    /// http://api.win-nevis.com/searcher.aspx?s=Iran
    /// use konim, site khodkar tuye har 2 site kalame ei ke dadim ro search mikone
    /// va maghadir ro be sorate joda return mikone. maghadire har site joda khahad bod
    /// 
    /// </summary>
    public class SearchCollection
    {
        // Jsoni ke in class oun ro parse mikone:
        //{
        //  "Mp3PM": [
        //    {
        //      "ComingSoon": false,
        //      "Date": null,
        //      "ImageUrl": null,
        //      "Name": null,
        //      "Singer": null,
        //      "Song": null,
        //      "Type": null,
        //      "Url": "http:\/\/cs1.mp3.pm\/download\/44372070\/S2VaYW9MTDBlazJYQ294UmdISjM1enVFM1VGdjVpbnVCSE5CSlArN3k1dVh0cHoyMmcrME9hOE1zVXNkdGhBOERqV24zUm9hQWFJQjMyQnZnaEJuT3lhYjlZaEZsdWJHK1M2Zzg3a0Z2K3hMRStyTFdvTFplNlc3TytXUUVNL1k\/Radians_-_Iran_(mp3.pm).mp3",
        //      "View": null,
        //      "Author": "Radians",
        //      "Duration": "02:24",
        //      "Size": null,
        //      "Sound": "http:\/\/cs1.mp3.pm\/listen\/44372070\/S2VaYW9MTDBlazJYQ294UmdISjM1enVFM1VGdjVpbnVCSE5CSlArN3k1dVh0cHoyMmcrME9hOE1zVXNkdGhBOERqV24zUm9hQWFJQjMyQnZnaEJuT3lhYjlZaEZsdWJHK1M2Zzg3a0Z2K3hMRStyTFdvTFplNlc3TytXUUVNL1k\/Radians_-_Iran_(mp3.pm).mp3",
        //      "Title": "Iran",
        //      "TrackId": null
        //    },
        //    {
        //      "ComingSoon": false,
        //      "Date": null,
        //      "ImageUrl": null,
        //      "Name": null,
        //      "Singer": null,
        //      "Song": null,
        //      "Type": null,
        //      "Url": "http:\/\/cs1.mp3.pm\/download\/46496682\/S2VaYW9MTDBlazJYQ294UmdISjM1enVFM1VGdjVpbnVCSE5CSlArN3k1dlZHT2Zkb28vWi9IY2ZKdjVVVWxZdlVvUHo4WnRpZ0p6VEJWWU42bWhzSkY1aWt1L3YyOSsrSU1MRU9qZFJubzU3TklqS1FpalR2S3ExS1U2bU93eGg\/Iran_-_Dokhtare_(mp3.pm).mp3",
        //     "View": null,
        //      "Author": "Iran",
        //      "Duration": "05:21",
        //      "Size": null,
        //      "Sound": "http:\/\/cs1.mp3.pm\/listen\/46496682\/S2VaYW9MTDBlazJYQ294UmdISjM1enVFM1VGdjVpbnVCSE5CSlArN3k1dlZHT2Zkb28vWi9IY2ZKdjVVVWxZdlVvUHo4WnRpZ0p6VEJWWU42bWhzSkY1aWt1L3YyOSsrSU1MRU9qZFJubzU3TklqS1FpalR2S3ExS1U2bU93eGg\/Iran_-_Dokhtare_(mp3.pm).mp3",
        //      "Title": "Dokhtare",
        //      "TrackId": null
        //    }
        //  ],
        //  "ZeneFarm": [
        //    {
        //      "ComingSoon": false,
        //      "Date": null,
        //      "ImageUrl": "",
        //      "Name": "A river runs through it: Iran",
        //      "Singer": null,
        //      "Song": null,
        //      "Type": null,
        //      "Url": "http:\/\/zenefarm.com\/download\/Iran\/164270325",
        //      "View": null,
        //      "Author": null,
        //      "Duration": " 0:12:56",
        //      "Size": " 11.85 MB",
        //      "Sound": null,
        //      "Title": null,
        //      "TrackId": "164270325"
        //    },
        //    {
        //      "ComingSoon": false,
        //      "Date": null,
        //      "ImageUrl": "https:\/\/i1.sndcdn.com\/artworks-000081550370-bdkz0t-large.jpg",
        //      "Name": "Corruption and reform in Iran",
        //      "Singer": null,
        //      "Song": null,
        //      "Type": null,
        //      "Url": "http:\/\/zenefarm.com\/download\/Iran\/152986113",
        //      "View": null,
        //      "Author": null,
        //      "Duration": " 0:11:29",
        //      "Size": " 10.52 MB",
        //      "Sound": null,
        //      "Title": null,
        //      "TrackId": "152986113"
        //    }
        //  ]
        //}

        public SearchList Mp3PM { get; set; }
        public SearchList ZeneFarm { get; set; }
    }


    /// <summary>
    /// objecte jsone ma in shekli hast:
    ///{
    ///  "Address": string,
    ///  "IsRightToLeft": boolean,
    ///  "IsSearcher": boolean,
    ///  "Name": string
    ///}
    /// </summary>
    public class WebSite
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public bool? IsSearcher { get; set; }
        public bool? IsRightToLeft { get; set; }
        public override string ToString()
        {
            return Name.ToString();
        }
    }


    /// <summary>
    /// Json Collectione site ha injori hast
    /// ke ba SiteList oun ro Parse mikonim
    ///[
    ///  {
    ///    "Address": string,
    ///    "IsRightToLeft": boolean,
    ///    "IsSearcher": boolean,
    ///    "Name": string
    ///  },
    ///  {
    ///    "Address": string,
    ///    "IsRightToLeft": boolean,
    ///    "IsSearcher": boolean,
    ///    "Name": string
    ///  }
    ///]
    /// </summary>
    public class SiteList : List<WebSite> { }


    /// <summary>
    /// jsoni k in List parse mikone:
    /// [
    ///  {
    ///    "ComingSoon": false,
    ///    "Date": "23rd Mar 2016",
    ///    "ImageUrl": "http:\/\/www.27farskids.com\/wp-content\/uploads\/2016\/03\/ddd-1-e1458748085612.jpg",
    ///    "Name": "Matin M.T – Daghigheha",
    ///    "Singer": null,
    ///    "Song": null,
    ///    "Type": "music",
    ///    "Url": "http:\/\/www.27farskids.com\/music\/matin-m-t-daghigheha\/",
    ///    "View": "42,178 Views"
    ///  },
    ///  {
    ///    "ComingSoon": false,
    ///    "Date": "23rd Mar 2016",
    ///    "ImageUrl": "http:\/\/www.27farskids.com\/wp-content\/uploads\/2016\/03\/Mahyar-Tn-Ft-Andish-Ro-Ravane-Hame-Chi.jpg",
    ///    "Name": "Mahyar TN FT. Andish – Ro Ravane Hame Chi",
    ///    "Singer": null,
    ///    "Song": null,
    ///    "Type": "music",
    ///    "Url": "http:\/\/www.27farskids.com\/music\/mahyar-tn-ft-andish-ro-ravane-hame-chi\/",
    ///    "View": "41,083 Views"
    ///  }
    ///]
    /// </summary>
    public class TMusicList : List<Music> { }


    /// <summary>
    /// jsoni ke in class parse mikone
    /// {
    ///    "CommingSoon": false,
    ///    "Dislike": null,
    ///    "Like": null,
    ///    "Name": "Ahmad Saeedi - Yadet Biad [320].mp3",
    ///    "PostId": null,
    ///    "Url": "http:\/\/dl3.27farskids.com\/R1\/Music\/95\/01\/03\/Ahmad%20Saeedi%20-%20Yadet%20Biad%20[320].mp3",
    ///    "Views": null
    ///  }
    /// </summary>
    public class DownloadLink
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Like { get; set; }
        public string Dislike { get; set; }
        public string Views { get; set; }
        public string PostId { get; set; }
        public bool CommingSoon { get; set; }
        public override string ToString()
        {
            return Name.ToString();
        }
    }


    /// <summary>
    /// jsoni ke in class parse mikone
    /// [
    ///  {
    ///    "CommingSoon": false,
    ///    "Dislike": null,
    ///    "Like": null,
    ///    "Name": "Ahmad Saeedi - Yadet Biad [320].mp3",
    ///    "PostId": null,
    ///    "Url": "http:\/\/dl3.27farskids.com\/R1\/Music\/95\/01\/03\/Ahmad%20Saeedi%20-%20Yadet%20Biad%20[320].mp3",
    ///    "Views": null
    ///  },
    ///  {
    ///    "CommingSoon": false,
    ///    "Dislike": null,
    ///    "Like": null,
    ///    "Name": "Ahmad Saeedi - Yadet Biad [128].mp3",
    ///    "PostId": null,
    ///    "Url": "http:\/\/dl3.27farskids.com\/R1\/Music\/95\/01\/03\/Ahmad%20Saeedi%20-%20Yadet%20Biad%20[128].mp3",
    ///    "Views": null
    ///  }
    ///]
    /// </summary>
    public class DownloadedList : List<DownloadLink> { }


    /// <summary>
    /// dataye site e iranmusic kami fargh dare
    /// iranmusic ham link haye download ro migire
    /// ham posthaye mortabet ba link e download
    /// </summary>
    public class DownloadsList
    {
        public List<DownloadLink> Downloads { get; set; }
        public List<Music> Relative { get; set; }
    }
}
