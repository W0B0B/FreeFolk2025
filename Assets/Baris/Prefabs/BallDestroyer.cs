using UnityEngine;

public class BubbleDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eğer çarptığı obje "Bubble" değilse kendini yok et
        if (!collision.gameObject.CompareTag("Bubble"))
        {
            Debug.Log(gameObject.name + " yok edilecek!");

            // Hareketi durdurmadan kendini yok et
            Destroy(gameObject, 0.1f);
        }
    }
}
