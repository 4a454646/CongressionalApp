using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDot : MonoBehaviour {
    public SlideController slideController;
    public float storedX;
    public Vector3 parent;
    public GameObject sliderBar;
    private SpriteRenderer spriteRenderer;
    public ParticleSystem sliderParticles;
    public ParticleSystem fireworkParticles;    
    public float lerpValue;
    public bool isChecked;
    // hello github!

    private void Start() {
        parent = transform.parent.transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        sliderParticles.Play();
    }

    private void Update() {
        sliderBar.transform.localScale = new Vector2(2 - (slideController.outerBound - transform.localPosition.x) * 2, 1);
    }

    private void OnMouseEnter() { 
        if (isChecked) { spriteRenderer.sprite = slideController.dotHoveredBlack; }
        else { spriteRenderer.sprite = slideController.dotHovered; }
    }
    private void OnMouseExit() { 
        if (isChecked) { spriteRenderer.sprite = slideController.dotBlack; }
        else { spriteRenderer.sprite = slideController.dotRegular; }
    }

    private void OnMouseDrag() {
        transform.position = new Vector3(
            Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + storedX,
            parent.x - slideController.outerBound, parent.x + slideController.outerBound),
            parent.y, -2);
        if (slideController.sendParticle && !isChecked) { 
            sliderParticles.Emit(1);
            slideController.sendParticle = false; 
        }
        lerpValue = Mathf.Sqrt(slideController.outerBound - transform.localPosition.x);
        if (!isChecked) {
            slideController.setColor(Color.Lerp(slideController.green, slideController.gray, lerpValue));
            slideController.scaleSideBar(lerpValue, true);
        }
        else {
            slideController.scaleSideBar(lerpValue, false);
        }
    }

    private void OnMouseDown() { 
        if (isChecked) { spriteRenderer.sprite = slideController.dotHoveredBlack; }
        else { spriteRenderer.sprite = slideController.dotHovered; }
        storedX = transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x; 
    }

    private void OnMouseUp() {
        if (transform.localPosition.x <= 0) {
            transform.localPosition = new Vector3(-slideController.outerBound, 0, -2f); 
            StartCoroutine(SetSliderStatusCoro(false));
        }
        else { 
            transform.localPosition = new Vector3(slideController.outerBound, 0, -2f); 
            StartCoroutine(SetSliderStatusCoro(true));
        }
        if (isChecked) { spriteRenderer.sprite = slideController.dotBlack; }
        else { spriteRenderer.sprite = slideController.dotRegular; }
    }
    
    private IEnumerator SetSliderStatusCoro(bool status) {
        if (status) {
            transform.parent.GetComponent<SpriteRenderer>().sprite = slideController.backBlack;
            sliderBar.GetComponent<SpriteRenderer>().sprite = slideController.slideBlack;
            spriteRenderer.sprite = slideController.dotBlack;
            sliderParticles.Stop();
            if (!isChecked) { 
                fireworkParticles.Play(); 
                slideController.soundManager.PlayClip("ding");
                slideController.setColor(Color.black);
                // slideController.setColor(Color.white);
                yield return slideController.waitTimeMed;
                slideController.setColor(slideController.green);
                yield return slideController.waitTimeMed;
            }
        }
        else {
            transform.parent.GetComponent<SpriteRenderer>().sprite = slideController.backRegular;
            sliderBar.GetComponent<SpriteRenderer>().sprite = slideController.slideRegular;
            spriteRenderer.sprite = slideController.dotRegular;
            sliderParticles.Play();
        }
        slideController.setColor(slideController.gray);
        isChecked = status;
        StartCoroutine(slideController.setSideBarCoro());
    }
}
