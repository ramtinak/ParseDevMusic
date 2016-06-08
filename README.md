# Parse Dev Music
### API version 1.0.3  updated 06/05/2016

### Source codes available for Windows 8.1, Windows Phone 8.1 and Windows 10(UWP APP).


[Parse Dev Studio](http://www.parsedev.com) Music Api is a service that is easily to search, download or listen music by direct.
With this service you can easily access to any sites that is supported by Parse Dev Studio (for now is 14 sites: FazMusic, Farskids, IranMusic, Nex1Music, MusicIrooni, PopMusic, Ahangestan, PlayMusic, MusicBaz, MusicBaran, TakTaraneh, TehranMusic, Zenefarm music searcher, Mp3PM music searcher). 
just type that site name you want api will returns a result to that music/videos

### Supported Sites

Url for gets sites lists that supported:

	http://api.win-nevis.com/sites.aspx

Returns a Json value:

	    [
	      {
	        "Address": string,
	        "IsRightToLeft": boolean,
	        "IsSearcher": boolean,
	        "Name": string
	      },
	      {
	        "Address": string,
	        "IsRightToLeft": boolean,
	        "IsSearcher": boolean,
	        "Name": string
	      }
	    ]

### Gets a special site:

	http://api.win-nevis.com/index.aspx?siteAddress=SITENAME

Replace your site name(farskids, fazmusic, iranmusic and...) with SITENAME
For example gets farskids:

	http://api.win-nevis.com/index.aspx?siteAddress=farskids

Json response:

	     [
	      {
	        "ComingSoon": false,
	        "Date": "23rd Mar 2016",
	        "ImageUrl": "http:\/\/www.27farskids.com\/wp-content\/uploads\/2016\/03\/ddd-1-e1458748085612.jpg",
	        "Name": "Matin M.T – Daghigheha",
	        "Singer": null,
	        "Song": null,
	        "Type": "music",
	        "Url": "http:\/\/www.27farskids.com\/music\/matin-m-t-daghigheha\/",
	        "View": "42,178 Views"
	      },
	      {
	        "ComingSoon": false,
	        "Date": "23rd Mar 2016",
	        "ImageUrl": "http:\/\/www.27farskids.com\/wp-content\/uploads\/2016\/03\/Mahyar-Tn-Ft-Andish-Ro-Ravane-Hame-Chi.jpg",
	        "Name": "Mahyar TN FT. Andish – Ro Ravane Hame Chi",
	        "Singer": null,
	        "Song": null,
	        "Type": "music",
	        "Url": "http:\/\/www.27farskids.com\/music\/mahyar-tn-ft-andish-ro-ravane-hame-chi\/",
	        "View": "41,083 Views"
	      }
	    ]

### Gets a specific page of sites:

	http://api.win-nevis.com/index.aspx?siteAddress=SITENAME&p=PAGENUMBER
example:

	http://api.win-nevis.com/index.aspx?siteAddress=fazmusic&p=4

Json response is same of using without page number.



### Search in site:

	http://api.win-nevis.com/index.aspx?siteAddress=SITENAME&s=SEARCHWORD
Example:

	http://api.win-nevis.com/index.aspx?siteAddress=iranmusic&s=saman%20jalili



### Search with page number:

	http://api.win-nevis.com/index.aspx?siteAddress=SITENAME&s=SEARCHWOR&p=PAGENUMBER
Example:

	http://api.win-nevis.com/index.aspx?siteAddress=iranmusic&s=saman%20jalili&p=2

Json response looks like anothers.



### Gets download links of a song, video

	http://api.win-nevis.com/index.aspx?siteAddress=SITENAME&getDownloadLink=POSTLINK
Example:

	http://api.win-nevis.com/index.aspx?siteAddress=farskids&getDownloadLink=http://www.28farskids.com/music/ahmad-saeedi-yadet-biad/

Json response

	     [
	      {
	        "CommingSoon": false,
	        "Dislike": null,
	        "Like": null,
	        "Name": "Ahmad Saeedi - Yadet Biad [320].mp3",
	        "PostId": null,
	        "Url": "http:\/\/dl3.27farskids.com\/R1\/Music\/95\/01\/03\/Ahmad%20Saeedi%20-%20Yadet%20Biad%20[320].mp3",
	        "Views": null
	      },
	      {
	        "CommingSoon": false,
	        "Dislike": null,
	        "Like": null,
	        "Name": "Ahmad Saeedi - Yadet Biad [128].mp3",
	        "PostId": null,
	        "Url": "http:\/\/dl3.27farskids.com\/R1\/Music\/95\/01\/03\/Ahmad%20Saeedi%20-%20Yadet%20Biad%20[128].mp3",
	        "Views": null
	      }
	    ]

Persian training:
[Parse Dev Music API in Win Nevis forum](http://www.win-nevis.com/topic/230-%D8%A2%D9%85%D9%88%D8%B2%D8%B4-%D8%A7%D8%B3%D8%AA%D9%81%D8%A7%D8%AF%D9%87-%D8%A7%D8%B2-api-%D9%85%D9%88%D8%B2%DB%8C%DA%A9-%D8%A7%D8%B3%D8%AA%D8%AF%DB%8C%D9%88-%D9%BE%D8%A7%D8%B1%D8%B3%D9%87-%D9%86%D8%B3%D8%AE%D9%87-103/)
