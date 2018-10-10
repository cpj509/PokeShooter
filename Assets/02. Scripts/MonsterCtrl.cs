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

    public float company = 1.0f;

    //추적을 시작할 거리
    public float traceDist = 10.0f;
	  //공격을 시작할 거리
    public float attackDist = 2.0f;
	  //몬스터의 체력
    private int hp = 100;
    //초기 생존 상태
    private bool isDie = false;
  
    void Awake()
    {
        monsterTr = this.gameObject.GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        animator = this.gameObject.GetComponent<Animator>();
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
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

        //company확률로 주인공 뒤에 스
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
