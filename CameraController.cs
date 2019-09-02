using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public  Transform car;
	public float distance = 6.4f;
	public float height = 1.4f;
	public float rotationDamp = 3f;
	public float heightDamp = 2f;


	public  float defaultFov = 60;
	public float zoomRatio = .5f;


	int camNum;
	Vector3 rotaionVector;





	void LateUpdate () {
		
		float wantAngle = rotaionVector.y;
		float wantHeight = car.position.y + height;

		float myAngle = transform.eulerAngles.y;
		float myHeight = transform.position.y;

		myAngle = Mathf.LerpAngle (myAngle, wantAngle, rotationDamp * Time.deltaTime);
		myHeight = Mathf.Lerp (myHeight, wantHeight, heightDamp * Time.deltaTime);

		Quaternion currentRotation = Quaternion.Euler (0, myAngle, 0);
		transform.position = car.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		transform.position = new Vector3(transform.position.x, myHeight, transform.position.z);
		transform.LookAt (car);

	}
	void FixedUpdate(){
		
		Vector3 localVelocity = car.InverseTransformDirection (car.GetComponent<Rigidbody> ().velocity);

		if (localVelocity.z < -1 || Input.GetKey (KeyCode.Keypad8)) {
			
			rotaionVector.y = car.eulerAngles.y + 180;
		} else if (Input.GetKey (KeyCode.Keypad6)) {
			Debug.Log ("gating key");
			rotaionVector.y = car.eulerAngles.y + 90;
		

		} else if (Input.GetKey (KeyCode.Keypad4)) {
			rotaionVector.y = car.eulerAngles.y - 90;
		}	 else if (Input.GetKey (KeyCode.Keypad2)) {
		rotaionVector.y = car.eulerAngles.y ;
	}

		else {
			rotaionVector.y = car.eulerAngles.y;
		}

		float accelration = car.gameObject.GetComponent<Rigidbody> ().velocity.magnitude ;
		GetComponent<Camera> ().fieldOfView = defaultFov + accelration * zoomRatio * Time.deltaTime;
	


		if (camNum == 0) {
			
		//	rotaionVector.y = car.eulerAngles.y;
		} else if (camNum == 1) {
			rotaionVector.y = car.eulerAngles.y - 90;
		}else if (camNum == 2) {
			rotaionVector.y = car.eulerAngles.y + 180;
		} else {
			rotaionVector.y = car.eulerAngles.y + 90;
		}
	}

	public void CameraB(){
		if (camNum == 0) {
			camNum = 1;

		} else if (camNum == 1) {
			camNum = 2;

		} else if (camNum == 2) {
			camNum = 3;
		} else {
			camNum = 0;
		}
	}


}
