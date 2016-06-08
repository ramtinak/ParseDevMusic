/* 

Based on music, mp3 searcher sites
Made for http://api.win-nevis.com
api.win-nevis.com may change

Download from http://www.win-nevis.com


Parse Dev Studio

http://www.ParseDev.com

*/
using System;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Serialization.Json;
using APIForParseDevMusic;

/// <summary>
/// Based on WebGard Win Nevis class at:
/// http://www.win-nevis.com/topic/116-%DA%A9%D9%84%D8%A7%D8%B3-webgard-%D9%86%D9%85%D9%88%D9%86%D9%87-%DA%A9%D8%AF/
/// </summary>
public class WebGard
{
    public string URL = null;
    public event ResultGrabber OnResult;
    public event ProgressGrabber OnProgress;
    async public void Begin(string address)
    {
        URL = address;
        try
        {
            SetResult(new Results { ShowType = ShowType.RetrievingData, Filter = Filter.No });
            SetProgress(new Progresses { ShowType = ShowType.RetrievingData });
            Uri targetUri = new Uri(address);
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, targetUri);
            HttpResponseMessage response = await client.SendAsync(request);

            string pageSource = string.Empty;
            using (Stream responseStream = await response.Content.ReadAsStreamAsync())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                pageSource = reader.ReadToEnd();
            }
            if (response.StatusCode != HttpStatusCode.Forbidden && !pageSource.Contains("peyvandha.ir"))
            {
                SetResult(new Results { Source = pageSource, ShowType = ShowType.DataRetrieved, Filter = Filter.No });
                SetProgress(new Progresses { ShowType = ShowType.DataRetrieved });
            }
            else
            {
                SetResult(new Results { Source = pageSource, ShowType = ShowType.Filter, Filter = Filter.Yes });
                SetProgress(new Progresses { ShowType = ShowType.Filter });
            }
            response.Dispose();
        }
        catch (HttpRequestException ex)
        {
            SetResult(new Results { Filter = Filter.Error, ShowType = ShowType.Error, Error = "HttpRequestException:\t" + ex.Message });
            SetProgress(new Progresses { ShowType = ShowType.Error });
        }
        catch (WebException ex)
        {
            SetResult(new Results { Filter = Filter.Error, ShowType = ShowType.Error, Error = "WebException:\t" + ex.Message });
            SetProgress(new Progresses { ShowType = ShowType.Error });
        }
        catch (Exception ex)
        {
            SetResult(new Results { Filter = Filter.Error, ShowType = ShowType.Error, Error = "Exception:\t" + ex.Message });
            SetProgress(new Progresses { ShowType = ShowType.Error });
        }
    }

    public async static Task<SiteList> GetSites()
    {
        SiteList sites = new SiteList();
        try
        {
            // addresse gereftane site ha:
            // http://api.win-nevis.com/sites.aspx
            // meghdare json ro bazgasht mide:
            //[
            //  {
            //    "Address": string,
            //    "IsRightToLeft": boolean,
            //    "IsSearcher": boolean,
            //    "Name": string
            //  },
            //  {
            //    "Address": string,
            //    "IsRightToLeft": boolean,
            //    "IsSearcher": boolean,
            //    "Name": string
            //  }
            //]
            string url = Constants.SitesAddress;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using (var stream = response.GetResponseStream())
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(SiteList));

                sites = (SiteList)jsonSerializer.ReadObject(stream);
                return sites;
            }
        }
        catch (Exception ex) { Debug.WriteLine("GetSites ex: " + ex.Message); }
        return sites;
    }

    private void SetResult(Results result)
    {
        OnResult(this, result);
    }
    private void SetProgress(Progresses progress)
    {
        OnProgress(this, progress);
    }
}
public delegate void ResultGrabber(object sender, Results result);
public delegate void ProgressGrabber(object sender, Progresses progress);

public class Progresses
{
    public ShowType ShowType { get; set; }
}
public class Results
{
    /// <summary>
    /// Is this address filter?!
    /// </summary>
    public Filter Filter { get; set; }
    /// <summary>
    /// Page source
    /// </summary>
    public string Source { get; set; }
    /// <summary>
    /// Error message
    /// </summary>
    public string Error { get; set; }
    /// <summary>
    /// Show type of your web requested
    /// </summary>
    public ShowType ShowType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>Returns Filter, Show type, Error message and source page</returns>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Filter: ");
        sb.Append(Filter);
        sb.Append(Environment.NewLine);
        sb.Append("ShowType: ");
        sb.Append(ShowType);
        sb.Append(Environment.NewLine);
        sb.Append("Error: ");
        sb.Append(Error);
        sb.Append(Environment.NewLine);
        sb.Append("Source: ");
        sb.Append(Source);
        sb.Append(Environment.NewLine);
        return sb.ToString();
    }
}
public enum Filter
{
    /// <summary>
    /// Is Filtered
    /// </summary>
    Yes,
    /// <summary>
    /// Not Filtered
    /// </summary>
    No,
    /// <summary>
    /// Error, can't find out your requested address is filter or not 
    /// </summary>
    Error
}
public enum ShowType
{
    /// <summary>
    /// Start Retrieving data
    /// </summary>
    RetrievingData,
    /// <summary>
    /// Data retrievd
    /// </summary>
    DataRetrieved,
    /// <summary>
    /// Error with Retrieving data
    /// </summary>
    Error,
    /// <summary>
    /// Address is filtered or blocked
    /// </summary>
    Filter,
    /// <summary>
    /// Null is first type
    /// </summary>
    Null = -1
}

