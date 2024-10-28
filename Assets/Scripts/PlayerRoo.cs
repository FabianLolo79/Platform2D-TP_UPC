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
    [SerializeField] private float _rebotingForce;
    
    [SerializeField] private  Rigidbody2D _rb; // referencia desde el editor
    [SerializeField] private GameObject _enemy;


    private Vector2 _movement;

    //Boleans comprobation
    private bool _isGrounded;
    private bool _facingRight = true;
    //public bool _hit = false;
    //private bool _isAttacking;


    private void FixedUpdate() // es mejor para las f�sicas
    {
        Move();
        Jump();


        //if (_hit == true)
        //{
        //    _rb.AddForce(Vector2.up * _jumpHitForce, ForceMode2D.Impulse);
        //    _hit = false;
        //}

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
            //_rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);

            // prueba con el ejemplo del profe y texto 
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
        }
    }

    


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            _isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Head_Enemy"))
        {
            Reboting();
            _enemy.SetActive(false);

        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            gameObject.SetActive(false);
        }
    }

    private void Reboting()
    {
        _rb.AddForce(Vector2.up * _rebotingForce, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;  
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }



    public void TakeDamage()
    {
        _lives --;
        if (_lives < 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log($"Character has died");
    }
}

