using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubiCollected : MonoBehaviour
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
