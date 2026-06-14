using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections; // Zamanlayýcý (Coroutine) kullanabilmek için ÞART!

[System.Serializable]
public class OyunVeriKalibi
{
    public string oyunAdi = "Ölü Bölge: Kaçýþ";
    public bool bossOlduMu = false;
    public int oyuncuSkoru = 0;
}

public class OyunYoneticisi : MonoBehaviour
{
    public GameObject kazanmaPaneli;
    public GameObject kaybetmePaneli;

    [Header("Baþlangýç Görevi Ayarlarý")]
    public GameObject baslangicYazisiUI; // Ekranda 10 saniye kalacak yazý objesi

    private string jsonDosyaYolu;
    private OyunVeriKalibi verilerim = new OyunVeriKalibi();

    void Start()
    {
        jsonDosyaYolu = Application.persistentDataPath + "/osimhen_games.json";

        int toplamGiris = PlayerPrefs.GetInt("ToplamGirisSayisi", 0);
        toplamGiris++;
        PlayerPrefs.SetInt("ToplamGirisSayisi", toplamGiris);
        PlayerPrefs.Save();

        // Oyun baþladýðýnda yazý zamanlayýcýsýný baþlat
        StartCoroutine(BaslangicYazisiniKapatSistemi());
    }

    // ZAMANLAYICI FONKSÝYONU
    IEnumerator BaslangicYazisiniKapatSistemi()
    {
        if (baslangicYazisiUI != null)
        {
            baslangicYazisiUI.SetActive(true);
        }

        // DÜZELTÝLEN KISIM: Tam 10 saniye boyunca bekler
        yield return new WaitForSeconds(10f);

        if (baslangicYazisiUI != null)
        {
            baslangicYazisiUI.SetActive(false);
        }
    }

    public void BossOlduSistemi()
    {
        Debug.Log("GameManager: Boss oldu. Kaçýþ aþamasý baþladý!");
        verilerim.bossOlduMu = true;
        verilerim.oyuncuSkoru = 500;

        string jsonMetni = JsonUtility.ToJson(verilerim, true);
        File.WriteAllText(jsonDosyaYolu, jsonMetni);
    }

    public void KacisBasariliSistemi()
    {
        if (kazanmaPaneli != null) kazanmaPaneli.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        PlayerPrefs.SetInt("OyunKazanildi", 1);
        PlayerPrefs.Save();
    }

    public void OyunuKaybetmeSistemi()
    {
        if (kaybetmePaneli != null) kaybetmePaneli.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void YenidenBasla()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}