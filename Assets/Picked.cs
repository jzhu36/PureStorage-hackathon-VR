using UnityEngine;
using System.Collections;

public class Picked : MonoBehaviour {

	public int code;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent.tag == "Player") {
			transform.rotation = transform.parent.rotation;
			Vector3 eulers = transform.eulerAngles;
			eulers.x = 0;
			transform.eulerAngles = eulers;
		}


	}
}
