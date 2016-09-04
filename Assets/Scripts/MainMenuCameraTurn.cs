using UnityEngine;
using System.Collections;

public class MainMenuCameraTurn : MonoBehaviour {
	public Animator camAnim;
	
	public void ToStats(){
		camAnim.SetTrigger("MainToStats");
	}

	public void ToMainMenu(){
		camAnim.SetTrigger("StatsToMain");
	}
}
