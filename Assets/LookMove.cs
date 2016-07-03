using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class LookMove : MonoBehaviour {
	public GameObject Wall;
	public Transform player;
	private Clicker clicker = new Clicker ();
	private bool moving = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Transform camera = Camera.main.transform;
		Ray ray;
		RaycastHit hit;
		GameObject hitObject;

		Debug.DrawRay (camera.position, camera.rotation * Vector3.forward * 100.0f);

		ray = new Ray (camera.position, camera.rotation * Vector3.forward);
		if (Physics.Raycast (ray, out hit)) {
			//Debug.Log ("Hit (x,y,z):");
			hitObject = hit.collider.gameObject;
			if (clicker.clicked ()) {
				moving = !moving;
			}
			//if (hitObject.tag == "Wall" || hitObject.tag == "ct") {
				//Debug.Log ("Hit (x,y,z):" + hit.point.ToString ("F2"));
			if (moving) {
				transform.position = hit.point;
			} else {
				transform.position = player.position;
			}
		}
	}
}
