using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerRoo : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rb; // referencia desde el editor

    [Header("Movement")] //Variables
    [SerializeField] private float _horizontalInput = 0f;
    [SerializeField] private float _speed;
    [Range(0, 0.3f)][SerializeField] private float _moveSmooth;
    private Vector3 _speedZ  = Vector3.zero;
    private bool _facingRight = true;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groudLayerMaskQueEsSuelo;
    private bool _hasToJump;
    [SerializeField] private float _distanceJumpChecker;
    [SerializeField] private bool _enSuelo;
    //[SerializeField] private Transform _controlGrounded;
    //[SerializeField] private Vector3 dimensionCaja;

    [Header("Reboting")]
    [SerializeField] private float _rebotingForce;
    
    
    

    [Header("HUD")]
    [SerializeField] private int _lives;
    public int score;
    public TMP_Text scoreText;
    public TMP_Text livesText;

    [Header("trigger for cam")]
    public GameObject hitTrigger;

    //[SerializeField] private GameObject _enemy; se usa esto? revisar 07/11/2024
    [Header("ANIMATOR")]
    [SerializeField] private Animator _animator; // referencia desde el editor

    //public bool _hit = false;
    //private bool _isAttacking;


    private void Start()
    {
        score = 0;
        UpdateScoreText();
        UpdateScoreLive();
    }

    public void Update()
    {
        CheckInputs();
        Jump();
    }

    private void FixedUpdate() // es mejor para las f�sicas
    {
        //CamControll camController = hitTrigger.GetComponent<CamController>();

        Move(_horizontalInput * Time.deltaTime);
        //if (camController.stopPlayerMovement == false)
        //{
            
        //}
        //else return;
    }

    private void CheckInputs()
    {
        _hasToJump = Input.GetButtonDown("Jump"); // Input.GetButtonDown("Jump") Input.GetKey(KeyCode.V) 
        _horizontalInput = Input.GetAxisRaw("Horizontal") * _speed;
    }

    private void Move(float mover)
    {
        //_rb.velocity = new Vector2(_horizontalInput , _rb.velocity.y);

        Vector3 velocidadObjetivo = new Vector2(mover, _rb.velocity.y);
        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, velocidadObjetivo, ref _speedZ, _moveSmooth);

        _animator.SetFloat("Horizontal", Mathf.Abs(_horizontalInput));

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
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    _isGrounded = true;
        //}

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
            //gameObject.SetActive(false);
        }
    }

    private void Jump() // MEJORANDO POR AC�
    {
        
        //enSuelo = Physics2D.OverlapBox(_controlGrounded.position, dimensionCaja, 0f, _groudLayerMaskQueEsSuelo);
        if (_hasToJump) // Input.GetButtonDown("Jump") Input.GetKey(KeyCode.V) 
        {
            //_animator.SetBool("enSuelo", true);
            var raycast2d = Physics2D.Raycast(transform.position, Vector2.down, _distanceJumpChecker, _groudLayerMaskQueEsSuelo);

            //_animator.SetBool("enSuelo", true);

            if (raycast2d.collider == null) return;
            //_rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);

            // prueba con el ejemplo del profe y texto 
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            
            _animator.SetBool("enSuelo", false);
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
        _lives--;
        if (_lives == 0)
        {
            //Die();
            SceneManager.LoadScene(0);
        }
        UpdateScoreLive();
    }

    //TODO 
    public void AddLife()
    {
        _lives++;
        int maxLife = 3;


        if (_lives > maxLife)
        {
            _lives = maxLife;
        }
        UpdateScoreLive();
    }

    public virtual void Die()
    {
        Debug.Log($"Character has died");
    }

    public void UpdateScoreLive()
    {
        //scoreText.text = $"SCORE: {score}";   "Score" + _score ;
        livesText.text = $"x {_lives}";
    }

    //TODO FALTA
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = $"SCORE: {score}";  // "Score" + _score ;
    }

    //public void GameOver()
    //{
    //    ResetScore();
        
    //}

    //public void ResetScore()
    //{

    //    _lives = 1;
    //    score = 0;
    //    UpdateScoreText();
    //    UpdateScoreLive();
    //}

   
}

