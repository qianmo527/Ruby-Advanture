using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCdialog : MonoBehaviour
{
    public GameObject img_dialog;
    public float displayTime = 5.0f;
    private float timer;
    public Text text;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        img_dialog.SetActive(false);
        timer = displayTime;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (img_dialog.activeInHierarchy == true) {
            timer -= Time.deltaTime;
        }
        if (timer<=0) {
            img_dialog.SetActive(false);
            timer = displayTime;
        }
        if (UIHealthBar.instance.robotNum <= 0) {
            UIHealthBar.instance.ifCompleteTask = true;
        }
    }

    public void DisplayDialog() {
        img_dialog.SetActive(true);
        UIHealthBar.instance.hasTask = true;
        if (UIHealthBar.instance.ifCompleteTask) {
            text.text = "完成的不错嘛";
            audioSource.Play();
            Invoke("Mute", 3);
        }
    }

    private void Mute() {
        audioSource.mute = true;
    }
}
