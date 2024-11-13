using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollection : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    //private void Awake()
    //{
    //    _animator = GetComponent<Animator>();
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerRoo>();

            player.AddLife();
            //_animator.SetBool("Hit", true);
            gameObject.SetActive(false);
        }
    }

}
