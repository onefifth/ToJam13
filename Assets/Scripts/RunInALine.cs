using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunInALine : MonoBehaviour
{

    Player player;
    public float runRate;
    Animator anim;
    //Rigidbody rb;
    public bool idle = true;

    public Vector3 runDir; 

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        //rb = GetComponentInChildren<Rigidbody>();
    }

    void Update()
    {
        //anim.SetBool("walking", false);

        if (idle)
        {
            Vector3 walkDir = runDir.normalized;
            Vector3 walkVec = walkDir * runRate * Time.deltaTime;
            transform.position += walkVec;

            anim.SetBool("walking", true);
            anim.SetFloat("xWalk", walkDir.x);
            anim.SetFloat("yWalk", walkDir.z);


        }

    }

    private void OnDrawGizmos()
    {
        //if (UnityEditor.Selection.activeGameObject == gameObject)
        //{
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, runDir);
        //}
    }
}