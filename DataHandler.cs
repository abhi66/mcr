using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DataHandler : MonoBehaviour {
	public int carSel;
	public int car1_texSelected;
	public float speed;
	public float acc;
	public float nos;
	public float handle;
	public int money;
	public int totalLaps;
	public int totalTm;


	public static DataHandler tmp;

	void Awake () {

		if (tmp == null) {
			tmp = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);

		}
	}

	void Start(){
		carSel = 1;
		car1_texSelected = 1;
	}
		

}
