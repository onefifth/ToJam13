using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomlyFlipSprite : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(GetComponent<SpriteRenderer>())
        {
            if(Random.value < 0.5f)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

        }

	}
	
}
