using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Target : MonoBehaviour {

    public GameObject target;
    public Image circle;

    public float scalingRate;
    public float[] allScalingRates;
    public float startingScale;

    private bool wouldDie;
    private float scale;

	// Use this for initialization
	void Start () {
        scale = startingScale;
        wouldDie = false;
        scalingRate = allScalingRates[Mathf.FloorToInt(Random.Range(0, allScalingRates.Length))];
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(wouldDie) return;

        scale -= scalingRate;
        if(scale < 0f) scale = 0f;

        circle.transform.localScale = new Vector3(scale, scale, scale);

        if (scale <= 0f) {
            wouldDie = true;
            setSkull();
        }
	
	}

    private void setSkull() {
        // set skull
    }
}
