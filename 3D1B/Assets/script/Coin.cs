using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(scoreValue);

            Destroy(gameObject);
        }
    }
}