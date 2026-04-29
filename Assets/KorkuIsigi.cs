using UnityEngine;

public class KorkuIsigi : MonoBehaviour
{
    public Light lamba; // Kontrol edeceðimiz ýþýk
    public float minimumGuc = 0.1f; // Iþýðýn en sönük hali
    public float maksimumGuc = 2.0f; // Iþýðýn en parlak hali

    private float beklemeSuresi;
    private float zamanlayici;

    void Start()
    {
        // Kodu attýðýmýz objedeki ýþýðý otomatik bulur
        if (lamba == null) lamba = GetComponent<Light>();
    }

    void Update()
    {
        zamanlayici += Time.deltaTime;

        // Rastgele sürelerde ýþýðýn gücünü deðiþtirir (Tam bir cýzýrtý hissi verir)
        if (zamanlayici >= beklemeSuresi)
        {
            lamba.intensity = Random.Range(minimumGuc, maksimumGuc);

            // Bir sonraki titremenin ne zaman olacaðýný rastgele belirler (0.05 saniye ile 0.2 saniye arasý)
            beklemeSuresi = Random.Range(0.05f, 0.2f);
            zamanlayici = 0f;
        }
    }
}