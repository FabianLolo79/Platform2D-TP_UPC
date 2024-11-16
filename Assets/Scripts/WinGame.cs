using TMPro;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    //[SerializeField] private GameObject _playerScore;
    //public TMP_Text scoreText;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Win();
        }
    }

    public void Win()
    {
       // gameObject.SetActive(true);
       _panel.SetActive(true);
    }
}
