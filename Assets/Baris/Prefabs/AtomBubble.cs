using UnityEngine;

public class AtomBubble : MonoBehaviour
{
    // Karakterin küçülme oraný (örneðin, 0.5f = %50 küçültme)
    public float shrinkFactor = 0.5f;

    // Karakterin küçültülme süresi (saniye cinsinden)
    public float shrinkDuration = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpýlan nesnenin Entity bileþenini al
        Entity entity = collision.GetComponent<Entity>();

        // Eðer çarpýlan nesne bir Entity ise veya Player tag'ine sahipse
        if (entity != null || collision.CompareTag("Player"))
        {
            Debug.Log($"AtomBubble {collision.gameObject.name} ile çarpýþtý, küçültme baþlatýlýyor.");
            
            // Çarpýlan nesneyi küçült
            StartCoroutine(ShrinkTarget(collision.gameObject));

            // AtomBubble'ý yok et
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("AtomBubble baþka bir nesneye çarptý, yok ediliyor.");
            // Baþka bir yere çarptýðýnda sadece AtomBubble'ý yok et
            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator ShrinkTarget(GameObject target)
    {
        // Orijinal parent objeyi oluþtur
        GameObject parentObject = new GameObject("ShrinkParent");
        parentObject.transform.position = target.transform.position;
        parentObject.transform.rotation = target.transform.rotation;

        Debug.Log($"ShrinkParent oluþturuldu ve {target.name} için ayarlandý.");

        // Karakteri yeni parent objeye baðla
        target.transform.SetParent(parentObject.transform);

        // Orijinal boyutu kaydet
        Vector3 originalScale = parentObject.transform.localScale;

        // Yeni boyutu hesapla
        Vector3 shrunkScale = originalScale * shrinkFactor;

        // Parent objeyi küçült
        parentObject.transform.localScale = shrunkScale;

        Debug.Log($"{target.name} küçültüldü. Yeni boyut: {shrunkScale}");

        // Belirli bir süre bekle
        yield return new WaitForSeconds(shrinkDuration);

        Debug.Log($"{target.name} küçültme süresi sona erdi.");

        // Parent objeyi eski boyutuna geri döndür
        parentObject.transform.localScale = originalScale;

        Debug.Log($"{target.name} eski boyutuna geri döndü. Orijinal boyut: {originalScale}");

        // Karakteri parent objeden çýkar ve parent objeyi yok et
        target.transform.SetParent(null);
        Destroy(parentObject);

        Debug.Log($"ShrinkParent yok edildi ve {target.name} eski parent'ýna geri döndü.");
    }
}