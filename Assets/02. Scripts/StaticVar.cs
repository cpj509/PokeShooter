using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class pokevar
{
    public static int coin = 0;//코인 획득이나 사용시 증감
    public static int balldamage = 20;//상점에서 볼 구매시 static에 있는 변수 수정
    public static GameObject[] companyMonster = new GameObject[3];//동료용 게임오브젝트
}

/*
 프리팹 적용 예제

 // 리소스에서 게임오브젝트를 로드하여 배열에 넣는 경우. 
companyMonster[0] = Resources.Load("/Prefabs/Prefab1") as GameObject; 
companyMonster[1] = Resources.Load("/Prefabs/Prefab2") as GameObject; 
companyMonster[2] = Resources.Load("/Prefabs/Prefab2") as GameObject; 

// 씬에 올라가있는 게임오브젝트를 검색하여 배열에 넣는 경우. 
companyMonster[0] = GameObject.Find("PrefabName1"); 
companyMonster[1] = GameObject.Find("PrefabName2"); 
companyMonster[2] = GameObject.Find("PrefabName2"); 
*/
