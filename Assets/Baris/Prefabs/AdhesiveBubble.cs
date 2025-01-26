using UnityEngine;

public class AdhesiveBubble : MonoBehaviour
{
    // WallLeft ve WallRight prefablerini buraya atayýn.
    public GameObject wallLeftPrefab;
    public GameObject wallRightPrefab;

    // Pozisyon ofset deðerleri
    public Vector3 wallLeftOffset = new Vector3(-0.1f, 0, 0); // Hafif sola kaydýrma
    public Vector3 wallRightOffset = new Vector3(0.1f, 0, 0);  // Hafif saða kaydýrma

    // Prefablerin yok olma süresi (saniye cinsinden)
    public float destroyDelay = 10f;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("WallLeft"))
        {
            // WallLeft için prefab oluþtur ve pozisyonu biraz sola kaydýr
            GameObject spawnedWallLeft = Instantiate(wallLeftPrefab, transform.position + wallLeftOffset, Quaternion.identity);

            // Prefab'i 10 saniye sonra yok et
            Destroy(spawnedWallLeft, destroyDelay);

            // Bu bubble'ý yok et
            Destroy(gameObject);
        }
        else if (trigger.gameObject.CompareTag("WallRight"))
        {
            // WallRight için prefab oluþtur ve pozisyonu biraz saða kaydýr
            GameObject spawnedWallRight = Instantiate(wallRightPrefab, transform.position + wallRightOffset, Quaternion.identity);

            // Prefab'i 10 saniye sonra yok et
            Destroy(spawnedWallRight, destroyDelay);

            // Bu bubble'ý yok et
            Destroy(gameObject);
        }
        else
        {
            // Eðer baþka bir yere çarparsa sadece bubble'ý yok et
            Destroy(gameObject);
        }
    }
}