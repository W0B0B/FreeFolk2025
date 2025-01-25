using UnityEngine;

public class TNTBubble : MonoBehaviour
{
    public GameObject explosionPrefab; // Slime Trap prefab referansý

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name}, Player'a etki etmedi.");
            return;
        }

        // Slime Trap oluþtur
        explosion(); // Burada 'SpawnExplosion' yerine 'explosion' çaðrýlýyor.

        // Slime Bubble'ý yok et
        Destroy(gameObject);
    }

    private void explosion()
    {
        // Slime Trap'i Slime Bubble'ýn pozisyonunda oluþtur
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        GameObject explosion = Instantiate(explosionPrefab, spawnPosition, Quaternion.identity);

        // Slime Trap'i 4 saniye sonra yok et
        Destroy(explosion, 4f);

        Debug.Log("Slime Trap oluþturuldu ve 4 saniye sonra yok edilecek.");
    }
}
