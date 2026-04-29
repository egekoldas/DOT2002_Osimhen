using UnityEngine;

public class FareBakisi : MonoBehaviour
{
    public float hassasiyet = 200f;
    public Transform oyuncuGövdesi; // Karakterin ana (root) objesi
    float xDönüþü = 0f;

    void Start()
    {
        // Oyun baþlar baþlamaz fareyi ekranýn ortasýna kilitle ve görünmez yap (PUBG stili)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Hiçbir tuþa basmadan sürekli farenin hareketlerini al
        float fareX = Input.GetAxis("Mouse X") * hassasiyet * Time.deltaTime;
        float fareY = Input.GetAxis("Mouse Y") * hassasiyet * Time.deltaTime;

        // 1. YUKARI - AÞAÐI (Sadece Kamerayý Döndürür)
        xDönüþü -= fareY;
        xDönüþü = Mathf.Clamp(xDönüþü, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xDönüþü, 0f, 0f);

        // 2. SAÐA - SOLA (Tüm Vücudu Döndürür)
        if (oyuncuGövdesi != null)
        {
            oyuncuGövdesi.Rotate(Vector3.up * fareX);
        }
    }
}