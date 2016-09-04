using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject targetPrefab;
    public int minNbTargets, maxNbTargets;

    [Tooltip("corners of screen")]
    public Transform lowerLeft, upperRight;


    private List<GameObject> targets;

	// Use this for initialization
	void Start () {
        if(instance != null) {
            Destroy(this.gameObject);
        }

        instance = this;
	    targets = new List<GameObject>();
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ImDead(){
        
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
}
