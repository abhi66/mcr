using UnityEngine;
using System.Collections;

public class E_C_WRotater : MonoBehaviour {


	void FixedUpdate () {
		transform.Rotate (Vector3.right * Time.deltaTime * 250);
	}
}
