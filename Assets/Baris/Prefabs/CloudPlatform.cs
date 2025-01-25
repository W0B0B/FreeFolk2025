using UnityEngine;
using System.Collections;


public class CloudPlatform : MonoBehaviour
{
    private void Start()
    {
        // Baþlangýçta SpriteRenderer'ý devre dýþý býrak
        SpriteRenderer platformRenderer = GetComponent<SpriteRenderer>();
        if (platformRenderer != null)
        {
            platformRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sadece CloudBubble tag'ine sahip nesnelerle etkileþim
        if (collision.CompareTag("CloudBubble"))
        {
            // SpriteRenderer'ý etkinleþtir
            SpriteRenderer platformRenderer = GetComponent<SpriteRenderer>();
            if (platformRenderer != null)
            {
                platformRenderer.enabled = true;

                // 10 saniye sonra SpriteRenderer'ý devre dýþý býrak
                StartCoroutine(DisableSpriteRendererAfterDelay(10f, platformRenderer));
            }

            // CloudBubble'ý yok et
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator DisableSpriteRendererAfterDelay(float delay, SpriteRenderer renderer)
    {
        // Belirtilen süre kadar bekle
        yield return new WaitForSeconds(delay);

        // SpriteRenderer'ý devre dýþý býrak
        if (renderer != null)
        {
            renderer.enabled = false;
        }
    }
}
