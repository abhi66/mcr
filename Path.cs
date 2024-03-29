// Written by Stephen P S AKA Zergling103 - Yoshiegg900@hotmail.com - 25-12-2011

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Path : MonoBehaviour {
	
	//[HideInInspector]
	public Transform[] nodes;

	
	// Use this for initialization
	void Start () {
		 Transform[] transforms = GetComponentsInChildren<Transform>();
		
		 nodes = new Transform[transforms.Length - 1];
		
		for(int n = 0; n < nodes.Length; n++)
		{
			nodes[n] = transforms[n+1];
		}
	}
	
	void Update()
	{
		for(int n = 0; n < nodes.Length; n++)
		{
			
			Debug.DrawLine(nodes[n].position - Vector3.down, nodes[(n+1)%nodes.Length].position - Vector3.down, Color.red);
		}	
	}
}
