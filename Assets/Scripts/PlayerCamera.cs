using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    private Transform playerTransform;
    private Player player;

    Vector3 cameraTarget;

    // Use this for initialization
    void Start () {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerGO.transform;
        player = playerGO.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        if (player.canMove) {
            Vector3 lerpdTransform = Vector3.Lerp(transform.localPosition, playerTransform.localPosition, Time.deltaTime * 2f);
            lerpdTransform.y = transform.localPosition.y;

            transform.localPosition = lerpdTransform;
        }
    }
}
