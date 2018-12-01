using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager {
    public class GameplayManager : GameManager<GameplayManager> {

        [Header("Game Settings")]
        [Space(5)]

        [Tooltip("Time in seconds before Rhythm game begins")]
        [Range(2, 10)]
        [SerializeField] public float gameOffset = 2f;

        [Tooltip("Time in seconds before Score Screen appears after game ends")]
        [Range(2, 10)]
        [SerializeField] public float gameEndLagTime = 2f;

        [Tooltip("How fast the notes will appear to move (Unity units per second)\nA minimum speed of four because otherwise Unity will start to chug due to amount of notes on screen, and a maximum speed of 10 because otherwise the physics system starts to fail.")]
        [Range(4, 10)]
        public static float gameSpeed = 4f;

        //public static float gameOffsetStatic;
        //public static float gameSpeedStatic;
        public static float gameScoreStatic;

        public static bool isLoudInstrument;
        public static bool isInLoudSection;
        
        public void Start() {
            isLoudInstrument = GameManager_Data.Instance.selectedInstrument.chorusChart == ChorusChart.Loud ? true : false;
        }

        public static void SetLoudBool(bool val) {
            if (isLoudInstrument) {
                isInLoudSection = val;
            }
        }

        public static void EndGame(GameObject scoreScreen) {

            gameScoreStatic = ScoreManager.score;
            scoreScreen.GetComponent<ScoreScreen>().ShowScore();
        }

    }
}