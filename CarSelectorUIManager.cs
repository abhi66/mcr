using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelectorUIManager : MonoBehaviour {

	public GameObject car1;
	public GameObject car2;
	public GameObject car3;
	public GameObject car4;


	public GameObject carC1;
	public GameObject carC2;
	public GameObject carC3;
	public GameObject carC4;

	public Text carNmT;
	public Text speedT; // 800 3500
	public Text accT; // 100 400
	public Text NOST; //2000 - 10000
	public Text handleT;  // 15-60
	public Text carPriceT;


	int speedV; // 800 3500
	int accV; // 100 400
	int NOSV; //2000 - 10000
	int handleV;  // 15-60
	int purched;


	public Text totalMoneyT1;
	public Text totalMoneyT2;
	public Text totalMoneyT3;
	 int totalMoney;

	public int carSelected ;

	public Texture[] allTexture;
	public GameObject[] locks;
	public GameObject lockB;
	public GameObject loadingC;

	public Animator mainC;
	public AudioSource aud_S;
	public AudioClip[] clip;
	public GameObject[] carMesh_01;
	public GameObject[] carMesh_02;
	public GameObject[] carMesh_03;
	public GameObject[] carMesh_04;
	public GameObject damagedImageObj;
	public Image damagedImageFill;
	public Animator repairAnim;
	int damagedC_01;
	int damagedC_02;
	int damagedC_03;
	int damagedC_04;
	void PlayAudio(AudioClip clp){
		aud_S.clip = clp;
		aud_S.Play ();

	}
	void DamagedCarsBody(int dam, GameObject[] obj){
		for (int i = 0; i < obj.Length; i++) {
			if (i == dam) {
				
				obj [dam].SetActive (true);

			}
					else{
				obj [i].SetActive (false);

					}
			if (dam == 0) {
				damagedImageObj.SetActive (false);
			} else {
				damagedImageObj.SetActive (true);
				float tmp =  damagedC_01;
				damagedImageFill.fillAmount = tmp/10 ;

			}
		}
	}
	public void RepairButton(){
		repairAnim.SetBool ("Open",!repairAnim.GetBool ("Open"));
	}

	public void RepairCar(){
		totalMoney = PlayerPrefs.GetInt ("Money", 500);

		if (totalMoney >= 50) {
			totalMoney -= 50;
			totalMoneyT1.text = totalMoney.ToString ();
			totalMoneyT2.text = totalMoney.ToString ();
			totalMoneyT3.text = totalMoney.ToString ();
			PlayerPrefs.SetInt ("Money", totalMoney );
			if (carSelected == 1) {
				StartCoroutine (Repair (carMesh_01, damagedC_01));
				damagedImageObj.SetActive (false);
				PlayerPrefs.SetInt ("DamagedC_01", 0);
				PlayAudio (clip[1]);
			} else if (carSelected == 2) {
				StartCoroutine (Repair (carMesh_02, damagedC_02));
				damagedImageObj.SetActive (false);
				PlayerPrefs.SetInt ("DamagedC_02", 0);
				PlayAudio (clip[1]);
			} else if (carSelected == 3) {
				StartCoroutine (Repair (carMesh_03, damagedC_03));
				damagedImageObj.SetActive (false);
				PlayerPrefs.SetInt ("DamagedC_03", 0);
				PlayAudio (clip[1]);
			} else if (carSelected == 4) {
				StartCoroutine (Repair (carMesh_04, damagedC_04));
				damagedImageObj.SetActive (false);
				PlayerPrefs.SetInt ("DamagedC_04", 0);
				PlayAudio (clip[1]);
			}
		} else {
			GetComponent<CarSelectionUI>().ShopB();
			PlayAudio (clip[3]);
		}
	}

	 IEnumerator Repair(GameObject[] car, int dmag){
		for(int i = dmag; i > 0; i--){
			
			car[i].SetActive(false);
			car[i-1].SetActive(true);
			yield return new WaitForSeconds(.2f);

		}
	}
	void Start(){
		damagedC_01 = PlayerPrefs.GetInt ("DamagedC_01", 0);
		damagedC_02 = PlayerPrefs.GetInt ("DamagedC_02", 0);
		damagedC_03 = PlayerPrefs.GetInt ("DamagedC_03", 0);
		damagedC_04 = PlayerPrefs.GetInt ("DamagedC_04", 0);

		DamagedCarsBody (damagedC_01, carMesh_01);
		//PlayerPrefs.DeleteAll ();
		carNmT.text = "HDR111";
		carPriceT.gameObject.SetActive (false);
		lockB.SetActive (false);
		HandleLocks ();
		GameObject.Find ("DataHandler").GetComponent<DataHandler>().carSel = 1;
		GameObject.Find ("DataHandler").GetComponent<DataHandler>().car1_texSelected = 1;
		carSelected = 1;

		speedV = PlayerPrefs.GetInt ("MDR111_S", 60);
		accV = PlayerPrefs.GetInt ("MDR111_A", 1);
		NOSV = PlayerPrefs.GetInt ("MDR111_N", 200);
		handleV = PlayerPrefs.GetInt ("MDR111_H", 20);
		totalMoney = PlayerPrefs.GetInt ("Money", 500);

		speedT.text = (speedV).ToString ();
		accT.text = (accV * 10).ToString ();
		NOST.text = (NOSV).ToString ();
		handleT.text = handleV.ToString ();

	}
	void HandleCarLocks(){
		if (carSelected == 1) {
			lockB.SetActive (false);
		}else if (carSelected == 2) {
			if(PlayerPrefs.GetInt ("MDR112_P", 0) == 0){
				lockB.SetActive (true);
			}

		}
	}

	public void BuyCarB(){
		
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
	//	Debug.Log ("Calling out");
		if (carSelected == 2) {
			purched = PlayerPrefs.GetInt ("MDR112_P", 0);
			if (purched == 0 && totalMoney >= 10000 ) {
				PlayAudio (clip[0]);
				purched = 1;
				PlayerPrefs.SetInt ("MDR112_P", 1);
				lockB.SetActive (false);
				carPriceT.gameObject.SetActive (false);

				GameObject.Find ("DataHandler").GetComponent<DataHandler> ().carSel = 2;
				totalMoney -= 10000;
				PlayerPrefs.SetInt ("Money", totalMoney);
				totalMoneyT1.text = totalMoney.ToString ();
				totalMoneyT2.text = totalMoney.ToString ();
				totalMoneyT3.text = totalMoney.ToString ();
				Debug.Log ("Calling buy");
				mainC.SetBool("Purched",true);
			}else if(purched == 0){
				GetComponent<CarSelectionUI>().ShopB();
				PlayAudio (clip[3]);
			}
		}  else if (carSelected == 3) {
			purched = PlayerPrefs.GetInt ("MDR113_P", 0);
			if (purched == 0 && totalMoney >= 15000 ) {
				PlayAudio (clip[0]);
				purched = 1;
				PlayerPrefs.SetInt ("MDR113_P", 1);
				lockB.SetActive (false);
				carPriceT.gameObject.SetActive (false);
			//	carSelected = 3;
				GameObject.Find ("DataHandler").GetComponent<DataHandler> ().carSel = 3;
				totalMoney -= 15000;
				PlayerPrefs.SetInt ("Money", totalMoney);
				totalMoneyT1.text = totalMoney.ToString ();
				totalMoneyT2.text = totalMoney.ToString ();
				totalMoneyT3.text = totalMoney.ToString ();
				mainC.SetBool("Purched",true);
			}else if(purched == 0){
				GetComponent<CarSelectionUI>().ShopB();
				PlayAudio (clip[3]);
			}
		}else if (carSelected == 4) {
			purched = PlayerPrefs.GetInt ("MDR114_P", 0);
			if (purched == 0 && totalMoney >= 18000 ) {
				PlayAudio (clip[0]);
				purched = 1;
				PlayerPrefs.SetInt ("MDR114_P", 1);
				lockB.SetActive (false);
				carPriceT.gameObject.SetActive (false);
			//	carSelected = 4;
				GameObject.Find ("DataHandler").GetComponent<DataHandler> ().carSel = 4;
				totalMoney -= 18000;
				PlayerPrefs.SetInt ("Money", totalMoney);
				totalMoneyT1.text = totalMoney.ToString ();
				totalMoneyT2.text = totalMoney.ToString ();
				totalMoneyT3.text = totalMoney.ToString ();
				mainC.SetBool("Purched",true);
			}else if(purched == 0){
				GetComponent<CarSelectionUI>().ShopB();
				PlayAudio (clip[3]);
			}
		}
			
			
		}


	public void car01 () {
		
		PlayAudio (clip[0]);
		lockB.SetActive (false);
		carPriceT.gameObject.SetActive (false);
		carSelected = 1;
		GameObject.Find ("DataHandler").GetComponent<DataHandler>().carSel = 1;
		car1.SetActive (true);
		car2.SetActive (false);
		car3.SetActive (false);
		car4.SetActive (false);


		carC1.SetActive (true);
		carC2.SetActive (false);
		carC3.SetActive (false);
		carC4.SetActive (false);

		carNmT.text = "MDR111";
		speedT.text = PlayerPrefs.GetInt ("MDR111_S", 60).ToString();
		accT.text = (PlayerPrefs.GetInt ("MDR111_A", 1) * 10).ToString();
				NOST.text = (PlayerPrefs.GetInt ("MDR111_N", 200)).ToString();
		handleT.text = PlayerPrefs.GetInt ("MDR111_H", 20).ToString();
		mainC.SetBool("Purched",true);
		damagedC_01 = PlayerPrefs.GetInt ("DamagedC_01", 0);
		DamagedCarsBody (damagedC_01, carMesh_01);

	}
	public void car02 () {
		
			SelectedCar2 ();
		PlayAudio (clip[0]);
		}
	void SelectedCar2(){
		if (PlayerPrefs.GetInt ("MDR112_P", 0) == 1) {
			lockB.SetActive (false);
			carPriceT.gameObject.SetActive (false);
			carSelected = 2;
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().carSel = 2;
			mainC.SetBool("Purched",true);
			damagedC_01 = PlayerPrefs.GetInt ("DamagedC_02", 0);
			DamagedCarsBody (damagedC_01, carMesh_02);
		} else {
			lockB.SetActive (true);
			carPriceT.gameObject.SetActive (true);
			carPriceT.text = "10,000";
			mainC.SetBool("Purched",false);

		}
		carSelected = 2;


		car1.SetActive (false);
		car2.SetActive (true);
		car3.SetActive (false);
		car4.SetActive (false);


		carC1.SetActive (false);
		carC2.SetActive (true);
		carC3.SetActive (false);
		carC4.SetActive (false);

		carNmT.text = "MDR112";
		speedT.text = (PlayerPrefs.GetInt ("MDR112_S", 70) ).ToString ();
		accT.text = (PlayerPrefs.GetInt ("MDR112_A", 1) * 10).ToString ();
				NOST.text =( PlayerPrefs.GetInt ("MDR112_N", 600)).ToString ();
		handleT.text = PlayerPrefs.GetInt ("MDR112_H", 20).ToString ();
	}
	public void car03 () {
		
		PlayAudio (clip[0]);
			SelectedCar3 ();


	}



	void SelectedCar3(){
		if (PlayerPrefs.GetInt ("MDR113_P", 0) == 1) {
			lockB.SetActive (false);
			carPriceT.gameObject.SetActive (false);
			carSelected = 3;
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().carSel = 3;
			mainC.SetBool("Purched",true);
		
			damagedC_01 = PlayerPrefs.GetInt ("DamagedC_03", 0);
			DamagedCarsBody (damagedC_01, carMesh_03);
		} else {
			lockB.SetActive (true);
			carPriceT.gameObject.SetActive (true);
			carPriceT.text = "15,000";
			mainC.SetBool("Purched",false);
		}

		carSelected = 3;
		car1.SetActive (false);
		car2.SetActive (false);
		car3.SetActive (true);
		car4.SetActive (false);


		carC1.SetActive (false);
		carC2.SetActive (false);
		carC3.SetActive (true);
		carC4.SetActive (false);
		carNmT.text = "MDR113";
		speedT.text = (PlayerPrefs.GetInt ("MDR113_S", 90) / 2).ToString();
		accT.text = (PlayerPrefs.GetInt ("MDR113_A", 1) * 10) .ToString();
		NOST.text = (PlayerPrefs.GetInt ("MDR113_N", 1400)).ToString();
		handleT.text = PlayerPrefs.GetInt ("MDR113_H", 20).ToString();
	}

	public void car04 () {
		PlayAudio (clip[0]);

			SelectedCar4 ();


	}
		

	void SelectedCar4(){

		if (PlayerPrefs.GetInt ("MDR114_P", 0) == 1) {
			lockB.SetActive (false);
			carPriceT.gameObject.SetActive (false);
			carSelected = 4;
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().carSel = 4;
			mainC.SetBool("Purched",true);
		
			damagedC_01 = PlayerPrefs.GetInt ("DamagedC_04", 0);
			DamagedCarsBody (damagedC_01, carMesh_04);
		} else {
			lockB.SetActive (true);
			carPriceT.gameObject.SetActive (true);
			carPriceT.text = "18,000";
			mainC.SetBool("Purched",false);
		}
		carSelected = 4;
		car1.SetActive (false);
		car2.SetActive (false);
		car3.SetActive (false);
		car4.SetActive (true);


		carC1.SetActive (false);
		carC2.SetActive (false);
		carC3.SetActive (false);
		carC4.SetActive (true);
		carNmT.text = "MDR114";
		speedT.text = (PlayerPrefs.GetInt ("MDR114_S", 100)).ToString();
		accT.text = (PlayerPrefs.GetInt ("MDR114_A", 1) * 10).ToString();
				NOST.text =( PlayerPrefs.GetInt ("MDR114_N", 1800) ).ToString();
		handleT.text = PlayerPrefs.GetInt ("MDR114_H", 20).ToString();
	}


	public void car01Tex01 () {
		PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 1;
	//	carMesh_01[damagedC_01].GetComponent<Renderer>().material.mainTexture = allTexture [0];
		foreach (GameObject car in carMesh_01) {
			car.GetComponent<Renderer>().material.mainTexture = allTexture [0];
		}
		//	car1.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [0];
		
	}
	public void car01Tex02 () {
		
		purched = PlayerPrefs.GetInt ("MDR111_TP2", 0);
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (purched == 0 && totalMoney >= 250) {
			PlayAudio (clip[0]);
			locks [0].SetActive (false);
			purched = 1;
			PlayerPrefs.SetInt ("MDR111_TP2", 1);
			totalMoney -= 250;
			PlayerPrefs.SetInt ("Money", totalMoney);
			totalMoneyT1.text = totalMoney.ToString ();
			totalMoneyT2.text = totalMoney.ToString ();
			totalMoneyT3.text = totalMoney.ToString ();

			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 2;
		//	carMesh_01[damagedC_01].GetComponent<Renderer>().material.mainTexture = allTexture [1];
		//	car1.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [1];
			foreach (GameObject car in carMesh_01) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [1];
			}

		} else if (purched == 1) {
			PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 2;
		//	car1.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [1];
		//	carMesh_01[damagedC_01].GetComponent<Renderer>().material.mainTexture = allTexture [1];
			foreach (GameObject car in carMesh_01) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [1];
			}
		} else {
			GetComponent<CarSelectionUI>().ShopB();
			PlayAudio (clip[3]);
		}


	}	public void car01Tex03 () {
		
		purched = PlayerPrefs.GetInt ("MDR111_TP3", 0);
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (purched == 0 && totalMoney >= 250) {
			PlayAudio (clip[0]);
			locks [1].SetActive (false);
			purched = 1;
			PlayerPrefs.SetInt ("MDR111_TP3", 1);
			totalMoney -= 250;
			PlayerPrefs.SetInt ("Money", totalMoney);
			totalMoneyT1.text = totalMoney.ToString ();
			totalMoneyT2.text = totalMoney.ToString ();
			totalMoneyT3.text = totalMoney.ToString ();

			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 3;
		//	carMesh_01[damagedC_01].GetComponent<Renderer>().material.mainTexture = allTexture [2];
			foreach (GameObject car in carMesh_01) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [2];
			}
		//	car1.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [2];

		} else if (purched == 1) {
			PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 3;
		//	car1.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [2];
		//	carMesh_01[damagedC_01].GetComponent<Renderer>().material.mainTexture = allTexture [2];
			foreach (GameObject car in carMesh_01) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [2];
			}
		} else {
			GetComponent<CarSelectionUI>().ShopB();
			PlayAudio (clip[3]);
		}
	
	}	public void car01Tex04 () {
		
		purched = PlayerPrefs.GetInt ("MDR111_TP4", 0);
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (purched == 0 && totalMoney >= 250) {
			PlayAudio (clip[0]);
			locks [2].SetActive (false);
			purched = 1;
			PlayerPrefs.SetInt ("MDR111_TP4", 1);
			totalMoney -= 250;
			PlayerPrefs.SetInt ("Money", totalMoney);
			totalMoneyT1.text = totalMoney.ToString ();
			totalMoneyT2.text = totalMoney.ToString ();
			totalMoneyT3.text = totalMoney.ToString ();

			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 4;
		//	car1.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [3];
		//	carMesh_01[damagedC_01].GetComponent<Renderer>().material.mainTexture = allTexture [3];
			foreach (GameObject car in carMesh_01) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [3];
			}

		} else if (purched == 1) {
			PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 4;
		//	car1.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [3];
		//	carMesh_01[damagedC_01].GetComponent<Renderer>().material.mainTexture = allTexture [3];
			foreach (GameObject car in carMesh_01) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [3];
			}
		} else {
			PlayAudio (clip[3]);
			GetComponent<CarSelectionUI>().ShopB();
		//	Debug.Log("Playing sound");
		}

	}

	public void car02Tex01 () {
		PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 1;
	//	carMesh_02[damagedC_02].GetComponent<Renderer>().material.mainTexture = allTexture [0];
		foreach (GameObject car in carMesh_02) {
			car.GetComponent<Renderer>().material.mainTexture = allTexture [0];
		}
		//	car2.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [0];

	

	}
	public void car02Tex02 () {
		
		purched = PlayerPrefs.GetInt ("MDR112_TP2", 0);
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (purched == 0 && totalMoney >= 250) {
			PlayAudio (clip[0]);
			locks [3].SetActive (false);
			purched = 1;
			PlayerPrefs.SetInt ("MDR112_TP2", 1);
			totalMoney -= 250;
			PlayerPrefs.SetInt ("Money", totalMoney);
			totalMoneyT1.text = totalMoney.ToString ();
			totalMoneyT2.text = totalMoney.ToString ();
			totalMoneyT3.text = totalMoney.ToString ();

			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 2;
		//	carMesh_02[damagedC_02].GetComponent<Renderer>().material.mainTexture = allTexture [1];
			foreach (GameObject car in carMesh_02) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [1];
			}
		//	car2.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [1];

		} else if (purched == 1) {
			PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 2;
		//	carMesh_02[damagedC_02].GetComponent<Renderer>().material.mainTexture = allTexture [1];
			foreach (GameObject car in carMesh_02) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [1];
			}
		//	car2.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [1];
		} else {
			GetComponent<CarSelectionUI>().ShopB();
			PlayAudio (clip[3]);
		}


	}	public void car02Tex03 () {
		
		purched = PlayerPrefs.GetInt ("MDR112_TP3", 0);
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (purched == 0 && totalMoney >= 250) {
			PlayAudio (clip[0]);
			locks [4].SetActive (false);
			purched = 1;
			PlayerPrefs.SetInt ("MDR112_TP3", 1);
			totalMoney -= 250;
			PlayerPrefs.SetInt ("Money", totalMoney);
			totalMoneyT1.text = totalMoney.ToString ();
			totalMoneyT2.text = totalMoney.ToString ();
			totalMoneyT3.text = totalMoney.ToString ();

			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 3;
			//carMesh_02[damagedC_02].GetComponent<Renderer>().material.mainTexture = allTexture [2];
			foreach (GameObject car in carMesh_02) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [2];
			}
		//	car2.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [2];

		} else if (purched == 1) {
			PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 3;
			//carMesh_02[damagedC_02].GetComponent<Renderer>().material.mainTexture = allTexture [2];
			foreach (GameObject car in carMesh_02) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [2];
			}
		//	car2.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [2];
		} else {
			PlayAudio (clip[3]);
			GetComponent<CarSelectionUI>().ShopB();
		}


	}	public void car02Tex04 () {
		
		purched = PlayerPrefs.GetInt ("MDR112_TP4", 0);
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (purched == 0 && totalMoney >= 250) {
			PlayAudio (clip[0]);
			locks [5].SetActive (false);
			purched = 1;
			PlayerPrefs.SetInt ("MDR112_TP4", 1);
			totalMoney -= 250;
			PlayerPrefs.SetInt ("Money", totalMoney);
			totalMoneyT1.text = totalMoney.ToString ();
			totalMoneyT2.text = totalMoney.ToString ();
			totalMoneyT3.text = totalMoney.ToString ();

			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 4;
		//	carMesh_02[damagedC_02].GetComponent<Renderer>().material.mainTexture = allTexture [3];
			foreach (GameObject car in carMesh_02) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [3];
			}
		//	car2.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [3];

		} else if (purched == 1) {
			PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 4;
		//	carMesh_02[damagedC_02].GetComponent<Renderer>().material.mainTexture = allTexture [3];
			foreach (GameObject car in carMesh_02) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [3];
			}
		//	car2.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [3];
		} else {
			GetComponent<CarSelectionUI>().ShopB();
			PlayAudio (clip[3]);
		}

	}
	public void car03Tex01 () {
		PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 1;
	//	carMesh_03[damagedC_03].GetComponent<Renderer>().material.mainTexture = allTexture [0];
		foreach (GameObject car in carMesh_03) {
			car.GetComponent<Renderer>().material.mainTexture = allTexture [0];
		}
		//	car3.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [0];
	
	
	}
	public void car03Tex02 () {
		
		purched = PlayerPrefs.GetInt ("MDR113_TP2", 0);
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (purched == 0 && totalMoney >= 250) {
			PlayAudio (clip[0]);
			locks [6].SetActive (false);
			purched = 1;
			PlayerPrefs.SetInt ("MDR113_TP2", 1);
			totalMoney -= 250;
			PlayerPrefs.SetInt ("Money", totalMoney);
			totalMoneyT1.text = totalMoney.ToString ();
			totalMoneyT2.text = totalMoney.ToString ();
			totalMoneyT3.text = totalMoney.ToString ();

			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 2;
		//	carMesh_03[damagedC_03].GetComponent<Renderer>().material.mainTexture = allTexture [1];
			foreach (GameObject car in carMesh_03) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [1];
			}
		//	car3.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [1];

		} else if (purched == 1) {
			PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 2;
		//	carMesh_03[damagedC_03].GetComponent<Renderer>().material.mainTexture = allTexture [1];
			foreach (GameObject car in carMesh_03) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [1];
			}
		//	car3.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [1];
		} else {
			GetComponent<CarSelectionUI>().ShopB();
			PlayAudio (clip[3]);
		}


	}	public void car03Tex03 () {
		
		purched = PlayerPrefs.GetInt ("MDR113_TP3", 0);
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (purched == 0 && totalMoney >= 250) {
			PlayAudio (clip[0]);
			locks [7].SetActive (false);
			purched = 1;
			PlayerPrefs.SetInt ("MDR113_TP3", 1);
			totalMoney -= 250;
			PlayerPrefs.SetInt ("Money", totalMoney);
			totalMoneyT1.text = totalMoney.ToString ();
			totalMoneyT2.text = totalMoney.ToString ();
			totalMoneyT3.text = totalMoney.ToString ();

			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 3;
		//	carMesh_03[damagedC_03].GetComponent<Renderer>().material.mainTexture = allTexture [2];
			foreach (GameObject car in carMesh_03) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [2];
			}
		//	car3.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [2];

		} else if (purched == 1) {
			PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 3;
		//	carMesh_03[damagedC_03].GetComponent<Renderer>().material.mainTexture = allTexture [2];
			foreach (GameObject car in carMesh_03) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [2];
			}
		//	car3.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [2];
		} else {
			PlayAudio (clip[3]);
			GetComponent<CarSelectionUI>().ShopB();
		}

	
	}	public void car03Tex04 () {
		
		purched = PlayerPrefs.GetInt ("MDR113_TP4", 0);
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (purched == 0 && totalMoney >= 250) {
			PlayAudio (clip[0]);
			locks [8].SetActive (false);
			purched = 1;
			PlayerPrefs.SetInt ("MDR113_TP4", 1);
			totalMoney -= 250;
			PlayerPrefs.SetInt ("Money", totalMoney);
			totalMoneyT1.text = totalMoney.ToString ();
			totalMoneyT2.text = totalMoney.ToString ();
			totalMoneyT3.text = totalMoney.ToString ();

			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 4;
		//	carMesh_03[damagedC_03].GetComponent<Renderer>().material.mainTexture = allTexture [3];
			foreach (GameObject car in carMesh_03) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [3];
			}
		//	car3.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [3];

		} else if (purched == 1) {
			PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 4;
		//	carMesh_03[damagedC_03].GetComponent<Renderer>().material.mainTexture = allTexture [3];
			foreach (GameObject car in carMesh_03) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [3];
			}
		//	car3.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [3];
		} else {
			GetComponent<CarSelectionUI>().ShopB();
			PlayAudio (clip[3]);
		}

	}

	public void car04Tex01 () {
		PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 1;
	//	carMesh_04[damagedC_04].GetComponent<Renderer>().material.mainTexture = allTexture [0];
		foreach (GameObject car in carMesh_04) {
			car.GetComponent<Renderer>().material.mainTexture = allTexture [0];
		}
			//car4.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [0];
	


	}
	public void car04Tex02 () {
		
		purched = PlayerPrefs.GetInt ("MDR114_TP2", 0);
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (purched == 0 && totalMoney >= 250) {
			PlayAudio (clip[0]);
			locks [9].SetActive (false);
			purched = 1;
			PlayerPrefs.SetInt ("MDR114_TP2", 1);
			totalMoney -= 250;
			PlayerPrefs.SetInt ("Money", totalMoney);
			totalMoneyT1.text = totalMoney.ToString ();
			totalMoneyT2.text = totalMoney.ToString ();
			totalMoneyT3.text = totalMoney.ToString ();

			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 2;
		//	carMesh_04[damagedC_04].GetComponent<Renderer>().material.mainTexture = allTexture [1];
			foreach (GameObject car in carMesh_04) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [1];
			}
		//	car4.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [1];

		} else if (purched == 1) {
			PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 2;
		//	carMesh_04[damagedC_04].GetComponent<Renderer>().material.mainTexture = allTexture [1];
			foreach (GameObject car in carMesh_04) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [1];
			}
		//	car4.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [1];
		} else {
			GetComponent<CarSelectionUI>().ShopB();
			PlayAudio (clip[3]);
		}


	}	public void car04Tex03 () {
		
		purched = PlayerPrefs.GetInt ("MDR114_TP3", 0);
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (purched == 0 && totalMoney >= 250) {
			PlayAudio (clip[0]);
			locks [10].SetActive (false);
			purched = 1;
			PlayerPrefs.SetInt ("MDR114_TP3", 1);
			totalMoney -= 250;
			PlayerPrefs.SetInt ("Money", totalMoney);
			totalMoneyT1.text = totalMoney.ToString ();
			totalMoneyT2.text = totalMoney.ToString ();
			totalMoneyT3.text = totalMoney.ToString ();

			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 3;
		//	carMesh_04[damagedC_04].GetComponent<Renderer>().material.mainTexture = allTexture [2];
			foreach (GameObject car in carMesh_04) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [2];
			}
		//	car4.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [1];

		} else if (purched == 1) {
			PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 3;
			//carMesh_04[damagedC_04].GetComponent<Renderer>().material.mainTexture = allTexture [2];
			foreach (GameObject car in carMesh_04) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [2];
			}
		//	car4.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [1];
		} else {
			GetComponent<CarSelectionUI>().ShopB();
			PlayAudio (clip[3]);
		}


	}	public void car04Tex04 () {
		
		purched = PlayerPrefs.GetInt ("MDR114_TP4", 0);
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (purched == 0 && totalMoney >= 250) {
			PlayAudio (clip[0]);
			locks [11].SetActive (false);
			purched = 1;
			PlayerPrefs.SetInt ("MDR114_TP4", 1);
			totalMoney -= 250;
			PlayerPrefs.SetInt ("Money", totalMoney);
			totalMoneyT1.text = totalMoney.ToString ();
			totalMoneyT2.text = totalMoney.ToString ();
			totalMoneyT3.text = totalMoney.ToString ();

			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 4;
		//	carMesh_04[damagedC_04].GetComponent<Renderer>().material.mainTexture = allTexture [3];
			foreach (GameObject car in carMesh_04) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [3];
			}
		//	car4.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [3];

		} else if (purched == 1) {
			PlayAudio (clip[0]);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().car1_texSelected = 4;
		//	carMesh_04[damagedC_04].GetComponent<Renderer>().material.mainTexture = allTexture [3];
			foreach (GameObject car in carMesh_04) {
				car.GetComponent<Renderer>().material.mainTexture = allTexture [3];
			}
		//	car4.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTexture [3];
		} else {
			PlayAudio (clip[3]);
			GetComponent<CarSelectionUI>().ShopB();
		}



	}

	public void SpeedUB(){
		
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (totalMoney >= 500) {
			
			if (carSelected == 1) {
				speedV = PlayerPrefs.GetInt ("MDR111_S", 60) ;
				if (speedV < 75) {
					PlayAudio (clip[1]);
					speedV = PlayerPrefs.GetInt ("MDR111_S", 60) + 5 ;
					PlayerPrefs.SetInt ("MDR111_S", speedV );
					speedT.text = (speedV).ToString ();
					totalMoney -= 500;
					PlayerPrefs.SetInt ("Money", totalMoney);
					totalMoneyT1.text = totalMoney.ToString ();
					totalMoneyT2.text = totalMoney.ToString ();
					totalMoneyT3.text = totalMoney.ToString ();


				}
			} else if (carSelected == 2) {
				speedV = PlayerPrefs.GetInt ("MDR112_S", 70) ;
				if (speedV < 90) {
					PlayAudio (clip[1]);
					speedV = PlayerPrefs.GetInt ("MDR112_S", 70) + 5;
					PlayerPrefs.SetInt ("MDR112_S", speedV);
					speedT.text = speedV.ToString ();
					totalMoney -= 500;
					PlayerPrefs.SetInt ("Money", totalMoney);
					totalMoneyT1.text = totalMoney.ToString ();
					totalMoneyT2.text = totalMoney.ToString ();
					totalMoneyT3.text = totalMoney.ToString ();
				}
			} else if (carSelected == 3) {
				speedV = PlayerPrefs.GetInt ("MDR113_S", 90) ;
				if (speedV < 110) {
					PlayAudio (clip[1]);
					speedV = PlayerPrefs.GetInt ("MDR113_S", 90) + 5;
					PlayerPrefs.SetInt ("MDR113_S", speedV);
					totalMoney -= 500;
					speedT.text = (speedV).ToString ();
					PlayerPrefs.SetInt ("Money", totalMoney);
					totalMoneyT1.text = totalMoney.ToString ();
					totalMoneyT2.text = totalMoney.ToString ();
					totalMoneyT3.text = totalMoney.ToString ();
				}
			} else if (carSelected == 4) {
				speedV = PlayerPrefs.GetInt ("MDR114_S", 100);
				if (speedV < 120) {
					PlayAudio (clip[1]);
					speedV = PlayerPrefs.GetInt ("MDR114_S", 100) + 5;
					PlayerPrefs.SetInt ("MDR114_S", speedV);
					speedT.text = (speedV).ToString ();
					totalMoney -= 500;
					PlayerPrefs.SetInt ("Money", totalMoney);
					totalMoneyT1.text = totalMoney.ToString ();
					totalMoneyT2.text = totalMoney.ToString ();
					totalMoneyT3.text = totalMoney.ToString ();
				}
			}

		}else{
			GetComponent<CarSelectionUI>().ShopB();
			PlayAudio (clip[3]);
		}
	}


	public void AccUB(){
		
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (totalMoney >= 500) {  //800-3500
			
			if (carSelected == 1) {
				accV = PlayerPrefs.GetInt ("MDR111_A", 1);
				if (accV < 50) {
					PlayAudio (clip[1]);
					accV = PlayerPrefs.GetInt ("MDR111_A", 1) + 10;
					PlayerPrefs.SetInt ("MDR111_A", accV);
					accT.text = (accV * 10).ToString ();
					totalMoney -= 500;
					PlayerPrefs.SetInt ("Money", totalMoney);
					totalMoneyT1.text = totalMoney.ToString ();
					totalMoneyT2.text = totalMoney.ToString ();
					totalMoneyT3.text = totalMoney.ToString ();

				} 
			} else if (carSelected == 2) {
				accV = PlayerPrefs.GetInt ("MDR112_A", 1);
				if (accV < 50) {
					PlayAudio (clip[1]);
					accV = PlayerPrefs.GetInt ("MDR112_A", 1) + 10;
					PlayerPrefs.SetInt ("MDR112_A", accV);
					accT.text = (accV * 10).ToString ();
					totalMoney -= 500;
					PlayerPrefs.SetInt ("Money", totalMoney);
					totalMoneyT1.text = totalMoney.ToString ();
					totalMoneyT2.text = totalMoney.ToString ();
					totalMoneyT3.text = totalMoney.ToString ();

				}
			}
			if (carSelected == 3) {
				accV = PlayerPrefs.GetInt ("MDR113_A", 1);
				if (accV < 50) {
					PlayAudio (clip[1]);
					accV = PlayerPrefs.GetInt ("MDR113_A", 1) + 10;
					PlayerPrefs.SetInt ("MDR113_A", accV);
					accT.text = (accV * 10).ToString ();
					totalMoney -= 500;
					PlayerPrefs.SetInt ("Money", totalMoney);
					totalMoneyT1.text = totalMoney.ToString ();
					totalMoneyT2.text = totalMoney.ToString ();
					totalMoneyT3.text = totalMoney.ToString ();

				}
			}
			if (carSelected == 4) {
				accV = PlayerPrefs.GetInt ("MDR114_A", 1);
				if (accV < 50) {
					PlayAudio (clip[1]);
					accV = PlayerPrefs.GetInt ("MDR114_A", 1) + 10;
					PlayerPrefs.SetInt ("MDR114_A", accV);
					accT.text = (accV * 10).ToString ();
					totalMoney -= 500;
					PlayerPrefs.SetInt ("Money", totalMoney);
					totalMoneyT1.text = totalMoney.ToString ();
					totalMoneyT2.text = totalMoney.ToString ();
					totalMoneyT3.text = totalMoney.ToString ();

				}
			}
		}else{
				GetComponent<CarSelectionUI>().ShopB();
			PlayAudio (clip[3]);


		}
	}

	public void NOSUB(){

		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (totalMoney >= 500) {  //2000-10000
			
			if (carSelected == 1) {
				NOSV = PlayerPrefs.GetInt ("MDR111_N", 200) ;
				if (NOSV < 400) {
					PlayAudio (clip[1]);
					NOSV = PlayerPrefs.GetInt ("MDR111_N", 200) + 50;
					PlayerPrefs.SetInt ("MDR111_N", NOSV);
							NOST.text = (NOSV).ToString ();
					totalMoney -= 500;
					PlayerPrefs.SetInt ("Money", totalMoney);
					totalMoneyT1.text = totalMoney.ToString ();
					totalMoneyT2.text = totalMoney.ToString ();
					totalMoneyT3.text = totalMoney.ToString ();

				}
			}else if (carSelected == 2 ) {
				NOSV = PlayerPrefs.GetInt ("MDR112_N", 600) ;

				if (NOSV < 1000) {
					PlayAudio (clip[1]);
				NOSV = PlayerPrefs.GetInt ("MDR112_N", 600) + 100;
				PlayerPrefs.SetInt ("MDR112_N", NOSV);
							NOST.text =( NOSV).ToString ();
				totalMoney -= 500;
				PlayerPrefs.SetInt ("Money", totalMoney);
				totalMoneyT1.text = totalMoney.ToString ();
				totalMoneyT2.text = totalMoney.ToString ();
				totalMoneyT3.text = totalMoney.ToString ();

			}} else if (carSelected == 3 ) {
				NOSV = PlayerPrefs.GetInt ("MDR113_N", 1400) ;
				if (NOSV < 2200) {
					PlayAudio (clip[1]);
				NOSV = PlayerPrefs.GetInt ("MDR113_N", 1400) + 200;
				PlayerPrefs.SetInt ("MDR113_N", NOSV);
							NOST.text = (NOSV).ToString ();
				totalMoney -= 500;
				PlayerPrefs.SetInt ("Money", totalMoney);
				totalMoneyT1.text = totalMoney.ToString ();
				totalMoneyT2.text = totalMoney.ToString ();
				totalMoneyT3.text = totalMoney.ToString ();

			}} else if (carSelected == 4 ) {
				NOSV = PlayerPrefs.GetInt ("MDR114_N", 1800) ;
				if (NOSV < 2600) {
					PlayAudio (clip[1]);
				NOSV = PlayerPrefs.GetInt ("MDR114_N", 1800) + 20;
				PlayerPrefs.SetInt ("MDR114_N", NOSV);
							NOST.text = (NOSV).ToString ();
				totalMoney -= 500;
				PlayerPrefs.SetInt ("Money", totalMoney);
				totalMoneyT1.text = totalMoney.ToString ();
				totalMoneyT2.text = totalMoney.ToString ();
				totalMoneyT3.text = totalMoney.ToString ();

				}}
		}else{
				GetComponent<CarSelectionUI>().ShopB();
			PlayAudio (clip[3]);
			 
		}


	}

	public void HandleUB(){
		
		totalMoney = PlayerPrefs.GetInt ("Money", 500);
		if (totalMoney >= 500) {  //15-60
			
			if (carSelected == 1) {
				handleV = PlayerPrefs.GetInt ("MDR111_H", 20);
				if (handleV < 35) {
					PlayAudio (clip[1]);
					handleV = PlayerPrefs.GetInt ("MDR111_H", 20) + 5;
					PlayerPrefs.SetInt ("MDR111_H", handleV);
					handleT.text = handleV.ToString ();
					totalMoney -= 500;
					PlayerPrefs.SetInt ("Money", totalMoney);
					totalMoneyT1.text = totalMoney.ToString ();
					totalMoneyT2.text = totalMoney.ToString ();
					totalMoneyT3.text = totalMoney.ToString ();

				}
			} else if (carSelected == 2) {
				handleV = PlayerPrefs.GetInt ("MDR112_H", 20);
				if (handleV < 35) {
					PlayAudio (clip[1]);
					handleV = PlayerPrefs.GetInt ("MDR112_H", 20) + 5;
					PlayerPrefs.SetInt ("MDR112_H", handleV);
					handleT.text = handleV.ToString ();
					totalMoney -= 500;
					PlayerPrefs.SetInt ("Money", totalMoney);
					totalMoneyT1.text = totalMoney.ToString ();
					totalMoneyT2.text = totalMoney.ToString ();
					totalMoneyT3.text = totalMoney.ToString ();

				}
			} else if (carSelected == 3) {
				handleV = PlayerPrefs.GetInt ("MDR113_H", 20);
				if (handleV < 35) {
					PlayAudio (clip[1]);
					handleV = PlayerPrefs.GetInt ("MDR113_H", 20) + 5;
					PlayerPrefs.SetInt ("MDR113_H", handleV);
					handleT.text = handleV.ToString ();
					totalMoney -= 500;
					PlayerPrefs.SetInt ("Money", totalMoney);
					totalMoneyT1.text = totalMoney.ToString ();
					totalMoneyT2.text = totalMoney.ToString ();
					totalMoneyT3.text = totalMoney.ToString ();

				}
			} else if (carSelected == 4) {
				handleV = PlayerPrefs.GetInt ("MDR114_H", 20);
				if (handleV < 35) {
					PlayAudio (clip[1]);
					handleV = PlayerPrefs.GetInt ("MDR114_H", 20) + 5;
					PlayerPrefs.SetInt ("MDR114_H", handleV);
					handleT.text = handleV.ToString ();
					totalMoney -= 500;
					PlayerPrefs.SetInt ("Money", totalMoney);
					totalMoneyT1.text = totalMoney.ToString ();
					totalMoneyT2.text = totalMoney.ToString ();
					totalMoneyT3.text = totalMoney.ToString ();

				}
			}
		} else {
			GetComponent<CarSelectionUI> ().ShopB ();
			PlayAudio (clip[3]);
		}
	}

	void HandleLocks(){
		
		if(PlayerPrefs.GetInt("MDR111_TP2",0) == 1)
		locks [0].SetActive (false);

		if(PlayerPrefs.GetInt("MDR111_TP3",0) == 1)
			locks [1].SetActive (false);
		if(PlayerPrefs.GetInt("MDR111_TP4",0) == 1)
			locks [2].SetActive (false);

		if(PlayerPrefs.GetInt("MDR112_TP2",0) == 1)
			locks [3].SetActive (false);
		if(PlayerPrefs.GetInt("MDR112_TP3",0) == 1)
			locks [4].SetActive (false);
		if(PlayerPrefs.GetInt("MDR112_TP4",0) == 1)
			locks [5].SetActive (false);
		if(PlayerPrefs.GetInt("MDR113_TP2",0) == 1)
			locks [6].SetActive (false);
		if(PlayerPrefs.GetInt("MDR113_TP3",0) == 1)
			locks [7].SetActive (false);
		if(PlayerPrefs.GetInt("MDR113_TP4",0) == 1)
			locks [8].SetActive (false);
		if(PlayerPrefs.GetInt("MDR114_TP2",0) == 1)
			locks [9].SetActive (false);
		if(PlayerPrefs.GetInt("MDR114_TP3",0) == 1)
			locks [10].SetActive (false);
		if(PlayerPrefs.GetInt("MDR114_TP4",0) == 1)
			locks [11].SetActive (false);
	}
	public void LoadLapLB(){
		PlayAudio (clip[2]);
		if(carSelected == 1){
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().speed = PlayerPrefs.GetInt ("MDR111_S", 60);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().acc = PlayerPrefs.GetInt ("MDR111_A", 1); //800 2000 15
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().nos = PlayerPrefs.GetInt ("MDR111_N", 200);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().handle = PlayerPrefs.GetInt ("MDR111_H", 20);
		}else 	if(carSelected == 2){
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().speed = PlayerPrefs.GetInt ("MDR112_S", 70);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().acc = PlayerPrefs.GetInt ("MDR112_A", 1); //800 2000 15
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().nos = PlayerPrefs.GetInt ("MDR112_N", 600);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().handle = PlayerPrefs.GetInt ("MDR112_H", 20);
		}else	if(carSelected == 3){
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().speed = PlayerPrefs.GetInt ("MDR113_S", 90);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().acc = PlayerPrefs.GetInt ("MDR113_A", 1); //800 2000 15
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().nos = PlayerPrefs.GetInt ("MDR113_N", 1400);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().handle = PlayerPrefs.GetInt ("MDR113_H", 20);
		}else 	if(carSelected == 4){
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().speed = PlayerPrefs.GetInt ("MDR114_S", 100);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().acc = PlayerPrefs.GetInt ("MDR114_A", 1); //800 2000 15
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().nos = PlayerPrefs.GetInt ("MDR114_N", 1800);
			GameObject.Find ("DataHandler").GetComponent<DataHandler> ().handle = PlayerPrefs.GetInt ("MDR114_H", 20);
		}
		GameObject.Find ("DataHandler").GetComponent<DataHandler> ().money = PlayerPrefs.GetInt ("Money", 500);
	//	StartCoroutine (LoadLev());
		loadingC.SetActive(true);
		SceneManager.LoadScene (2);
	}

	IEnumerator LoadLev(){
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene (2);
	}

	public void GetFreeCoins(){
	//	PlayerPrefs.SetInt ("Money", 20000);
		Debug.Log ("money updated");
	}


}
