using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(player != null)
            player.ObjTriggerEnter(other.transform.parent.gameObject);
    }
   
}
