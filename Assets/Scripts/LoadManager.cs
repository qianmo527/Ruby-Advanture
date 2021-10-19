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
    public GameObject warningUI;

    public static LoadManager instance;

    private void Awake() {
        // 单例
        if (instance==null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else {
            Destroy(gameObject);
        }
    }

    private void Update() {
        
    }

    // 显示警告面板
    public void ShowWarning(string content) {
        

        StartCoroutine("_ShowWarning", content);
    }
    private IEnumerator _ShowWarning(string content) {
        
        warningUI.SetActive(true);

        yield return null;
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
