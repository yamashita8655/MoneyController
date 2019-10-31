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

    public void OnClickDecideButton()
    {
		if (string.IsNullOrEmpty(CanUseMoneyInputField.text)) {
			return;
		}

		DateTime dt = DateTime.Now;
		string day = dt.ToString("yyyy/MM/dd");
		int year = int.Parse(dt.ToString("yyyy"));
		int month = int.Parse(dt.ToString("MM"));
		int daysInMonth  = DateTime.DaysInMonth(year, month);

		int canUseMoney = int.Parse(CanUseMoneyInputField.text);

		var PPM = PlayerPrefsManager.Instance;
		PPM.SaveParameter(PlayerPrefsManager.SaveType.CanUseMoney, canUseMoney.ToString());
		PPM.SaveParameter(PlayerPrefsManager.SaveType.StartDays, day);
		PPM.SaveParameter(PlayerPrefsManager.SaveType.EndDays, string.Format("{0}/{1}/{2}", year, month, daysInMonth));

		// 使った用途のセーブ情報リセット
		PPM.SaveParameter(PlayerPrefsManager.SaveType.Item, "");

        LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Continue, null);
        
    }
}
