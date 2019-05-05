using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
	//	High Score Stuff
	public float highScore;
	public float currentScore;

	void Awake()
	{   // LevelManager instance Stuff.
		//Check if instance already exists
		if (Instance == null)
		{
			//if not, set instance to this
			Instance = this;
		}
		//If instance already exists and it's not this:
		else if (Instance != this)
		{

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a LevelManager.
			Destroy(gameObject);

		}
		//DontDestroyOnLoad(gameObject);
		// LevelManager instance Stuff End.
	}
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
		currentScore = timePlayed;
		Debug.Log("Current Score is " + currentScore);
		if (currentScore > PlayerPrefs.GetFloat("HighScore", 0))
		{
			PlayerPrefs.SetFloat("HighScore", currentScore);
		}
		Debug.Log("High Score Is " + PlayerPrefs.GetFloat("HighScore"));
		GameManager.Instance.LoadMainMenu();

	}

	public void DeleteSprayKey()
	{
		PlayerPrefs.DeleteKey("SprayCount");
		//	Set the spray count text to the player prefs spray count value.
		sprayCountText.text = PlayerPrefs.GetInt("SprayCount", 0).ToString();
	}

	public void DeleteHSKey()
	{
		PlayerPrefs.DeleteKey("HighScore");
		Debug.Log("High Score Key Deleted");
	}
}
