using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }

    public void Attack()
    {
        Debug.Log("공격!");
        player.OnAttack();
    }
}
