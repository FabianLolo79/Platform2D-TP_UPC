
using UnityEngine;

public class FruitCollected : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetBool("Hit", true);
            gameObject.SetActive(false);
        }
    }
}
