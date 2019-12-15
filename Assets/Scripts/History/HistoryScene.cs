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
		string saveItemString = GetItem(PlayerPrefsManager.Instance.SelectIndex);
		HistoryText.text = saveItemString;
    }
	
	private string GetItem(int index) {
		string output = "";
		var PPM = PlayerPrefsManager.Instance;
		int type = ((int)PlayerPrefsManager.SaveType.Item1)+index;
		output = PPM.GetParameter((PlayerPrefsManager.SaveType)type);

		return output;
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
