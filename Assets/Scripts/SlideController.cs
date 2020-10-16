using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour {
    public bool isChecked = false;
    public Sprite backBlack;
    public Sprite backRegular;
    public Sprite dotBlack;
    public Sprite dotRegular;
    public Sprite dotHovered;
    public Sprite dotHoveredBlack;
    public Sprite slideRegular;
    public Sprite slideBlack;
    public Color gray;
    public Color green;
    public float outerBound = 0.5f;
    public SpriteRenderer backgroundCube;
    public bool sendParticle = false;
    private WaitForSeconds waitTimeLong;
    private WaitForSeconds waitTimeShort;
    public SliderDot[] sliderDots;
    public GameObject sideBarFill;
    
    private void Start() {
        waitTimeLong = new WaitForSeconds(0.4f);
        waitTimeShort = new WaitForSeconds(0.025f);
        StartCoroutine(particleCoro());
    }

    private IEnumerator particleCoro() {
        while (true) { 
            yield return waitTimeLong;
            sendParticle = !sendParticle;
        }
    }

    public IEnumerator scaleSideBar() {
        float oldScale = sideBarFill.transform.localScale.y;
        float newScale = (countChecked() / 6.0f) * 29.65f;
        for (int i = 1; i < 11; i++) {
            sideBarFill.transform.localScale = new Vector3(1, 
            sideBarFill.transform.localScale.y + (newScale - oldScale) / 10
            ,1);
            yield return waitTimeShort;
        }
    }

    private void Update() {
        
    }

    public int countChecked() {
        int counter = 0;
        for (int i = 0; i < sliderDots.Length; i++) {
            if (sliderDots[i].isChecked) { counter++; }
        }
        return counter;
    }
}
