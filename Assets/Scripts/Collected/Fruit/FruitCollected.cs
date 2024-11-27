using System.Collections;
using UnityEngine;

public class FruitCollected : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _points;

    private bool _waitingToDisable = false;

    private IEnumerator DisableDelay()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || _waitingToDisable) return;
        
        var player = collision.gameObject.GetComponent<PlayerRoo>();
        player.AddScore(_points);

        _animator.SetBool("Hit", true);
        StartCoroutine(DisableDelay());
            
        _waitingToDisable = true;   
    }
}
