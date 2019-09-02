using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class DataImplimenter : MonoBehaviour {


	public GameObject[] cars;
	public Texture[] allTextures;
	GMManager gmMngr;


	public int carSelected;
	public int carTex;
	public float speed;
	public float acc;
	public float nos;
	public float handle;
	public int totalLap;
	public int totalTm;


	DataHandler dtaHndlr;
//	int damaged;

	void Awake () {
		
		gmMngr = GetComponent<GMManager> ();
		dtaHndlr = GameObject.Find ("DataHandler").GetComponent<DataHandler>();
		carSelected = dtaHndlr.carSel;
		carTex = dtaHndlr.car1_texSelected;
		speed = dtaHndlr.speed;
		acc = dtaHndlr.acc;
		GetComponent<UIButtons>().carAcc = acc;
		nos = dtaHndlr.nos;
		handle = dtaHndlr.handle;
		totalLap = dtaHndlr.totalLaps;
		totalTm = dtaHndlr.totalTm;

	

		gmMngr.totalLaps = totalLap;
		HandleCars ();
	}

	void HandleCars(){
		
		if (carSelected == 1) {
			cars [0].SetActive (true);
			cars [1].SetActive (false);
			cars [2].SetActive (false);
			cars [3].SetActive (false);
			Camera.main.GetComponent<CameraController> ().car = cars [0].transform;
			gmMngr.allCar [5] = cars [0].GetComponent<CarPoint>();
		//	damaged = PlayerPrefs.GetInt ("DamagedC_01",0);
			foreach (GameObject car in cars[0].GetComponent<Damage>().carDamagedBody) {
				car.GetComponent<Renderer>().material.mainTexture = allTextures[carTex - 1];
			}
		//	cars[0].GetComponent<Damage>().carDamagedBody[damaged].GetComponent<Renderer>().material.mainTexture = allTextures[carTex - 1];

		//	cars [0].transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTextures [carTex - 1];

			cars [0].GetComponent<CarController> ().m_Topspeed = speed;
			//cars [0].GetComponent<CarController> ().m_FullTorqueOverAllWheels = acc;

			//cars [0].GetComponent<CarController> ().nosTorque = nos;
			cars [0].GetComponent<CarController> ().m_BrakeTorque = nos;
			cars [0].GetComponent<CarController> ().m_MaximumSteerAngle = handle;

		}else if (carSelected == 2) {
			cars [0].SetActive (false);
		cars [1].SetActive (true);
			cars [2].SetActive (false);
			cars [3].SetActive (false);
			Camera.main.GetComponent<CameraController> ().car = cars [1].transform;
			gmMngr.allCar [5] = cars [1].GetComponent<CarPoint>();
		//	damaged = PlayerPrefs.GetInt ("DamagedC_02",0);
		//	cars[1].GetComponent<Damage>().carDamagedBody[damaged].GetComponent<Renderer>().material.mainTexture = allTextures[carTex - 1];
			foreach (GameObject car in cars[1].GetComponent<Damage>().carDamagedBody) {
				car.GetComponent<Renderer>().material.mainTexture = allTextures[carTex - 1];
			}
			//	cars [1].transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTextures [carTex - 1];

			cars [1].GetComponent<CarController> ().m_Topspeed = speed;
		//	cars [1].GetComponent<CarController> ().m_FullTorqueOverAllWheels = acc;
		//	cars [1].GetComponent<CarController> ().nosTorque = nos;
			cars [1].GetComponent<CarController> ().m_BrakeTorque = nos;
			cars [1].GetComponent<CarController> ().m_MaximumSteerAngle = handle;
		}else if (carSelected == 3) {
			cars [0].SetActive (false);
			cars [1].SetActive (false);
			cars [2].SetActive (true);
			cars [3].SetActive (false);
			Camera.main.GetComponent<CameraController> ().car = cars [2].transform;
			gmMngr.allCar [5] = cars [2].GetComponent<CarPoint>();
		//	damaged  = PlayerPrefs.GetInt ("DamagedC_03",0);
			foreach (GameObject car in cars[2].GetComponent<Damage>().carDamagedBody) {
				car.GetComponent<Renderer>().material.mainTexture = allTextures[carTex - 1];
			}
		//	cars[2].GetComponent<Damage>().carDamagedBody[damaged].GetComponent<Renderer>().material.mainTexture = allTextures[carTex - 1];
		//	cars [2].transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTextures [carTex - 1];

			cars [2].GetComponent<CarController> ().m_Topspeed = speed;
		//	cars [2].GetComponent<CarController> ().m_FullTorqueOverAllWheels = acc;
		//	cars [2].GetComponent<CarController> ().nosTorque = nos;
			cars [2].GetComponent<CarController> ().m_BrakeTorque = nos;
			cars [2].GetComponent<CarController> ().m_MaximumSteerAngle = handle;
		}else if (carSelected == 4) {
			cars [0].SetActive (false);
			cars [1].SetActive (false);
			cars [2].SetActive (false);
			cars [3].SetActive (true);
			Camera.main.GetComponent<CameraController> ().car = cars [3].transform;
			gmMngr.allCar [5] = cars [3].GetComponent<CarPoint>();
		//	damaged  = PlayerPrefs.GetInt ("DamagedC_04",0);
			foreach (GameObject car in cars[3].GetComponent<Damage>().carDamagedBody) {
				car.GetComponent<Renderer>().material.mainTexture = allTextures[carTex - 1];
			}
		//	cars[3].GetComponent<Damage>().carDamagedBody[damaged].GetComponent<Renderer>().material.mainTexture = allTextures[carTex - 1];
		//	cars [3].transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = allTextures [carTex - 1];

			cars [3].GetComponent<CarController> ().m_Topspeed = speed;
		//	cars [3].GetComponent<CarController> ().m_FullTorqueOverAllWheels = acc;
		//	cars [3].GetComponent<CarController> ().nosTorque = nos;
			cars [3].GetComponent<CarController> ().m_BrakeTorque = nos;
			cars [3].GetComponent<CarController> ().m_MaximumSteerAngle = handle;
		}
	}



}
