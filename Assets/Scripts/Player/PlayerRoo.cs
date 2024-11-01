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
    [SerializeField] private float _distanceJumpChecker;
    [SerializeField] private LayerMask _groudLayerMask;
    [SerializeField] private  Rigidbody2D _rb; // referencia desde el editor
    [SerializeField] private GameObject _enemy;
    

    private Vector2 _movement;

    //Boleans comprobation
    private bool _isGrounded = false;
    private bool _facingRight = true;
    private bool _hasToJump;
    private float _horizontalInput;
    //public bool _hit = false;
    //private bool _isAttacking;

    public void Update()
    {
        CheckInputs();
        Jump();

    }

    private void FixedUpdate() // es mejor para las físicas
    {
        Move();
    }

    private void CheckInputs()
    {
        _hasToJump = Input.GetButtonDown("Jump"); // Input.GetButtonDown("Jump") Input.GetKey(KeyCode.V) 
        _horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {
        _rb.velocity = new Vector2(_horizontalInput * _speed, _rb.velocity.y);

        //Flip character
        if (_horizontalInput < 0f && _facingRight == true)
        {
            Flip();
        }
        else if (_horizontalInput > 0f && _facingRight == false)
        {
            Flip();
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
            var enemyHead = collision.gameObject.GetComponent<EnemyHead>();
            
            if (enemyHead != null)
            {
                enemyHead.EnemyHit();
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            gameObject.SetActive(false);
        }
    }

    private void Jump()
    {
        if (_hasToJump) // Input.GetButtonDown("Jump") Input.GetKey(KeyCode.V) 
        {
            var raycast2d = Physics2D.Raycast(transform.position, Vector2.down, _distanceJumpChecker, _groudLayerMask);

            if(raycast2d.collider == null) return;
            //_rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);

            // prueba con el ejemplo del profe y texto 
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
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

