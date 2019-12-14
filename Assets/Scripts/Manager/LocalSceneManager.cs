using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalSceneManager : SimpleMonoBehaviourSingleton<LocalSceneManager> {

	private List<string> SceneNameList = new List<string>() {
		"Title",
		"New",
		"Continue",
		"Payment",
		"History",
		"Select"
	};

	public enum SceneName : int {
		Title = 0,
		New,
		Continue,
		Payment,
		History,
		Select,
		None
	};

	private SceneName FirstSceneName = SceneName.Title;

	private SceneName CurrentSceneName = SceneName.None;
	
	public SceneDataBase SceneData { get; private set;}

	public void Initialize() {
		SceneData = null;
	}
	
	public SceneName GetFirstSceneName() {
		return FirstSceneName;
	}
	
	public void LoadScene(SceneName name, SceneDataBase sceneData) {
		SceneData = sceneData;

		// 本来は、この辺りでフェードなどの切り替え処理が入るので、
		// LoadとUnloadは一辺に行うべきではない
		SceneManager.LoadScene(SceneNameList[(int)name], LoadSceneMode.Additive);
		if (CurrentSceneName != SceneName.None) {
			SceneManager.UnloadSceneAsync(SceneNameList[(int)CurrentSceneName]);
		}

		CurrentSceneName = name;
	}
}
