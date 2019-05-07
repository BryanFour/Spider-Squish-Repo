using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// Follow Mouse - https://www.youtube.com/watch?v=7OJQ6MbHuvQ

public class SprayController : MonoBehaviour
{
	//	Move With Mouse Stuff.
	private float actualDistance;
	//	Bool to stop players activating can while acan is already active
	public bool canActive = false;
	// How long we can spray for
	private float lengthOfSpray = 10; // ----------- I dont seem to be using this.
	//	How long the spray cooldown is.
	private float sprayCoolDownLength = 15;
	//	where we are in the cooldown
	private int coolDownValue = 15;
	//	The poit in time that we started the cooldown
	private float coolDownStartTime;
	//	A bool to tell if we are on cooldown or not.
	private bool onCoolDown;
	//	Cans children Plus the spray trail gameobject
	public Transform[] childrenArray;
	// CoolDown text componant
	public TextMeshProUGUI coolDownText;
	

    void Start()
    {
		// Disable all the can parts
		for (int i = 0; i < childrenArray.Length; i++)
		{
			childrenArray[i].gameObject.SetActive(false);
		}
		//	Trick the app into letting us use the spray immediately
		coolDownStartTime = Time.time - sprayCoolDownLength;
		// Disable the cooldown text on runtime.
		coolDownText.gameObject.SetActive(false);

		//	Move With Mouse Stuff.
		Vector3 toObjectVevtor = transform.position - Camera.main.transform.position;
		Vector3 linearDistanceVector = Vector3.Project(toObjectVevtor, Camera.main.transform.forward);
		actualDistance = linearDistanceVector.magnitude;
	}

	void Update()
    {
		//	Move with Mouse Stuff.
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = actualDistance;
		transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
	}

	public void ActivateCan()
	{   //	If the spray can is not active and we have atleast 1 in our inventory and the pre start countdown has finished and we are not on cooldown.
		//--- can active bool Probally not need now we have a onCoolDown bool
		if (!canActive && PlayerPrefs.GetInt("SprayCount") > 0 && LevelManager.Instance.countDownHasFinished == true && onCoolDown == false)
		{
			if (sprayCoolDownLength < Time.time - coolDownStartTime)
			{
				// Enable all the can parts
				for (int i = 0; i < childrenArray.Length; i++)
				{
					childrenArray[i].gameObject.SetActive(true);
				}
				//	Bool to stop players activating can while acan is already active
				canActive = true;
				//	PlayerPrefs Stuff.
				int sprayCount = PlayerPrefs.GetInt("SprayCount");
				sprayCount -= 1;
				PlayerPrefs.SetInt("SprayCount", sprayCount);
				LevelManager.Instance.UpdateSprayCount();
				//	Start the spray can SFX.
				SoundManager.Instance.StartSpraySFX();
				StartCoroutine(SprayDuration());
			}
		}
	}

	IEnumerator SprayDuration()
	{
		yield return new WaitForSecondsRealtime(lengthOfSpray);
		//	Bool to stop players activating can while acan is already active
		canActive = false;
		onCoolDown = true;
		coolDownStartTime = Time.time;
		// Disable all the can parts
		for (int i = 0; i < childrenArray.Length; i++)
		{
			childrenArray[i].gameObject.SetActive(false);
		}
		//	Enable the cooldowntext componant
		coolDownText.gameObject.SetActive(true);
		//	Start the cooldown coroutine.
		StartCoroutine(CoolDown());
	}

	IEnumerator CoolDown()
	{
		if(coolDownValue >= 1)
		{
			coolDownText.text = coolDownValue.ToString();
			coolDownValue -= 1;
			yield return new WaitForSecondsRealtime(1);
			StartCoroutine(CoolDown());
		}
		else
		{
			//	Disable the cooldowntext componant
			coolDownText.gameObject.SetActive(false);
			//	Set the cooldownvalue to 15
			coolDownValue = 15;
			//	Set the cooldown bool to false
			onCoolDown = false;
		}
	}
}
