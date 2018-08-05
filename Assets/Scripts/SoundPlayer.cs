using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

}
