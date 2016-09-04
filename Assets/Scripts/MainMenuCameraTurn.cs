using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuCameraTurn : MonoBehaviour {
	public Animator camAnim;

    public void StartGame()
    {
        SceneManager.LoadScene("game");
    }
	
	public void ToStats(){
		camAnim.SetTrigger("MainToStats");
	}

	public void ToMainMenu(){
		camAnim.SetTrigger("StatsToMain");
	}
}
