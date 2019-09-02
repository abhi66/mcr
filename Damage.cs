using UnityEngine;

using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {


	public GameObject[] carDamagedBody;

	public AudioSource[] clp;
	public string carDamagePrefs;
	public SpeedoMeter speedMtr;

	float carSpeed ;
//	SpeedoMeter speedFspdmtr;
	int count;
	bool damaging = false;


	void Start(){

		count =	PlayerPrefs.GetInt (carDamagePrefs,count);
		DamagedCarsBody (count, carDamagedBody);
	}
	void DamagedCarsBody(int dam, GameObject[] obj){
		for (int i = 0; i < obj.Length; i++) {
			if (i == dam)
				obj [dam].SetActive (true);
			else
				obj [i].SetActive (false);
		}
	}


	void OnCollisionEnter(Collision hit){
		carSpeed = speedMtr.currentSpeed;

		if (carSpeed > 25 && !damaging) {
			StartCoroutine (CarDamage());
			damaging = true;
	//		Debug.Log ("Damaging...");
		}
		if (hit.gameObject.CompareTag ("EnemyCar") ) {
			if(!clp[1].isPlaying && carSpeed > 20)
			clp [1].Play ();
		//	Debug.Log ("car collide");
		} else {
			if (!clp [0].isPlaying && carSpeed > 20) {

				clp [0].Play ();
			}

		}
	
	}


	IEnumerator CarDamage(){
		
		yield return new WaitForSeconds (1f);


			if (count < carDamagedBody.Length - 1) {
				count++;
				carDamagedBody [count - 1].SetActive (false);
				carDamagedBody [count].SetActive (true);
				PlayerPrefs.SetInt (carDamagePrefs,count);
				damaging = false;
			}

	}

}
