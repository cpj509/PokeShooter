using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Floor" 
           || other.gameObject.tag == "WALL"){
            Destroy(gameObject, 2.0f);
        }
    }
}
