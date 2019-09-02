using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceLoader : MonoBehaviour {


	public GameObject shopC;
	public GameObject mainC;
	public GameObject loadingC;


	public Text moneyT;
	public Text moneyT1;

	 
	public Text totalStarsT;
	public Text starsT;
	public Text raceTime;

	public Sprite[] backIList;
	public Image backI;
	public Image loadingCI;
	public Text raceT;
	public GameObject lapI2;
	public GameObject lockP;
	public GameObject startRB;

	int currentLevel = 0;


	int totalStars;
	int stars;
	int money;

	public AudioSource aud_S;
	public AudioClip[] clp;

	DataHandler dtaHandlr;

	 void PlayAudio(AudioClip clip){
		aud_S.clip = clip;
		aud_S.Play ();
	}

	void Start(){

	
	//	PlayerPrefs.DeleteAll ();
		lockP.SetActive(false);
		dtaHandlr = GameObject.Find ("DataHandler").GetComponent<DataHandler> ();
		dtaHandlr.totalTm = 1;
		dtaHandlr.totalLaps = 1;

		mainC.GetComponent<Animator> ().SetBool ("Hide", false);
		shopC.GetComponent<Animator> ().SetBool ("Hide", true);


		money = dtaHandlr.money;
		moneyT.text = moneyT1.text  = money.ToString ();

		totalStars = PlayerPrefs.GetInt ("L_1_S", 0) + PlayerPrefs.GetInt ("L_2_S", 0) + PlayerPrefs.GetInt ("L_3_S", 0);
		stars = PlayerPrefs.GetInt ("L_1_S", 0);
		totalStarsT.text = totalStars.ToString ()+ " / " + 15;
		starsT.text = stars.ToString () + " / " + 5;
		raceTime.text = "01:00:00";
	}


	public void ShopB(){
		mainC.GetComponent<Animator> ().SetBool ("Hide", true);
		shopC.GetComponent<Animator> ().SetBool ("Hide", false);
		PlayAudio (clp[0]);

	}
	public void ShopRB(){
		mainC.GetComponent<Animator> ().SetBool ("Hide", false);
		shopC.GetComponent<Animator> ().SetBool ("Hide", true);
		PlayAudio (clp[0]);

	}

	public void CallAds(){
		Debug.Log ("Calling ads");
		money = PlayerPrefs.GetInt ("Money", 500);
		money = money + 50;
		moneyT.text = moneyT1.text = money.ToString();

		PlayerPrefs.SetInt ("Money", money);
	}
	public void CallLeaderBoard(){
		Debug.Log ("Calling LBD");
	}

	public void Lap1B(){
		PlayAudio (clp[0]);
		dtaHandlr.totalLaps = 1;
		stars = PlayerPrefs.GetInt ("L_1_S", 0);
		starsT.text = stars.ToString ()+ " / " + 5;;
		raceTime.text = "01:00:00";
		dtaHandlr.totalTm = 1;
		}
	public void Lap2B(){
		PlayAudio (clp[0]);
		dtaHandlr.totalLaps = 2;
		stars = PlayerPrefs.GetInt ("L_2_S", 0);
		starsT.text = stars.ToString ()+ " / " + 5;;
		raceTime.text = "02:00:00";
		dtaHandlr.totalTm = 2;
	}


	public void LoadRace(){
		PlayAudio (clp[1]);
		loadingC.SetActive (true);
		if (currentLevel == 0) {
			loadingCI.sprite = backIList [2];
			SceneManager.LoadScene (3);

		} else if (currentLevel == 1) {
			loadingCI.sprite = backIList [3];
			dtaHandlr.totalLaps = 1;
			SceneManager.LoadScene (4);
		}else if (currentLevel == 2) {
			loadingCI.sprite = backIList [5];
			dtaHandlr.totalLaps = 1;
			SceneManager.LoadScene (5);
		}
		

	}
	public void NextLevelB(){
		
		PlayAudio (clp[0]);
		if(currentLevel == 0){
			stars = PlayerPrefs.GetInt ("L_2_S", 0);
			starsT.text = stars.ToString () + " / " + 5;
			currentLevel = 1;
			backI.sprite = backIList [1];
			raceT.text = "Medival Challenge II";
			int tmp = PlayerPrefs.GetInt ("UnlockedLevel_1",0);
			lapI2.SetActive (false);
			if (tmp != 0) {
				
				lockP.SetActive (false);
				startRB.SetActive (true);
			} else {
				lockP.SetActive (true);
				startRB.SetActive (false);
			}
		}else if(currentLevel == 1){
			stars = PlayerPrefs.GetInt ("L_3_S", 0);
			starsT.text = stars.ToString () + " / " + 5;
			lapI2.SetActive (true);
			currentLevel = 2;
			backI.sprite = backIList [4];
			raceT.text = "Medival Challenge III";
			int tmp = PlayerPrefs.GetInt ("UnlockedLevel_2",0);
			lapI2.SetActive (false);
			if (tmp != 0) {

				lockP.SetActive (false);
				startRB.SetActive (true);
			} else {
				lockP.SetActive (true);
				startRB.SetActive (false);
			}

		}else if(currentLevel == 2){
			stars = PlayerPrefs.GetInt ("L_1_S", 0);
			starsT.text = stars.ToString () + " / " + 5;
			lapI2.SetActive (true);
				currentLevel = 0;
				backI.sprite = backIList [0];
				raceT.text = "Medival Challenge I";
			startRB.SetActive (true);
			lockP.SetActive (false);

		}
		}

	public void UnlockNextLB(){
		money = PlayerPrefs.GetInt ("Money", 500);
		if (money >= 8000) {
			PlayAudio (clp[2]);
			if (currentLevel == 1) {
				money -= 8000;
				moneyT.text = moneyT1.text = money.ToString ();

				PlayerPrefs.SetInt ("Money", money);
				lockP.SetActive (false);
				PlayerPrefs.SetInt ("UnlockedLevel_1", 1);
				startRB.SetActive (true);
			}else if (currentLevel == 2) {
					money -= 8000;
					moneyT.text = moneyT1.text = money.ToString ();

					PlayerPrefs.SetInt ("Money", money);
					lockP.SetActive (false);
					PlayerPrefs.SetInt ("UnlockedLevel_2", 1);
					startRB.SetActive (true);
				}
		} else {
			PlayAudio (clp[3]);
			mainC.GetComponent<Animator> ().SetBool ("Hide", true);
			shopC.GetComponent<Animator> ().SetBool ("Hide", false);
		}
	}

}
