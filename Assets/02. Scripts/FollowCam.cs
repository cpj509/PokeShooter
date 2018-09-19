using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public Transform targetTr; //추적할 타겟 오브젝트의 transform
    public float dist = 10.0f;
    public float height = 3.0f;
    public float dampTrace = 20.0f; //부드러운 추적

    private Transform tr;



	void Start () {
        tr = GetComponent<Transform>();

        SoundMgr.instance.PlayLoopSound(SoundMgr.instance.bgmOST3); //BGM
	}

	
    //추적할 타깃의 이동이 종료된 후 카메라가 추적하기 위해 LateUpdate()를 사용
	void LateUpdate () {
        //부드러운 이동을 위해 두 벡터를 선형보간시키는 함수 사용
        //카메라를 dist만큼 뒤로 height만큼 위로 올림
        tr.position = Vector3.Lerp(tr.position
                                   , targetTr.position
                                       - (targetTr.forward * dist)
                                       + (Vector3.up * height)
                                       , Time.deltaTime * dampTrace);
        tr.LookAt(targetTr.position);
	}
}
