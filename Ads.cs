using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class Ads : MonoBehaviour {
	int totalMoney;
	public Text totalMoneyT1;
	public Text totalMoneyT2;
	public Text totalMoneyT3;

	public void ShowSimpleAd(){
		if (Advertisement.IsReady ()) {
			Advertisement.Show ();

			totalMoney = PlayerPrefs.GetInt ("Money", 500);
			totalMoney = totalMoney + 50;
			totalMoneyT1.text = totalMoneyT2.text = totalMoneyT3.text = totalMoney.ToString();

			PlayerPrefs.SetInt ("Money", totalMoney);

		}
	} 

	public void ShowSimpleAd_bkup(){
		if (Advertisement.IsReady ()) {
			Advertisement.Show ();

			totalMoney = PlayerPrefs.GetInt ("Money", 500);
			totalMoney = totalMoney + 50;
			totalMoneyT1.text = totalMoneyT2.text = totalMoneyT3.text = totalMoney.ToString();

			PlayerPrefs.SetInt ("Money", totalMoney);

		}
	} 

	public void ShowRewardedAd(){
		if (Advertisement.IsReady ()) {
			
			Advertisement.Show ("rewardedVideo", new ShowOptions (){ resultCallback = HandleAdResult });

		}
	}
	void HandleAdResult (ShowResult result){
		switch(result){
		case ShowResult.Finished:
			Debug.Log ("Player watched full the video");
			totalMoney = PlayerPrefs.GetInt ("Money", 500);
			totalMoney = totalMoney + 100;
			totalMoneyT1.text = totalMoneyT2.text = totalMoneyT3.text = totalMoney.ToString();

			PlayerPrefs.SetInt ("Money", totalMoney);
			break;
		case ShowResult.Skipped:
			totalMoney = PlayerPrefs.GetInt ("Money", 500);
		//	totalMoney = totalMoney + 50;
		//	totalMoneyT1.text = totalMoneyT2.text = totalMoneyT3.text = totalMoney.ToString();

		//	PlayerPrefs.SetInt ("Money", totalMoney);
			Debug.Log ("video skiped");
			break;

		case ShowResult.Failed:
			
			Debug.Log ("No Internet available");
			break;
		}

	}


}
