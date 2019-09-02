using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
public class GMManager : MonoBehaviour {
	
	public Text carPosT;
	public Text currentLapT;
	public Text plyrTmT;
	public Text finalTm;
	public Text totalMoneyT;
	public Text earnedMoneyT;
	public Text plyrPosT;
	public GameObject freeCB;
	public GameObject freeCBT;
	public GameObject[] finalStars;

	public int earnedStars;
	int totalMoney;


	public int playerCarPos;


	public CarPoint[] allCar;

	public int totalLaps = 1;
	public Animator startC;
	public Animator finishC;


	int disableTm = 10;
	CarPoint plyr;
	int carSelected ;
	int carTexture;
	int tmCount;
	float sec;
	float minuts;

	public Sprite[] signalI;
	public GameObject SpeedInfoT;
	public Image signalObj;


	public bool moveNow;

	float time;
	public AudioSource aud_s;
	public AudioClip[] a_c; 
	bool gameFinished ;

	void PlayAudio(AudioClip clp){
		aud_s.clip = clp;
		aud_s.Play ();
	}

	void Awake(){
		
			Application.targetFrameRate = 24;

	}

	void FixedUpdate(){
		if(gameFinished){
			plyr.transform.GetChild(1).GetChild(0). Rotate(Vector3.right * Time.deltaTime * 850);
			plyr.transform.GetChild(1).GetChild(1). Rotate(Vector3.right * Time.deltaTime * 850);
			plyr. transform.GetChild(1).GetChild(2). Rotate(Vector3.right * Time.deltaTime * 850);
			plyr.transform.GetChild(1).GetChild(3). Rotate(Vector3.right * Time.deltaTime * 850);

		}
	}

	void Start () {
		
		gameFinished = false;
		startC.SetBool ("Hide",true);
		moveNow = false;
	
		StartCoroutine (Signal ());

		StartCoroutine (ManualUpdate());
		plyr = Camera.main.GetComponent<CameraController> ().car.GetComponent<CarPoint> ();
		plyr.GetComponent<PathFollow> ().enabled = false;
		plyr.GetComponent<MoveToObject> ().enabled = false;

		totalMoney = PlayerPrefs.GetInt("Money",500); 

	}


	IEnumerator  ManualUpdate() {
		yield return new WaitForSeconds (2f);
		carPosT.text = plyr.GetCarPosition (allCar).ToString();
		int tmp = plyr.currentLap ;
		if (tmp < 1) {
			currentLapT.text = 1 + " / " + totalLaps;
		} else {
			currentLapT.text = tmp + " / " + totalLaps;
		}

		StartCoroutine (ManualUpdate());

	}
	void Update(){
		if (moveNow) {
			time += Time.deltaTime;
			minuts = Mathf.FloorToInt (time / 60);
			sec = Mathf.FloorToInt (time - minuts * 60);
		
			plyrTmT.text = minuts + " : " + sec;		
		}

		if (!gameFinished) {
			
			if (plyr.GetComponent<CarPoint> ().currentLap > 1 && totalLaps == 1) {
				gameFinished = true;
				HandleUI ();
				Debug.Log ("Game Over by lap");
				if (SceneManager.GetActiveScene ().buildIndex == 4) {
					Camera.main.GetComponent<CameraController> ().enabled = false;
				}
				else if(SceneManager.GetActiveScene ().buildIndex != 4){
					plyr.GetComponent<PathFollow> ().enabled = true;
					plyr.GetComponent<MoveToObject> ().enabled = true;
					gameFinished = true;
				}
			}else if (plyr.GetComponent<CarPoint> ().currentLap > 2 && totalLaps == 2) {
				gameFinished = true;
				HandleUI ();
				Debug.Log ("Game Over by lap");
				if (SceneManager.GetActiveScene ().buildIndex == 4) {
					Camera.main.GetComponent<CameraController> ().enabled = false;
				}
			
			}
		}


	}
	void OnTriggerEnter(Collider other){

		if (other.CompareTag ("Player") && other.GetComponent<CarPoint>().currentLap >= totalLaps) {

			tmCount++;
			HandleUI ();
		
			if (SceneManager.GetActiveScene ().buildIndex == 4) {
				Camera.main.GetComponent<CameraController> ().enabled = false;
			}
			else if(SceneManager.GetActiveScene ().buildIndex != 4){
				plyr.GetComponent<PathFollow> ().enabled = true;
				plyr.GetComponent<MoveToObject> ().enabled = true;
			}
			Debug.Log ("Game Over");
			gameFinished = true;

		} else if (other.CompareTag ("EnemyCar") && other.GetComponent<CarPoint>().currentLap >= totalLaps) {
			StartCoroutine(StopFinishedCar(other.gameObject , disableTm));
			disableTm--;

			tmCount++;

		}
	}
	IEnumerator StopFinishedCar(GameObject finishedCar , int tm){
		yield return new 	WaitForSeconds (tm);
		finishedCar.GetComponent<MoveToObject> ().speed = 5;
	}

