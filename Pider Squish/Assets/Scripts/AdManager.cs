using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
//https://www.youtube.com/watch?v=XHTTjyRzxmw

public class AdManager : MonoBehaviour
{
	public static AdManager Instance;

	[Header("Config")]
	[SerializeField] private string gameID = "";
	[SerializeField] private bool testMode = true;
	[SerializeField] private string rewardedVideoPlacementID;
	[SerializeField] private string regularPlacementID;


	void Awake()
	{
		#region Instance Stuff
		// GameManager instance Stuff.
		//Check if instance already exists
		if (Instance == null)
		{
			//if not, set instance to this
			Instance = this;
		}
		//If instance already exists and it's not this:
		else if (Instance != this)
		{

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);

		}
		DontDestroyOnLoad(gameObject);
		// GameManager instance Stuff End.
		#endregion

		Advertisement.Initialize(gameID, testMode);
	}

	public void ShowRegularAd(Action<ShowResult> callback)
	{
#if UNITY_ADS
		if (Advertisement.IsReady(regularPlacementID))
		{
			ShowOptions so = new ShowOptions();
			so.resultCallback = callback;
			Advertisement.Show(regularPlacementID, so);
		}
		else
		{
			Debug.Log("Ad not ready yet.");
		}
#else
		Debug.Log("Ads not supported");
#endif
	}

	public void ShowRewardedAd(Action<ShowResult> callback)
	{
#if UNITY_ADS
		if (Advertisement.IsReady(rewardedVideoPlacementID))
		{
			ShowOptions so = new ShowOptions();
			so.resultCallback = callback;
			Advertisement.Show(rewardedVideoPlacementID, so);
		}
		else
		{
			Debug.Log("Ad not ready yet.");
		}
#else
		Debug.Log("Ads not supported");
#endif
	}
}
