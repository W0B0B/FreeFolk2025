using UnityEngine;

public class BubbleDestroyer : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        // Rigidbody2D bile�enini al
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // E�er �arpt��� obje "Bubble" de�ilse kendini yok et
        if (!collision.gameObject.CompareTag("Bubble"))
        {
            Debug.Log(gameObject.name + " yok edilecek (0.1 saniye sonra)!");

            // Topu yap��t�r (hareketi durdur)
            rb.linearVelocity = Vector2.zero; // Hareketi durdur
            rb.angularVelocity = 0f; // D�nmeyi durdur
            rb.isKinematic = true; // Fizik hareketini devre d��� b�rak

            // Kendini 0.1 saniye sonra yok et
            Destroy(gameObject, 0.1f);
        }
    }
}
