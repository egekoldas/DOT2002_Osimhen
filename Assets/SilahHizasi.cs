using UnityEngine;
using UnityEngine.Animations.Rigging; // Ellerini silaha baðlayacak sihirli kütüphane

public class SilahHizasi : MonoBehaviour
{
    [Header("Normal Durum (Sað Týk Basýlý Deðilken)")]
    public Vector3 normalPozisyon;
    public Vector3 normalAci;

    [Header("Niþan Durumu (Sað Týk Basýlýyken)")]
    public Vector3 nisanPozisyon;
    public Vector3 nisanAci;

    [Header("Rig Sistemi (Eller Ýçin)")]
    public Rig silahRig; // Hiyerarþide oluþturduðun "Silah_Rig" objesini buraya sürükle

    [Header("Ayarlar")]
    public float gecisHizi = 10f; // Silahýn ne kadar hýzlý pozisyon alacaðý

    void Update()
    {
        Vector3 hedefPozisyon;
        Vector3 hedefAci;

        // Sað týka basýlý mý kontrol et
        if (Input.GetMouseButton(1))
        {
            hedefPozisyon = nisanPozisyon;
            hedefAci = nisanAci;

            // Niþan alýrken ellerin silaha yapýþmasýný saðla (Rig aðýrlýðýný 1 yap)
            if (silahRig != null)
            {
                silahRig.weight = Mathf.Lerp(silahRig.weight, 1f, Time.deltaTime * gecisHizi);
            }
        }
        else
        {
            hedefPozisyon = normalPozisyon;
            hedefAci = normalAci;

            // Niþaný býrakýnca elleri serbest býrak (Rig aðýrlýðýný 0 yap)
            if (silahRig != null)
            {
                silahRig.weight = Mathf.Lerp(silahRig.weight, 0f, Time.deltaTime * gecisHizi);
            }
        }

        // Silahý yumuþak bir þekilde yeni konuma ve açýya kaydýr
        transform.localPosition = Vector3.Lerp(transform.localPosition, hedefPozisyon, Time.deltaTime * gecisHizi);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(hedefAci), Time.deltaTime * gecisHizi);
    }
}