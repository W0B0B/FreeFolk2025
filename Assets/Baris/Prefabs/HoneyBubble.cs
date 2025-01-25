using UnityEngine;

public class HoneyBubble : MonoBehaviour
{
    public GameObject honeyTrapPrefab; // Honey Trap prefab referansı
    public float immobilizeDuration = 4f; // Düşmanın sabit kalacağı süre

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name}, Player'a etki etmedi.");
            return;
        }

        Entity entity = collision.GetComponent<Entity>();
        if (entity != null)
        {
            // Düşmanı sabitle
            ImmobilizeEnemy(entity);
            Debug.Log($"{gameObject.name} {collision.gameObject.name}'i {immobilizeDuration} saniye boyunca sabitledi!");
        }

        // Honey Trap oluştur
        SpawnHoneyTrap();

        // Honey Bubble'ı yok et
        Destroy(gameObject);
    }

    private void ImmobilizeEnemy(Entity entity)
    {
        // Düşmanı sabitler
        Rigidbody2D rb = entity.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Hareketi durdur
            rb.constraints = RigidbodyConstraints2D.FreezeAll; // Tüm hareketleri ve rotasyonu kilitle

            // Belirli bir süre sonra düşmanı serbest bırak
            entity.StartCoroutine(ReleaseEnemy(rb));
        }
    }

    private System.Collections.IEnumerator ReleaseEnemy(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(immobilizeDuration);
        rb.constraints = RigidbodyConstraints2D.None; // Kilitleri kaldır
        Debug.Log("Düşman serbest bırakıldı.");
    }

    private void SpawnHoneyTrap()
    {
        // Honey Trap'i Honey Bubble'ın pozisyonunda oluştur
        GameObject honeyTrap = Instantiate(honeyTrapPrefab, transform.position, Quaternion.identity);

        // Honey Trap'i 7 saniye sonra yok et
        Destroy(honeyTrap, 4f);

        Debug.Log("Honey Trap oluşturuldu ve 4 saniye sonra yok edilecek.");
    }
}
