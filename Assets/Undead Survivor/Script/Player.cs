using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float m_speed = 3.0f;
    public Vector2 inputVec;
    public InputAction moveAction;
    public Scanner scanner;
    public Hand[] hands; // [left, right]
    public RuntimeAnimatorController[] animatorControllers;
    public FloatingJoystick joystick;

    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void OnEnable()
    {
        m_speed *= Character.Speed;
        animator.runtimeAnimatorController = animatorControllers[GameManager.instance.playerId];
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }
        // inputVec = moveAction.ReadValue<Vector2>();
        inputVec = joystick != null && joystick.gameObject.activeInHierarchy
        ? joystick.Direction
        : moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }
        // 1. 힘을 주기
        // rigid.AddForce(inputVec);

        // 2. 속도 제어
        // rigid.linearVelocity = inputVec;

        // 3. 위치 이동
        Vector2 nextVec = inputVec.normalized * m_speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        // m_player.transform.position = new Vector2(inputVec.x * Time.fixedDeltaTime ,inputVec.y * Time.fixedDeltaTime);
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        animator.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (GameManager.instance.isLive == false)
        {
            return;
        }

        GameManager.instance.health -= Time.deltaTime * 10;

        if (GameManager.instance.health < 0)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
