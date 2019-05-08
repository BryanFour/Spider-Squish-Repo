using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCollision : MonoBehaviour
{
	//	Spiders sprayed count
	private int spidersSprayedCount;

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnParticleCollision(GameObject col)
	{
		//	get the amount of spiders sprayed.
		spidersSprayedCount = PlayerPrefs.GetInt("SpidersSprayed", 0);
		//	Add 1 to the spider squished count
		spidersSprayedCount += 1;
		//	set the player prefs SpidersSquished value to the new spidersSquishedcount.
		PlayerPrefs.SetInt("SpidersSprayed", spidersSprayedCount);
		Debug.Log("Spiders Sprayed = " + PlayerPrefs.GetInt("SpidersSprayed"));

		//	Play the die SFX	
		SoundManager.Instance.DieSFX();
		Destroy(transform.parent.gameObject);
	}
}
