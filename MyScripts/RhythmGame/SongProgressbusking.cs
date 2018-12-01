using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongProgressbusking : MonoBehaviour {


    public Slider audioSlider;
	public AudioSource currentSongPlayer;

    // Use this for initialization
    void Awake()
    {
        
    }

    void Start()
    {
        currentSongPlayer = gameObject.GetComponent<AudioSource>();
        //audioSlider.direction = Slider.Direction.LeftToRight;
        audioSlider.minValue = 0;
        audioSlider.maxValue = currentSongPlayer.clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        audioSlider.value = currentSongPlayer.time;
    }
}
