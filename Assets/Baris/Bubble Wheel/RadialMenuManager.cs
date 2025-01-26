using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuManager : MonoBehaviour
{
    public Transform center; // Radial men�n�n merkezi
    public Transform selectObject; // Se�im g�stergesi
    public GameObject RadialMenuRoot; // Radial men� k�k�
    public bool isRadialMenuActive; // Men� aktif mi?

    public Text HighLightedWeaponName; // Vurgulanan silah�n ad�
    public Text selectedBubble; // Se�ilen bubble'�n ad�

    public string[] WeaponNames; // Bubble isimleri
    public Transform[] itemSlots; // Radial men�deki slotlar

    // Se�ilen bubble'�n indeksini saklayacak
    public int selectedBubbleIndex = 0;

    void Start()
    {
        isRadialMenuActive = false;
        RadialMenuRoot.SetActive(false); // Men� ba�lang��ta kapal�
    }

    void Update()
    {
        // Radial men�y� a�/kapat
        if (Input.GetKeyDown(KeyCode.E))
        {
            isRadialMenuActive = !isRadialMenuActive;
            RadialMenuRoot.SetActive(isRadialMenuActive);
        }

        // E�er men� aktif de�ilse, i�lem yapma
        if (!isRadialMenuActive)
            return;

        // Fare pozisyonuna g�re a�� hesaplama
        Vector2 delta = center.position - Input.mousePosition;
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        angle += 180;

        int currentWeapon = 0;

        for (int i = 0; i < 360; i += 36)
        {
            // Dizilerin s�n�rlar�n� kontrol et
            if (currentWeapon >= WeaponNames.Length || currentWeapon >= itemSlots.Length)
            {
                Debug.LogWarning("WeaponNames veya itemSlots dizisinin boyutu yetersiz! L�tfen dizileri kontrol edin.");
                break; // D�ng�y� durdur
            }

            // E�er a��, bu dilime denk geliyorsa
            if (angle >= i && angle < i + 36)
            {
                // Se�im g�stergesini d�nd�r
                selectObject.eulerAngles = new Vector3(0, 0, i + 180);

                // Vurgulanan silah�n ad�n� g�ncelle
                HighLightedWeaponName.text = WeaponNames[currentWeapon];

                // T�m slotlar�n boyutunu s�f�rla
                foreach (Transform t in itemSlots)
                {
                    t.transform.localScale = new Vector3(1, 1, 1);
                }

                // Se�ili slotun boyutunu b�y�t
                itemSlots[currentWeapon].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

                // Sol t�k ile se�im yap�ld���nda
                if (Input.GetMouseButtonDown(0))
                {
                    selectedBubble.text = WeaponNames[currentWeapon];
                    selectedBubbleIndex = currentWeapon; // Se�ilen bubble'�n indeksini kaydet
                    isRadialMenuActive = false; // Men�y� kapat
                    RadialMenuRoot.SetActive(false);
                }
            }

            currentWeapon++;
        }
    }
}
