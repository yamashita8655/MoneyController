using UnityEngine;
using System;
using System.Collections;
using GoogleMobileAds.Api;

public class GoogleAdmobManager : SimpleSingleton<GoogleAdmobManager> {

//支出メモるくんca-app-pub-4566588771611947~6217312383
//リワード導入ガイドに沿って SDK を組み込んでください。広告の種類とプレースメントは、この広告ユニット ID を使用してコードを設定する際に指定します。
//動画リワードca-app-pub-4566588771611947/3118158711
#if UNITY_EDITOR
	// 広告ユニット ID を記述します
	private string appId = "unexpected_platform";// テストらしい
	private string adUnitId = "unexpected_platform";// テストらしい
	private string interstitialAdUnitId = "unexpected_platform";// テストらしい
#elif UNITY_ANDROID
	private string appId = "ca-app-pub-4566588771611947~6217312383";
	private string adUnitId = "ca-app-pub-4566588771611947/3118158711";// 本番
	private string interstitialAdUnitId = "ca-app-pub-4566588771611947/3399577356";// 本番
	//private string adUnitId = "ca-app-pub-3940256099942544/5224354917";// テスト
	//private string interstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712";// (Android)
#elif UNITY_IPHONE
	//private string appId = "ca-app-pub-7884723313792273~7289925750";
	//private string adUnitId = "ca-app-pub-7884723313792273/3350680742";// 本番
	//private string interstitialAdUnitId = "ca-app-pub-4566588771611947/3399577356";// (Android)
	//private string adUnitId = "ca-app-pub-3940256099942544/1712485313";// テスト
#endif

	private InterstitialAd InterstitialAd = null;

	private RewardBasedVideoAd RewardBasedVideo;

	private Action<bool> RewardCallback = null;
	private Action<bool> RequestVideoCallback = null;

	private bool IsInitializedFlag = false;

	public bool IsInitialized() {
		return IsInitializedFlag;
	}

	public void Initialize() {

		if (IsInitializedFlag == true) {
			return;
		}

		if (Application.internetReachability == NetworkReachability.NotReachable) {
			return;
		}

		IsInitializedFlag = true;

		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(appId);

		// Get singleton reward based video ad reference.
		RewardBasedVideo = RewardBasedVideoAd.Instance;

		// Called when an ad request has successfully loaded.
		RewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
		// Called when an ad request failed to load.
		RewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
		// Called when an ad is shown.
		RewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
		// Called when the ad starts to play.
		RewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
		// Called when the user should be rewarded for watching a video.
		RewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
		// Called when the ad is closed.
		RewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
		// Called when the ad click caused the user to leave the application.
		RewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

		//RequestRewardBasedVideo();
		
		// インターステーシャルの初期化
		InterstitialAd = new InterstitialAd(interstitialAdUnitId);
		InterstitialAd.OnAdFailedToLoad += OnAdFailedToLoad;
		InterstitialAd.OnAdLoaded += OnAdLoaded;
		InterstitialAd.OnAdClosed += OnAdClosed;
	}

	public void RequestRewardBasedVideo(Action<bool> requestVideoCallback)
	{
#if UNITY_EDITOR
		if (requestVideoCallback != null) {
			requestVideoCallback(true);
		}
#else
		RequestVideoCallback = requestVideoCallback;

	 	// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded video ad with the request.
		RewardBasedVideo.LoadAd(request, adUnitId);
#endif
	}

	public bool IsVideoLoaded() {
		return RewardBasedVideo.IsLoaded();
	}

	public void ShowVideo(Action<bool> rewardCallback) {
#if UNITY_EDITOR
		if (rewardCallback != null) {
			rewardCallback(true);
		}
#else
		RewardCallback = rewardCallback;
		RewardBasedVideo.Show();
#endif
	}

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		Debug.Log("HandleRewardBasedVideoLoaded");
		if (RequestVideoCallback != null) {
			RequestVideoCallback(true);
		}
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		Debug.Log("HandleRewardBasedVideoFailedToLoad");
		if (RequestVideoCallback != null) {
			RequestVideoCallback(false);
		}
	}

	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		Debug.Log("HandleRewardBasedVideoOpened");
#if UNITY_IOS
        MobileAds.SetiOSAppPauseOnBackground(true);
#endif
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		Debug.Log("HandleRewardBasedVideoStarted");
	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		Debug.Log("HandleRewardBasedVideoClosed");
        //RequestRewardBasedVideo();
#if UNITY_IOS
        MobileAds.SetiOSAppPauseOnBackground(false);
#endif
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		Debug.Log("HandleRewardBasedVideoRewarded");
		string type = args.Type;
		double amount = args.Amount;
        if (RewardCallback != null) {
            RewardCallback(true);
        }
    }

	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
	}
	
	// インターステーシャル
	public void OnAdLoaded(object sender, System.EventArgs arg) {
		InterstitialAd.Show();
	}

	public void OnAdClosed(object sender, System.EventArgs arg) {
	}

	public void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs arg) {
	}

	public bool IsInterstitialAdInitialized() {
		return InterstitialAd.IsLoaded();
	}
	
	// 流れとしては
	// LoadAd->ちょっとかかる->OnAdLoaded->Show()で表示
	// Show()を行うと、自動的にLoadAdが呼び出されて準備が整うっぽい
	public void ShowInterstitial() {
#if UNITY_EDITOR
#else
		AdRequest request = new AdRequest.Builder().Build();
		InterstitialAd.LoadAd(request);
#endif
	}
}

