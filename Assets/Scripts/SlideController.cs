using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// git add * 
// git commit -a
// git push origin master
// maybe github desktop will work

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
    public GameObject _backgroundCube;
    private SpriteRenderer backgroundCube;
    public bool sendParticle = false;
    public WaitForSeconds waitTimeLong;
    public WaitForSeconds waitTimeMed;
    public WaitForSeconds waitTimeShort;
    public SliderDot[] sliderDots;
    public GameObject sideBarFill;
    public GameObject sideBarParticles;
    public SoundManager soundManager;
    
    private void Start() {
        backgroundCube = _backgroundCube.GetComponent<SpriteRenderer>();
        waitTimeLong = new WaitForSeconds(0.4f);
        waitTimeMed = new WaitForSeconds(0.05f);
        waitTimeShort = new WaitForSeconds(0.01f);
        soundManager = FindObjectOfType<SoundManager>();
        StartCoroutine(particleCoro());
    }

    private IEnumerator particleCoro() {
        while (true) { 
            yield return waitTimeLong;
            sendParticle = !sendParticle;
        }
    }

    public void scaleSideBar(float lerpValue, bool addOne) {
        float oldScale = sideBarFill.transform.localScale.y;
        float newScale = addOne ? 
            ((countChecked() + 1 - lerpValue) / 6.0f) * 29.65f : 
            ((countChecked() - lerpValue) / 6.0f) * 29.65f ;
        sideBarFill.transform.localScale = new Vector3(
            0.95f, sideBarFill.transform.localScale.y + (newScale - oldScale), 1);
    }
    
    public IEnumerator setSideBarCoro() {
        float oldScale = sideBarFill.transform.localScale.y;
        float newScale = (countChecked() / 6.0f) * 29.65f;
        for (int i = 0; i < 10; i++) {
            sideBarFill.transform.localScale = new Vector3(
                0.95f, sideBarFill.transform.localScale.y + (newScale - oldScale) / 10, 1);
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
        // probably a way to do this with a list comprehension, but no need
    }

    public void setColor(Color _color) {
        backgroundCube.color = _color;
    }
}
