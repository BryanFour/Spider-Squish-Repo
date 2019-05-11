using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	// GameManager instance Stuff.
	public static GameManager Instance { get; set; }
	// GameManager instance Stuff End.

	//	Is this the first time playing the game.
	public bool hasPlayedBefore = false;    //--------- Remove this if its not needed.

	

	private GameObject highScoreText;

	void Awake()
	{   // GameManager instance Stuff.
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
	}

	private void Start()
	{
		#region Has Played Before
		//	If the player has played before.	
		if (PlayerPrefs.HasKey("HasPlayedBefore") == true)
		{
			hasPlayedBefore = true;
		}
		//	If the player hasnt played before
		else if(PlayerPrefs.HasKey("HasPlayedBefore") == false)
		{
			hasPlayedBefore = false;
			//	Run the FirstTimePlaying method.
			FirstTimePlaying();
		}
		#endregion

		
	}

	public void Update()
	{
		//	Display the high score on the main menu scene
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;
		if(sceneName == "MainMenu")
		{
			UpdateHighScore();
		}
	}

	private void FirstTimePlaying()
	{
		PlayerPrefs.SetInt("SprayCount", 5);
		PlayerPrefs.SetString("HasPlayedBefore", "Yes");
		hasPlayedBefore = true;
	}

	

	private void UpdateHighScore()
	{
		//	Get access to the high score text componant.
		TextMeshProUGUI highScoreText = GameObject.Find("HighScoreValue").GetComponent<TextMeshProUGUI>();
		//	Store the high score value inside a temp float "highScore".
		float highScore = PlayerPrefs.GetFloat("HighScore", 0);
		//	Make the time show in minutes and seconds.
		string minutes = ((int)highScore / 60).ToString("00"); // Used to have the timer show in seconds and minutes rather that just seconds.
		string seconds = (highScore % 60).ToString("00.00"); // Used to have the timer show in seconds and minutes rather that just seconds.
		//	Change the high score text componant to our high score.
		highScoreText.text = minutes + ":" + seconds;
	}

	public void LoadMainMenu()
	{
		//	Play the button SFX
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

	public void QuitGame()
	{
		//	Play the button SFX
		SoundManager.Instance.ButtonSFX();
		//	Quit the game
		Application.Quit();
	}
}
