using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.OnEnemyAppear(other.transform.parent.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
