using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public Transform target;
	public float speed;
	// Use this for initialization
	void Start () {
		speed = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		Vector3 realTarget = new Vector3 (target.position.x, 0, target.position.z);
		transform.position = Vector3.MoveTowards (transform.position, realTarget, step);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.transform.tag == "SlowDown") {
			speed = 0.5f;
		} else if (other.gameObject.transform.tag == "Reset") {
			Debug.Log ("Restart Game");
			Application.LoadLevel (0);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.transform.tag == "SlowDown") {
			speed = 2.0f;
		}
	}

}
