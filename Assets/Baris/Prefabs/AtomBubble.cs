using UnityEngine;

public class AtomBubble : MonoBehaviour
{
    // Karakterin k���lme oran� (�rne�in, 0.5f = %50 k���ltme)
    public float shrinkFactor = 0.5f;

    // Karakterin k���lt�lme s�resi (saniye cinsinden)
    public float shrinkDuration = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �arp�lan nesnenin Entity bile�enini al
        Entity entity = collision.GetComponent<Entity>();

        // E�er �arp�lan nesne bir Entity ise veya Player tag'ine sahipse
        if (entity != null || collision.CompareTag("Player"))
        {
            Debug.Log($"AtomBubble {collision.gameObject.name} ile �arp��t�, k���ltme ba�lat�l�yor.");
            
            // �arp�lan nesneyi k���lt
            StartCoroutine(ShrinkTarget(collision.gameObject));

            // AtomBubble'� yok et
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("AtomBubble ba�ka bir nesneye �arpt�, yok ediliyor.");
            // Ba�ka bir yere �arpt���nda sadece AtomBubble'� yok et
            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator ShrinkTarget(GameObject target)
    {
        // Orijinal parent objeyi olu�tur
        GameObject parentObject = new GameObject("ShrinkParent");
        parentObject.transform.position = target.transform.position;
        parentObject.transform.rotation = target.transform.rotation;

        Debug.Log($"ShrinkParent olu�turuldu ve {target.name} i�in ayarland�.");

        // Karakteri yeni parent objeye ba�la
        target.transform.SetParent(parentObject.transform);

        // Orijinal boyutu kaydet
        Vector3 originalScale = parentObject.transform.localScale;

        // Yeni boyutu hesapla
        Vector3 shrunkScale = originalScale * shrinkFactor;

        // Parent objeyi k���lt
        parentObject.transform.localScale = shrunkScale;

        Debug.Log($"{target.name} k���lt�ld�. Yeni boyut: {shrunkScale}");

        // Belirli bir s�re bekle
        yield return new WaitForSeconds(shrinkDuration);

        Debug.Log($"{target.name} k���ltme s�resi sona erdi.");

        // Parent objeyi eski boyutuna geri d�nd�r
        parentObject.transform.localScale = originalScale;

        Debug.Log($"{target.name} eski boyutuna geri d�nd�. Orijinal boyut: {originalScale}");

        // Karakteri parent objeden ��kar ve parent objeyi yok et
        target.transform.SetParent(null);
        Destroy(parentObject);

        Debug.Log($"ShrinkParent yok edildi ve {target.name} eski parent'�na geri d�nd�.");
    }
}