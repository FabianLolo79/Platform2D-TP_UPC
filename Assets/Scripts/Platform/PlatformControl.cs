using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{

    PlatformEffector2D _platformEffector2D;

    bool _leftPlatform;

    void Start()
    {
        _platformEffector2D = GetComponent<PlatformEffector2D>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown("Vertical") && !_leftPlatform)
        {
            _platformEffector2D.rotationalOffset = 180;

            _leftPlatform = true;
        }
    }

    private void OnCollisionExit2D()
    {
        _platformEffector2D.rotationalOffset = 0;

        _leftPlatform = false;
    }
}
