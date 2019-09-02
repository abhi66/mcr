

using UnityEngine;
using System.Collections;

public class MoveToObject : MonoBehaviour {

	public Transform objectToMoveTowards;
	public float speed;
	public float turnSpeed;
	public float currentSpeed;

	bool moveNow;

	void Start(){
		moveNow = false;
		StartCoroutine (MoveNow ());
	}

	void Update () {
		
			if (moveNow) {
				MoveCars ();

		} 
	}

	void MoveCars(){
		objectToMoveTowards.position = new Vector3 (objectToMoveTowards.transform.position.x, transform.position.y, objectToMoveTowards.transform.position.z);
		float ts = Mathf.Clamp01 (turnSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (objectToMoveTowards.position - transform.position), ts); 

		//		float s = Mathf.Clamp01(speed * Time.deltaTime);
		//transform.position += (objectToMoveTowards.position - transform.position).normalized * s;
		transform.position += transform.forward * currentSpeed * Time.deltaTime;

		Debug.DrawLine (transform.position, objectToMoveTowards.position, Color.cyan);
	}
	IEnumerator MoveNow(){
		yield return new WaitForSeconds (4f);
		moveNow = true;
		currentSpeed = 2;
		yield return new WaitForSeconds (2f);
		currentSpeed = 4;
		yield return new WaitForSeconds (2f);
		currentSpeed = 6;
		yield return new WaitForSeconds (2f);
		currentSpeed = 8;

		yield return new WaitForSeconds (2f);
		currentSpeed = speed;
	}
	}



