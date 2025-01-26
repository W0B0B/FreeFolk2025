using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuManager : MonoBehaviour
{
    public Transform center; // Radial menünün merkezi
    public Transform selectObject; // Seçim göstergesi
    public GameObject RadialMenuRoot; // Radial menü kökü
    public bool isRadialMenuActive; // Menü aktif mi?

    public Text HighLightedWeaponName; // Vurgulanan silahýn adý
    public Text selectedBubble; // Seçilen bubble'ýn adý

    public string[] WeaponNames; // Bubble isimleri
    public Transform[] itemSlots; // Radial menüdeki slotlar

    // Seçilen bubble'ýn indeksini saklayacak
    public int selectedBubbleIndex = 0;

    void Start()
    {
        isRadialMenuActive = false;
        RadialMenuRoot.SetActive(false); // Menü baþlangýçta kapalý
    }

    void Update()
    {
        // Radial menüyü aç/kapat
        if (Input.GetKeyDown(KeyCode.E))
        {
            isRadialMenuActive = !isRadialMenuActive;
            RadialMenuRoot.SetActive(isRadialMenuActive);
        }

        // Eðer menü aktif deðilse, iþlem yapma
        if (!isRadialMenuActive)
            return;

        // Fare pozisyonuna göre açý hesaplama
        Vector2 delta = center.position - Input.mousePosition;
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        angle += 180;

        int currentWeapon = 0;

        for (int i = 0; i < 360; i += 36)
        {
            // Dizilerin sýnýrlarýný kontrol et
            if (currentWeapon >= WeaponNames.Length || currentWeapon >= itemSlots.Length)
            {
                Debug.LogWarning("WeaponNames veya itemSlots dizisinin boyutu yetersiz! Lütfen dizileri kontrol edin.");
                break; // Döngüyü durdur
            }

            // Eðer açý, bu dilime denk geliyorsa
            if (angle >= i && angle < i + 36)
            {
                // Seçim göstergesini döndür
                selectObject.eulerAngles = new Vector3(0, 0, i + 180);

                // Vurgulanan silahýn adýný güncelle
                HighLightedWeaponName.text = WeaponNames[currentWeapon];

                // Tüm slotlarýn boyutunu sýfýrla
                foreach (Transform t in itemSlots)
                {
                    t.transform.localScale = new Vector3(1, 1, 1);
                }

                // Seçili slotun boyutunu büyüt
                itemSlots[currentWeapon].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

                // Sol týk ile seçim yapýldýðýnda
                if (Input.GetMouseButtonDown(0))
                {
                    selectedBubble.text = WeaponNames[currentWeapon];
                    selectedBubbleIndex = currentWeapon; // Seçilen bubble'ýn indeksini kaydet
                    isRadialMenuActive = false; // Menüyü kapat
                    RadialMenuRoot.SetActive(false);
                }
            }

            currentWeapon++;
        }
    }
}
