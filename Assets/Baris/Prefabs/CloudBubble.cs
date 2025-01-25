using UnityEngine;

public class CloudBubble : MonoBehaviour
{
    private void Start()
    {
        // CloudBubble 5 saniye sonra yok olacak
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // CloudPlatform ile �arp��ma kontrol�
        if (collision.CompareTag("CloudPlatform"))
        {
            // CloudPlatform �zerindeki SpriteRenderer'� etkinle�tir
            SpriteRenderer platformRenderer = collision.GetComponent<SpriteRenderer>();
            if (platformRenderer != null)
            {
                platformRenderer.enabled = true;
            }

            // CloudBubble'� yok et
            Destroy(gameObject);
        }
    }
}
