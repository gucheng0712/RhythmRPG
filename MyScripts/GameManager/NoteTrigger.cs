using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTrigger : MonoBehaviour {

    public GameObject deathParticlesHit;
    public GameObject deathParticlesMiss;

    public void Die(string type) {

        switch (type)
        {
            case "miss":
                if(PerformanceRating.currentNotes > 0)
                {
                    PerformanceRating.currentNotes--;
                }
                Instantiate(deathParticlesMiss, gameObject.transform.position, Quaternion.identity);
                break;
            case "hit":
                if(PerformanceRating.currentNotes < 50)
                {
                    PerformanceRating.currentNotes++;
                }
                Instantiate(deathParticlesHit, gameObject.transform.position, Quaternion.identity);
                break;

            default:
                Debug.LogError("GO FUCK YOURSELF");
                break;
        }
        Destroy(this.gameObject);
    }
}