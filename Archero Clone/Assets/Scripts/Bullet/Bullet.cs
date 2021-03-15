using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;


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

    public void ObjTriggerEnter(GameObject obj)
    {
        if (obj.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(obj);
        }
    }
}
