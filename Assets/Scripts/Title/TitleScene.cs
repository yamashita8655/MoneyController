using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : SceneBase
{
	[SerializeField]
	private Button NewButton = null;
    // Start is called before the first frame update
    void Start()
    {
		var PPM = PlayerPrefsManager.Instance;
		string saveIdList = PPM.GetParameter(PlayerPrefsManager.SaveType.SaveIdList);
		NewButton.interactable = true;
		if (string.IsNullOrEmpty(saveIdList) == false) {
			string[] idList = saveIdList.Split(',');
			bool isEmpty = false;
			for (int i = 0; i < idList.Length; i++) {
				if (string.IsNullOrEmpty(idList[i]) == true) {
					isEmpty = true;
					break;
				}
			}
			if (isEmpty == false) {
				NewButton.interactable = false;
			}
		}
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    
    //}
	//
	
	public void OnClickStartButton() {
		LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.New, null);
	}

    public void OnClickContinueButton()
    {
        LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Select, null);
    }

    public void OnClickPrivacyButton()
    {
        Application.OpenURL("https://natural-nail-eye.sakura.ne.jp/privacy_policy.html");
    }
}
