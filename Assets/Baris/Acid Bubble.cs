using UnityEngine;

public class AcidBubble : MonoBehaviour
{
    public float Damage = 11f; // Baloncu�un verdi�i hasar
    public float ExplosionRadius = 1.5f; // Patlama etkisinin yar��ap�
    public LayerMask EnemyLayer; // D��man katman�
    public float Speed = 10f; // Baloncu�un hareket h�z�
    public GameObject ExplosionEffect; // Patlama efekti prefab

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Baloncu�u ileri do�ru hareket ettir
        rb.linearVelocity = transform.right * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // E�er d��mana �arparsa
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // D��mana hasar ver
            DealDamage(collision.gameObject.GetComponent<Entity>());
            // Baloncu�u yok et
            Destroy(gameObject);
        }
        // E�er bir y�zeye �arparsa
        else if (collision.gameObject.CompareTag("Surface"))
        {
            // Patlama efektini olu�tur
            Explode();
            // Baloncu�u yok et
            Destroy(gameObject);
        }
    }

    private void DealDamage(Entity entity)
    {
        if (entity != null)
        {
            // D��man�n mevcut sa�l���n� azalt
            entity.CurrentHealth -= Damage;

            // E�er d��man�n sa�l��� s�f�r�n alt�na d��erse, yok et
            if (entity.CurrentHealth <= 0)
            {
                Destroy(entity.gameObject); // D��man� yok et
            }
        }
    }

    private void Explode()
    {
        // Patlama efektini olu�tur
        if (ExplosionEffect != null)
        {
            Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        }

        // Patlama alan�ndaki d��manlar� bul
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius, EnemyLayer);

        // Her bir d��mana hasar ver
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            Entity enemyEntity = enemyCollider.GetComponent<Entity>();
            DealDamage(enemyEntity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Patlama yar��ap�n� g�rselle�tirmek i�in bir daire �izin
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
