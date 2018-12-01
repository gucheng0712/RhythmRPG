using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ChartLoader.NET.Utils;
using ChartLoader.NET.Framework;

public class ChartLoaderPrototype : MonoBehaviour
{

    // Declare chart reader
    public static ChartReader chartReader;

    public Transform[] notePrefabs;

    // Speed        Note: should probably make this global
    public float speed = 1f;


    void Start()
    {

        // Instantiate a new chart reader
        chartReader = new ChartReader();

        // Get the path of the chart file       Note: we need to find a way to get path inside of Unity
        Chart megalovaniaChart = chartReader.ReadChartFile("D:\\Unity Projects\\Troubadour\\Assets\\Charts\\Megalovania\\MEGALOVANIA.chart");

        // Create the array of notes            Note: we should figure out if we should load them in all at once or spawn them as we need them
        Note[] expertSingleNotes = megalovaniaChart.GetNotes("ExpertSingle");

        // Spawn the notes into our game
        SpawnNotes(expertSingleNotes);

    }

    // Spawn all of the notes
    public void SpawnNotes(Note[] notes)
    {

        // For each note in our array we're going to spawn a note
        foreach (Note note in notes)
        {
            SpawnNote(note);
        }
    }

    // Spawn single note
    public void SpawnNote(Note note)
    {

        Vector3 point;

        // This loop iterates through every single button index, so every time in the song that 1 or more notes are played.
        for (int i = 0; i < note.ButtonIndexes.Length; i++)
        {
            if (note.ButtonIndexes[i])
            {
                point = new Vector3(i - 2f, 0f, note.Seconds * speed);
                SpawnPrefab(notePrefabs[i], point);
            }
        }
    }

    // Spawn the note prefab
    public void SpawnPrefab(Transform prefab, Vector3 point)
    {
        Transform tmp = Instantiate(prefab);
        tmp.SetParent(transform);
        tmp.position = point;
    }

}
