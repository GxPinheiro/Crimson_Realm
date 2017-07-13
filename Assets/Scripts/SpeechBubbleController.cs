using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleController : MonoBehaviour {

    public int activationCounter;

    public int autoActivationLayer;
    
    public bool playerCloseEnough;
    public int buttonPressActivationLayer;

    private Text autoText;
    private Text buttonPressText;

	// Use this for initialization
	void Start () {
        activationCounter = 0;
        autoText = gameObject.GetComponentsInChildren<Text>()[0];
        buttonPressText = gameObject.GetComponentsInChildren<Text>()[1];
        autoText.enabled = false;
        buttonPressText.enabled = false;
        print(autoText.text);
        print(buttonPressText.text);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow) && playerCloseEnough) {
            autoText.enabled = false;
            buttonPressText.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            activationCounter++;
            if(activationCounter >= autoActivationLayer)
                autoText.enabled = true;
            if (activationCounter >= buttonPressActivationLayer)
                playerCloseEnough = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            activationCounter--;
            if (activationCounter < buttonPressActivationLayer)
                playerCloseEnough = false;
            if (activationCounter < autoActivationLayer) {
                autoText.enabled = false;
                buttonPressText.enabled = false;
            }
        }
    }
}
