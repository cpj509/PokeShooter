using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Text txtScore;
    public Slider sliderHP;
	public GameObject txtSuccess;
	//public Text txtFail;
	public Text txtGoal;

    //누적 점수
    private int totScore = 0;

    void Start()
    {
        DispScore(0);
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
        sliderHP.value = hp;
    }
}
