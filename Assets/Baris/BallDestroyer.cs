using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Çarpışmadan 0.5 saniye sonra topu yok et
        Destroy(gameObject, 0.1f);
    }
}