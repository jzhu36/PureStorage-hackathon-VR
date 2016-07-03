using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	// Use this for initialization
	public bool haveSocket = false;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Transform camera = Camera.main.transform;
		Ray ray;
		RaycastHit hit;
		GameObject hitObject;

		Debug.DrawRay (camera.position, camera.rotation * Vector3.forward * 2.0f);

		ray = new Ray (camera.position, camera.rotation * Vector3.forward);
		if (Physics.Raycast (ray, out hit, 2.0f)) {
			//Debug.Log ("Hit (x,y,z):");
			hitObject = hit.collider.gameObject;
			if (hitObject.tag == "Socket" && hitObject.transform.parent.tag != "Trigger" && !haveSocket) {
				Debug.Log ("Hit (x,y,z):" + hit.point.ToString ("F2"));
				//TODO modify the position of socket

				Vector3 newPosition = new Vector3 (1.0f, 1.0f, 2.0f);
				newPosition = 1.0f * transform.forward;
				hitObject.transform.position = newPosition + transform.parent.position;
				newPosition = Quaternion.AngleAxis (90, Vector3.right) * transform.forward * 0.2f;
				hitObject.transform.position += newPosition;
				hitObject.transform.parent = camera.parent;
				haveSocket = true;
			}
		}
	}
}
