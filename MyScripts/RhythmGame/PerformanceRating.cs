using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceRating : MonoBehaviour {


    public Slider performanceSlider;

    public int noteRange = 50;

    [HideInInspector]
    public static int currentNotes = 25;

    // Use this for initialization
    void Awake()
    {
        
    }

    void Start()
    {
        //audioSlider.direction = Slider.Direction.LeftToRight;
        performanceSlider.minValue = 0;
        performanceSlider.maxValue = noteRange;
        currentNotes = noteRange / 2;
        performanceSlider.value = currentNotes;

    }

    // Update is called once per frame
    void Update()
    {
        performanceSlider.value = currentNotes;
    }
}
