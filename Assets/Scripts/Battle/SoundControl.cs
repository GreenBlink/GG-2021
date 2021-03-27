using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;


public class SoundControl : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    [SerializeField] private List<AudioClip> death;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip move;
    [SerializeField] private float volume;
    private AudioSource asource;


    // Start is called before the first frame update
    void Start()
    {
        asource = this.gameObject.AddComponent<AudioSource>() as AudioSource;
        asource.volume = volume;
        asource.clip = music;
        asource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playDeath()
    {
        int r = Random.Range(0, death.Capacity);
        asource.PlayOneShot(death[r]);
    }

    public void playClick()
    {
        asource.PlayOneShot(click);
    }

    public void playMove()
    {
        asource.PlayOneShot(move);
    }
}
