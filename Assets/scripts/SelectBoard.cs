using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using Newtonsoft.Json;

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
            GameObject boardObject = JsonConvert.DeserializeObject<GameObject>(jsonData);
            Debug.Log(boardObject);

        } else {
            Debug.Log ("Szopóka");
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
