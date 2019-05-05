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

	public void LoadMainMenu()
	{	
		//	Load the main menu scene.	
		SceneManager.LoadScene(0);
	}

	public void LoadLevel()
	{
		//	Load the level scene.
		SceneManager.LoadScene(1);
	}
}
