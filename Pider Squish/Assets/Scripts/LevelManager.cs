using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	// GameManager instance Stuff.
	public static LevelManager Instance { get; set; }
	// GameManager instance Stuff End.

	//	Spray spawning stuff
	private float timeSinceLastSpraySpawn; //--------------- delete if not needed

	//	The count down text componant.
	public TextMeshProUGUI countDownText;

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

	//	----- The spiders speed to be increased in the coroutine.
	//	The speed the spiders start with.
	public float spiderSpeed = 1f;
	//	The spiders maximum speed.
	public float spiderMaxSpeed = 2f;
	
	//	----- Tutorial stuff
	//	Get access to the tutorial game object.
	public GameObject tutorialPanel;

	//	----- Game Over Stuff
	public bool gameOver = false;
	public GameObject gameOverPanel;
	//	Everything that has to be disabled when game is over
	public GameObject[] disableOnGameOverArray;
	//	The amount of spiders squished throughout all the games played
	private int squishedLifetimeBeforePlay;
	//	The amount of spiders squished this game
	private int squishedThisGame;
	//	The amount of spiders sprayed throughout all the games played
	private int sprayedLifetimeBeforePlay;
	//	The amount of spiders sprayed this game
	private int sprayedThisGame;
	//	The Squished this game text componant.
	public TextMeshProUGUI squishedThisGameText;
	//	Lifetime squishes after play
	public TextMeshProUGUI squishedLifetimeAfterPlayText;
	//	The Squished this game text componant.
	public TextMeshProUGUI sprayedThisGameText;
	//	Lifetime squishes after play
	public TextMeshProUGUI sprayedLifetimeAfterPlayText;
	//	New High Score text.
	public TextMeshProUGUI newHighScore;

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

		//	Get the amount of spiders squished throughout all the games played before the level is played.
		squishedLifetimeBeforePlay = PlayerPrefs.GetInt("SpidersSquished");
		//	Get the amount of spiders sprayed throughout all the games played before the level is played.
		sprayedLifetimeBeforePlay = PlayerPrefs.GetInt("SpidersSprayed");

		// Set the time scale to 1 on runtime
		Time.timeScale = 1;

		//Disable the new high score text at runtime.
		newHighScore.gameObject.SetActive(false);

		// Disable the tutorial panal at runtime
		tutorialPanel.SetActive(false);

		//	Disable the game over panel at runtime.
		gameOverPanel.SetActive(false);
		
		//	Set the spray count text to the player prefs spray count value.
		sprayCountText.text = PlayerPrefs.GetInt("SprayCount", 0).ToString();
		
		//	Find out how low the app has been running for, used in later calculations.
		timeSinceStart = Time.time;

		//	Disable the count down text at runtime.
		countDownText.gameObject.SetActive(false);

		//	Forloop to enable everything that gets disabled when game is over
		for (int i = 0; i < disableOnGameOverArray.Length; i++)
		{
			disableOnGameOverArray[i].SetActive(true);
		}
		//	Start the countdown coroutine
		StartCoroutine(CountDown());
	}

    void Update()
	{
		//Debug.Log(Time.timeScale);
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
	#region Open / Close Tutorial
	//	Open the tutorial
	public void OpenSprayTutorial()
	{
		Time.timeScale = 0;
		//	Enable the tutorial panel
		tutorialPanel.SetActive(true);
	}

	public void CloseSprayTutorial()
	{
		Time.timeScale = 1;
		//	Disable the tutorial panel
		tutorialPanel.SetActive(false);
		//	Create a key to tell if the tutorial has been opened before
		PlayerPrefs.SetString("HasSprayedBefore", "Yes");
		//	Play the button SFX
		SoundManager.Instance.ButtonSFX();
	}
	#endregion

	public void GameOver()
	{
		#region Squished Display Stuff
		//	Get the amount of spiders squished this game.
		squishedThisGame = PlayerPrefs.GetInt("SpidersSquished") - squishedLifetimeBeforePlay;
		//	Display the amount of spiders squished this game.
		squishedThisGameText.text = squishedThisGame.ToString();
		//	Get the new amount of spiders squished over lifetime.
		squishedLifetimeAfterPlayText.text = PlayerPrefs.GetInt("SpidersSquished").ToString();
		#endregion

		#region Sprayed Display Stuff
		//	Get the amount of spiders sprayed this game.
		sprayedThisGame = PlayerPrefs.GetInt("SpidersSprayed") - sprayedLifetimeBeforePlay;
		//	Display the amount of spiders sprayed this game.
		sprayedThisGameText.text = sprayedThisGame.ToString();
		//	Get the new amount of spiders sprayed over lifetime.
		sprayedLifetimeAfterPlayText.text = PlayerPrefs.GetInt("SpidersSprayed").ToString();
		#endregion

		//	Create a score from the time played value.
		currentScore = timePlayed;
		//	If the current score is higher than the players high score...
		if (currentScore > PlayerPrefs.GetFloat("HighScore", 0))
		{
			//	Set the high score to the current score
			PlayerPrefs.SetFloat("HighScore", currentScore);
			Debug.Log("New High Score");
			//	Start the new high score text coroutine.
			StartCoroutine(NewHighScore());
		}

		//	Forloop to enable everything that gets disabled when game is over
		for (int i = 0; i < disableOnGameOverArray.Length; i++)
		{
			disableOnGameOverArray[i].SetActive(false);
		}

		//	Set the time scaleto 0 / Pause everything.
		Time.timeScale = 0;
		//	Enablethe game over panel
		gameOverPanel.SetActive(true);
		//	Set the game over bool to true.
		gameOver = true;
	}

	IEnumerator CountDown()
	{   
		countDownText.gameObject.SetActive(true);
		countDownText.text = 3.ToString();
		SoundManager.Instance.PlayCountDownSFX();
		yield return new WaitForSecondsRealtime(1);
		countDownText.text = 2.ToString();
		SoundManager.Instance.PlayCountDownSFX();
		yield return new WaitForSecondsRealtime(1);
		countDownText.text = 1.ToString();
		SoundManager.Instance.PlayCountDownSFX();
		yield return new WaitForSecondsRealtime(1);
		countDownText.text = "GO";
		SoundManager.Instance.PlayCountDownSFX();
		yield return new WaitForSecondsRealtime(1);
		countDownText.gameObject.SetActive(false);

		//	Store the Time.time value when gameplay started after the countdown finishes.
		gamePlayHasStarted = Time.time;
		//	Start the spider speed increase coroutine after the countdown finishes.
		StartCoroutine(SpiderSpeed());
		//	Start the SprayLastSpawned coroutine after the countdown finishes.
		StartCoroutine(SprayLastSpawned());
	}

	IEnumerator SpiderSpeed()
	{
		if(spiderSpeed < spiderMaxSpeed)
		{
			yield return new WaitForSecondsRealtime(15);
			spiderSpeed = spiderSpeed + 0.5f;
			Debug.Log("speed inscreased");
			StartCoroutine(SpiderSpeed()); 
		}
		else if(Time.timeScale == 0)
		{
			Debug.Log("Max Speed Reached");
		}
	}

	IEnumerator SprayLastSpawned()
	{
		if(Time.timeScale == 1)
		{
			yield return new WaitForSecondsRealtime(1);
			float sprayLastSpawnedValue = PlayerPrefs.GetFloat("SprayLastSpawned", 0);
			//	Add the time played to the the player prefs "SprayLastSpawned value".
			//float newSprayLastSpawnedValue = sprayLastSpawnedValue + timePlayed;
			float newSprayLastSpawnedValue = sprayLastSpawnedValue + 1;
			//	Change the player prefs "SprayLastSpawned" value to the newSprayLastSpawnedValue
			PlayerPrefs.SetFloat("SprayLastSpawned", newSprayLastSpawnedValue);
			//Debug.Log(newSprayLastSpawnedValue);
			StartCoroutine(SprayLastSpawned());
		}
		else
		{
			yield return new WaitForSecondsRealtime(1);
			StartCoroutine(SprayLastSpawned());
		}
		
	}

	IEnumerator NewHighScore()
	{
		newHighScore.gameObject.SetActive(true);
		yield return new WaitForSecondsRealtime(1.2f);
		newHighScore.gameObject.SetActive(false);
	}

	#region Scene Loading Stuff
	public void LoadMainMenu()
	{   //	Play the button SFX
		SoundManager.Instance.ButtonSFX();
		//	Load the main menu scene.	
		SceneManager.LoadScene(0);
	}

	public void LoadLevel()
	{
		//	Play the button SFX
		SoundManager.Instance.ButtonSFX();
		//	Load the level scene.
		SceneManager.LoadScene(1);
	}
	#endregion

	#region Debugging Buttons
	public void DeleteSprayKey()
	{
		PlayerPrefs.DeleteKey("SprayCount");
		//	Set the spray count text to the player prefs spray count value.
		sprayCountText.text = PlayerPrefs.GetInt("SprayCount", 0).ToString();
	}

	public void DeleteTutorialKey()
	{
		PlayerPrefs.DeleteKey("HasSprayedBefore");
		Debug.Log("Tutorial Key Deleted");
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

	public void DeleteAllKeys()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log("All Keys Deleted");
	}
	#endregion
}
