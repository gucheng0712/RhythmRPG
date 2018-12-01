using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegmanRandom : MonoBehaviour {

		public GameObject Legman;



	// Use this for initialization
	void Start () {
		Vector3 position = new Vector3 (Random.Range (28.0f, 50.0f), -1.79f, 0);
		//Vector3 position1 = new Vector3 (Random.Range (55.0f, 128.0f), -1.79f, 0);
		transform.position = position;
		print ("Dude is teleporting");
		//Destroy (gameObject);
				//StartCoroutine (LegmanTeleport ());
		
	}
		/*IEnumerator LegmanTeleport()
			{
				yield return new WaitForSeconds (5);
				Vector3 position = new Vector3 (Random.Range (28.0f, 120.0f), -1.79f, 0);
				Instantiate (Legman, position, Quaternion.identity);
				Destroy (gameObject);
				print ("Legman is teleporting");

			}*/
	// Update is called once per frame
	void Update () {

		/*Vector3 position = new Vector3 (Random.Range (28.0f, 150.0f), 0, 0);
		Instantiate (Legman, position, Quaternion.identity);
		print ("Legman is teleporting");*/
				
	}
}
