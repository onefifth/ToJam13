using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct SpawnItem {
    GameObject g;
    float dist;
}

public class FoliageSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject FoliagePrefab;

    private Transform playerTransform;
    private Player player;

    public float m_startingDistance { get; private set; }
    public int m_lastSpawnedIndex { get; private set; }


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerTransform = player.transform;

        // Resources.LoadAll("obsticles");
    }

    void StartGame( float distance = 0f ) {
        m_startingDistance = playerTransform.position.y + distance;
        //m_lastSpawnedDistance = m_startingDistance;
    }




    // Update is called once per frame
    void Update () {
        
	}
}
