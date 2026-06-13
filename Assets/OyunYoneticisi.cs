using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement; // Sahneleri yeniden baþlatmak için bu satýr ÞART!

[System.Serializable]
public class OyunVeriKalibi
{
    public string oyunAdi = "Ölü Bölge: Kaçýþ";
    public bool bossOlduMu = false;
    public int oyuncuSkoru = 0;
}

public class OyunYoneticisi : MonoBehaviour
{
    public GameObject kazanmaPaneli; // UI'daki WinPanel
    public GameObject kaybetmePaneli; // YENÝ: UI'daki LosePanel

    private string jsonDosyaYolu;
    private OyunVeriKalibi verilerim = new OyunVeriKalibi();

    void Start()
    {
        jsonDosyaYolu = Application.persistentDataPath + "/osimhen_games.json";

        int toplamGiris = PlayerPrefs.GetInt("ToplamGirisSayisi", 0);
        toplamGiris++;
        PlayerPrefs.SetInt("ToplamGirisSayisi", toplamGiris);
        PlayerPrefs.Save();
    }

    public void BossOlduSistemi()
    {
        Debug.Log("GameManager: Boss öldü. Kaçýþ aþamasý baþladý!");
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

    // --- YENÝ KISIM: KARAKTER ÖLÜNCE ÇALIÞACAK SÝSTEM ---
    public void OyunuKaybetmeSistemi()
    {
        Debug.Log("GameManager: Oyuncu öldü! Kaybetme ekraný açýlýyor.");

        if (kaybetmePaneli != null) kaybetmePaneli.SetActive(true); // Kaybetme panelini aç
        Time.timeScale = 0f; // Oyunu ve zombileri dondur
        Cursor.visible = true; // Fareyi görünür yap
        Cursor.lockState = CursorLockMode.None; // Fare kilidini serbest býrak
    }

    // --- YENÝ KISIM: BAÞTAN BAÞLA BUTONUNUN ÇALIÞTIRACAÐI KOD ---
    public void YenidenBasla()
    {
        Time.timeScale = 1f; // ÇOK KRÝTÝK: Donmuþ zamaný geri açmazsak yeni oyun da donuk baþlar!
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Þu an açýk olan sahneyi sýfýrdan yükle
    }
}