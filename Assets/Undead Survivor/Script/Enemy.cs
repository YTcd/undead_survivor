using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2.0f;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animController;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!isLive)
        {
            return;
        }
        
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);


        rigid.linearVelocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }

        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.m_player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animController[data.enemyType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
}
