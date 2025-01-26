using UnityEngine;
using System.Collections;

public class EnemyEntity : MonoBehaviour
{
    [Header("Properties")]
    public float Health = 100f; // Maksimum can
    public float CurrentHealth; // Þu anki can
    public float Damage = 10f; // Verdiði hasar
    public float MovementSpeed = 5f; // Yürüme hýzý
    public float RunSpeed = 8f; // Koþma hýzý
    public float JumpForce = 10f; // Zýplama gücü

    [Header("Components")]
    public Animator EntityAnimator; // Animasyon kontrolcüsü
    public Rigidbody2D EntityBody; // Fizik bileþeni
    public Collider2D EntityCollider; // Çarpýþma bileþeni
    public SpriteRenderer EntitySprite; // Görsel bileþeni (renk deðiþtirmek için)

    [Header("AI Properties")]
    public GameObject player; // Takip edilecek oyuncu nesnesi
    public float speed; // Takip hýzý
    public float distanceBetween; // Oyuncuyla arasýndaki mesafe
    private float distance; // Oyuncuyla arasýndaki mesafe
    private bool movingRight; // Karakterin yönü
    private Vector2[] patrolPoints; // Devriye noktalarý
    private int currentPatrolIndex = 0; // Þu anki devriye noktasý indeksi
    private float patrolRange = 10f; // Devriye alaný

    private void Start()
    {
        // Rigidbody ve Collider bileþenlerini otomatik olarak al
        EntityBody = GetComponent<Rigidbody2D>();
        EntityCollider = GetComponent<Collider2D>();
        EntitySprite = GetComponent<SpriteRenderer>();

        // Baþlangýç canýný maksimum cana eþitle
        CurrentHealth = Health;

        // Devriye noktalarýný tanýmla
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
            // Devriye moduna geç
            PatrolMode();
        }
    }

    private void PatrolMode()
    {
        // Karakteri devriye noktalarýna hareket ettir
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex], MovementSpeed * Time.deltaTime);

        // Eðer karakterin konumu devriye noktasýna ulaþtýysa, bir sonraki noktaya geç
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex]) < 0.1f)
        {
            currentPatrolIndex++;
            if (currentPatrolIndex >= patrolPoints.Length)
            {
                currentPatrolIndex = 0; // Devriye noktalarýnýn sonuna ulaþýldýysa, baþa dön
            }

            // Karakterin yönünü deðiþtir
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y == 0 ? 180 : 0, 0);
        }
    }

    /// <summary>
    /// Hasar alma metodu
    /// </summary>
    /// <param name="damageAmount">Alýnan hasar miktarý</param>
    public void TakeDamage(float damageAmount)
    {
        Debug.Log($"{gameObject.name} hasar aldý: {damageAmount}");
        CurrentHealth -= damageAmount; // Caný azalt

        // Hasar aldýðýnda renk deðiþimi animasyonu baþlat
        StartCoroutine(FlashRed());

        // Eðer can sýfýr veya altýna düþerse öl
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Ölüm metodu
    /// </summary>
    private void Die()
    {
        Debug.Log($"{gameObject.name} öldü!");

        // Ölüm animasyonu varsa tetiklenebilir
        if (EntityAnimator != null)
        {
            EntityAnimator.SetTrigger("Die");
        }

        // Ýsteðe baðlý: Nesneyi yok et
        Destroy(gameObject, 0.5f); // 0.5 saniye sonra nesneyi yok et
    }

    /// <summary>
    /// Hasar aldýðýnda kýrmýzýya dönme efekti
    /// </summary>
    private IEnumerator FlashRed()
    {
        int flashCount = 3; // Kaç kez kýrmýzýya dönüþ yapýlacak
        float flashDuration = 0.1f; // Her bir kýrmýzýya dönüþ süresi

        // Orijinal rengi kaydet
        Color originalColor = EntitySprite.color;

        for (int i = 0; i < flashCount; i++)
        {
            // Kýrmýzýya dön
            EntitySprite.color = Color.red;

            // Bekle
            yield return new WaitForSeconds(flashDuration);

            // Orijinal renge dön
            EntitySprite.color = originalColor;

            // Bekle
            yield return new WaitForSeconds(flashDuration);
        }
    }

    /// <summary>
    /// Can yenileme metodu
    /// </summary>
    /// <param name="healAmount">Yenilenecek can miktarý</param>
    public void Heal(float healAmount)
    {
        CurrentHealth += healAmount; // Caný artýr
        if (CurrentHealth > Health)
        {
            CurrentHealth = Health; // Maksimum caný aþma
        }

        Debug.Log($"{gameObject.name} iyileþti: {healAmount}. Güncel can: {CurrentHealth}");
    }

private void OnTriggerEnter2D(Collider2D collision)
{
    // Çarpýþan nesnenin adýný ve tag'ini yazdýr
    Debug.Log($"Çarpýþma algýlandý! Çarpýþan nesne: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");

    // Çarpýþan nesne "Enemy" tag'ine sahip mi kontrol et
    if (collision.CompareTag("Enemy"))
    {
        Debug.Log("Enemy tag'i bulundu, hasar veriliyor...");

        // EnemyEntity bileþenini al ve hasar uygula
        EnemyEntity entity = collision.GetComponent<EnemyEntity>();
        if (entity != null)
        {
            entity.TakeDamage(Damage); // Hasar ver
        }
        else
        {
            Debug.LogWarning($"{collision.gameObject.name} nesnesinde EnemyEntity bileþeni bulunamadý!");
        }
    }
    else if (collision.CompareTag("Bubble"))
    {
        // Eðer çarpýþan nesne "Bubble" tag'ine sahipse yok et
        Debug.Log($"{collision.gameObject.name} yok edilecek!");
        Destroy(collision.gameObject); // Nesneyi yok et
    }
    else
    {
        Debug.Log($"{collision.gameObject.name} nesnesi Enemy veya Bubble tag'ine sahip deðil.");
    }
}


}