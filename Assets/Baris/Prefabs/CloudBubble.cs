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
        // CloudPlatform ile çarpýþma kontrolü
        if (collision.CompareTag("CloudPlatform"))
        {
            // CloudPlatform üzerindeki SpriteRenderer'ý etkinleþtir
            SpriteRenderer platformRenderer = collision.GetComponent<SpriteRenderer>();
            if (platformRenderer != null)
            {
                platformRenderer.enabled = true;
            }

            // CloudBubble'ý yok et
            Destroy(gameObject);
        }
    }
}
