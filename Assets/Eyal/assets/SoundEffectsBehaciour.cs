using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsBehaciour : MonoBehaviour
{
    AudioSource m_AudioSource;

    // Use this for initialization
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void _explosion()
    {
        m_AudioSource.Play();
    }

    public static void explosion()
    {
        GameObject.FindObjectOfType<SoundEffectsBehaciour>()._explosion();
    }
}
