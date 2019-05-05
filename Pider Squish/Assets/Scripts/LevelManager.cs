using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
	// GameManager instance Stuff.
	public static LevelManager Instance { get; set; }
	// GameManager instance Stuff End.

	public TextMeshProUGUI sprayCountText;
	//	Timer Stuff.
	public TextMeshProUGUI timerText;
	private float timePlayed;
	public float timeSinceStart;


	void Start()
    {
		//	Set the spray count text to the player prefs spray count value.
		sprayCountText.text = PlayerPrefs.GetInt("SprayCount", 0).ToString();

		//	Reset Time.time Stuff.
		timeSinceStart = Time.time;
	}

    void Update()
    {
		//	Timer and time formatting stuff.
		timePlayed = Time.time - timeSinceStart;
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

	public void GameOver()
	{
		Debug.Log("Game Over method ran.");
	}

	public void DeleteKey()
	{
		PlayerPrefs.DeleteKey("SprayCount");
		//	Set the spray count text to the player prefs spray count value.
		sprayCountText.text = PlayerPrefs.GetInt("SprayCount", 0).ToString();
	}
}
