using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregarCenas : MonoBehaviour
{
    public void loadScene(string nome)
    {
        SceneManager.LoadScene(nome);
    }

    public void sair()
    {
        Debug.Log("sair");
        Application.Quit();
    }
}
