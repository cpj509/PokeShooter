using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCtrl : MonoBehaviour {

	//총알의 파괴력
	public int damage = 20;
	//총알 발사 속도
	public float speed = 1000.0f;


	void Start () {
		//GetComponent<Rigidbody> ().AddForce(transform.forward * speed);
        SoundMgr.instance.PlaySound(SoundMgr.instance.soundBallThrowing);
    }

	
	void Update () {

	}
}
