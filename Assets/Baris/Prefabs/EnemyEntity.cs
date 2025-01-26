using UnityEngine;
using System.Collections;

public class EnemyEntity : MonoBehaviour
{
    [Header("Properties")]
    public float Health = 100f; // Maksimum can
    public float CurrentHealth; // �u anki can
    public float Damage = 10f; // Verdi�i hasar
    public float MovementSpeed = 5f; // Y�r�me h�z�
    public float RunSpeed = 8f; // Ko�ma h�z�
    public float JumpForce = 10f; // Z�plama g�c�

    [Header("Components")]
    public Animator EntityAnimator; // Animasyon kontrolc�s�
    public Rigidbody2D EntityBody; // Fizik bile�eni
    public Collider2D EntityCollider; // �arp��ma bile�eni
    public SpriteRenderer EntitySprite; // G�rsel bile�eni (renk de�i�tirmek i�in)

    [Header("AI Properties")]
    public GameObject player; // Takip edilecek oyuncu nesnesi
    public float speed; // Takip h�z�
    public float distanceBetween; // Oyuncuyla aras�ndaki mesafe
    private float distance; // Oyuncuyla aras�ndaki mesafe
    private bool movingRight; // Karakterin y�n�
    private Vector2[] patrolPoints; // Devriye noktalar�
    private int currentPatrolIndex = 0; // �u anki devriye noktas� indeksi
    private float patrolRange = 10f; // Devriye alan�

    private void Start()
    {
        // Rigidbody ve Collider bile�enlerini otomatik olarak al
        EntityBody = GetComponent<Rigidbody2D>();
        EntityCollider = GetComponent<Collider2D>();
        EntitySprite = GetComponent<SpriteRenderer>();

        // Ba�lang�� can�n� maksimum cana e�itle
        CurrentHealth = Health;

        // Devriye noktalar�n� tan�mla
        patrolPoints = new Vector2[]
        {
            new Vector2(transform.position.x - patrolRange, transform.position.y),
            new Vector2(transform.position.x + patrolRange, transform.position.y)
        };
    }

    private void Update()
    {
        // Oyuncuyu takip etme
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else
        {
            // Devriye moduna ge�
            PatrolMode();
        }
    }

    private void PatrolMode()
    {
        // Karakteri devriye noktalar�na hareket ettir
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex], MovementSpeed * Time.deltaTime);

        // E�er karakterin konumu devriye noktas�na ula�t�ysa, bir sonraki noktaya ge�
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex]) < 0.1f)
        {
            currentPatrolIndex++;
            if (currentPatrolIndex >= patrolPoints.Length)
            {
                currentPatrolIndex = 0; // Devriye noktalar�n�n sonuna ula��ld�ysa, ba�a d�n
            }

            // Karakterin y�n�n� de�i�tir
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y == 0 ? 180 : 0, 0);
        }
    }

    /// <summary>
    /// Hasar alma metodu
    /// </summary>
    /// <param name="damageAmount">Al�nan hasar miktar�</param>
    public void TakeDamage(float damageAmount)
    {
        Debug.Log($"{gameObject.name} hasar ald�: {damageAmount}");
        CurrentHealth -= damageAmount; // Can� azalt

        // Hasar ald���nda renk de�i�imi animasyonu ba�lat
        StartCoroutine(FlashRed());

        // E�er can s�f�r veya alt�na d��erse �l
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// �l�m metodu
    /// </summary>
    private void Die()
    {
        Debug.Log($"{gameObject.name} �ld�!");

        // �l�m animasyonu varsa tetiklenebilir
        if (EntityAnimator != null)
        {
            EntityAnimator.SetTrigger("Die");
        }

        // �ste�e ba�l�: Nesneyi yok et
        Destroy(gameObject, 0.5f); // 0.5 saniye sonra nesneyi yok et
    }

    /// <summary>
    /// Hasar ald���nda k�rm�z�ya d�nme efekti
    /// </summary>
    private IEnumerator FlashRed()
    {
        int flashCount = 3; // Ka� kez k�rm�z�ya d�n�� yap�lacak
        float flashDuration = 0.1f; // Her bir k�rm�z�ya d�n�� s�resi

        // Orijinal rengi kaydet
        Color originalColor = EntitySprite.color;

        for (int i = 0; i < flashCount; i++)
        {
            // K�rm�z�ya d�n
            EntitySprite.color = Color.red;

            // Bekle
            yield return new WaitForSeconds(flashDuration);

            // Orijinal renge d�n
            EntitySprite.color = originalColor;

            // Bekle
            yield return new WaitForSeconds(flashDuration);
        }
    }

    /// <summary>
    /// Can yenileme metodu
    /// </summary>
    /// <param name="healAmount">Yenilenecek can miktar�</param>
    public void Heal(float healAmount)
    {
        CurrentHealth += healAmount; // Can� art�r
        if (CurrentHealth > Health)
        {
            CurrentHealth = Health; // Maksimum can� a�ma
        }

        Debug.Log($"{gameObject.name} iyile�ti: {healAmount}. G�ncel can: {CurrentHealth}");
    }

private void OnTriggerEnter2D(Collider2D collision)
{
    // �arp��an nesnenin ad�n� ve tag'ini yazd�r
    Debug.Log($"�arp��ma alg�land�! �arp��an nesne: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");

    // �arp��an nesne "Enemy" tag'ine sahip mi kontrol et
    if (collision.CompareTag("Enemy"))
    {
        Debug.Log("Enemy tag'i bulundu, hasar veriliyor...");

        // EnemyEntity bile�enini al ve hasar uygula
        EnemyEntity entity = collision.GetComponent<EnemyEntity>();
        if (entity != null)
        {
            entity.TakeDamage(Damage); // Hasar ver
        }
        else
        {
            Debug.LogWarning($"{collision.gameObject.name} nesnesinde EnemyEntity bile�eni bulunamad�!");
        }
    }
    else if (collision.CompareTag("Bubble"))
    {
        // E�er �arp��an nesne "Bubble" tag'ine sahipse yok et
        Debug.Log($"{collision.gameObject.name} yok edilecek!");
        Destroy(collision.gameObject); // Nesneyi yok et
    }
    else
    {
        Debug.Log($"{collision.gameObject.name} nesnesi Enemy veya Bubble tag'ine sahip de�il.");
    }
}


}