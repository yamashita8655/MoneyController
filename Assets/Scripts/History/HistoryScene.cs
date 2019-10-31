using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryScene : SceneBase
{
	[SerializeField]
	Text HistoryText = null;
	
    // Start is called before the first frame update
    void Start()
    {
		var PPM = PlayerPrefsManager.Instance;
		string saveItemString = PPM.GetParameter(PlayerPrefsManager.SaveType.Item);
		HistoryText.text = saveItemString;
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

    public void OnClickCopyClipboardButton()
    {
		GUIUtility.systemCopyBuffer = HistoryText.text;
    }
}
