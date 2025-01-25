using UnityEngine;

public class SlimeBubble : MonoBehaviour
{
    public GameObject slimeTrapPrefab; // Slime Trap prefab referans�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name}, Player'a etki etmedi.");
            return;
        }

        // Slime Trap olu�tur
        SpawnSlimeTrap();

        // Slime Bubble'� yok et
        Destroy(gameObject);
    }

    private void SpawnSlimeTrap()
    {
        // Slime Trap'i Slime Bubble'�n pozisyonunda olu�tur
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        GameObject slimeTrap = Instantiate(slimeTrapPrefab, spawnPosition, Quaternion.identity);

        // Slime Trap'i 4 saniye sonra yok et
        Destroy(slimeTrap, 4f);

        Debug.Log("Slime Trap olu�turuldu ve 4 saniye sonra yok edilecek.");
    }
}
