using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    public GameObject alertUI; //알림창 UI
    public string sceneName; // 전환할 씬 이름
    private bool isPlayerInRange = false;

    private void Update(){
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F)){
            SceneManager.LoadScene(sceneName); //씬전환
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")){
            isPlayerInRange = true;
            alertUI.SetActive(true); // 알림창 활성화
            Debug.Log("플레이어가 포탈에 접촉");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            isPlayerInRange = false;
            alertUI.SetActive(false);
            Debug.Log("플레이어가 포탈에서 나감");

        }
    }
}
