using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceManager : SimpleMonoBehaviourSingleton<ResourceManager> {
	private Dictionary<string, UnityEngine.Object> CacheDict = new Dictionary<string, UnityEngine.Object>();

	public void Initialize() {
    }
	
    // あくまで、返すのはRAWデータなので、ユニーク扱いしたい場合は、
    // 呼び出し側でコピーやInstantiateをする。
    // 参照系（画像など）は、多分そのまま使っても大丈夫だとは思う
	public void Load(string path, Action<UnityEngine.Object> loadedCallback) {
        if (CacheDict.ContainsKey(path)) {
            loadedCallback(CacheDict[path]);
            return;
        }
        UnityEngine.Object loadedObj = Resources.Load(path);
        CacheDict.Add(path, loadedObj);
        loadedCallback(loadedObj);
    }
}
