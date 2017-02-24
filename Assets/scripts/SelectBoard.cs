using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.IO;

public class SelectBoard : MonoBehaviour {

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
			string jsonData = www.text;

			JSONObject json = new JSONObject (jsonData);

			accessData(json);

			for (int i = 0; i < imageList.Count; i++) {
				Debug.Log (imageList[i].url);
			}
		} else {
			Debug.Log ("Szopóka");
		}
	}

	void accessData (JSONObject obj) {
		switch(obj.type) {
		case JSONObject.Type.OBJECT:
			for(int i = 0; i < obj.list.Count; i++) {
				string key = (string)obj.keys[i];
				if (key == "url") {
					imageObject imageURL = new imageObject (obj.list[i].str);
					imageList.Add (imageURL);
				}
				JSONObject j = (JSONObject)obj.list[i];
				accessData(j);
			}
			break;
		case JSONObject.Type.ARRAY:
			foreach(JSONObject j in obj.list) {
				accessData(j);
			}
			break;
		case JSONObject.Type.STRING:
//			Debug.Log(obj.str);
			break;
		case JSONObject.Type.NUMBER:
//			Debug.Log(obj.n);
			break;
		case JSONObject.Type.BOOL:
//			Debug.Log(obj.b);
			break;
		case JSONObject.Type.NULL:
//			Debug.Log("NULL");
			break;

		}
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
}
