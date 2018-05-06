using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour {

    [SerializeField]
    Color GroundColour;

    BiomeManager biomeManager;
    BiomeObject[] biomeObjects;
    private int lastSpawnedBiomeObject;

    // Use this for initialization
    void Start () {

    }
    int SortByTransformZ(BiomeObject x, BiomeObject y) { return x.transform.position.z.CompareTo(y.transform.position.z); }

    public void EnableBiome( BiomeManager bm ) {
        biomeManager = bm;
        lastSpawnedBiomeObject = 0;
        gameObject.SetActive(true);

        biomeObjects = GetComponentsInChildren<BiomeObject>();
        System.Array.Sort(biomeObjects, SortByTransformZ);
    }

    // Update is called once per frame
    void Update () {
        for (int i = lastSpawnedBiomeObject; i < biomeObjects.Length; i++) {
            if (biomeObjects[i].transform.position.z < biomeManager.m_currentDistance) {
                biomeObjects[i].OnSpawned();
                lastSpawnedBiomeObject = i;
            } else {
                break;
            }
        }
    }
}
