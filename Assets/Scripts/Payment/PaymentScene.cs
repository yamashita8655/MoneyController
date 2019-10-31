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
	
    void Start()
    {
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
		var PPM = PlayerPrefsManager.Instance;
		string saveItemString = PPM.GetParameter(PlayerPrefsManager.SaveType.Item);

		string currentItemString = string.Format("{0},{1}", TitleInputField.text, PriceField.text);
		if (string.IsNullOrEmpty(saveItemString)) {
			saveItemString = currentItemString;
		} else {
			saveItemString = string.Format("{0}\n{1}", saveItemString, currentItemString);
		}
		PPM.SaveParameter(PlayerPrefsManager.SaveType.Item, saveItemString);
		LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Continue, null);
    }
}
