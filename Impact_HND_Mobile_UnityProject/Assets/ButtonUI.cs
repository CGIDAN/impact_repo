using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "Impact";
    public void NemGameButton()
    {
        SceneManager.LoadScene(newGameLevel);
    }
}
