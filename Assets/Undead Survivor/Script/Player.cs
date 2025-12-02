using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 3.0f;
    public Vector2 inputVec;
    [SerializeField]
    public InputAction moveAction;

    private GameObject m_player;
    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator animator;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        m_player = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVec = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
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

        animator.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
        
    }
}
