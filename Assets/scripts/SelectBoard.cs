using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class SelectBoard : MonoBehaviour {

    public class FullBoard {

        public string status { get; set; }
        public int code { get; set; }
        public string host { get; set; }
        public string generated_at { get; set; }
        public string message { get; set; }
        public Data data { get; set; }

        public class Attribution {
            public string title { get; set; }
            public string url { get; set; }
            public string provider_icon_url { get; set; }
            public string author_name { get; set; }
            public string provider_favicon_url { get; set; }
            public string author_url { get; set; }
            public string provider_name { get; set; }
        }

        public class Pinner {
            public string about { get; set; }
            public string location { get; set; }
            public string full_name { get; set; }
            public int follower_count { get; set; }
            public string image_small_url { get; set; }
            public int pin_count { get; set; }
            public string id { get; set; }
            public string profile_url { get; set; }
        }

        public class imageAttr {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Images {
            public imageAttr imageAttr { get; set; }
        }

        public class Pin {
            public string domain { get; set; }
            public Attribution attribution { get; set; }
            public string description { get; set; }
            public Pinner pinner { get; set; }
            public int repin_count { get; set; }
            public string dominant_color { get; set; }
            public int like_count { get; set; }
            public string link { get; set; }
            public Images images { get; set; }
            public object embed { get; set; }
            public bool is_video { get; set; }
            public string id { get; set; }
        }

        public class User {
            public string about { get; set; }
            public string location { get; set; }
            public string full_name { get; set; }
            public int follower_count { get; set; }
            public string image_small_url { get; set; }
            public int pin_count { get; set; }
            public string id { get; set; }
            public string profile_url { get; set; }
        }

        public class Board {
            public string description { get; set; }
            public string url { get; set; }
            public int follower_count { get; set; }
            public string image_thumbnail_url { get; set; }
            public int pin_count { get; set; }
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Data {
            public List<Pin> pins { get; set; }
            public User user { get; set; }
            public Board board { get; set; }
        }
    }

	public class imageObject {
		public string url;

		public imageObject (string URL) {
			this.url = URL;
		}
	}

	List<imageObject> imageList = new List<imageObject>();

	public IEnumerator getBoard (string url) {
		WWW www = new WWW (url);
		yield return www;

		if (www.error == null) {
            string jsonString = www.text.Replace("237x", "imageAttr");
            FullBoard boardObject = JsonConvert.DeserializeObject<FullBoard>(jsonString);
            for (int i = 0; i < boardObject.data.pins.Count; i++) {
                imageObject imageURL = new imageObject(boardObject.data.pins[i].images.imageAttr.url);
                imageList.Add(imageURL);
            }
		} else {
			Debug.Log ("Szopóka");
		}

        fetchImageList (imageList);
	}

	public void SelectSubmit () {
		int chosenInt = GameObject.Find ("MainDropdown").GetComponent<Dropdown>().value;

		string pinUser = "kilmarinen";

		ArrayList boardList = new ArrayList();
		boardList.Insert( 0, "catastrophe" );
		boardList.Insert( 1, "cp" );
		boardList.Insert( 2, "living-beauty" );

		string boardURL = "https://api.pinterest.com/v3/pidgets/boards/" + pinUser + "/" + boardList[chosenInt] + "/pins/";
		StartCoroutine(getBoard(boardURL));
	}

    public void fetchImageList(List<imageObject> imageList) {
        for (int i = 0; i < imageList.Count; i++) { 
            Debug.Log(imageList[i].url.Replace("imageAttr", "237x"));
        }

        loadMain();
    }

    public void loadMain() {
        SceneManager.LoadScene ("main", LoadSceneMode.Single);
    }
}
