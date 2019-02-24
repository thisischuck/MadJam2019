using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame()
	{
        Time.timeScale = 1f;
		this.transform.parent.gameObject.SetActive(false);
		GameObject.Find("GameManager").GetComponent<GameManager>().StartGame();
		//FindObjectOfType<AudioManager>().Stop("MenuTheme");
		//FindObjectOfType<AudioManager>().Play("Theme");
	}

    public void Endless()
    {
        SceneManager.LoadScene(1);
    }

	public void ExitGame()
	{
		Application.Quit();
	}
}
