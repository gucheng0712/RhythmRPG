using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class RhythmGameCamera : MonoBehaviour
{

    [HideInInspector] private float speed = 1f;
    [Tooltip("A slight adjustment to make sure the game is in sync")]
    [Range(-2, 2)]
    [SerializeField] public float synchronization;
    AudioSource currentSong;
    private float endTime;
    private bool hasStarted = true;
    private bool canEndGame = true;

    public GameObject scoreScreen;

    private void Awake()
    {
        this.transform.position = new Vector3(0f, 0f, synchronization);
        scoreScreen.SetActive(true);
    }

    private void Start()
    {
        currentSong = GameObject.Find("Music & Chart Manager").GetComponent<AudioSource>();
        endTime = currentSong.clip.length - 1f;
        speed = GameplayManager.gameSpeed;
    }

    private void Update()
    {
        if (transform.position.z >= (endTime * speed + synchronization) && canEndGame)
        {
            canEndGame = false;
            Invoke("EndGame", 2f);
        }
    }

    private void FixedUpdate()
    {
        Vector3 syncVector = new Vector3(0, 0, synchronization);
        transform.Translate((Vector3.forward * currentSong.time * speed + syncVector) - transform.position);
    }

    private void EndGame()
    {
        GameplayManager.EndGame(scoreScreen);
    }


    public void CheckIfOver()
    {
        if (hasStarted && !currentSong.isPlaying)
        {

            hasStarted = false;
            GameplayManager.EndGame(scoreScreen);
        }

        //Andyshit
    }
}
