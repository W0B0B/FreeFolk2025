using UnityEngine;

public class AdhesiveBubble : MonoBehaviour
{
    // WallLeft ve WallRight prefablerini buraya atay�n.
    public GameObject wallLeftPrefab;
    public GameObject wallRightPrefab;

    // Pozisyon ofset de�erleri
    public Vector3 wallLeftOffset = new Vector3(-0.1f, 0, 0); // Hafif sola kayd�rma
    public Vector3 wallRightOffset = new Vector3(0.1f, 0, 0);  // Hafif sa�a kayd�rma

    // Prefablerin yok olma s�resi (saniye cinsinden)
    public float destroyDelay = 10f;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("WallLeft"))
        {
            // WallLeft i�in prefab olu�tur ve pozisyonu biraz sola kayd�r
            GameObject spawnedWallLeft = Instantiate(wallLeftPrefab, transform.position + wallLeftOffset, Quaternion.identity);

            // Prefab'i 10 saniye sonra yok et
            Destroy(spawnedWallLeft, destroyDelay);

            // Bu bubble'� yok et
            Destroy(gameObject);
        }
        else if (trigger.gameObject.CompareTag("WallRight"))
        {
            // WallRight i�in prefab olu�tur ve pozisyonu biraz sa�a kayd�r
            GameObject spawnedWallRight = Instantiate(wallRightPrefab, transform.position + wallRightOffset, Quaternion.identity);

            // Prefab'i 10 saniye sonra yok et
            Destroy(spawnedWallRight, destroyDelay);

            // Bu bubble'� yok et
            Destroy(gameObject);
        }
        else
        {
            // E�er ba�ka bir yere �arparsa sadece bubble'� yok et
            Destroy(gameObject);
        }
    }
}