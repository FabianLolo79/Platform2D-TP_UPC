using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject _player;
    

    private void Start()
    {
        Respawner();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawner();
        }
    }

    private void Respawner()
    {
        _player.SetActive(true);
        _playerTransform.position = transform.position;

    }
}
