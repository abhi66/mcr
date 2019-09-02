using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour {
	public bool game_paused;
	public GameObject pauseC;
	public Animator resetB;
	public float carAcc = 1f;

	CarController plyr;

	bool buttonInput = false;
	bool breakInput = false;
	float v ;
	float h;
	bool resetNow = true;
	bool moveNow = false;
	bool resetCheck = false;
	float carVel;
	bool takingBreak = false;

	
	void Start(){
		
		StartCoroutine (MoveCarNow());
		StartCoroutine (CheckResetNow());
		plyr = Camera.main.GetComponent<CameraController> ().car.GetComponent<CarController> ();
		game_paused = false;
	}

	public void Pause () {
		game_paused = true;
		pauseC.SetActive ( true);

	}	public void Resume () {
		game_paused = false;
		pauseC.SetActive ( false);
	}
	public void ExitG(){
		Application.Quit ();
	}


	public void MainMenuB () {
	game_paused = false;
	
		SceneManager.LoadScene (1);
}
	public void RestrtB () {
		game_paused = false;

		//SceneManager.LoadScene (3);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	void Update(){
		carVel = plyr.GetComponent<Rigidbody> ().velocity.magnitude;

	
		if (moveNow) {
			if(!buttonInput){
				h = Input.GetAxis("Horizontal");
				v = Input.GetAxis("Vertical");
			}	if (takingBreak && carVel > 5) {
				//		buttonInput = true;
				v = -1.0f;
				Debug.Log ("taking break..");
			} else {
				//	buttonInput = false;
				v = Input.GetAxis("Vertical");

			}

			plyr.Move (h, 1 * carAcc, v, 0f);
			if (carVel < 2 && resetNow && resetCheck) {
				ResetCarB ();
				resetNow = false;
			}
		}

		if (game_paused) {

			Time.timeScale = Mathf.Lerp (1, 0, 2f);

		} else {
			Time.timeScale = Mathf.Lerp (0, 1, 2f);

		}
	}

	public void RHandlePointerDown(){
		buttonInput = true;
		h = 1.0f;


	}	public void RHandlePointerUp(){
		buttonInput = false;
		h = 0f;


	}
	public void LHandlePointerDown(){
		buttonInput = true;
		h = -1.0f;


	}	public void LHandlePointerUp(){
		buttonInput = false;
		h = 0f;


	}	public void DeAcelPointerDown(){
		takingBreak = true;

	}	public void DeAcelPointerUp(){
		
		takingBreak = false;
	}

	public void ResetCarB(){

		StartCoroutine (ReserCarNow());

	}

	public void ShowResetBB(){
		
		resetB.SetBool ("Show",!resetB.GetBool("Show"));

	}

	IEnumerator ReserCarNow(){
		
		yield return new WaitForSeconds (1);

		plyr.transform.position = new Vector3 (plyr.GetComponent<CarPoint> ().checkPoint[plyr.GetComponent<CarPoint> ().lastCheckPoint].position.x, 2.5f, plyr.GetComponent<CarPoint> ().checkPoint[plyr.GetComponent<CarPoint> ().lastCheckPoint].position.z);

		plyr.transform.localRotation = plyr.GetComponent<CarPoint> ().checkPoint[plyr.GetComponent<CarPoint> ().lastCheckPoint].localRotation;
	//	resetB.SetBool ("Show",false);
		yield return new WaitForSeconds (2);
		resetNow = true;

	}

	IEnumerator MoveCarNow(){
		yield return new WaitForSeconds (4);
//		Debug.Log ("Moving now");
		moveNow = true;
}
	IEnumerator CheckResetNow(){
		yield return new WaitForSeconds (14);
		resetCheck = true;
	}
}
