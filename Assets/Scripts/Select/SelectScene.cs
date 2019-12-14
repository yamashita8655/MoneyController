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
		if (string.IsNullOrEmpty(saveIdList) == false) {
			string[] list = saveIdList.Split(',');
			for (int i = 0; i < list.Length; i++) {
				string data = list[i];
				if (string.IsNullOrEmpty(data) == false) {
					SelectButtonObjects[i].SetActive(true);
				}
			}	
		}
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
		PlayerPrefsManager.Instance.SelectIndex = index;
		LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Continue, null);
    }
    
	public void OnClickDeleteButton(int index)
    {
    }
}
