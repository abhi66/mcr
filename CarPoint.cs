using UnityEngine;
using System.Collections;

public class CarPoint : MonoBehaviour {

	public GameObject chechPoints;
	public Transform[] checkPoint;
	public int currentCheckPoint = 0;
	public int lastCheckPoint = 0;
	public int currentLap ;






	void Start () {
		currentLap = 1;
	//	StartCoroutine (CheckDir ());
	//	transform.GetChild (1).gameObject.SetActive (true);
		Transform[] transforms = chechPoints. GetComponentsInChildren<Transform>();

		checkPoint = new Transform[transforms.Length - 1];

		for(int n = 0; n < checkPoint.Length; n++)
		{
			checkPoint[n] = transforms[n+1];
		}
	}
	 float GetDistance(){
		 return (transform.position -checkPoint[lastCheckPoint].position).magnitude + currentCheckPoint * 1000 + currentLap * 50000;
	//	return ( currentCheckPoint * 100 + currentLap * 1000);
	}

	public int GetCarPosition(CarPoint[] allCars) {
		float distance = GetDistance();
		int position = 1;	
		foreach (CarPoint car in allCars) {
			if (car.GetDistance () > distance) {
				position++;
			}
		
		}
		return position;
	}



}
