using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewScene : SceneBase
{
	[SerializeField]
	Text StartDateText = null;
	
	[SerializeField]
	Text EndDateText = null;
	
	[SerializeField]
	InputField CanUseMoneyInputField = null;
	
	[SerializeField]
	InputField NameInputField = null;
    // Start is called before the first frame update
    void Start()
    {
		DateTime dt = DateTime.Now;
        StartDateText.text = dt.ToString("yyyy/MM/dd");
		int year = int.Parse(dt.ToString("yyyy"));
		int month = int.Parse(dt.ToString("MM"));

		int daysInMonth  = DateTime.DaysInMonth(year, month);
        EndDateText.text = string.Format("{0}/{1}/{2}", year, month, daysInMonth);
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

    //public void OnClickDecideButton()
    //{
	//	if (string.IsNullOrEmpty(NameInputField.text)) {
	//		return;
	//	}

	//	if (string.IsNullOrEmpty(CanUseMoneyInputField.text)) {
	//		return;
	//	}



	//	DateTime dt = DateTime.Now;
	//	string saveId = dt.ToString("yyyyMMddmmss");
	//	string day = dt.ToString("yyyy/MM/dd");
	//	int year = int.Parse(dt.ToString("yyyy"));
	//	int month = int.Parse(dt.ToString("MM"));
	//	int daysInMonth  = DateTime.DaysInMonth(year, month);

	//	int canUseMoney = int.Parse(CanUseMoneyInputField.text);

	//	string name = NameInputField.text;

	//	var PPM = PlayerPrefsManager.Instance;

	//	string saveIdList = PPM.GetParameter(PlayerPrefsManager.SaveType.SaveIdList);

	//	int saveIdIndex = 0;
	//	if (string.IsNullOrEmpty(saveIdList) == true) {
	//		saveIdList = string.Format("{0},,,", saveId);
	//	} else {
	//		string[] list = saveIdList.Split(',');
	//		saveIdList = "{0},{1},{2},{3}";
	//		bool setNew = false;
	//		for (int i = 0; i < list.Length; i++) {
	//			string data = list[i];
	//			if (string.IsNullOrEmpty(data)) {
	//				if (setNew == false) {
	//					saveIdIndex = i;
	//					setNew = true;
	//					saveIdList = saveIdList.Replace("{" + i.ToString() + "}", saveId);
	//				} else {
	//					saveIdList = saveIdList.Replace("{" + i.ToString() + "}", "");
	//				}
	//			} else {
	//				saveIdList = saveIdList.Replace("{" + i.ToString() + "}", data);
	//			}
	//		}	
	//	}

	//	string saveString = "";
	//	// SaveId,Name,Start,End,Money
	//	saveString = string.Format("{0},{1},{2},{3},{4}",
	//			saveId,
	//			name,
	//			day,
	//			string.Format("{0}/{1}/{2}", year, month, daysInMonth),
	//			canUseMoney
	//		);


	//	PPM.SaveParameter((PlayerPrefsManager.SaveType)saveIdIndex, saveString);
	//	PPM.SaveParameter(PlayerPrefsManager.SaveType.SaveIdList, saveIdList);
	//	int itemIndex = ((int)PlayerPrefsManager.SaveType.Item1) + saveIdIndex;
	//	PPM.SaveParameter((PlayerPrefsManager.SaveType)itemIndex, "");

	//	//// 使った用途のセーブ情報リセット
	//	//PPM.SaveParameter(PlayerPrefsManager.SaveType.Item, "");

    //    //LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Continue, null);
    //    LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Select, null);
    //    
    //}
    
	public void OnClickDecideButton()
    {
		if (string.IsNullOrEmpty(NameInputField.text)) {
			return;
		}

		if (string.IsNullOrEmpty(CanUseMoneyInputField.text)) {
			return;
		}

		string firstParam = PlayerPrefsManager.Instance.GetFirstParameter();
		if (firstParam == "") {
			DecideFunction();
			PlayerPrefsManager.Instance.SaveParameter(PlayerPrefsManager.SaveType.IsFirstCreate, "1");
		} else {
			/*GoogleAdmobManager.Instance.RequestRewardBasedVideo(
				(res) => {
                	if (res == true) {
						GoogleAdmobManager.Instance.ShowVideo(
							(res2) => {
								DecideFunction();
							}
						);
					} else {
						DecideFunction();
					}
				}
			);*/
			DecideFunction();
		}
    }

	private void DecideFunction() {
		DateTime dt = DateTime.Now;
		string saveId = dt.ToString("yyyyMMddmmss");
		string day = dt.ToString("yyyy/MM/dd");
		int year = int.Parse(dt.ToString("yyyy"));
		int month = int.Parse(dt.ToString("MM"));
		int daysInMonth  = DateTime.DaysInMonth(year, month);

		int canUseMoney = int.Parse(CanUseMoneyInputField.text);

		string name = NameInputField.text;

		var PPM = PlayerPrefsManager.Instance;

		string saveIdList = PPM.GetParameter(PlayerPrefsManager.SaveType.SaveIdList);

		int saveIdIndex = 0;
		if (string.IsNullOrEmpty(saveIdList) == true) {
			saveIdList = string.Format("{0},,,", saveId);
		} else {
			string[] list = saveIdList.Split(',');
			saveIdList = "{0},{1},{2},{3}";
			bool setNew = false;
			for (int i = 0; i < list.Length; i++) {
				string data = list[i];
				if (string.IsNullOrEmpty(data)) {
					if (setNew == false) {
						saveIdIndex = i;
						setNew = true;
						saveIdList = saveIdList.Replace("{" + i.ToString() + "}", saveId);
					} else {
						saveIdList = saveIdList.Replace("{" + i.ToString() + "}", "");
					}
				} else {
					saveIdList = saveIdList.Replace("{" + i.ToString() + "}", data);
				}
			}	
		}

		string saveString = "";
		// SaveId,Name,Start,End,Money
		saveString = string.Format("{0},{1},{2},{3},{4}",
				saveId,
				name,
				day,
				string.Format("{0}/{1}/{2}", year, month, daysInMonth),
				canUseMoney
			);


		PPM.SaveParameter((PlayerPrefsManager.SaveType)saveIdIndex, saveString);
		PPM.SaveParameter(PlayerPrefsManager.SaveType.SaveIdList, saveIdList);
		int itemIndex = ((int)PlayerPrefsManager.SaveType.Item1) + saveIdIndex;
		PPM.SaveParameter((PlayerPrefsManager.SaveType)itemIndex, "");

		//// 使った用途のセーブ情報リセット
		//PPM.SaveParameter(PlayerPrefsManager.SaveType.Item, "");

        //LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Continue, null);
        LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Select, null);
        
	}
}
