using UnityEngine;
using Unity.Cinemachine; // Eðer hata verirse burayý 'using Cinemachine;' yapmayý dene

public class KameraSistemi : MonoBehaviour
{
    [Header("Kameralar (Novagon Prototip)")]
    public CinemachineCamera kameraAna;
    public CinemachineCamera kamera3Sahis;
    public CinemachineCamera kameraKusBakisi;

    void Update()
    {
        // Kutularýn boþ olup olmadýðýný kontrol ederek hata almaný engelledim
        if (Input.GetKeyDown(KeyCode.Alpha1) && kameraAna != null) KameraDegistir(kameraAna);
        if (Input.GetKeyDown(KeyCode.Alpha2) && kamera3Sahis != null) KameraDegistir(kamera3Sahis);
        if (Input.GetKeyDown(KeyCode.Alpha3) && kameraKusBakisi != null) KameraDegistir(kameraKusBakisi);
    }

    void KameraDegistir(CinemachineCamera hedefKamera)
    {
        // Null kontrolü (Çarpý hatasýný engellemek için güvenlik)
        if (kameraAna == null || kamera3Sahis == null || kameraKusBakisi == null) return;

        // Öncelikleri sýfýrla
        kameraAna.Priority = 10;
        kamera3Sahis.Priority = 10;
        kameraKusBakisi.Priority = 10;

        // Seçileni öne çýkar
        hedefKamera.Priority = 20;
    }
}