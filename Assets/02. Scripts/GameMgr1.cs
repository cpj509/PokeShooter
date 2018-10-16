using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr1 : MonoBehaviour
{

    public Transform[] points;// 출현 위치 배열

    public GameObject monsterPrefab;// 몬스터 프리팹 할당할 변수
    public List<GameObject> monsterPool = new List<GameObject>();


    public float createTime = 2.0f;
    public int maxMonster = 10;
    public bool isGameOver = false;
    public static GameMgr1 instance = null;

    public GameObject ShopScreen;
    public bool isShopScreen = false;

    void Awake()
    {
        instance = this;
    }
  
    void Start()
    {
        points = GameObject.Find("SpawnPoint1").GetComponentsInChildren<Transform>();

        for (int i = 0; i < maxMonster; i++)
        {
            //int idx = Random.Range(0, monsterPrefab);
            //transform.GetChild(idx).gameObject.SetActive(true);
            //몬스터 프리팹 생성
            GameObject monster = (GameObject)Instantiate(monsterPrefab);
            //생성한 몬스터 이름설정
            monster.name = "Monster_" + i.ToString();
            monster.SetActive(false);
            monsterPool.Add(monster);
        }

        if (points.Length > 0)
        {
            StartCoroutine(this.CreateMonster());//몬스터 생성 코루틴 함수 호출
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            if(!isShopScreen){
                ShopScreen.SetActive(true);
                isShopScreen = true;
            }
            else{
                ShopScreen.SetActive(false);
                isShopScreen = false;
            }

        }
    }

    //몬스터 생성 코루틴 함수
    IEnumerator CreateMonster()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(createTime);

            if (isGameOver) yield break;

            foreach (GameObject monster in monsterPool)
            {
                if (!monster.activeSelf)
                {
                    int idx = Random.Range(1, points.Length);
                    monster.transform.position = points[idx].position;
                    monster.SetActive(true);
                    break;
                }
            }

        }
    }

}
