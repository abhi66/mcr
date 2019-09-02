using UnityEngine;
using System.Collections;

public class PlayerPref : MonoBehaviour {

	public bool rst;
	void Awake () {
		if (rst)
			PlayerPrefs.DeleteAll ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
