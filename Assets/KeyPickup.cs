using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject isinKilici; // Gökyüzüne uzanan kýrmýzý ýþýn sütunu

    public void TakeKey()
    {
        PlayerInteract.hasKey = true; // Anahtar artýk envanterde

        if (isinKilici != null)
        {
            isinKilici.SetActive(true); // Kapýdaki devasa kýrmýzý ýþýný yak
        }

        Destroy(gameObject); // Yerden anahtar modelini yok et
    }
}