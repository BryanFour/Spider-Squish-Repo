using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	// GameManager instance Stuff.
	public static GameManager Instance { get; set; }
	// GameManager instance Stuff End.

	public TextMeshProUGUI sprayCountText;
	public TextMeshProUGUI timerText;
	private float timePlayed;
	
	void Awake()
	{	// GameManager instance Stuff.
		if (Instance == null) { Instance = this; } else { Debug.Log("Warning: multiple " + this + " in scene!"); }
		// GameManager instance Stuff End.
	}
	void Start()
    {
		//	Set the spray count text to the player prefs spray count value.
		sprayCountText.text = PlayerPrefs.GetInt("SprayCount", 0).ToString();
    }

	private void Update()
	{
		//	Timer and time formatting stuff.
		timePlayed = Time.time;
		string minutes = ((int)timePlayed / 60).ToString("00"); // Used to have the timer show in seconds and minutes rather that just seconds.
		string seconds = (timePlayed % 60).ToString("00.00"); // Used to have the timer show in seconds and minutes rather that just seconds.
		timerText.text = minutes + ":" + seconds;
		//	Timer and time formatting stuff end.
	}

	public void UpdateSprayCount()
	{
		//	Set the spray count text to the player prefs spray count value.
		sprayCountText.text = PlayerPrefs.GetInt("SprayCount", 0).ToString();
		
	}

	public void DeleteKey()
	{
		PlayerPrefs.DeleteKey("SprayCount");
		//	Set the spray count text to the player prefs spray count value.
		sprayCountText.text = PlayerPrefs.GetInt("SprayCount", 0).ToString();
	}
}
