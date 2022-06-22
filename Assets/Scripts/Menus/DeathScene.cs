using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene: MonoBehaviour
{

	void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
	public void PlayGame()
	{
		SceneManager.LoadScene(4);

	}

		public void MainMenu()
	{
		SceneManager.LoadScene(0);

	}
}
