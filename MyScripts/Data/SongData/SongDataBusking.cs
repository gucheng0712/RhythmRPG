using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameData/Song/NewSongBusking", fileName = "New Song Busking.asset")]
public class SongDataBusking : ScriptableObject 
{
	[System.Serializable]
    public class ChorusSections 
	{
        public float startTime;
        public float endTime;
        public ChorusSections(float start, float end) 
		{
            startTime = start;
            endTime = end;
        }
    }

    public AudioClip clip;
    public int id;
    public string songName;
    public string baseChartPath;

    [Header("Random mode Sections")]
    [Space(18)]
    [Tooltip("How many solo sections are in the piece and where do they start and begin (in seconds).")]
    [SerializeField] public ChorusSections[] soloSections;
    [Space(18)]
	//easy set1
	public string easy1;


	/*//easy set2
	public string easyeasy1;


	//medium set1
	public string medium1;


	//hard set2
	public string mediummedium1;


	//hard set1
	public string hard1;


	//hard set2
	public string hardhard1;*/

}