using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

    public Text txtScore;
    public Text txtGoal;
    public Image sliderHP;
    public Text txtHP;
	public GameObject txtSuccess;
    public Text txtGold;
    //public Text txtFail;

    public GameObject[] ballImage;
    public GameObject[] CompanyImage;

    //상점 UI
    public GameObject ShopUI;

    //누적 점수
    private int totScore = -1;

    public int goalScore = 10;
    public GameObject HudUI;

    void Start()
    {
        DispScore(0);
        DispHP(100);
		txtSuccess.SetActive (false);
        ShopUI.SetActive(false);
        txtGold.text = GameData.m_gold.ToString();
        txtGoal.text = "Kill " + goalScore + " Monsters!";

        HudUI.SetActive(true);
        SetBallImage();
        SetCompanyImage();
    }

    public void DispScore(int score)
    {
        totScore += 1;
        txtScore.text = "SCORE <color=#ff0000>" + totScore.ToString() + "/"+ goalScore.ToString() + " </color>";
        if (totScore >= goalScore) {
			txtSuccess.SetActive (true);
            ShopUI.SetActive(true);
            HudUI.SetActive(false);
		}
    }

    public void DispHP(int hp)
    {
        if (hp>= 0) {
            sliderHP.fillAmount = hp / 100f;
            txtHP.text = hp.ToString();
        }
    }

    public void DisGold(){
        txtGold.text = GameData.m_gold.ToString();
    }

    public void BuySuperBall(){
        if(GameData.m_gold >= 20){
            GameData.m_ballIdx = 1;
            GameData.m_gold -= 20;
            DisGold();
        }
    }

    public void BuyHyperBall(){
        if (GameData.m_gold >= 30)
        {
            GameData.m_ballIdx = 2;
            GameData.m_gold -= 30;
            DisGold();
        }
    }

    public void BuyMasterBall(){
        if (GameData.m_gold >= 40)
        {
            GameData.m_ballIdx = 3;
            GameData.m_gold -= 40;
            DisGold();
        }
    }

    public void RestartStage(){
        GameData.m_stage = 0;
        SceneManager.LoadScene("gfPlay1");

    }

    public void NextStage(){
        GameData.m_stage = 1;
        SceneManager.LoadScene("GameField_Desert");

    }

    public void BossStage(){
        GameData.m_stage = 2;
        SceneManager.LoadScene("GameField_Boss");

    }

    void SetBallImage(){
        switch(GameData.m_ballIdx){
            case 0:
                ballImage[1].SetActive(false);
                ballImage[2].SetActive(false);
                ballImage[3].SetActive(false);
                ballImage[0].SetActive(true);
                break;
            case 1:
                ballImage[1].SetActive(true);
                ballImage[2].SetActive(false);
                ballImage[3].SetActive(false);
                ballImage[0].SetActive(false);
                break;
            case 2:
                ballImage[1].SetActive(false);
                ballImage[2].SetActive(true);
                ballImage[3].SetActive(false);
                ballImage[0].SetActive(false);
                break;
            case 3:
                ballImage[1].SetActive(false);
                ballImage[2].SetActive(false);
                ballImage[3].SetActive(true);
                ballImage[0].SetActive(false);
                break;
        }
    }

    public void SetCompanyImage(){
        for (int i = 0; i < 3; i++){
            if (!GameData.m_companyArray[i])
            {
                CompanyImage[i].SetActive(false);
            }
            else { CompanyImage[i].SetActive(true); }
        }
    }
}
