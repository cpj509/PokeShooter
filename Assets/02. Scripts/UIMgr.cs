using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMgr : MonoBehaviour {

    public GameObject keyImage;

    public void OnClickStartBtn()
    {
        keyImage.SetActive(true);

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame(){
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("gfPlay1");
    }
}
