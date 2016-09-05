using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Target : MonoBehaviour {

    public GameObject target, point;
    public Image circle;
    public Image currentChar;

    public Sprite[] characters;

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
        target.gameObject.SetActive(false);
        point.gameObject.SetActive(true);
        scale = startingScale;
        circle.transform.localScale = new Vector3(scale, scale, scale);
        wouldDie = false;
        int index = Mathf.FloorToInt(Random.Range(0, characters.Length));
        scalingRate = allScalingRates[index];
        HP = totalHP[index];
        currentChar.sprite = characters[index];
	
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

    public void HideSkull() {
        this.gameObject.SetActive(false);
    }

    private void setSkull() {
        // set skull
        target.gameObject.SetActive(true);
        point.gameObject.SetActive(false);
        Debug.Log("target set skull");
    }
}
