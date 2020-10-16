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
    

    private void Start() {
        parent = transform.parent.transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        sliderParticles.Play();
    }

    private void Update() {
        sliderBar.transform.localScale = new Vector2(2 - (slideController.outerBound - transform.localPosition.x) * 2, 1);
        lerpValue = Mathf.Sqrt(slideController.outerBound - transform.position.x);
        if (!isChecked) {
            slideController.backgroundCube.color = Color.Lerp(slideController.green, slideController.gray, lerpValue);
        }
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
    }

    private void OnMouseDown() { 
        if (isChecked) { spriteRenderer.sprite = slideController.dotHoveredBlack; }
        else { spriteRenderer.sprite = slideController.dotHovered; }
        storedX = transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x; 
    }

    private void OnMouseUp() {
        if (transform.localPosition.x <= 0) {
            transform.localPosition = new Vector3(-slideController.outerBound, 0, -2f); 
            SetSliderStatus(false);
        }
        else { 
            transform.localPosition = new Vector3(slideController.outerBound, 0, -2f); 
            SetSliderStatus(true);
        }
        if (isChecked) { spriteRenderer.sprite = slideController.dotBlack; }
        else { spriteRenderer.sprite = slideController.dotRegular; }
    }
    
    private void SetSliderStatus(bool status) {
        if (status) {
            transform.parent.GetComponent<SpriteRenderer>().sprite = slideController.backBlack;
            sliderBar.GetComponent<SpriteRenderer>().sprite = slideController.slideBlack;
            spriteRenderer.sprite = slideController.dotBlack;
            sliderParticles.Stop();
            if (!isChecked) { fireworkParticles.Play(); }
            slideController.backgroundCube.color = slideController.gray;
        }
        else {
            transform.parent.GetComponent<SpriteRenderer>().sprite = slideController.backRegular;
            sliderBar.GetComponent<SpriteRenderer>().sprite = slideController.slideRegular;
            spriteRenderer.sprite = slideController.dotRegular;
            sliderParticles.Play();
        }
        isChecked = status;
        StartCoroutine(slideController.scaleSideBar());
    }
}
