using UnityEngine;
using UnityEngine.Tilemaps;


public class AcidBubble : MonoBehaviour
{
    public int DamageAmount = 11; // Acid Bubble'ın vereceği hasar miktarı
    public Color TileHighlightColor = Color.green; // Tile rengi değişimi
    public Tilemap tilemap; // Tilemap referansı

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name}, Player'a zarar vermedi.");
            return;
        }

        Entity entity = collision.GetComponent<Entity>();
        if (entity != null)
        {
            // Düşmana hasar ver
            entity.TakeDamage(DamageAmount);
            Debug.Log($"{gameObject.name} {collision.gameObject.name}'e {DamageAmount} hasar verdi!");
        }
        else
        {
            // Yere çarptığında çevredeki tile'ların rengini değiştir
            ChangeTileColors(collision.transform.position);
        }

        // Acid Bubble'ı yok et
        Destroy(gameObject);
    }

    private void ChangeTileColors(Vector3 position)
    {
        // Tilemap'teki tile pozisyonunu bul
        Vector3Int tilePosition = tilemap.WorldToCell(position);

        // Çarptığı tile ve çevresindeki tile'ların pozisyonlarını bul
        Vector3Int[] directions = {
            Vector3Int.zero, // Çarptığı tile
            Vector3Int.left, // Sol tile
            Vector3Int.right, // Sağ tile
            Vector3Int.up, // Üst tile
            Vector3Int.down // Alt tile
        };

        foreach (var direction in directions)
        {
            Vector3Int neighborPosition = tilePosition + direction;

            // Tilemap'teki tile'ı al
            if (tilemap.HasTile(neighborPosition))
            {
                // Tile'ın rengini değiştir
                tilemap.SetTileFlags(neighborPosition, TileFlags.None); // Tile'ın renk değiştirilmesine izin ver
                tilemap.SetColor(neighborPosition, TileHighlightColor); // Tile'ın rengini değiştir
                Debug.Log($"Tile {neighborPosition} rengi değiştirildi!");
            }
            else
            {
                Debug.LogWarning($"Tile {neighborPosition} bulunamadı.");
            }
        }
    }
}
