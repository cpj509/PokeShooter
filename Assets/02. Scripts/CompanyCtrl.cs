using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanyCtrl : MonoBehaviour {

    public enum CompanyState { idle, trace, attack, die, enemy};
    public CompanyState companyState = CompanyState.idle;

    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    //추적을 멈출 거리
    public float traceDist = 5.0f;

    //공격 혹은 피격
    private Transform enemyTr = null;

    // Use this for initialization
    void Awake () {
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = gameObject.GetComponent<NavMeshAgent>();

        animator = gameObject.GetComponent<Animator>();
	}

    private void OnEnable()
    {
        PlayerCtrl.OnPlayerDie += this.OnPlayerDie;
        StartCoroutine(CheckCompanyState());
        StartCoroutine(CompanyAction());
    }

    private void OnDisable()
    {
        PlayerCtrl.OnPlayerDie -= this.OnPlayerDie;
    }

    IEnumerator CheckCompanyState(){
        while(true){
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTr.position, gameObject.transform.position);

            if (GameMgr1.instance.companyAttack == null){
                enemyTr = null;
                if (dist >= traceDist)
                {
                    companyState = CompanyState.trace;
                }
                else
                {
                    companyState = CompanyState.idle;
                }
            }
            else{
                enemyTr = GameMgr1.instance.companyAttack.transform;
                companyState = CompanyState.enemy;
            }
            Debug.Log("Target : " + GameMgr1.instance.companyAttack);
        }
    }

    IEnumerator CompanyAction(){
        while(true){
            switch(companyState){
                case CompanyState.idle:
                    nvAgent.isStopped = true;
                    animator.SetBool("IsTrace", false);
                    break;
                case CompanyState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false;

                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", true);
                    break;
                case CompanyState.enemy:
                    nvAgent.destination = enemyTr.position;
                    nvAgent.isStopped = false;
                    animator.SetBool("IsTrace",false);
                    animator.SetBool("IsAttack", true);
                    break;
            }
            yield return null;
        }
    }

    void OnPlayerDie()
    {
        Debug.Log("Wow!");
        StopAllCoroutines();
        //nvAgent.Stop();
        nvAgent.isStopped = true;
        animator.SetTrigger("IsPlayerDie");
    }

    // Update is called once per frame
    void Update () {
        //if(GameMgr1.instance.companyAttack != null){
        //    enemyTr = GameMgr1.instance.companyAttack.transform;
        //    AttackEnemy();
        //}

	}

    void AttackEnemy(){
        //Debug.Log("Attack");
        
    }
}
