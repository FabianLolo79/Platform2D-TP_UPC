using UnityEngine;

public class GemaCollected : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.CompareTag("Player"))
         {
            var player = collision.gameObject.GetComponent<PlayerRoo>();
            player.AddScore(_points);

            //_animator.SetBool("Hit", true);
            gameObject.SetActive(false);
         }
    }


}
