using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
	public AudioSource aud;

	public void LoadLevel(){
		SceneManager.LoadScene (1);
		aud.Play ();
	}

	public void ExitGm(){
		Application.Quit ();
	}
}
