using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public GameObject loadScreen;
    public GameObject screen;
    public GameObject login;
    public Slider slider;
    public Text text;
    public bool cancel = false;

    private void Update() {
        
    }

    public void LoadSingleLevel() {
        StartCoroutine(LoadLevel());
    }

    public void LoadMultiLevel() {
        // login.SetActive(true);
    }

    public void CancelButton() {
        if (loadScreen.activeInHierarchy) {
            //TODO 解决加载时将取消键也归入任意键的问题
            loadScreen.SetActive(false);
        }
        if (screen.activeInHierarchy) {
            screen.SetActive(false);
        }
        if (login.activeInHierarchy) {
            login.SetActive(false);
        }
    }

    // 异步加载场景并显示进度条
    IEnumerator LoadLevel() {
        loadScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        
        operation.allowSceneActivation = false;
        while (!operation.isDone) {
            slider.value = operation.progress;
            text.text = (int)operation.progress * 100 + "%";

            if (operation.progress >= 0.9f) {
                slider.value = 1;
                text.text = "按任意按键继续";

                if (Input.anyKeyDown) {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
