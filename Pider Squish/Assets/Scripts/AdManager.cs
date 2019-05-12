using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
	public static AdManager instance;

	public string appID = "ca-app-pub-6894419772863169~2522741580";

	private BannerView bannerView;
	private string bannerID = "	ca-app-pub-3940256099942544/6300978111";

	private InterstitialAd fullScreenAd;
	private string fullScreenAdID = "ca-app-pub-3940256099942544/8691691433";

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		RequestFullScreenAd();
	}

	public void RequestBanner()
	{
		bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

		AdRequest request = new AdRequest.Builder().Build();

		bannerView.LoadAd(request);

		bannerView.Show();
	}

	public void HideBanner()
	{
		bannerView.Hide();
	}

	public void RequestFullScreenAd()
	{
		fullScreenAd = new InterstitialAd(fullScreenAdID);

		AdRequest request = new AdRequest.Builder().Build();

		fullScreenAd.LoadAd(request);
	}

	public void ShowFullScreenAd()
	{
		if (fullScreenAd.IsLoaded())
		{
			fullScreenAd.Show();
		}
		else
		{

			Debug.Log("Full Screen Ad Not Loaded");
		}
	}
}	
	

