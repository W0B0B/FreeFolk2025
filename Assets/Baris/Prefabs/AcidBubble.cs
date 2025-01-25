using UnityEngine;

public class AcidBubble : MonoBehaviour
{
    public int DamageAmount = 11; // Acid Bubble'ın vereceği hasar miktarı

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name}, Player'a zarar vermedi.");
            return;
        }

        Entity entity = collision.GetComponent<Entity>();
        if (entity != null)
        {
            // Düşmana hasar ver
            entity.TakeDamage(DamageAmount);
            Debug.Log($"{gameObject.name} {collision.gameObject.name}'e {DamageAmount} hasar verdi!");
        }
    }
}
