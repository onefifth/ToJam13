using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeStatic : MonoBehaviour {
    Transform cameraTransform;

    // Use this for initialization
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(cameraTransform.position.x, transform.localPosition.y, transform.localPosition.z);
    }
}
