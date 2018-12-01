using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameData/Song/NewSong", fileName = "New Song.asset")]
public class SongData : ScriptableObject {

    [System.Serializable]
    public class ChorusSections {
        public float startTime;
        public float endTime;
        public ChorusSections(float start, float end) {
            startTime = start;
            endTime = end;
        }
    }

    public AudioClip clip;
    public int id;
    public string songName;
    public string baseChartPath;

    [Header("Solo Sections")]
    [Space(5)]
    [Tooltip("How many solo sections are in the piece and where do they start and begin (in seconds).")]
    [SerializeField] public ChorusSections[] soloSections;
    [Space(5)]

	public string ambientChartPath;
	public string experimentalChartPath;
	public string hipChartPath;
	public string loudChartPath;

}
