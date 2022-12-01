using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void EndGame()
    {
        
        SceneManager.LoadScene("GameOver");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


    }

    public void WonGame()
    {

        SceneManager.LoadScene("WonScene");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


    }
}
