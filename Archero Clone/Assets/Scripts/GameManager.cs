using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public List<GameObject> enemies;
    [SerializeField] private GameObject bottomObj;
    [SerializeField] private GameObject topObj;

    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start()
    {
        enemies = new List<GameObject>();

        MakeWall();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Player.Instance.SetAttackSpeed(0.1f);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            Player.Instance.SetAttackSpeed(-0.1f);
    }

    public void OnEnemyAppear(GameObject obj)
    {
        enemies.Add(obj);
        Player.Instance.enemyies = enemies;
    }

    public void OnEnemyDisappear(GameObject obj)
    {
        enemies.Remove(obj);
        Player.Instance.enemyies = enemies;
    }

    private void MakeWall()
    {
        // Bottom
        Vector3 bootomOrigin = new Vector3(0, 0, -Camera.main.orthographicSize - 1.5f);
        Instantiate(bottomObj, bootomOrigin, Quaternion.identity);

        // Top
        Vector3 TopOrigin = new Vector3(0, 0, 30 - Camera.main.orthographicSize - 1.5f + 2);
        Instantiate(topObj, TopOrigin, Quaternion.identity);
    }

}
