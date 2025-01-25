using UnityEngine;

public class SlimeBubble : MonoBehaviour
{
    public GameObject slimeTrapPrefab; // Slime Trap prefab referansý

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name}, Player'a etki etmedi.");
            return;
        }

        // Slime Trap oluþtur
        SpawnSlimeTrap();

        // Slime Bubble'ý yok et
        Destroy(gameObject);
    }

    private void SpawnSlimeTrap()
    {
        // Slime Trap'i Slime Bubble'ýn pozisyonunda oluþtur
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        GameObject slimeTrap = Instantiate(slimeTrapPrefab, spawnPosition, Quaternion.identity);

        // Slime Trap'i 4 saniye sonra yok et
        Destroy(slimeTrap, 4f);

        Debug.Log("Slime Trap oluþturuldu ve 4 saniye sonra yok edilecek.");
    }
}
