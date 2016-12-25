using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelScroller : MonoBehaviour {

	public GameObject obstaclePrefab;
	public float rowSpawnRate = 3f;
	public int objectsInRow = 5;
	public float levelZoneWidth = 10f;
	public float levelZoneHeight = 10f;
	private Queue<GameObject> scrollingObjectQueue = new Queue<GameObject>();
	private int i = 0;
	private GameObject tempObject;
	private GameObject parentRow;

	// Use this for initialization
	void Start () {
		StartCoroutine ("queueObjects");
	}

	private IEnumerator queueObjects() {
		while (true) {
			yield return new WaitForSeconds (rowSpawnRate);
			// Queue obstacle row
			for (i = 0; i < objectsInRow; i++) {
				GameObject tmp = Instantiate<GameObject> (obstaclePrefab);
				yield return null;
				scrollingObjectQueue.Enqueue (tmp);
				yield return null;
			}
			parentRow = new GameObject ();
			//parentRow.transform.rotation = Quaternion.identity;
			parentRow.transform.SetParent (this.transform, false);
			yield return null;
			parentRow.transform.localPosition = new Vector3 (-levelZoneWidth/2f, 0f, levelZoneHeight / 2f);
			yield return null;
			i = 0;
			while (scrollingObjectQueue.Count > 0) {
				tempObject = scrollingObjectQueue.Dequeue ();
				tempObject.transform.SetParent (parentRow.transform, false);
				tempObject.transform.localPosition = new Vector3 ((levelZoneWidth / ((float)objectsInRow-1)) * (float)i, 0f, 0f);
				i++;
				yield return null;
			}
			Debug.Log ("Row created");
			StartCoroutine ("startRowMovement");
		}
	}

	private IEnumerator startRowMovement() {
		GameObject temporaryRow = parentRow;
		while (temporaryRow.transform.localPosition.z > -levelZoneHeight / 2f) {
			temporaryRow.transform.Translate (0f, 0f, -0.05f, temporaryRow.transform);
			yield return new WaitForSeconds (0.01f);
		}
		foreach (Transform del in temporaryRow.GetComponentsInChildren<Transform>()) {
			Destroy (del.gameObject);
			yield return null;
		}
	}
}
