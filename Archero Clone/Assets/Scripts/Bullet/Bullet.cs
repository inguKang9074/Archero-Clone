using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;
    private int wallCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    public void ObjTriggerEnter(GameObject target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();

        if (damageable != null)
        {
            DamageMessage damageMessage;
            damageMessage.damager = Player.Instance.gameObject;
            damageMessage.amount = Player.Instance.currentGun.damage;

            damageable.ApplyDamage(damageMessage);
        }
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (wallCount > 0)
        {
            wallCount--;
            transform.forward = Vector3.Reflect(transform.forward, collision.GetContact(0).normal);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
