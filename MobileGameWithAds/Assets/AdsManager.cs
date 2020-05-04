using UnityEngine;
using GoogleMobileAds.Api;
using System;


public class AdsManager : MonoBehaviour {

	#region AdMob
	[Header("Admob")]
	private string adMobAppID = "";
    private string videoAdMobId = "ca-app-pub-3940256099942544/5224354917";
	private BannerView bannerView;
	InterstitialAd interstitialAdMob;
	private RewardBasedVideoAd rewardBasedAdMobVideo; 
	AdRequest requestAdMobInterstitial, AdMobVideoRequest;
	#endregion
	[Space(15)]
	

	static AdsManager instance;
	public static AdsManager Instance
	{
		get
		{
			if(instance == null)
				instance = GameObject.FindObjectOfType(typeof(AdsManager)) as AdsManager;
			
			return instance;
		}
	}

	void Awake ()
	{
		gameObject.name = this.GetType().Name;
		DontDestroyOnLoad(gameObject);
		InitializeAds();	
	}
	
	public void ShowInterstitial()
	{
		ShowAdMob();
	}

	public void VideoAvailable()
	{
		if(isVideoAvaiable())
		{
			ShowVideoReward();
		}
		
	}

	public void ShowVideoReward()
	{
		if(rewardBasedAdMobVideo.IsLoaded())
		{
			AdMobShowVideo();
		}
	}

	private void RequestInterstitial()
	{

		interstitialAdMob = new InterstitialAd("ca-app-pub-3940256099942544/1033173712");

	
		requestAdMobInterstitial = new AdRequest.Builder().Build();
		interstitialAdMob.LoadAd(requestAdMobInterstitial);
	}

	public void ShowAdMob()
	{
		if(interstitialAdMob.IsLoaded())
		{
			interstitialAdMob.Show();
		}
		else
		{
			interstitialAdMob.LoadAd(requestAdMobInterstitial);
		}
	}



	private void RequestRewardedVideo()
	{
	
		AdMobVideoRequest = new AdRequest.Builder().Build();
		this.rewardBasedAdMobVideo.LoadAd(AdMobVideoRequest, videoAdMobId);
	}



	void InitializeAds()
	{
		MobileAds.Initialize(adMobAppID);
		this.RequestBanner();
		this.rewardBasedAdMobVideo = RewardBasedVideoAd.Instance;
		this.RequestRewardedVideo();
		RequestInterstitial();
	}


	void AdMobShowVideo()
	{
		rewardBasedAdMobVideo.Show();	
	}



	bool isVideoAvaiable()
	{
		#if !UNITY_EDITOR
		 if(rewardBasedAdMobVideo.IsLoaded())
		{
			return true;
		}
		#endif
		return false;
	}

	private void RequestBanner()
	{
		
		bannerView = new BannerView("ca-app-pub-3940256099942544/6300978111", AdSize.Banner, AdPosition.TopRight);
		

		AdRequest request = new AdRequest.Builder().Build();

		bannerView.LoadAd(request);
	}

}
