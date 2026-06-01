using UnityEngine;

public class GateController : MonoBehaviour
{
    private bool isOpening = false;

    public Transform leftDoor;  // Kapýnýn sol kanadý
    public Transform rightDoor; // Kapýnýn sað kanadý

    public float openSpeed = 2f;     // Kapýlarýn açýlma hýzý
    public float targetAngle = 90f;  // Kapýlarýn döneceði açý derecesi

    // YENÝ: Unity Inspector paneline eklenecek tik kutucuðu
    public bool iceDogruAcilsin = false;

    public GameObject isinSutunu;

    public void OpenGate()
    {
        isOpening = true;

        if (leftDoor.GetComponent<Collider>() != null)
            leftDoor.GetComponent<Collider>().enabled = false;

        if (rightDoor.GetComponent<Collider>() != null)
            rightDoor.GetComponent<Collider>().enabled = false;

        if (isinSutunu != null)
        {
            isinSutunu.SetActive(false);
        }
    }

    void Update()
    {
        if (isOpening)
        {
            // Eðer Unity'den kutucuðu iþaretlediysek açý pozitif, iþaretlemediysek negatif olacak
            float aciYonu = iceDogruAcilsin ? targetAngle : -targetAngle;

            leftDoor.localRotation = Quaternion.Slerp(leftDoor.localRotation, Quaternion.Euler(0, aciYonu, 0), Time.deltaTime * openSpeed);
            rightDoor.localRotation = Quaternion.Slerp(rightDoor.localRotation, Quaternion.Euler(0, aciYonu, 0), Time.deltaTime * openSpeed);
        }
    }
}