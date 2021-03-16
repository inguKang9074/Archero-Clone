using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 현 스테이지에 있는 모든 Enemy를 체크
        GameManager.Instance.OnEnemyAppear(other.transform.parent.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
