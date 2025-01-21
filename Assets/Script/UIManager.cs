using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;

    public Button startButton;
    public Button exitButton;
    public Button[] songButtons;
    public StartGame data;
    public MusicData[] dataSong;
    private void Start()
    {
        // Panel awal diaktifkan, panel lagu disembunyikan
        panel1.SetActive(true);
        panel2.SetActive(false);

        // Tambahkan listener ke tombol
        startButton.onClick.AddListener(OpenSongPanel);
        exitButton.onClick.AddListener(ExitGame);

        songButtons[0].onClick.AddListener(() => SetSong(dataSong[0]));
        songButtons[1].onClick.AddListener(() => SetSong(dataSong[1]));
        songButtons[2].onClick.AddListener(() => SetSong(dataSong[2]));
        songButtons[3].onClick.AddListener(() => SetSong(dataSong[3]));
       
   
        // for (int i = 0; i < songButtons.Length; i++)
        // {
            
        //     songButtons[i].onClick.AddListener(() => SetSong(dataSong[i]));
        // }
    }

    void SetSong(MusicData music)
    {
        data.songSelect = music;
        SceneManager.LoadSceneAsync(1);
    }
    // Fungsi untuk membuka panel lagu
    void OpenSongPanel()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }

    // Fungsi untuk memuat scene langsung sesuai tombol yang ditekan
    void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Fungsi untuk keluar dari game
    void ExitGame()
    {
        Application.Quit();
    }
}
