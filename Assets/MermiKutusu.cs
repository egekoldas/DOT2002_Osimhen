using UnityEngine;

public class MermiKutusu : MonoBehaviour
{
    public int verilecekMermi = 30; // Kutu kaç mermi verecek?
    public float etkilesimMesafesi = 3f;
    public KeyCode etkilesimTusu = KeyCode.E;
    public GameObject pressE_Yazisi;

    private GameObject oyuncu;
    private AtesSistemi atesSistemi;
    private bool oyuncuYakinMi = false;

    void Start()
    {
        oyuncu = GameObject.FindGameObjectWithTag("Oyuncu"); // Tagýný hatýrlýyorum :)
        if (oyuncu != null)
        {
            atesSistemi = oyuncu.GetComponentInChildren<AtesSistemi>();
        }
        if (pressE_Yazisi != null) pressE_Yazisi.SetActive(false);
    }

    void Update()
    {
        if (oyuncu == null || atesSistemi == null) return;

        float mesafe = Vector3.Distance(transform.position, oyuncu.transform.position);

        if (mesafe <= etkilesimMesafesi)
        {
            if (!oyuncuYakinMi)
            {
                oyuncuYakinMi = true;
                if (pressE_Yazisi != null) pressE_Yazisi.SetActive(true);
            }

            if (Input.GetKeyDown(etkilesimTusu))
            {
                atesSistemi.YedekMermiEkle(verilecekMermi); // Karakterin mermisini arttýr

                if (pressE_Yazisi != null) pressE_Yazisi.SetActive(false);
                Destroy(gameObject); // Kutuyu yok et
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
}