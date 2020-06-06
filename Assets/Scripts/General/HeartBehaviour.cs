using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _heartSound;

    public void PlayHeartSound()
    {
        _audio.PlayOneShot(_heartSound);
    }
}
