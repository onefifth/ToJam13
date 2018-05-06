using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAnimCallback : MonoBehaviour {

	public void PlayLandSFX()
    {
        if(GetComponentInParent<ChasePlayer>())
        {
            GetComponentInParent<ChasePlayer>().PlayLandSFX();
        }
    }

}
