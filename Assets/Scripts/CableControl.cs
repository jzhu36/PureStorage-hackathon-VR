using UnityEngine;
using System.Collections;

// Require a Rigidbody and LineRenderer object for easier assembly
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (LineRenderer))]

public class CableControl : MonoBehaviour {
	/*========================================
	==  Physics Based Rope				==
	==  File: Rope.js					  ==
	==  Original by: Jacob Fletcher		==
	==  Use and alter Freely			 ==
	==  CSharp Conversion by: Chelsea Hash  ==
	==========================================
	How To Use:
	 ( BASIC )
	 1. Simply add this script to the object you want a rope teathered to
	 2. In the "LineRenderer" that is added, assign a material and adjust the width settings to your likeing
	 3. Assign the other end of the rope as the "Target" object in this script
	 4. Play and enjoy!
 
	 ( Advanced )
	 1. Do as instructed above
	 2. If you want more control over the rigidbody on the ropes end go ahead and manually
		 add the rigidbody component to the target end of the rope and adjust acordingly.
	 3. adjust settings as necessary in both the rigidbody and rope script
 
	 (About Character Joints)
	 Sometimes your rope needs to be very limp and by that I mean NO SPRINGY EFFECT.
	 In order to do this, you must loosen it up using the swingAxis and twist limits.
	 For example, On my joints in my drawing app, I set the swingAxis to (0,0,1) sense
	 the only axis I want to swing is the Z axis (facing the camera) and the other settings to around -100 or 100.
 
 
	*/
	public GameObject target;
	public float resolution = 5.0F;							  //  Sets the amount of joints there are in the rope (1 = 1 joint for every 1 unit)
	public float ropeDrag = 10.0F;								 //  Sets each joints Drag
	public float ropeMass = 0.1F;							//  Sets each joints Mass
	public float ropeColRadius = 0.5F;					//  Sets the radius of the collider in the SphereCollider component
	//public float ropeBreakForce = 25.0F;					 //-------------- TODO (Hopefully will break the rope in half...
	private Vector3[] segmentPos;			//  DONT MESS!	This is for the Line Renderer's Reference and to set up the positions of the gameObjects
	private LineRenderer line;							//  DONT MESS!	 The line renderer variable is set up when its assigned as a new component
	private int segments = 0;					//  DONT MESS!	The number of segments is calculated based off of your distance * resolution

	private int[] weights;

	private Vector3 prev_pos;
	private Vector3 target_prev_pos;

	void UpdatePos()
	{
		prev_pos = transform.position;
		target_prev_pos = target.transform.position;
	}
	void Start()
	{
		BuildRope ();
	}

	void Update()
	{
		
		if ( transform.position != prev_pos || target.transform.position != target_prev_pos) {
			BuildRope ();
			UpdatePos ();
		}
		prev_pos = transform.position;
	}

	void LateUpdate()
	{
		for(int i=0;i<segments;i++) {
			if(i == 0) {
				line.SetPosition(i,transform.position);
			} else
				if(i == segments-1) {
					line.SetPosition(i,target.transform.position);	
				} else {
					line.SetPosition(i,segmentPos[i]);
				}
		}
		line.enabled = true;
	}

	void BuildRope()
	{
		Debug.Log ("Build Rope");
		line = gameObject.GetComponent<LineRenderer>();
		// TODO: delete new objects
		//		delete segmentPos;
		//		delete weignts;

		// Find the amount of segments based on the distance and resolution
		// Example: [resolution of 1.0 = 1 joint per unit of distance]
		segments = (int)(Vector3.Distance(transform.position,target.transform.position)*resolution);
		segments = 10;
		line.SetVertexCount(segments);
		Debug.Log ("segments " + segments);
		segmentPos = new Vector3[segments];

		// HACK: find the delta y applied downwards on each point to simulate a rope shape
		weights = new int[segments];
		for (int i = 0; i < segments; i++) 
		{
			segmentPos [i] = new Vector3 (0,0,0);
			weights [i] = 0;
		}
		segmentPos[0] = transform.position;
		segmentPos[segments-1] = target.transform.position;

		// Find the distance between each segment
		var segs = segments-1;
		var seperation = ((target.transform.position - transform.position)/segs);

		// Add extra Y-axis delta to the whole 
		var dropY = (target.transform.position - transform.position).y;
		var downY = 0 - Mathf.Abs(dropY);
		// set a limit ??
		downY = Mathf.Max(downY, 0.0f -10.0f);
		weights [0] = 0;

		var weights_sum = 0;
		for (int i = 1; i < segments; i++) {
			weights [i] = weights [i - 1] + i;
		}

		for (int i = segments / 2; i < segments; i++) {
			weights [i] = 0 - weights [i - segments / 2];
		}

		for (int i = 1; i < segments / 2; i++) {
			weights [i] = 0 - weights [segments - 1 - i];
			weights_sum += weights [i];
		}

		Debug.Log ("weight sum is " + weights_sum);
		var deltaY_step = downY / weights_sum;
		var deltaY = 0.0f;
		for(int s=1;s < segments-1;s++)
		{
			// Find the each segments position using the slope from above
			Vector3 vector = (seperation*s) + transform.position;	
			segmentPos[s] = vector;

			deltaY += deltaY_step * (float)weights [s];
			Debug.Log("seg " + s + "y weight is " + weights[s] + "deltaY is " + deltaY);
			segmentPos [s].y += deltaY;
			if (target.transform.position.y > 0.0 && transform.position.y > 0.0 && segmentPos [s].y < 0.0) {
				segmentPos [s].y = 0.0f;
			}
		}

	}
}