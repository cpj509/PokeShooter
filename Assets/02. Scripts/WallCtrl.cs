using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCtrl : MonoBehaviour {
	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.tag == "Ball") {
			Destroy (coll.gameObject);
		}
	}
}
