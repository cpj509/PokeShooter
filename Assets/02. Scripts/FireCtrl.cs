using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireCtrl : MonoBehaviour {

	//총알 프리맵
	public GameObject bullet;
	//총알 발사 좌표
	public Transform firePos;

	void Start () {
	}

	
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Fire ();
		}
	}
	//추후 효과 추가를 위해 따로 생성
	void Fire(){
		CreateBullet ();
	}

	void CreateBullet(){
		Instantiate (bullet, firePos.position, firePos.rotation);
	}

}
