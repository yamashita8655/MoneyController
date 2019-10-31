using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour {

	public void Initialize () {
		ResourceManager.Instance.Initialize();
		PlayerPrefsManager.Instance.Initialize();
		SerializeFieldResourceManager.Instance.Initialize();
		LocalSceneManager.Instance.Initialize();
		LocalSceneManager.Instance.LoadScene(LocalSceneManager.Instance.GetFirstSceneName(), null);
	}

	//// Use this for initialization
	//public void Initialize () {
	//	StartCoroutine(CoInitialize());
	//}

	//public IEnumerator CoInitialize () {
	//	Debug.Log("EntryPoint CoInitialize before");
	//	// ↑ここまでは、同一フレーム内で実行される

	//	// ↓ここで処理が返ると、他のMonoBehaviourのStartやAwakeが呼び出されてしまい
	//	// 初期化の役割を果たせなくなる
	//	// なので、初期化の役割を果たさせる場合は
	//	// 1．コルーチンを使わないようにするか
	//	// 2．有効となるシーンの先々で、ここで行う初期化が完了しているかどうかを待つ
	//	// 3．この初期化が終わるまで、シーン上に有効なオブジェクトを配置しない。つまり、ここで配置を行うようにする
	//	// どれかになるのではないかと思われる。
	//	yield return null;
	//	Debug.Log("EntryPoint CoInitialize after");
	//}

	//// Use this for initialization
	//void Awake () {
	//	Debug.Log("EntryPoint Awake");
	//}

	//// Use this for initialization
	//IEnumerator Start () {
	//	// 今回の実験と関係ないけど、IEnumeratorが戻り値のStartは、デフォで用意されているっぽい
	//	// 自分ではStartCoroutineやMoveNextは呼び出していないが、自動的に最後の処理まで実行される
	//	Debug.Log("EntryPoint CoStart before");
	//	yield return null;
	//	Debug.Log("EntryPoint CoStart after");
	//}
}
