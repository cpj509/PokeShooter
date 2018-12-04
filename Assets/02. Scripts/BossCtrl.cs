using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossCtrl : MonoBehaviour {

    public float bossHp = 1000;
    private Animator bossAnim;

    public float skillTime = 5.0f;

    public Slider hpSlider;

    public Transform[] rockSpawn;
    public GameObject rockPrefab;
    public GameObject meteorPrefab;


    public Transform[] meteorSpawn;
    public int meteorNum = 20;
    //private Transform meteorSpawn;
    private Vector3 meteorVec = new Vector3();

    private bool isAlive = true;

	// Use this for initialization
	void Start () {
        bossHp = 1000;
        bossAnim = GetComponent<Animator>();

        hpSlider.maxValue = bossHp;
        hpSlider.value = bossHp;
        StartCoroutine(BossSkill());

	}

    IEnumerator BossSkill(){
        while(true){
            yield return new WaitForSeconds(skillTime);
            //유성 공격
            MeteorStrike();
            yield return new WaitForSeconds(skillTime);
            //총알 공격
            RockSkrike();
        }

    }

    void MeteorStrike(){
        bossAnim.SetTrigger("AttackA");
        Debug.Log("AttackA");

        for (int i = 0; i < 10/*meteorSpawn.Length*/; i++){

            float spawnX = Random.Range(-25, 25);
            float spawnZ = Random.Range(-25, 25);
            meteorVec.Set(spawnX,10,spawnZ);
            meteorSpawn[0].position = meteorVec;

            Instantiate(meteorPrefab, meteorSpawn[0].position, meteorSpawn[0].rotation);
        }

    }
    

    void RockSkrike(){
        bossAnim.SetTrigger("AttackB");
        Debug.Log("AttackB");

        for (int i = 0; i < rockSpawn.Length; i++){
            var bullet = Instantiate(rockPrefab, 
                                     rockSpawn[i].position, 
                                     rockSpawn[i].rotation);

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
            Destroy(bullet, 5.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GameMgr1.instance.companyAttack = gameObject;
            bossHp -= collision.gameObject.GetComponent<BallCtrl>().damage;
            hpSlider.value = bossHp;
            if (bossHp <= 0)
            {
                BossDie();
            }

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "COMPANY")
        {
            Debug.Log("COMPANY ATTACK");
            bossHp -= 10;
            hpSlider.value = bossHp;
            if (bossHp <= 0)
            {
                BossDie();
            }
            StartCoroutine(CompanyAttack());

        }
    }

    IEnumerator CompanyAttack(){
        while(isAlive){
            yield return new WaitForSeconds(1.0f);
            bossHp -= 10;
            hpSlider.value = bossHp;
            if (bossHp <= 0)
            {
                BossDie();
                break;
            }
        }

    }

    void BossDie(){
        bossAnim.SetTrigger("isDie");
        isAlive = false;
        StartCoroutine(EndingStage());
    }

    IEnumerator EndingStage(){
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Ending_Stage");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
