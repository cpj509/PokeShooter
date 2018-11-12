using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanyCtrl : MonoBehaviour {

    public enum CompanyState { idle, trace, attack, die };
    public CompanyState companyState = CompanyState.idle;

    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    //추적을 멈출 거리
    public float traceDist = 5.0f;

    //공격 혹은 피격

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

            if(dist >= traceDist){
                companyState = CompanyState.trace;
            }
            else{
                companyState = CompanyState.idle;
            }
        }
    }

    IEnumerator CompanyAction(){
        while(true){
            switch(companyState){
                case CompanyState.idle:
                    nvAgent.isStopped = true;
                    break;
                case CompanyState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false;

                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", true);
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
		
	}
}
