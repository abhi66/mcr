using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.UI;

public class SpeedoMeter : MonoBehaviour {

	public  float currentSpeed;
	public Transform niddle;
	public Text niddleT;
	public Text speedT;
	Rigidbody car;
	public float maxSpeed;

	// Use this for initialization
	void Start () {
		car = Camera.main.GetComponent<CameraController> ().car.GetComponent<Rigidbody>();
		maxSpeed = Camera.main.GetComponent<CameraController> ().car.GetComponent<CarController> ().m_Topspeed ;
	}
	
	// Update is called once per frame
	void Update () {
		
		currentSpeed =car.velocity.magnitude*2.23693629f; 
		float speedFactor = currentSpeed / maxSpeed;
		float angle = Mathf.Lerp (127, -127, speedFactor );
		Quaternion newanf = Quaternion.Euler (0, 0, angle);
		niddle.rotation = newanf;
		int tmp = (int)currentSpeed;
		niddleT.text = speedT.text = tmp.ToString ();

	}
}
