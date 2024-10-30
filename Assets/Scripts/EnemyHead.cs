using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    
    public void EnemyHit()
    {
        _enemy.SetActive(false);
    }
}
