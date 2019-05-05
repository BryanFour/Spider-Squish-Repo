using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnParticleCollision(GameObject col)
	{
		Debug.Log("Collision with spray");
		Destroy(transform.parent.gameObject);
	}
}
