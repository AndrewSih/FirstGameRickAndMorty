using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeadCanvas : MonoBehaviour
{
   
    [SerializeField] private GameObject deadCanvas;
    [SerializeField] private GameObject pauseCanvas;
    void Start()
    {
        deadCanvas.SetActive(false);
        pauseCanvas.SetActive(false);

    }
    public void PauseOn()
    {
        deadCanvas.SetActive(true);
        Time.timeScale = 0.01f;
    }
    public void Pause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void PlayButton()
    {
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
    }
}
