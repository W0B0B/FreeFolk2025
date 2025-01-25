using UnityEngine;

public class OilBubble : MonoBehaviour
{
    public GameObject oilTrapPrefab; // Oil Trap prefab referans�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name}, Player'a etki etmedi.");
            return;
        }

        // Oil Trap olu�tur
        SpawnOilTrap();

        // Oil Bubble'� yok et
        Destroy(gameObject);
    }

    private void SpawnOilTrap()
    {
        // Oil Trap'i Oil Bubble'�n pozisyonunda olu�tur
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z); // Y ekseninde biraz a�a�� kayd�r
        GameObject oilTrap = Instantiate(oilTrapPrefab, spawnPosition, Quaternion.identity);

        // Oil Trap'i 4 saniye sonra yok et
        Destroy(oilTrap, 4f);

        Debug.Log("Oil Trap olu�turuldu ve 4 saniye sonra yok edilecek.");
    }

}
