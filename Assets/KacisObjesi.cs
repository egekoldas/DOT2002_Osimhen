using UnityEngine;
using UnityEngine.UI;

public class KacisObjesi : MonoBehaviour
{
    [Header("Ayarlar")]
    public float etkilesimMesafesi = 3f;
    public KeyCode etkilesimTusu = KeyCode.E;

    [Header("UI (Ýsteðe Baðlý)")]
    public GameObject pressE_Yazisi;

    private GameObject oyuncuKarakteri;
    private bool oyuncuYakinMi = false;
    private OyunYoneticisi gameManager;

    void Start()
    {
        // Tag "Oyuncu" olarak kaldý
        oyuncuKarakteri = GameObject.FindGameObjectWithTag("Oyuncu");
        gameManager = FindObjectOfType<OyunYoneticisi>();

        if (oyuncuKarakteri == null) Debug.LogError("Sahnede 'Oyuncu' tagýna sahip obje bulunamadý!");
        if (gameManager == null) Debug.LogError("Sahnede GameManager objesi ve OyunYoneticisi kodu bulunamadý!");

        if (pressE_Yazisi != null) pressE_Yazisi.SetActive(false);
    }

    void Update()
    {
        if (oyuncuKarakteri == null || gameManager == null) return;

        // Mesafeyi ölç
        float mesafe = Vector3.Distance(transform.position, oyuncuKarakteri.transform.position);

        if (mesafe <= etkilesimMesafesi)
        {
            if (!oyuncuYakinMi)
            {
                oyuncuYakinMi = true;
                if (pressE_Yazisi != null) pressE_Yazisi.SetActive(true);
            }

            // Yakýndayken ve tuþa basarsa
            if (Input.GetKeyDown(etkilesimTusu))
            {
                ObjeyiAl();
            }
        }
        else
        {
            if (oyuncuYakinMi)
            {
                oyuncuYakinMi = false;
                if (pressE_Yazisi != null) pressE_Yazisi.SetActive(false);
            }
        }
    }

    void ObjeyiAl()
    {
        Debug.Log("Þýrýnga alýndý!");

        // UI yazýsýný kapat
        if (pressE_Yazisi != null) pressE_Yazisi.SetActive(false);

        // GameManager'a kaçýþýn baþarýlý olduðunu haber ver
        gameManager.KacisBasariliSistemi();

        // Þýrýnga objesini sahneden sil
        Destroy(gameObject);
    }
}