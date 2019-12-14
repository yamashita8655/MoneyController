using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsManager : SimpleMonoBehaviourSingleton<PlayerPrefsManager> {

	public static int MaxSaveCount = 4;
	public int SelectIndex { get; set; }

	public enum SaveType {
		Save1 = 0,
		Save2,
		Save3,
		Save4,
		//CanUseMoney,
		//StartDays,
		//EndDays,
		Item,
		//Name,
		SaveIdList,
		Max,
		None
	};
	
	// 定義用。これプログラム中で編集しちゃダメ。Readonlyにしたいけど、リストの初期化が多分無理
	private List<string> SaveKeyList = new List<string>(){
		"KeyCanUseMoney",
		"KeyStartDays",
		"KeyEndDays",
		"KeyItem",
		"KeyName",
		"KeySaveIdList",
	};
	
	public void Initialize() {
		SelectIndex = 0;
		CreateFirstData();
	}

	// 初回のセーブデータ作成
	private void CreateFirstData() {
		for (int i = 0; i < SaveKeyList.Count; i++) {
			string key = SaveKeyList[i];
			bool res = PlayerPrefs.HasKey(key);
			if (res == false) {
				PlayerPrefs.SetString(key, "");
			}
		}
	}
	
	public void SaveParameter(SaveType type, string parameter) {
		string key = SaveKeyList[(int)type];
		PlayerPrefs.SetString(key, parameter);
		PlayerPrefs.Save();
	}
	
	public string GetParameter(SaveType type) {
		string key = SaveKeyList[(int)type];
		string flag = PlayerPrefs.GetString(key);

		return flag;
	}
}

