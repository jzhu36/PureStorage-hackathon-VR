using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	GameObject head;
	GameObject wincontroller;
	public int code;
	private bool taken = false;

	void OnTriggerEnter(Collider other) {

		if (!taken && other.gameObject.tag == "Socket" && other.gameObject.GetComponent<Picked> ().code == code) {
			other.transform.parent = transform;
			Vector3 temp = new Vector3(0.0f, 0.0f, 0.2f);
			other.transform.position = transform.position + temp;
			other.transform.forward = -temp;
			head.GetComponent<PickUp> ().haveSocket = false;
			wincontroller.GetComponent<WinController> ().score += 1;
		}
	}
	// Use this for initialization
	void Start () {
		head = GameObject.Find ("Head");
		wincontroller = GameObject.Find ("WinController");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
