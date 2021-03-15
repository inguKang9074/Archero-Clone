using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    private Bullet bullet;

    private void Start()
    {
        bullet = transform.parent.GetComponent<Bullet>();
    }

    private void OnTriggerEnter(Collider other)
    {
        bullet.ObjTriggerEnter(other.transform.parent.gameObject);
    }
}
