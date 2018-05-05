using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject FoliagePrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ( Random.value > 0.8f ) {
            GameObject newFoliage = GameObject.Instantiate(FoliagePrefab, this.transform);
            newFoliage.transform.Translate(new Vector3((Random.value * 20f) - 10f, 0f, 0f));
 
        }
	}
}
