using UnityEngine;

public class AcidBubble : MonoBehaviour
{
    public int DamageAmount = 10; // Acid Bubble'ın vereceği hasar miktarı

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarptığı objenin "Entity" olup olmadığını kontrol et
        Entity entity = collision.GetComponent<Entity>();
        if (entity != null)
        {
            // Entity'nin canını azalt
            entity.TakeDamage(DamageAmount);
            Debug.Log($"{gameObject.name} {collision.gameObject.name}'e {DamageAmount} hasar verdi!");

            // Acid Bubble'ı yok et
            Destroy(gameObject);
        }
    }
}
