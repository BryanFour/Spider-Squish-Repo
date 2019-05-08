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

	//	The countdown aray --- TODO Change how this works.
	public GameObject[] countDownArray;

	//	Spray Count Stuff.
	public TextMeshProUGUI sprayCountText;
	
	//	----- Timer Stuff.
	//	The Timer text componant.
	public TextMeshProUGUI timerText;
	//	How long the player has been playing not including the countdown.
	private float timePlayed;
	//	How long the app has been running for.
	public float timeSinceStart;
	//	How long the countdown is.
	private const float PRE_START_COUNTDOWN = 4;
	//	A bool to tell weather the countdown has finished or not.
	public bool countDownHasFinished = false;
	//	When the game started / when the player starts playing, spiders come out, timers starts ect.
	public float gamePlayHasStarted;

	//	High Score Stuff
	public float highScore; //--------- dosnt seem to be used anywhere.
	//	The players current score (How long the player lasted with dieing).
	public float currentScore;

	//	The spiders speed to be increased in the coroutine.
	public float spiderStartSpeed = 1.5f;
	public float spiderMaxSpeed = 2f;


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
		//	Keep track of how long the player has been playing.
		timePlayed = Time.time - (timeSinceStart + PRE_START_COUNTDOWN);
		//	Format the timer time.
		string minutes = ((int)timePlayed / 60).ToString("00"); // Used to have the timer show in seconds and minutes rather that just seconds.
		string seconds = (timePlayed % 60).ToString("00.00"); // Used to have the timer show in seconds and minutes rather that just seconds.
		//	Display the time.
		timerText.text = minutes + ":" + seconds;
	}

	public void UpdateSprayCount()
	{
		//	Set the spray count text to the player prefs spray count value.
		sprayCountText.text = PlayerPrefs.GetInt("SprayCount", 0).ToString();

	}

	public void GameOver()
	{
		//	Create a score from the time played value.
		currentScore = timePlayed;
		//	If the current score is higher than the players high score...
		if (currentScore > PlayerPrefs.GetFloat("HighScore", 0))
		{
			//	Set the high score to the current score
			PlayerPrefs.SetFloat("HighScore", currentScore);
		}
		//	Load the main menu scene
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

		//	Store the Time.time value when gameplay started
		gamePlayHasStarted = Time.time;
		//	Start the spider speed increase coroutine.
		StartCoroutine(SpiderSpeed());
	}

	IEnumerator SpiderSpeed()
	{
		if(spiderStartSpeed < spiderMaxSpeed)
		{
			yield return new WaitForSecondsRealtime(15);
			spiderStartSpeed = spiderStartSpeed + 0.5f;
			Debug.Log("Spider speed increased");
			StartCoroutine(SpiderSpeed()); ;
		}
		else
		{
			Debug.Log("Max speed reached");
		}
	}

	#region Debugging Buttons
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
	#endregion
}
