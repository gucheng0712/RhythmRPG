using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using ChartLoader.NET.Utils;
using ChartLoader.NET.Framework;

public class GhostBuskingManager : MonoBehaviour
{

	public int easyOne = 1;
	public int easyTwo = 0;
	public int main = 1;
	public int test = 0;
	public int increment = 1;


    // Music Variables
    public AudioSource m_audioSource;
    SongDataGhostBusking m_currentSongGhostBusking;

    // Misc Variables
    [HideInInspector] public float laneAmount = 4;
    [HideInInspector] private float laneDisplacement;
    // Chart Loader Variables
    [HideInInspector] public ChartReader chartReaderBase;
	[HideInInspector] public ChartReader chartReaderChorus;
    [Header("Prefab References")]
    [Space(5)]
    [SerializeField] public Transform[] notePrefabs;
    [SerializeField] public Transform soloSectionPrefab;
    private string selectedChorusGhostBusking;
    private string selectedChorusPathGhostBusking;

    void Awake()
    {
        InitGame();
    }

    void Update()
	{
		//test += increment; 
		//easyOne = Random.Range (0, 5);
		//easyTwo = Random.Range (0, 5);
		if (GameManager_Menu.Instance.gameState == GameState.Pause) 
		{
			m_audioSource.Pause ();
		} 
		else 
		{
			m_audioSource.UnPause ();
		}
	}
		public void ChangeBGM(AudioClip music)
		{
			m_audioSource.Stop ();
			m_audioSource.clip = music;
			m_audioSource.Play ();
		}

    private void InitGame()
    {
        GhostBuskingMusicInitialization ();
        ChartInitialization();
        SpawnSoloSections();
        PlayMusic();
		//StartCoroutine ("easy2");
    }

    #region Initialize Music


    //Andys Shit
    private void GhostBuskingMusicInitialization() 
	{
		m_currentSongGhostBusking = GameManager_Data.Instance.selectedSongGhostBusking;
		if (GetComponent<AudioSource>() == false)
			m_audioSource = gameObject.AddComponent<AudioSource>();

		m_audioSource.clip = m_currentSongGhostBusking.clip;
	}


    #endregion

    #region Chart Initialization & Object Spawning
	/*private IEnumerator easy2()
	{
		yield return new WaitForSeconds (15.0f);
		print("AHHHHHHHHHHHHHHHHHHHH");
		GhostBuskingMusicInitialization ();
		ChartInitialization();
		SpawnSoloSections();
		test += 1;
		StartCoroutine ("medium1");
	}*/


    private void ChartInitialization()
	{

		// Instantiate chart readers
		chartReaderBase = new ChartReader ();
		// Get the path of the base chart file
		Chart selectedSongGhostBuskingChart = chartReaderBase.ReadChartFile (Application.streamingAssetsPath + "/charts/" + m_currentSongGhostBusking.songName + "/" + m_currentSongGhostBusking.baseChartPath);

		//Andys Shit
		if (test == 0) {
			switch (easyOne) {
			/*case 0:
				selectedChorusPathBusking = m_currentSongBusking.easy1;
				break;*/
			case 1:
				selectedChorusPathGhostBusking = m_currentSongGhostBusking.easy1;
				break;
			/*	case 2:
				selectedChorusPathBusking = m_currentSongBusking.easy3;
				break;
			case 3:
				selectedChorusPathBusking = m_currentSongBusking.easy4;
				break;
			case 4:
				selectedChorusPathBusking = m_currentSongBusking.easy5;
				break;*/
			}
		} 


		//Chart selectedSongBuskingGhostChart = chartReaderBase.ReadChartFile (Application.streamingAssetsPath + "/charts/" + m_currentSongBusking.songName + "/" + m_currentSongBusking.baseChartPath);
		//this is where the ghost notes will work


		//test++;
		//print (test);
        // Get the path of the chorus chart file
        chartReaderChorus = new ChartReader();
		//this is it
		Chart selectedChorusGhostBuskingChart = chartReaderChorus.ReadChartFile(Application.streamingAssetsPath + "/Charts/" + m_currentSongGhostBusking.songName + "/" + selectedChorusPathGhostBusking);


        // Get the difficulty
        string selectedSongDifficulty;
        switch (GameManager_Data.Instance.curDifficulty)
        {
            case Difficulty.Easy:
                selectedSongDifficulty = "EasySingle";
                break;
     
            default:
			selectedSongDifficulty = "EasySingle";
            break;
        }

        // Create the array of notes based on difficulty
		Note[] selectedSongBuskingNotes = selectedSongGhostBuskingChart.GetNotes(selectedSongDifficulty);
		Note[] selectedChorusBuskingNotes = selectedChorusGhostBuskingChart.GetNotes(selectedSongDifficulty);

		//GhostNote[] selectedSongBuskingGhostNotes = selectedSongBuskingGhostChart.GetNotes(selectedSongDifficulty);
		//GhostNote[] selectedChorusBuskingGhostNotes = selectedChorusBuskingGhostChart.GetNotes(selectedSongDifficulty);

        // Takes the note amount and uses it to space the notes and place them correctly
        laneDisplacement = -(laneAmount - 1) / 2;

        // Spawn the notes into our game
        SpawnNotes(selectedSongBuskingNotes);
		//SpawnNotes(selectedSongBuskingNotes2);
        SpawnNotes(selectedChorusBuskingNotes);

		// Spawn the notes into our game
		//SpawnNotes(selectedSongBuskingGhostNotes);
		//SpawnNotes(selectedSongBuskingNotes2);
		//SpawnNotes(selectedChorusBuskingGhostNotes);

    }

