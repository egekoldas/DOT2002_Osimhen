using UnityEngine;

public class GateController : MonoBehaviour
{
    private bool isOpening = false;

    public Transform leftDoor;  // Kapýnýn sol kanadý
    public Transform rightDoor; // Kapýnýn sað kanadý

    public float openSpeed = 2f;     // Kapýlarýn açýlma hýzý
    public float targetAngle = 90f;  // Kapýlarýn döneceði açý derecesi

    // YENÝ: Kapatýlacak olan kýrmýzý ýþýn sütunu objesi (IsinSutunu)
    public GameObject isinSutunu;

    public void OpenGate()
    {
        isOpening = true;

        // Kapýlar açýlýrken sað ve sol kanattaki fizikleri kapatýyoruz ki oyuncu geçebilsin
        if (leftDoor.GetComponent<Collider>() != null)
            leftDoor.GetComponent<Collider>().enabled = false;

        if (rightDoor.GetComponent<Collider>() != null)
            rightDoor.GetComponent<Collider>().enabled = false;

        // YENÝ: Oyuncu kapýyý açtýðý an kýrmýzý ýþýn sütununu söndür/gizle
        if (isinSutunu != null)
        {
            isinSutunu.SetActive(false);
        }
    }

    void Update()
    {
        if (isOpening)
        {
            // Ýki kapýyý da dýþarý (ýþýða doðru) açacak þekilde döndürmeye devam ediyoruz
            leftDoor.localRotation = Quaternion.Slerp(leftDoor.localRotation, Quaternion.Euler(0, -targetAngle, 0), Time.deltaTime * openSpeed);
            rightDoor.localRotation = Quaternion.Slerp(rightDoor.localRotation, Quaternion.Euler(0, -targetAngle, 0), Time.deltaTime * openSpeed);
        }
    }
}