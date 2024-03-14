using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseFootstepScript : MonoBehaviour
{

    [SerializeField] AudioSource gooseAudio;
    public AudioClip[] slapSFX;

    public void GooseFootfall()
    {
        gooseAudio.PlayOneShot(slapSFX[Random.Range(0, slapSFX.Length)]);
        //Debug.Log("Play Footfall");
    }

}
