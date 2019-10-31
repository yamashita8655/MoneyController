﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : SceneBase
{
    //// Start is called before the first frame update
    //void Start()
    //{
    //    
    //}

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
        LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.Continue, null);
    }
}
