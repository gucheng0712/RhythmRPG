using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using ChartLoader.NET.Utils;
using ChartLoader.NET.Framework;

public class ChartManager : MonoBehaviour
{


    // Music Variables
    AudioSource m_audioSource;
    SongData m_currentSong;

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
    [SerializeField] public Transform gameEndTrigger;
    [HideInInspector] public static string selectedChorus;
    private string selectedChorusPath;
    public static int totalNotes;

    void Awake()
    {
        InitGame();

    }

    void Update()
    {
        if (GameManager_Menu.Instance.gameState == GameState.Pause)
        {
            m_audioSource.Pause();
        }
        else
        {
            m_audioSource.UnPause();
        }
    }

    private void InitGame()
    {
        totalNotes = 0;
        MusicInitialization();
        ChartInitialization();
        SpawnSoloSections();
        PlayMusic();
    }

    #region Initialize Music

    private void MusicInitialization()
    {
        m_currentSong = GameManager_Data.Instance.selectedSong;
        if (GetComponent<AudioSource>() == false)
            m_audioSource = gameObject.AddComponent<AudioSource>();

        m_audioSource.clip = m_currentSong.clip;
    }

    #endregion

    #region Chart Initialization & Object Spawning

    private void ChartInitialization()
    {

        // Instantiate chart readers
        chartReaderBase = new ChartReader();

        // Get the path of the base chart file
        Chart selectedSongChart = chartReaderBase.ReadChartFile(Application.streamingAssetsPath + "/Charts/" + m_currentSong.songName + "/" + m_currentSong.baseChartPath);


        // Get the path of the chorus chart file

        switch (GameManager_Data.Instance.selectedInstrument.chorusChart)
        {
            case ChorusChart.Ambient:
                selectedChorusPath = m_currentSong.ambientChartPath;
                break;
            case ChorusChart.Experimental:
                selectedChorusPath = m_currentSong.experimentalChartPath;
                break;
            case ChorusChart.Hip:
                selectedChorusPath = m_currentSong.hipChartPath;
                break;
            case ChorusChart.Loud:
                selectedChorusPath = m_currentSong.loudChartPath;
                break;
        }

        chartReaderChorus = new ChartReader();
        Chart selectedChorusChart = chartReaderChorus.ReadChartFile(Application.streamingAssetsPath + "/Charts/" + m_currentSong.songName + "/" + selectedChorusPath);


        // Get the difficulty
        string selectedSongDifficulty;

        switch (GameManager_Data.Instance.curDifficulty)
        {
            case Difficulty.Easy:
                selectedSongDifficulty = "EasySingle";
                break;
            case Difficulty.Normal:
                selectedSongDifficulty = "MediumSingle";
                break;
            case Difficulty.Hard:
                selectedSongDifficulty = "ExpertSingle";
                break;
            default:
                Debug.Log("What the fuck are you doing?");
                Debug.Log("Did you really just not choose a fucking difficulty?");
                Debug.Log("How did you even do that? You have to choose a difficulty to start. So like... how?");
                Debug.Log("You need to be a special breed of stupid to get to this point.");
                //    selectedSongDifficulty = null;
                selectedSongDifficulty = "EasySingle";
                break;
        }

        // Create the array of notes based on difficulty
        Note[] selectedSongNotes = selectedSongChart.GetNotes(selectedSongDifficulty);
        Note[] selectedChorusNotes = selectedChorusChart.GetNotes(selectedSongDifficulty);

        // Takes the note amount and uses it to space the notes and place them correctly
        laneDisplacement = -(laneAmount - 1) / 2;

        // Spawn the notes into our game
        SpawnNotes(selectedSongNotes);
        SpawnNotes(selectedChorusNotes);
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

    // Spawn single note
    private void SpawnNote(Note note)
    {

        Vector3 point;

        // This loop iterates through every single button index, so it loops through every note at one particular insance.
        for (int i = 0; i < laneAmount; i++)
        {
            if (note.ButtonIndexes[i])
            {
                point = new Vector3(i + laneDisplacement, 0.01f, (note.Seconds + 10f) * GameplayManager.gameSpeed);
                SpawnPrefab(notePrefabs[i], point);
                totalNotes += 1;
            }
        }
    }

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
        foreach (SongData.ChorusSections solo in m_currentSong.soloSections)
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
        //m_audioSource.time = 10f - GameplayManager.Instance.gameOffset;
        m_audioSource.time = 4f;
    }

    #endregion

}
