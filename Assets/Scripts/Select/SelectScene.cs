using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectScene : SceneBase
{
	[SerializeField]
	private GameObject[] SelectButtonObjects = null;
	
	[SerializeField]
	private Text[] SelectButtonTexts = null;
	
	// Start is called before the first frame update
	void Start()
	{
		UpdateButton();
	}

	private void UpdateButton() {
		// ボタン非表示
		for (int i = 0; i < SelectButtonObjects.Length; i++) {
			SelectButtonObjects[i].SetActive(false);
		}

		var PPM = PlayerPrefsManager.Instance;
		string saveIdList = PPM.GetParameter(PlayerPrefsManager.SaveType.SaveIdList);

		Debug.Log(saveIdList);
		if (string.IsNullOrEmpty(saveIdList) == false) {
			string[] list = saveIdList.Split(',');
			for (int i = 0; i < list.Length; i++) {
				string data = list[i];
				if (string.IsNullOrEmpty(data) == false) {
					SelectButtonObjects[i].SetActive(true);
					SelectButtonTexts[i].text = GetButtonIdText(data);
				}
			}	
		}

	}

	private string GetButtonIdText(string inId) {
		var PPM = PlayerPrefsManager.Instance;
		string name = "";
		// 名前の対応
		for (int i = 0; i < 4; i++) {
			string saveList = PPM.GetParameter((PlayerPrefsManager.SaveType)i);
			string[] paramList = saveList.Split(',');
			string id = paramList[0];
			if (id == inId) {
				name = paramList[1];
				break;
			}
		}

		return name;
	}

	//// Update is called once per frame
	//void Update()
	//{
	//	
	//}
	//
	
	public void OnClickBackButton() {
		LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Title, null);
	}

	public void OnClickSelectButton(int index)
	{
		//ContinueData data = new ContinueData();
		//data.SelectIndex = index;
		//LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Continue, data);
		
		//var PPM = PlayerPrefsManager.Instance;
		//string saveIdList = PPM.GetParameter(PlayerPrefsManager.SaveType.SaveIdList);
		//string[] list = saveIdList.Split(',');
		//string getId = list[index];
		//
		//PlayerPrefsManager.Instance.SelectId = getId;

		PlayerPrefsManager.Instance.SelectIndex = index;
		LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Continue, null);
	}
	
	public void OnClickDeleteButton(int index)
	{
		// セーブIDリストを取る
		// 絶対に空では無いので、IDリストを取得する
		// インデックスと同じ添え字は、削除する
		// 詰める
		// セーブする
		// ボタン表示する
		
		var PPM = PlayerPrefsManager.Instance;
		string saveIdList = PPM.GetParameter(PlayerPrefsManager.SaveType.SaveIdList);
		string[] list = saveIdList.Split(',');

		List<string> saveStringList = new List<string>();
		for (int i = 0; i < list.Length; i++) {
			string data = list[i];
			if (string.IsNullOrEmpty(data) == false) {
				if (i == index) {
					continue;
				}
				saveStringList.Add(data);
			}
		}
		
		string saveString = "";
		int saveStringIndex = 0;
		for (; saveStringIndex < saveStringList.Count; saveStringIndex++) {
			if (saveStringIndex == 0) {
				saveString = saveStringList[saveStringIndex];
			} else {
				saveString += "," + saveStringList[saveStringIndex];
			}
		}
		
		for (; saveStringIndex < 4; saveStringIndex++) {
			if (saveStringIndex == 0) {
				saveString = "";
			} else {
				saveString += ",";
			}
		}

		Debug.Log(saveString);

		PPM.SaveParameter(PlayerPrefsManager.SaveType.SaveIdList, saveString);
		
		// こっちは、パラメータの調整
		List<string> workParameterStringList = new List<string>();
		workParameterStringList.Add(PPM.GetParameter(PlayerPrefsManager.SaveType.Save1));
		workParameterStringList.Add(PPM.GetParameter(PlayerPrefsManager.SaveType.Save2));
		workParameterStringList.Add(PPM.GetParameter(PlayerPrefsManager.SaveType.Save3));
		workParameterStringList.Add(PPM.GetParameter(PlayerPrefsManager.SaveType.Save4));
		
		List<string> parameterStringList = new List<string>();

		for (int i = 0; i < workParameterStringList.Count; i++) {
			if (i == index) {
				continue;
			}
			parameterStringList.Add(workParameterStringList[i]);
		}

		int saveParameterIndex = 0;
		for (; saveParameterIndex < parameterStringList.Count; saveParameterIndex++) {
			int parameterType = ((int)PlayerPrefsManager.SaveType.Save1)+saveParameterIndex;
			PPM.SaveParameter((PlayerPrefsManager.SaveType)parameterType, parameterStringList[saveParameterIndex]);
		}
		
		for (; saveParameterIndex < 4; saveParameterIndex++) {
			int parameterType = ((int)PlayerPrefsManager.SaveType.Save1)+saveParameterIndex;
			PPM.SaveParameter((PlayerPrefsManager.SaveType)parameterType, "");
		}

		// こっちは、消費アイテムの削除
		List<string> workItemStringList = new List<string>();
		workItemStringList.Add(PPM.GetParameter(PlayerPrefsManager.SaveType.Item1));
		workItemStringList.Add(PPM.GetParameter(PlayerPrefsManager.SaveType.Item2));
		workItemStringList.Add(PPM.GetParameter(PlayerPrefsManager.SaveType.Item3));
		workItemStringList.Add(PPM.GetParameter(PlayerPrefsManager.SaveType.Item4));
		
		List<string> itemStringList = new List<string>();

		for (int i = 0; i < workItemStringList.Count; i++) {
			if (i == index) {
				continue;
			}
			itemStringList.Add(workItemStringList[i]);
		}

		int saveItemIndex = 0;
		for (; saveItemIndex < itemStringList.Count; saveItemIndex++) {
			int itemType = ((int)PlayerPrefsManager.SaveType.Item1)+saveItemIndex;
			PPM.SaveParameter((PlayerPrefsManager.SaveType)itemType, itemStringList[saveItemIndex]);
		}
		
		for (; saveItemIndex < 4; saveItemIndex++) {
			int itemType = ((int)PlayerPrefsManager.SaveType.Item1)+saveItemIndex;
			PPM.SaveParameter((PlayerPrefsManager.SaveType)itemType, "");
		}

		UpdateButton();
	}
}
