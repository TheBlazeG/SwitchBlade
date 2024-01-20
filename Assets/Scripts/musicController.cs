using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> musicList;
    [SerializeField] public AudioSource musicListener;
    public int musicTrack = 0;
    public bool changedMusic = false;

    private void Start()
    {
        musicListener = GetComponent<AudioSource>();
    }
    private void Update()
    {
        musicListener.clip = musicList[musicTrack];

        if (changedMusic)
        {
            musicListener.Play(0);
            changedMusic = false;
        }
    }
}
