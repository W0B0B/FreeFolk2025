using UnityEngine;
using System.Collections;


public class CloudPlatform : MonoBehaviour
{
    private void Start()
    {
        // Ba�lang��ta SpriteRenderer'� devre d��� b�rak
        SpriteRenderer platformRenderer = GetComponent<SpriteRenderer>();
        if (platformRenderer != null)
        {
            platformRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sadece CloudBubble tag'ine sahip nesnelerle etkile�im
        if (collision.CompareTag("CloudBubble"))
        {
            // SpriteRenderer'� etkinle�tir
            SpriteRenderer platformRenderer = GetComponent<SpriteRenderer>();
            if (platformRenderer != null)
            {
                platformRenderer.enabled = true;

                // 10 saniye sonra SpriteRenderer'� devre d��� b�rak
                StartCoroutine(DisableSpriteRendererAfterDelay(10f, platformRenderer));
            }

            // CloudBubble'� yok et
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator DisableSpriteRendererAfterDelay(float delay, SpriteRenderer renderer)
    {
        // Belirtilen s�re kadar bekle
        yield return new WaitForSeconds(delay);

        // SpriteRenderer'� devre d��� b�rak
        if (renderer != null)
        {
            renderer.enabled = false;
        }
    }
}
