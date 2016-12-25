using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public Transform handController;
	public Transform levelScroller;
	private Vector3 unitDirectionVector;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Vector3 rot = new Vector3 (45f, handController.rotation.eulerAngles.y, handController.rotation.eulerAngles.z);
		//this.transform.rotation = Quaternion.Euler(0f, handController.rotation.eulerAngles.y, handController.rotation.eulerAngles.z);
		this.transform.rotation = handController.rotation;
		unitDirectionVector = GvrController.Orientation * Vector3.forward;
		this.transform.Translate ((unitDirectionVector.x * 0.02f), 0f, 0f, Space.World);
	}
}
