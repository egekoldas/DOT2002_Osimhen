using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform takipHedefi; // Karakterin Head veya Neck kemiðini buraya koy

    void LateUpdate()
    {
        if (takipHedefi != null)
        {
            // 1. Pozisyonu Takip Et: Kamerayý karakterin kafasýna yapýþtýrýr
            transform.position = takipHedefi.position;

            // 2. Saða-Sola Dönüþü (Yaw) Eþitle: 
            // Karakter saða sola döndüðünde kamera da onunla beraber döner.
            // Ama kameranýn yukarý-aþaðý bakýþýný (X ekseni) ellemiyoruz, 
            // çünkü onu KarakterKontrol scripti fareye göre yönetiyor.
            Vector3 yeniRotasyon = transform.eulerAngles;
            yeniRotasyon.y = takipHedefi.root.eulerAngles.y;
            transform.eulerAngles = yeniRotasyon;
        }
    }
}