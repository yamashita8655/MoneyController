using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SerializeFieldResourceManager : BestPracticeSingleton<SerializeFieldResourceManager> {
	[SerializeField]
	private Sprite[] CuHoldImages = null;
	public Sprite[] HoldImages => CuHoldImages;
	
	[SerializeField]
	private GameObject[] CuEffectObjects = null;

	public enum EffectObject : int {
		Ready = 0,
		Go,
	};

	public void Initialize() {
		if ((CuHoldImages == null) || (CuHoldImages.Length == 0)) {
			Debug.Log("SerializeFieldResourceManager:CuHoldImages error");
		}
		
		if ((CuEffectObjects == null) || (CuEffectObjects.Length == 0)) {
			Debug.Log("SerializeFieldResourceManager:CuEffectObjects error");
		}
	}
	
	// 格納されている物は、Rawデータとして扱うので、コピーして返す
	public GameObject GetEffectObject(EffectObject type) {
		GameObject obj = GameObject.Instantiate(CuEffectObjects[(int)type]) as GameObject;
		return obj;
	}

}
