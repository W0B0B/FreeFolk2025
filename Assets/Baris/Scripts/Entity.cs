using UnityEngine;

public class Entity : MonoBehaviour
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

    private void Start()
    {
        // Rigidbody ve Collider bileþenlerini otomatik olarak al
        EntityBody = GetComponent<Rigidbody2D>();
        EntityCollider = GetComponent<Collider2D>();

        // Baþlangýç canýný maksimum cana eþitle
        CurrentHealth = Health;
    }

    /// <summary>
    /// Hasar alma metodu
    /// </summary>
    /// <param name="damageAmount">Alýnan hasar miktarý</param>
    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount; // Caný azalt
        Debug.Log($"{gameObject.name} hasar aldý: {damageAmount}. Kalan can: {CurrentHealth}");

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
}
