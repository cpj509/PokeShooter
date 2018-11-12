using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{

    public enum MonsterState { idle, trace, attack, die };

    public MonsterState monsterState = MonsterState.idle;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;
    private GameUI gameUI;

    //추적을 시작할 거리
    public float traceDist = 10.0f;
	  //공격을 시작할 거리
    public float attackDist = 2.0f;
	  //몬스터의 체력
    private int hp = 100;
    //초기 생존 상태
    private bool isDie = false;

    //동료로 만들기 위한 변수들
    public float perCompany =  1.0f;
    public GameObject companyMonster;
    public GameObject playerPos;
  
    void Awake()
    {
        monsterTr = this.gameObject.GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        animator = this.gameObject.GetComponent<Animator>();
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();

        playerPos = GameObject.FindWithTag("Player");
    }

    void OnEnable()
    {
        PlayerCtrl.OnPlayerDie += this.OnPlayerDie;
        StartCoroutine(this.CheckMonsterState());
        StartCoroutine(this.MonsterAction());
    }

    void OnDisable()
    {
        PlayerCtrl.OnPlayerDie -= this.OnPlayerDie;
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
			//0.2초 간격으로 호출 = Delay()와 비
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if (dist <= attackDist)
            {
                monsterState = MonsterState.attack;
            }
            else if (dist <= traceDist)
            {
                monsterState = MonsterState.trace;
            }
            else
            {
                monsterState = MonsterState.idle;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.Stop();
                    //nvAgent.isStopped = true;
                    animator.SetBool("IsTrace", false);
                    break;

                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.Resume();
                    //nvAgent.isStopped = true;

                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", true);
                    break;

                case MonsterState.attack:
                    //추적 중지
                    nvAgent.Stop();
                    //nvAgent.isStopped = true;
                    animator.SetBool("IsAttack", true);
                    break;
            }
            yield return null;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            //hp차감
            hp -= coll.gameObject.GetComponent<BallCtrl>().damage;
            if (hp <= 0)
            {
                MonsterDie();
            }
            print("!!!");
            //삭제
            Destroy(coll.gameObject);
            //
            animator.SetTrigger("IsHit");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ball"){
            //hp차감
            hp -= other.gameObject.GetComponent<BallCtrl>().damage;
            if (hp <= 0)
            {
                MonsterDie();
            }
            print("!!!");
            //삭제
            Destroy(other.gameObject);
            //
            animator.SetTrigger("IsHit");
        }
    }

    void MonsterDie()
    {

        gameObject.tag = "Untagged";

        StopAllCoroutines();

        isDie = true;
        monsterState = MonsterState.die;
        nvAgent.Stop();
        //nvAgent.isStopped = true;
        animator.SetTrigger("IsDie");

        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;

        foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }
        gameUI.DispScore(1);

        StartCoroutine(this.PushObjectPool());

        //company확률로 주인공 뒤에 스폰
        if(Random.Range(0.0f,1.0f) <= perCompany && GameMgr1.instance.idxCompany < 3){
            Debug.Log("Die!!");
            Transform comPos = playerPos.transform;
            GameMgr1.instance.AddCompany();
            //comPos.position += Vector3.right*3;
            //comPos.position += Vector3.back*3;
            Instantiate(companyMonster, comPos.position + Vector3.right*3, comPos.rotation);

        }
    }

    IEnumerator PushObjectPool()
    {
        yield return new WaitForSeconds(3.0f);

        isDie = false;
        hp = 100;
        gameObject.tag = "MONSTER";
        monsterState = MonsterState.idle;

        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;

        foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = true;
        }

        gameObject.SetActive(false);
    }

    void OnPlayerDie()
    {
        Debug.Log("Wow!");
        StopAllCoroutines();
        nvAgent.Stop();
        //nvAgent.isStopped = true;
        animator.SetTrigger("IsPlayerDie");
    }
}
