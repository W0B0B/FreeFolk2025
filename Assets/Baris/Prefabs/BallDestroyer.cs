using UnityEngine;

public class BubbleDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // E�er �arpt��� obje "Bubble" de�ilse kendini yok et
        if (!collision.gameObject.CompareTag("Bubble"))
        {
            Debug.Log(gameObject.name + " yok edilecek (0.1 saniye sonra)!");
            Destroy(gameObject, 0.1f); // Kendini 0.1 saniye sonra yok et
        }
    }
}
