using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int speed;
    public Rigidbody2D rigidbody2d;

    public bool vertical;
    private int direction = 1;
    public const float changeTime = 3;
    private float timer;

    private Animator animator;
    public ParticleSystem smokeEffect;

    public bool broken = true;
    public GameObject hitEffect;

    private AudioSource audioSource;
    public AudioClip fixSound;
    public List<AudioClip> hitSound;
    

    // Start is called before the first frame update
    void Start()
    {
        timer = changeTime;
        animator = GetComponent<Animator>();
        PlayMoveAnimation();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            // 修好机器人 不再移动
            return;
        }


        timer -= Time.deltaTime;
        if (timer<=0)
        {
            direction *= -1;
            PlayMoveAnimation();
            timer = changeTime;
        }

        // 控制机器人的移动
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y += speed * Time.deltaTime * direction;
        }
        else
        {
            position.x += speed * Time.deltaTime * direction;
        }
        rigidbody2d.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController  rubyController = other.gameObject.GetComponent<RubyController>();
        if (rubyController!=null)
        {
            rubyController.ChangeHealth(-1);
        }
    }

    private void PlayMoveAnimation()
    {
        if (vertical)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
    }

    // 修复方法
    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        Invoke("PlayFixSound", 0.5f);
        Invoke("StopAudio", 1);
        Instantiate(hitEffect, transform.position+Vector3.up*0.5f, Quaternion.identity);
        if (GameManager.instance.robotNum > 0) {
            GameManager.instance.robotNum -= 1;
        }
    }

    private void PlayFixSound() {
        audioSource.PlayOneShot(hitSound[Random.Range(0, 2)]);
    }
    private void StopAudio() {
        audioSource.Stop();
    }
}
