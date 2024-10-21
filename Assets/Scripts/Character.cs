using UnityEngine;

public class Character 
{
    // Atributos de la clase
    protected int _lives;
    protected float _speed;

    // Constructor de la clase
    public Character(int initialLives,float initialSpeed)
    {
        _lives = initialLives;
        _speed = initialSpeed;
    }

    
    public virtual void TakeDamage(int damage)
    {
        _lives -= damage;
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
