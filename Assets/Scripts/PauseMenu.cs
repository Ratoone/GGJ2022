using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUi;
    public GameObject winUi;
    public GameObject loseUi;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                resume();
            }
            else {
                pause();
            }
        }
    }

    public void resume() {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0;
        isPaused = false;
    }

    void pause() {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1;
        isPaused = true;
    }

    public void win() {
        Time.timeScale = 0;
        winUi.SetActive(true);
    }

    public void nextLevel() {
        string currentLevelNumber = SceneManager.GetActiveScene().name.Remove(0, 5);
        SceneManager.LoadScene("Level" + (Int32.Parse(currentLevelNumber) + 1));
    }

    public void warning() {

    }
    public void lose() {
        Time.timeScale = 0;
        loseUi.SetActive(true);
    }

    public void restart() {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
