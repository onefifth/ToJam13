using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeObject : MonoBehaviour {

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public bool setX = true;

    private Biome biome;

    // Use this for initialization
    void Start () {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    private void ResetPosition() {
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;
    }

    public void OnSpawned( Biome b ) {
        biome = b;
        gameObject.SetActive(true);
        ResetPosition();
        if(setX){
            transform.position += Vector3.right * GameStateController.Singleton.player.transform.position.x;
        }
       
    }

    // Update is called once per frame
    void Update () {
        if (transform.position.z < GameStateController.Singleton.player.transform.position.z - 15f) {
            ResetPosition();
            biome.ChildReset();
            gameObject.SetActive(false);
        }
	}
}
