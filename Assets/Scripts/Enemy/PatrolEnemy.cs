using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _waitTime;
    [SerializeField] private Transform[] _wayPoints;

    private bool _facingRight;
    private int _currentWaypoint;
    private bool _isWaiting;



    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(transform.position != _wayPoints[_currentWaypoint].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, _wayPoints[_currentWaypoint].position,
                                    _speed * Time.deltaTime);
        }
        else if(!_isWaiting)
        {
            StartCoroutine(Wait());
        }

        //Flip character
        if (_facingRight == false)
        {
            return;
        }
        else if (_facingRight)
        {
            Flip();
        }
    }

    IEnumerator Wait() // Currutina espra de tiempo
    {
        _isWaiting = true;
        yield return new WaitForSeconds(_waitTime);
        _currentWaypoint++;

        if (_currentWaypoint == _wayPoints.Length)
        {
            _currentWaypoint = 0;
        }

        _isWaiting = false;
    }


    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