	void HandleUI(){
		PlayAudio (a_c[0]);
		playerCarPos = plyr.GetCarPosition (allCar);

		if (playerCarPos >= 6) {
			earnedStars = 0;
		} else if (playerCarPos == 1) {
			earnedStars = 5;
			freeCB.SetActive (false);
			freeCBT.SetActive (false);
		}else if (playerCarPos == 2) {
			earnedStars = 4;
			freeCB.SetActive (false);
			freeCBT.SetActive (false);
		}else if (playerCarPos == 3) {
			earnedStars = 3;
		}else if (playerCarPos == 4) {
			earnedStars = 2;
		}else if (playerCarPos == 5) {
			earnedStars = 1;
		}



		HandleStarsFG ();

		startC.SetBool ("Hide",true);
		finishC.SetBool ("Hide",false);
		finalTm.text = minuts +" : " + sec ;
		if (playerCarPos <= 4) {
			int tmp = totalLaps * earnedStars * 500 + totalMoney;
			PlayerPrefs.SetInt ("Money", tmp);
			totalMoneyT.text = tmp.ToString ();
			int tmp2 = earnedStars * 500;
			earnedMoneyT.text = tmp2.ToString ();
		} else {
			int tmp =  totalMoney;
			PlayerPrefs.SetInt ("Money", tmp);
			totalMoneyT.text = tmp.ToString ();
			int tmp2 = 0;
			earnedMoneyT.text = tmp2.ToString ();
		}
		if(playerCarPos == 1)
		plyrPosT.text = "1st";
		if(playerCarPos == 2)
			plyrPosT.text = "2nd";
		if(playerCarPos == 3)
			plyrPosT.text = "3rd";
		if(playerCarPos == 4)
			plyrPosT.text = "4th";
		if (playerCarPos >= 5) {
			plyrPosT.text = "LAST";

		}
		
		

	}
	public void RateB(){
		Application.OpenURL ("https://play.google.com/store/apps/details?id=in.blogspot.abhi66.mcr");

	}

	void HandleLapeSave(){
		
		if(SceneManager.GetActiveScene ().buildIndex == 3){
			PlayerPrefs.SetInt ("L_1_S", earnedStars );
		}else if(SceneManager.GetActiveScene ().buildIndex == 4){
			PlayerPrefs.SetInt ("L_2_S", earnedStars );
		}else if(SceneManager.GetActiveScene ().buildIndex == 5){
			PlayerPrefs.SetInt ("L_3_S", earnedStars );
		}

	}
	void HandleStarsFG(){

		HandleLapeSave ();
		if (earnedStars == 1) {
			finalStars [0].SetActive (true);
			finalStars [1].SetActive (false);
			finalStars [2].SetActive (false);
			finalStars [3].SetActive (false);
			finalStars [4].SetActive (false);
		} else if (earnedStars == 2) {
			finalStars [0].SetActive (true);
			finalStars [1].SetActive (true);
			finalStars [2].SetActive (false);
			finalStars [3].SetActive (false);
			finalStars [4].SetActive (false);
		} else if (earnedStars == 3) {
			finalStars [0].SetActive (true);
			finalStars [1].SetActive (true);
			finalStars [2].SetActive (true);
			finalStars [3].SetActive (false);
			finalStars [4].SetActive (false);
		} else if (earnedStars == 4) {
			finalStars [0].SetActive (true);
			finalStars [1].SetActive (true);
			finalStars [2].SetActive (true);
			finalStars [3].SetActive (true);
			finalStars [4].SetActive (false);
		} else if (earnedStars == 5) {
			finalStars [0].SetActive (true);
			finalStars [1].SetActive (true);
			finalStars [2].SetActive (true);
			finalStars [3].SetActive (true);
			finalStars [4].SetActive (true);
		} else {
			finalStars [0].SetActive (false);
			finalStars [1].SetActive (false);
			finalStars [2].SetActive (false);
			finalStars [3].SetActive (false);
			finalStars [4].SetActive (false);
		}
	}


	IEnumerator Signal(){
		
		yield return new WaitForSeconds (.5f);
		signalObj.sprite = signalI [0];
		PlayAudio (a_c [3]);
		yield return new WaitForSeconds (1f);
		signalObj.sprite = signalI [1];
		PlayAudio (a_c [2]);
		yield return new WaitForSeconds (1f);
		signalObj.sprite = signalI [2];
		PlayAudio (a_c [1]);
		yield return new WaitForSeconds (1f);
		signalObj.sprite = signalI [3];
		startC.SetBool ("Hide",false);
		moveNow = true;
		PlayAudio (a_c [4]);
		SpeedInfoT.SetActive (false);
		yield return new WaitForSeconds (.5f);
		signalObj.transform.parent.GetComponent<Animator> ().SetBool ("Hide",true);


	}
	public void ShowRewardedAd(){
		if (Advertisement.IsReady ()) {
			Advertisement.Show ("rewardedVideo", new ShowOptions (){ resultCallback = HandleAdResult });
		}
	}
	void HandleAdResult (ShowResult result){
		switch(result){
		case ShowResult.Finished:
			HandleStarsFG ();
			PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money",500));
			int tmp = earnedStars * 500 + totalMoney;
			PlayerPrefs.SetInt ("Money", tmp);
			totalMoneyT.text = tmp.ToString();
			int tmp2 = earnedStars * 500 ;
			earnedMoneyT.text = tmp2.ToString() ;
			break;
		case ShowResult.Skipped:


			break;

		case ShowResult.Failed:
			
			break;
		}

}

	public void FreeCoinStar(){

		if (earnedStars < 4) {
			earnedStars++;
			Debug.Log ("show ad");
			ShowRewardedAd ();
		}if (earnedStars >= 4) {
			
			freeCB.SetActive (false);
			freeCBT.SetActive (false);
		}

	}

}
