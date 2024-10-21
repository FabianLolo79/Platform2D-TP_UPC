using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRoo : MonoBehaviour
{
    //Variables
    [SerializeField] private int _lives;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    //  [SerializeField] private float _jumpChargeForce; salto con carga
    [SerializeField] private  Rigidbody2D _rb;

    private Vector2 _movement;

    //Boleans comprobation
    private bool _isGrounded;
    private bool _facingRight = true;
    //private bool _isAttacking;
    //private bool _isCharging = false;

    // public float chargeDuration = 0.5f;  Duración de la carga

    private void Update()
    {
        Move();
        Jump();
    }


    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(horizontalInput * _speed, _rb.velocity.y);

        //Flip character
        if (horizontalInput < 0f && _facingRight == true)
        {
            Flip();
        }
        else if (horizontalInput > 0f && _facingRight == false)
        {
            Flip();
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _isGrounded = false;
        }

        //if (Input.GetKey(KeyCode.LeftControl) && _isGrounded && !_isCharging)
        //{

        //    _isCharging = true;
        //    StartCoroutine(Charge());
        //}

    }

    private void JumpWithCharge()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            _isGrounded = true;
        }
    }

    //IEnumerator Charge()
    //{
    //    float timer = 0;
    //    while (timer < chargeDuration)
    //    {
    //        // Aumentar la velocidad del personaje
    //        _rb.velocity += Vector2.up * _jumpChargeForce * Time.deltaTime;
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }
    //    _isCharging = false;
    //}

    private void Flip() // solucionado
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x; // error estaba transform.position.x fue cambiado al actual 
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
