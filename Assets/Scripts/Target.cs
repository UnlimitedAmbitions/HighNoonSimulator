using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Target : MonoBehaviour {

    public GameObject target;
    public Image circle;

    public float scalingRate;
    public float[] allScalingRates;
    public float startingScale;
    public float consideredDeathScale;
    public int[] totalHP;
    public int HP;

    private bool wouldDie;
    private float scale;

	// Use this for initialization
	void Start () {
        scale = startingScale;
        wouldDie = false;
        scalingRate = allScalingRates[Mathf.FloorToInt(Random.Range(0, allScalingRates.Length))];
        HP = totalHP[Mathf.FloorToInt(Random.Range(0, totalHP.Length))];
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(wouldDie || !GameManager.instance.gameStarted) return;

        scale -= scalingRate*Time.deltaTime;
        if(scale < 0f) scale = 0f;

        circle.transform.localScale = new Vector3(scale, scale, scale);

        if (scale <= consideredDeathScale) {
            wouldDie = true;
            setSkull();
        }
	
	}

    public float GetScale() {
        return scale;
    }

    public bool IsDead() {
        return wouldDie;
    }

    private void setSkull() {
        // set skull
        Debug.Log("target set skull");
    }
}
