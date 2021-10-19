using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Image healthBar;
    public RubyController rb;

    public static UIHealthBar instance {
        get;
        private set;
    }

    private void Awake() {
        // 单例
        instance = this;
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
