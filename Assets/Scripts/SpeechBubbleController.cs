using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleController : MonoBehaviour
{
    public bool healPlayer;

    private int activationCounter;

    public int autoActivationLayer;

    private bool playerCloseEnough;
    public int buttonPressActivationLayer;

    private Text autoText;
    private Text buttonPressText;

    public GameObject player;

    private HealthManager healthManager;

    // Use this for initialization
    void Start() {
        healthManager = FindObjectOfType<HealthManager>();
        activationCounter = 0;
        autoText = gameObject.GetComponentsInChildren<Text>()[0];
        buttonPressText = gameObject.GetComponentsInChildren<Text>()[1];
        autoText.enabled = false;
        buttonPressText.enabled = false;
        print(autoText.text);
        print(buttonPressText.text);
    }

    // Update is called once per frame
    void Update() {
        //ativa o texto "buttonPress" na camada correpondente a ele
        //uma ver ativo, o texto "buttonPress" não some até o personagem se afastar da camada mais externa
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (playerCloseEnough && buttonPressText.text != "") {
                autoText.enabled = false;
                buttonPressText.enabled = true;
            }
            if (healPlayer)
                healthManager.fullHealth();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //se o player entra em algum trigger, começa a verificação
        if (other.tag == "Player") {
            activationCounter++; 
            //ativa o primeiro texto da camada "auto" (só se o texto de "button press" estiver inativo
            if (activationCounter >= autoActivationLayer && !buttonPressText.enabled)
                autoText.enabled = true;
            //ativa a flag para que o texto apareça com o apertar de um botão (da camada button flag)
            if (activationCounter >= buttonPressActivationLayer) {
                playerCloseEnough = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        //se o player sai de algum trigger, começa o tratamento
        if (other.tag == "Player") {
            activationCounter--;
            //se está numa camada fora do trigger de "buttonPress" entao o botao nao funciona
            if (activationCounter < buttonPressActivationLayer)
                playerCloseEnough = false;
            //se o contador chega a zero, então desativa tudo
            if (activationCounter <= 0) {
                autoText.enabled = false;
                buttonPressText.enabled = false;
            }
        }
    }
}
