using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _wayPoint;

    public void Move()
    {
        //if (transform.position != _wayPoint.position)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, _wayPoint.position,
        //                            _speed * Time.deltaTime);
        //}
        gameObject.SetActive(false);
    }
}
