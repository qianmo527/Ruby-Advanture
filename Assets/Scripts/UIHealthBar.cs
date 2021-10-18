using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Image healthBar;
    public RubyController rb;
    public bool hasTask = false;
    public bool ifCompleteTask = false;
    public int robotNum;

    public static UIHealthBar instance {
        get;
        private set;
    }

    private void Awake() {
        // 单例
        instance = this;
        robotNum = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 改变血条样式
        healthBar.GetComponent<Image>().fillAmount = rb.currentHealth / rb.maxHealth;
    }
}
