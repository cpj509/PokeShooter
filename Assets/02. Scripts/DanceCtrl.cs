using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceCtrl : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        animator.SetTrigger("IsPlayerDie");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
