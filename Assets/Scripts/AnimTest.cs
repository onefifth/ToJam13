using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour {

    public Animator anim;
    bool idle = false;
    float walkRate = 2.0f;
    float hitDistance = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 joyDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        anim.SetFloat("xWalk", joyDir.x);
        anim.SetFloat("yWalk", joyDir.y);

        if (idle & joyDir.magnitude > 0)
        {
            Walk(joyDir);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetTrigger("disrobe");
        }
        if(Input.GetKey(KeyCode.E))
        {
            TakeHit(joyDir.x);
        }

	}

    void Walk(Vector2 dir)
    {
        dir.Set(dir.x, Mathf.Clamp(dir.y, 0, 1));
        transform.position += new Vector3(dir.x, dir.y, 0) * walkRate * Time.deltaTime;
        anim.SetBool("walking", dir.magnitude > 0);
    }


    public void TakeHit(float xDir)
    {
        anim.SetTrigger("hit");
        anim.SetFloat("hitX", xDir);
        idle = false;
    }

    public void OnIdle() {

        idle = true;

    }

    public void DropTrou() {

        GameObject clothes = (GameObject)Instantiate(Resources.Load("PlayerClothes"), transform.position, transform.rotation);
        clothes.transform.localScale = transform.lossyScale;

    }
}