    // Spawn all of the notes
    private void SpawnNotes(Note[] notes)
    {

        // For each note in our array we're going to spawn a note
        foreach (Note note in notes)
        {
            SpawnNote(note);
        }
    }

	/*private void SpawnNotes(GhostNote[] notes)
	{

		// For each note in our array we're going to spawn a note
		foreach (GhostNote note in notes)
		{
			SpawnGhostNote(note);
		}
	}*/

    // Spawn single note
    private void SpawnNote(Note note)
    {

        Vector3 point;

        // This loop iterates through every single button index, so it loops through every note at one particular insance.
        for (int i = 0; i < laneAmount; i++)
        {
            if (note.ButtonIndexes[i])
            {
                point = new Vector3(i + laneDisplacement + 4.95f, 0.01f, (note.Seconds + 10f) * GameplayManager.gameSpeed);
                SpawnPrefab(notePrefabs[i], point);
            }
        }
    }

	/*private void SpawnGhostNote(GhostNote note)
	{

		Vector3 point;

		// This loop iterates through every single button index, so it loops through every note at one particular insance.
		for (int i = 0; i < laneAmount; i++)
		{
			if (note.ButtonIndexes[i])
			{
				point = new Vector3(i + laneDisplacement, 0.01f, (note.Seconds + 10f) * GameplayManager.gameSpeed);
				SpawnPrefab(notePrefabs[i], point);
			}
		}
	}*/

    // Spawn the note prefab
    private void SpawnPrefab(Transform prefab, Vector3 point)
    {
        Transform tmp = Instantiate(prefab);
        tmp.SetParent(transform);
        tmp.position = point;
    }

    #endregion

    #region Defining Chorus/Solo Sections

    private void SpawnSoloSections()
    {
		foreach (SongDataGhostBusking.ChorusSections solo in m_currentSongGhostBusking.soloSections)
        {
            float length = solo.endTime - solo.startTime;
            float midpoint = length / 2 + solo.startTime;
            Transform currentSoloSection = soloSectionPrefab;
            currentSoloSection.transform.localScale = new Vector3(currentSoloSection.transform.localScale.x, currentSoloSection.transform.localScale.y, length * GameplayManager.gameSpeed);
            Instantiate(soloSectionPrefab, new Vector3(0, -0.6f, midpoint * GameplayManager.gameSpeed), Quaternion.identity);
        }
    }

    #endregion

    #region Play Music 

    private void PlayMusic()
    {
        m_audioSource.Play();
        m_audioSource.time = 10f - GameplayManager.Instance.gameOffset;
        m_audioSource.time = 8f;
    }

	public void Changem_audioSource(AudioClip music)
	{
		m_audioSource.Stop ();
		m_audioSource.clip = music;
		m_audioSource.Play ();
	}

    #endregion

}
