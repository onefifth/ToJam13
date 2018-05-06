using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour {
    BiomeManager biomeManager;
    BiomeObject[] biomeObjects;
    private int lastSpawnedBiomeObject;
    private int totalChildren;

    // Use this for initialization
    void Start () {

    }
    int SortByTransformZ(BiomeObject x, BiomeObject y) { return x.transform.position.z.CompareTo(y.transform.position.z); }

    public void EnableBiome( BiomeManager bm ) {
        biomeManager = bm;
        lastSpawnedBiomeObject = 0;
        gameObject.SetActive(true);

        if (biomeObjects == null) {
            biomeObjects = GetComponentsInChildren<BiomeObject>();
            System.Array.Sort(biomeObjects, SortByTransformZ);
        }
        totalChildren = biomeObjects.Length;
    }

    public void ChildReset() {
        totalChildren -= 1;
        if (totalChildren <= 0) {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
        for (int i = lastSpawnedBiomeObject; i < biomeObjects.Length; i++) {
            if (biomeObjects[i].transform.position.z > biomeManager.m_currentDistance) {
                break;
            } else {
                biomeObjects[i].OnSpawned( this );
                lastSpawnedBiomeObject = i + 1;
            }
        }
    }
}
