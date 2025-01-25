using UnityEngine;
using System.Collections;


public class Entity : MonoBehaviour
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

    private void Start()
    {
        // Rigidbody ve Collider bile�enlerini otomatik olarak al
        EntityBody = GetComponent<Rigidbody2D>();
        EntityCollider = GetComponent<Collider2D>();
        EntitySprite = GetComponent<SpriteRenderer>();

        // Ba�lang�� can�n� maksimum cana e�itle
        CurrentHealth = Health;
    }

    /// <summary>
    /// Hasar alma metodu
    /// </summary>
    /// <param name="damageAmount">Al�nan hasar miktar�</param>
    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount; // Can� azalt
        Debug.Log($"{gameObject.name} hasar ald�: {damageAmount}. Kalan can: {CurrentHealth}");

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
}
