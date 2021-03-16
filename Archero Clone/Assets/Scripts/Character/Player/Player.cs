using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    private enum PlayerState
    {
        IDLE,
        RUN,
        ATTACK,
        HIT
    }

    private StateMachine<Player> playerStateMachine;

    //스테이트들을 보관
    private Dictionary<PlayerState, State<Player>> dicState = new Dictionary<PlayerState, State<Player>>();

    public Rigidbody rb;
    public JoystickMove joystickMove;
    public PlayerAnimation playerAnimation;
    public Gun currentGun;

    public List<GameObject> enemyies; // 스테이지내 모든 적들
    public GameObject target; // 가장 가까운 적

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;

    public float Speed { private set; get; }
    [SerializeField] private float attackSpeed;

    private float recoverTime = 0f;


    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        joystickMove = GameObject.Find("JoyStick").GetComponent<JoystickMove>();
        playerAnimation = GetComponent<PlayerAnimation>();
        Speed = 5f;
        currentGun = GetComponentInChildren<Gun>();
    }

    private void Start()
    {
        // 상태 생성
        State<Player> idle = new PlayerIdleState();
        State<Player> run = new PlayerRunState();
        State<Player> hit = new PlayerHitState();
        State<Player> attack = new PlayerAttackState();

        dicState.Add(PlayerState.IDLE, idle);
        dicState.Add(PlayerState.RUN, run);
        dicState.Add(PlayerState.HIT, hit);
        dicState.Add(PlayerState.ATTACK, attack);

        // 기본상태 설정
        playerStateMachine = new StateMachine<Player>();
        playerStateMachine.SetState(dicState[PlayerState.IDLE], this);
    }

    // Update is called once per frame
    void Update()
    {
        // 조이스틱 입력
        InputHandle();

        playerStateMachine.DoOperateUpdate();
        playerAnimation.SetAttackSpeed(attackSpeed);
    }

    private void InputHandle()
    {
        if (joystickMove.touchState == JoystickMove.TouchState.DOWN)
        {
            playerStateMachine.SetState(dicState[PlayerState.IDLE], this);
        }
        else if (joystickMove.touchState == JoystickMove.TouchState.DRAG)
        {
            if (playerStateMachine.CurrentState == dicState[PlayerState.HIT])
            {
                if (recoverTime >= 0)
                {
                    recoverTime -= Time.deltaTime;
                    if (recoverTime <= 0)
                        playerStateMachine.SetState(dicState[PlayerState.IDLE], this);
                }
            }
            else
            {
                playerStateMachine.SetState(dicState[PlayerState.RUN], this);
            }
        }
        else if (joystickMove.touchState == JoystickMove.TouchState.UP)
        {
            target = RaycastEnemy();
            if (target != null)
                playerStateMachine.SetState(dicState[PlayerState.ATTACK], this);
            else
                playerStateMachine.SetState(dicState[PlayerState.IDLE], this);
        }
    }

    private void HitEvent(float recoverTime)
    {
        if (playerStateMachine.CurrentState == dicState[PlayerState.HIT])
        {
            if (recoverTime >= 0)
            {
                recoverTime -= Time.deltaTime;
                if (recoverTime <= 0)
                    playerStateMachine.SetState(dicState[PlayerState.IDLE], this);
            }
        }
    }

    public void ObjTriggerEnter(GameObject gameObject)
    {
        Debug.Log("부딧힘");
        if (gameObject.tag == "Enemy")
        {
            recoverTime = 0.5f;
            playerStateMachine.SetState(dicState[PlayerState.HIT], this);
        }
    }

    public void OnAttack()
    {
        Debug.Log("총알 발사");
        Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        if(attackSpeed > 0.0f)
            this.attackSpeed -= attackSpeed;
        else
            this.attackSpeed += attackSpeed;
    }

    // 가장 가까운 적을 반환함
    public GameObject RaycastEnemy()
    {
        RaycastHit hit;
        int shortesIndex = -1;
        float shortesDistance = Mathf.Infinity; // player와 가장 가까운 enemy 거리
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Enemy");

        for (int i = 0; i < enemyies.Count; i++)
        {
            bool ishit = Physics.Raycast(transform.position, enemyies[i].transform.position - transform.position, 
                out hit, currentGun.fireDistance);

            if (ishit && hit.transform.tag == "Enemy")
            {
                float distance = Vector3.Distance(transform.position, enemyies[i].transform.position);
                if (distance < shortesDistance)
                {
                    shortesDistance = distance;
                    shortesIndex = i;
                }
            }
        }
        if (shortesIndex == -1)
            return null;
        else
            return enemyies[shortesIndex];
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Enemy");
        Vector3 addPos = new Vector3(0,0.2f,0);
        bool ishit = Physics.Raycast(transform.position, transform.forward + addPos, out hit, 20f, layerMask);

        if (ishit && hit.transform.tag == "Enemy")
        {
            Gizmos.color = Color.blue;
            Debug.Log("hit");
        }
        else
            Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position + addPos, transform.forward * 20f);
    }

    public GameObject GetTarget()
    {
        return target;
    }
}
