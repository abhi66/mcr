using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CarSelectionUI : MonoBehaviour {

	public GameObject mainC;
	public GameObject shopC;
	public GameObject upgC;
	public GameObject infoC;
	public int totalMoney;

	public Text totalMoneyT1;
	public Text totalMoneyT2;
	public Text totalMoneyT3;

	public AudioClip a_clip;

	public Animation mainCAnim;
	public AudioSource aud_S;

	 void PlayAudio(AudioClip clp){
		aud_S.clip = clp;
		aud_S.Play ();
	}
	void Start(){
	//	PlayerPrefs.DeleteAll ();
		mainC.GetComponent<Animator>().SetBool("Purched",true);	
		mainC.GetComponent<Animator>().SetBool("MainCHide",false);	
	//	shopC.GetComponent<Animator>().SetBool("HideShopC",true);
		upgC.GetComponent<Animator>().SetBool("HideUC",true);
		infoC.SetActive (true);

		totalMoney =	PlayerPrefs.GetInt ("Money", 500);
		totalMoneyT1.text = totalMoneyT2.text = totalMoneyT3.text = totalMoney.ToString();

	}


	public void ShopB(){
		
		mainC.GetComponent<Animator>().SetBool("MainCHide",true);	
		shopC.GetComponent<Animator>().SetBool("HideShopC",false);
		upgC.GetComponent<Animator>().SetBool("HideUC",true);
		infoC.SetActive (false);
	//	PlayAudio (a_clip);

	}public void ShopButton(){

		mainC.GetComponent<Animator>().SetBool("MainCHide",true);	
		shopC.GetComponent<Animator>().SetBool("HideShopC",false);
		upgC.GetComponent<Animator>().SetBool("HideUC",true);
		infoC.SetActive (false);
		PlayAudio (a_clip);

	}
	public void ReturnFShopB(){
		
		mainC.GetComponent<Animator>().SetBool("MainCHide",false);
		shopC.GetComponent<Animator>().SetBool("HideShopC",true);

		upgC.GetComponent<Animator>().SetBool("HideUC",true);

	
		infoC.SetActive (true);
		PlayAudio (a_clip);
	}

	public void CallLeaderBoard(){
		Debug.Log ("Calling LBD");
		PlayAudio (a_clip);
	}
	public void UpgB(){
		mainC.GetComponent<Animator>().SetBool("MainCHide",true);
	
		shopC.GetComponent<Animator>().SetBool("HideShopC",true);
		upgC.GetComponent<Animator>().SetBool("HideUC",false);

		infoC.SetActive (true);
		PlayAudio (a_clip);
	}
	public void ReturnFUpgB(){
		mainC.GetComponent<Animator>().SetBool("Purched",true);
		mainC.GetComponent<Animator>().SetBool("MainCHide",false);
		shopC.GetComponent<Animator>().SetBool("HideShopC",true);
		upgC.GetComponent<Animator>().SetBool("HideUC",true);

		infoC.SetActive (true);
		PlayAudio (a_clip);
	}

}
