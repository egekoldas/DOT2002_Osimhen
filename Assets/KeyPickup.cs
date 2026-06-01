using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject isinKilici;
    public bool isBossKey = false; // YENÝ: Bu anahtar boss kapýsýna mý ait?

    public void TakeKey()
    {
        // Eðer bu bir boss anahtarýysa boss yetkisini aç, deðilse normal kapý yetkisini aç
        if (isBossKey)
        {
            PlayerInteract.hasBossKey = true;
        }
        else
        {
            PlayerInteract.hasKey = true;
        }

        if (isinKilici != null)
        {
            isinKilici.SetActive(true);
        }

        Destroy(gameObject);
    }
}