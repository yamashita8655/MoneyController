using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaymentScene : SceneBase
{
	[SerializeField]
	InputField TitleInputField = null;
	
	[SerializeField]
	InputField PriceField = null;

	private bool IsPaymentNow = false;
	
    void Start()
    {
		IsPaymentNow = false;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    
    //}
	//
	
	public void OnClickBackButton() {
		LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Continue, null);
	}

	public void OnClickPaymentButton()
    {
		if (IsPaymentNow == true) {
			return;
		}
		IsPaymentNow = true;

		StartCoroutine(CoShowInterstitial());
    }

	private IEnumerator CoShowInterstitial() {
		GoogleAdmobManager.Instance.ShowInterstitial();

		yield return new WaitForSeconds(3f);
	
		IsPaymentNow = false;

		var PPM = PlayerPrefsManager.Instance;
		//string saveItemString = PPM.GetParameter(PlayerPrefsManager.SaveType.Item);
		int selectIndex = PlayerPrefsManager.Instance.SelectIndex;
		string saveItemString = GetItem(selectIndex);

		string currentItemString = string.Format("{0},{1}", TitleInputField.text, PriceField.text);
		if (string.IsNullOrEmpty(saveItemString)) {
			saveItemString = currentItemString;
		} else {
			saveItemString = string.Format("{0}\n{1}", saveItemString, currentItemString);
		}
		int type = ((int)PlayerPrefsManager.SaveType.Item1)+selectIndex;
		PPM.SaveParameter((PlayerPrefsManager.SaveType)type, saveItemString);
		LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Continue, null);
	}
	
	private string GetItem(int index) {
		string output = "";
		var PPM = PlayerPrefsManager.Instance;
		int type = ((int)PlayerPrefsManager.SaveType.Item1)+index;
		output = PPM.GetParameter((PlayerPrefsManager.SaveType)(type));

		return output;
	}
}
