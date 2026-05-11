using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    public float timer = 0f;
    Player player;

    private InputAction jumpAction;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
        Init();
    }

    void Update()
    {
        switch(id)
        {
            case 0:
                rotateWeapons(id);
                break;
            case 1:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0;
                    Fire();
                }
                break;
            default:
                break;
        }

        if (jumpAction.WasPressedThisFrame())
        {
            LevelUp(10, 1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage += damage;
        this.count += count;
        
        if(id == 0)
        {
            setPosition();
        }
    }

    public void Init()
    {
        switch(id)
        {
            case 0:
                speed = 150;
                setPosition();
                break;
            case 1:
                speed = 0.3f;
                break; 
            default:
                break;
        }
    }

    private void rotateWeapons(int id)
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }

    private void setPosition()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;
            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            } else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;    
            }
            bullet.parent = transform;

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinity Per.
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
        {
            return;
        }

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
