using UnityEngine;
using System.Collections;

public class WinController : MonoBehaviour {

	public int score;

	public GameObject ball;
	public float startHeight = 10f;
	public float fireIntervel = 0.5f;
	private bool win = false;
	private float nextBallTime = 0.0f;
	private int count = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (score == 8) {
			win = true;
		}
		if (win && count < 20) {
			if (Time.time > nextBallTime) {
				nextBallTime = Time.time + fireIntervel;
				Vector3 position = new Vector3 (Random.Range (-4.0f, 4.0f), startHeight, Random.Range (-4.0f, 4.0f));
				GameObject array = Instantiate (ball, position, Quaternion.identity) as GameObject;
				array.transform.localScale = new Vector3 (1.8f, 0.5f, 3f);
				count++;
			}
		}
		if (count == 20) {
			Application.LoadLevel (0);
		}

	}
}
