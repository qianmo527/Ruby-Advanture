using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 血包(草莓)的脚本
public class HealthCollection : MonoBehaviour
{
    public AudioClip audioClip;
    public GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController rubyController = collision.GetComponent<RubyController>();
        if (rubyController!=null)
        {
            if (rubyController.currentHealth<rubyController.maxHealth)
            {
                Instantiate(effect, transform.position, Quaternion.identity);
                rubyController.PlaySound(audioClip);
                rubyController.ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
