﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueScene : SceneBase
{
	[SerializeField]
	Text RemainDayText = null;
	
	[SerializeField]
	Text CanUseMoneyText = null;
	
	//[SerializeField]
	//InputField CanUseMoneyInputField = null;
    // Start is called before the first frame update
    void Start()
    {
		//ContinueData data = LocalSceneManager.Instance.SceneData as ContinueData;
		//int selectIndex = data.SelectIndex;

		int selectIndex = PlayerPrefsManager.Instance.SelectIndex;

		var PPM = PlayerPrefsManager.Instance;

		string saveString = PPM.GetParameter((PlayerPrefsManager.SaveType)selectIndex);
		string[] list = saveString.Split(',');

		string canUseMoneyString = list[4];
		int canUseMoney = 0;
		if (string.IsNullOrEmpty(canUseMoneyString) == false) {
			canUseMoney = int.Parse(canUseMoneyString);
		}

		string start = list[2];
		string end = list[3];

		int startDay = 0;
		if (string.IsNullOrEmpty(start) == false) {
			string[] startList = start.Split('/');
			string startDayString = startList[2];
			startDay = int.Parse(startDayString);
		}
		
		int endDay = 0;
		if (string.IsNullOrEmpty(end) == false) {
			string[] endList = end.Split('/');
			string endDayString = endList[2];
			endDay = int.Parse(endDayString);
		}

		DateTime dt = DateTime.Now;
		int nowDay = int.Parse(dt.ToString("dd"));

		int canUseMoneyOfDay = canUseMoney / (endDay-startDay+1);
		int canUseMoneyBase = canUseMoneyOfDay * (nowDay-startDay+1);

		// ここで、使った分の金額を引く
		int useMoney = 0;
		string saveItemString = PPM.GetParameter(PlayerPrefsManager.SaveType.Item);
		Debug.Log(saveItemString);
		if (string.IsNullOrEmpty(saveItemString) == false) {
			string[] saveItemList = saveItemString.Split('\n');
			for (int i = 0; i < saveItemList.Length; i++) {
				Debug.Log(saveItemList[i]);
				string[] itemList = saveItemList[i].Split(',');
				useMoney += int.Parse(itemList[1]);
			}
			canUseMoneyBase -= useMoney;
		}
		
		RemainDayText.text = (endDay-nowDay+1).ToString();
		CanUseMoneyText.text = canUseMoneyBase.ToString();
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    
    //}
	//
	
	public void OnClickBackButton() {
		LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Select, null);
	}

    public void OnClickPaymentButton()
    {
		LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Payment, null);
    }
    
	public void OnClickHistoryButton()
    {
		LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.History, null);
    }
}
