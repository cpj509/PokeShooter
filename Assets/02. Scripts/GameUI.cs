using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Text txtScore;
    public Text txtGoal;
    public Image sliderHP;
    public Text txtHP;
	public GameObject txtSuccess;
	//public Text txtFail;

    //누적 점수
    private int totScore = 0;

    void Start()
    {
        DispScore(0);
        DispHP(100);
		txtSuccess.SetActive (false);
    }

    public void DispScore(int score)
    {
        totScore += score;
        txtScore.text = "SCORE <color=#ff0000>" + totScore.ToString() + "/10 </color>";
		if (totScore >= 10) {
			txtSuccess.SetActive (true);
		}
    }

    public void DispHP(int hp)
    {
        if (hp>= 0) {
            sliderHP.fillAmount = hp / 100f;
            txtHP.text = hp.ToString();
        }
    }
}
