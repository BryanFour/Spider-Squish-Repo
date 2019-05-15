using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : MonoBehaviour
{	
	//	Call the regular ad from here, not from the AdManager
    public void PlayRegularAd()
	{
		AdManager.Instance.ShowRegularAd(OnAdClosed);
	}
	//	Call the rewarded ad from here, not from the AdManager
	public void PlayRewardedAd()
	{
		AdManager.Instance.ShowRewardedAd(OnRewardedAdClosed);
	}

	private void OnAdClosed(ShowResult result)
	{
		Debug.Log("Regular ad closed");
	}

	private void OnRewardedAdClosed(ShowResult result)
	{
		Debug.Log("Rewarded ad closed");
		switch (result)
		{
			case ShowResult.Finished:
				Debug.Log("Ad finished, reward player");
				break;
			case ShowResult.Skipped:
				Debug.Log("Ad skipped, no reward");
				break;
			case ShowResult.Failed:
				Debug.Log("Ad failed");
				break;
		}
	}
}
