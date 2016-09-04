using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject targetPrefab;
    public int minNbTargets, maxNbTargets;

    [Tooltip("corners of screen")]
    public Transform lowerLeft, upperRight;

    public float startDelay;

    //UI
    public Button replayBtn, menuBtn;


    private List<GameObject> targets;
    private bool fired;
    public bool gameStarted;

    private int killed;
    private int remainingHp;
    private float damageDone;
    private float timeWaited;

	// Use this for initialization
	void Start () {
        Debug.Log("game manager start");
        if(instance != null) {
            Destroy(this.gameObject);
        }

        replayBtn.gameObject.SetActive(false);
        menuBtn.gameObject.SetActive(false);

        instance = this;
        gameStarted = false;
        fired = false;
        killed = 0;
        remainingHp = 0;
        damageDone = 0f;
        timeWaited = 0f;

	    targets = new List<GameObject>();

        // start game after delay
        Invoke("StartGame", startDelay);      
	}
	
	// Update is called once per frame
	void Update () {
        // timer
        if(gameStarted) {
            timeWaited += Time.deltaTime;
        }

        // logic for firing
        if(Input.GetButtonDown("Fire1") && !fired){
            fired = true;
            Debug.Log("BANG");
            EndGame();
        }
	}

    private void StartGame() {
        Debug.Log("start game");
        RandomizeTargets();
        gameStarted = true;
    }

    private void EndGame() {
        Debug.Log("end game");
        gameStarted = false;
        CheckTargets();
        StatAssessment();
        replayBtn.gameObject.SetActive(true);
        menuBtn.gameObject.SetActive(true);

    }

    public void ImDead(){
        EndGame();
    }

    public void RestartGame(){
        SceneManager.LoadScene("game");
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    private void RandomizeTargets()
    {
        //random nb of targets
       int nbTargets = Mathf.FloorToInt(Random.Range(minNbTargets, maxNbTargets));

       for(int i = 0; i < nbTargets; ++i) {
        
        //random position
        float x = Random.Range(lowerLeft.position.x, upperRight.position.x);
        float y = Random.Range(lowerLeft.position.y, upperRight.position.y);
        Vector3 pos = new Vector3(x, y, 0f);

        targets.Add(Instantiate(targetPrefab, pos, Quaternion.identity) as GameObject);
       }
    }

    private void CheckTargets(){
        Debug.Log("check target");
        // update nb of kill and dmg for each target
        foreach(GameObject target in targets) {
            float curr = target.GetComponent<Target>().GetScale();
            float HP = target.GetComponent<Target>().HP;
            float total = target.GetComponent<Target>().startingScale;
            bool isDead = target.GetComponent<Target>().IsDead();

            if(isDead) {
                damageDone  += HP;
                killed += 1;
            }
            else {
                damageDone += HP * (total - (curr/total));
            }
        }
    }

    private void StatAssessment() {
        // get old value
        //int prevTotalKilled = PlayerPrefs.GetInt("TotalKilled");
        //int prevRemainingHp = PlayerPrefs.GetInt("RemainingHp");
        float prevDamageDone = PlayerPrefs.GetFloat("DamageDone");
        float prevTimeWaited = PlayerPrefs.GetFloat("TimeWaited"+killed);
        Debug.Log("killed: "+killed);
        Debug.Log("dmg done: "+damageDone);
        Debug.Log("reaction time: "+timeWaited);

        // update stats
        //if(prevRemainingHp < remainingHp) PlayerPrefs.SetInt("TotalDeath", remainingHp);
        if(prevDamageDone < damageDone) PlayerPrefs.SetFloat("DamageDone", damageDone);
        if(prevTimeWaited < timeWaited) PlayerPrefs.SetFloat("TimeWaited"+killed, timeWaited);
        PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed") + 1);
        PlayerPrefs.SetInt("TotalKilled", PlayerPrefs.GetInt("TotalKilled") + 1);
        if(remainingHp <= 0) PlayerPrefs.SetInt("TotalDeath", PlayerPrefs.GetInt("TotalDeath") + 1);

    }
}
