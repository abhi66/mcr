using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {




	void OnTriggerEnter(Collider other){

		if(other.CompareTag("Player") || other.CompareTag("EnemyCar")){

			//Check so we dont exceed our checkpoint quantity
			if (other.GetComponent<CarPoint> ().currentCheckPoint + 1 < other.GetComponent<CarPoint> ().checkPoint.Length) {
				//Add to currentLap if currentCheckpoint is 0
			//	if (other.GetComponent<CarPoint> ().currentCheckPoint == 0)
			//		other.GetComponent<CarPoint> ().currentLap++;

				int myPos = 0;
				for (int i = 0; i < other.GetComponent<CarPoint> ().checkPoint.Length; i++) {
					if (transform == other.GetComponent<CarPoint> ().checkPoint [i]){
						 myPos = i + 1;

						}
				}



				//	Debug.Log (myPos);
				if (other.GetComponent<CarPoint> ().currentCheckPoint > 0 && myPos <= other.GetComponent<CarPoint> ().currentCheckPoint) {
				//	Debug.Log ("Wrong Dir");
				
				} else {


					other.GetComponent<CarPoint> ().lastCheckPoint = other.GetComponent<CarPoint> ().currentCheckPoint;
					other.GetComponent<CarPoint> ().currentCheckPoint++;
				}

			} else  {
					//If we dont have any Checkpoints left, go back to 0
				other.GetComponent<CarPoint> ().currentCheckPoint = 0;
				other.GetComponent<CarPoint> ().currentLap++;

				}

				//Run a coroutine to update the visual aid of our Checkpoints


		//	Debug.Log (other.GetComponent<CRCKPoint> ().carName + ": " + other.GetComponent<CRCKPoint> ().currentCheckPoint + "Lap: " + other.GetComponent<CRCKPoint> ().currentLap);



		
		}
	}			
}
