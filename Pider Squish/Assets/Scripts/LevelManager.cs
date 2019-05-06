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
	private const float PRE_START_COUNTDOWN = 4;
	public bool countDownHasFinished = false;
	public GameObject[] countDownArray;
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
		
		//	Find out how low the app has been running for, used in later calculations.
		timeSinceStart = Time.time;
		//	For loop to disable all elements in the countDownArray.
		for(int i = 0; i < countDownArray.Length; i++)
		{
			countDownArray[i].SetActive(false);
		}
		//	TODO - comment this
		StartCoroutine(CountDown());
	}

    void Update()
    {
		//	Timer and time formatting stuff.
		//	Exit out of this method if the pre start countdown hasnt finished
		if (Time.time - timeSinceStart < PRE_START_COUNTDOWN)
		{
			countDownHasFinished = false;
			return;
		}
		else
		{
			countDownHasFinished = true;
		}
		timePlayed = Time.time - (timeSinceStart + PRE_START_COUNTDOWN);
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
		if (currentScore > PlayerPrefs.GetFloat("HighScore", 0))
		{
			PlayerPrefs.SetFloat("HighScore", currentScore);
		}
		GameManager.Instance.LoadMainMenu();

	}

	IEnumerator CountDown()
	{   //	Enable/Disable elemets from the Count down array.
		countDownArray[0].SetActive(true);
		//Debug.Log("3");
		yield return new WaitForSecondsRealtime(.95f);
		countDownArray[0].SetActive(false);
		countDownArray[1].SetActive(true);
		//Debug.Log("2");
		yield return new WaitForSecondsRealtime(.95f);
		countDownArray[1].SetActive(false);
		countDownArray[2].SetActive(true);
		//Debug.Log("1");
		yield return new WaitForSecondsRealtime(.95f);
		countDownArray[2].SetActive(false);
		countDownArray[3].SetActive(true);
		//Debug.Log("GO");
		yield return new WaitForSecondsRealtime(.95f);
		countDownArray[3].SetActive(false);
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

	public void AddSprays()
	{
		int sprayCount = PlayerPrefs.GetInt("SprayCount");
		sprayCount += 1;
		PlayerPrefs.SetInt("SprayCount", sprayCount);
	}
}
