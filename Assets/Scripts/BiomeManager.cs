using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeManager : MonoBehaviour {

    Biome[] biomes;

    private Transform playerTransform;
    private Player player;

    [SerializeField]
    Material groundMaterial;

    public float m_startingDistance { get; private set; }
    public float m_currentDistance { get; private set; }
    //public int m_lastSpawnedIndex { get; private set; }
    public int m_activeBiomeIndex { get; private set; }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerTransform = player.transform;

        biomes = GetComponentsInChildren<Biome>();
        System.Array.Sort(biomes, SortByTransformZ);

        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
    }

    public void StartGame(float distance = 0f)
    {
        //m_startingDistance = playerTransform.position.z + distance;
        m_activeBiomeIndex = 0;

        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        //m_lastSpawnedDistance = m_startingDistance;
    }

    int SortByTransformZ(Biome x, Biome y) { return x.transform.position.z.CompareTo(y.transform.position.z); }

    // Update is called once per frame
    void Update () {
        m_currentDistance = playerTransform.position.z + 12f;

        Debug.DrawLine(new Vector3(-10f, 0f, m_currentDistance), new Vector3(10f, 0f, m_currentDistance), Color.red);

        for (int i = m_activeBiomeIndex; i < biomes.Length; i++) {
            if (biomes[i].transform.position.z > m_currentDistance) {
                break;
            } else {
                biomes[i].EnableBiome( this );
                m_activeBiomeIndex = i + 1;
            }
        }
    }

    public void setGroundColor(Color color) {
        groundMaterial.SetColor("_Color", color);
    }
}
