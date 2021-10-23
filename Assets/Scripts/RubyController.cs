using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    public VariableJoystick joystick;
    private Rigidbody2D rigidbody2d;
    public Vector2 respawn = new Vector2(0, 0);
    public float maxHealth = 5;
    public float currentHealth;
    public int speed = 3;
    public float attackCD = 1;
    private float attackTimer;

    // 无敌时间
    public float timeInvincible = 2.0f;
    private bool isInvincible;
    private float invincibleTimer;
    
    private Animator animator;
    private Vector2 lookDirection = new Vector2(1, 0);

    public GameObject bulletPrefab;

    public AudioSource audioSource;
    public AudioSource walkAudio;
    public AudioClip hurt;
    public AudioClip attack;
    public AudioClip walk;


    public static RubyController instance {
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
        // Application.targetFrameRate = 30
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 无敌时间的处理
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer<=0)
            {
                isInvincible = false;
            }
        }

        // 与NPC的对话
        if (Input.GetKeyDown(KeyCode.T)) {
            // 射线检测是否在NPC周围
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position+Vector2.up*0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider!=null) {
                NPCdialog npcdialog = hit.collider.gameObject.GetComponent<NPCdialog>();
                if (npcdialog!=null) {
                    npcdialog.DisplayDialog();
                }
            }
        }
    }

    void FixedUpdate()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0)||!Mathf.Approximately(move.y,0))
        {
            // 人物朝向
            lookDirection = move;
            lookDirection.Normalize();
            if (!walkAudio.isPlaying) {
                walkAudio.clip = walk;
                walkAudio.Play();
            }
        }
        else {
            walkAudio.Stop();
        }
        //移动
        Vector2 position = transform.position;
        //Ruby位置的移动
        position = position + speed * move * Time.deltaTime;
        rigidbody2d.MovePosition(position);

        //动画的控制
        animator.SetFloat("Look X",lookDirection.x);
        animator.SetFloat("Look Y",lookDirection.y);
        animator.SetFloat("Speed",move.magnitude);

        if (attackTimer > 0) {
            attackTimer -= Time.fixedDeltaTime;
        }
    }

    // 改变血量的方法
    public void ChangeHealth(int amount)
    {
        if (amount<0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
            PlaySound(hurt);
            animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth+amount, 0, maxHealth);

        if (currentHealth <= 0) {
            Respawn();
        }
    }

    // 发射子弹的方法
    public void Launch() {
        if (GameManager.instance.hasTask) {
            // 计时器
            if (attackTimer<=0) {
                attackTimer = attackCD;
            }else {
                return;
            }
            GameObject bulletObject = Instantiate(bulletPrefab,transform.position+Vector3.up*0.5f,Quaternion.identity);
            Bullet bullet= bulletObject.GetComponent<Bullet>();
            bullet.Launch(lookDirection,300);
            animator.SetTrigger("Launch");
            PlaySound(attack);
        }
    }

    public void PlaySound(AudioClip audioClip) {
        audioSource.PlayOneShot(audioClip);
    }

    public void Respawn() {
        currentHealth = maxHealth;
        transform.position = respawn;
    }
}
