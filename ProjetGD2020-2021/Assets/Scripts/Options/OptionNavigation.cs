using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionNavigation : MonoBehaviour
{
    //variable publique
    //liste des slidebar de options
    public Slider[] optionsSliders;

    //variables privées
    //gamepad dirigeant le menu
    private Gamepad leadGamePad;

    //début du prochain mouvement
    private float nextMove;
    //cooldown entre chaque déplacement dans le menu
    private float cooldown;

    //numéro du slider actuellement sélectionné
    private int currentSlider;

    //audioSource du tuto
    private AudioSource audioSource;

    // Start est appelé à la première activation de l'objet
    void Start()
    {
        //initialisation de leadGamePad
        leadGamePad = GameObject.FindGameObjectWithTag("LeadPad").GetComponent<SaveLeadPad>().GetLeadGamepad();
        //destruction de l'objet stockant les information précédemment listées
        Destroy(GameObject.FindGameObjectWithTag("LeadPad").gameObject);
        //initialisation du cooldown
        cooldown = 0.2f;
        //initialisation du début du prochain mouvement
        nextMove = 0;
        currentSlider = 0;
        optionsSliders[currentSlider].gameObject.GetComponent<RectTransform>().localScale = new Vector2(1f, 1.5f);
        //initialisation de audioSource
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update est appelé à chaque frames
    void Update()
    {
        //si joystick left
        if (leadGamePad.leftStick.left.ReadValue() > 0.5f && Time.time > nextMove)
        {
            //set du début du prochain mouvement
            nextMove = Time.time + cooldown;
            //modification de la slideBar
            UpdateSlider(optionsSliders[currentSlider],-1f);
        }
        //sinon si joystick rigth
        else if (leadGamePad.leftStick.right.ReadValue() > 0.5f && Time.time > nextMove)
        {
            //set du début du prochain mouvement
            nextMove = Time.time + cooldown;
            //modification de la slideBar
            UpdateSlider(optionsSliders[currentSlider], 1f);
        }//sinon si joystick up
        else if (leadGamePad.leftStick.up.ReadValue() > 0.5f && Time.time > nextMove)
        {
            //set du début du prochain mouvement
            nextMove = Time.time + cooldown;
            //modification de la slideBar sélectionné
            ChangeSlider(-1);
        }//sinon si joystick up
        else if (leadGamePad.leftStick.down.ReadValue() > 0.5f && Time.time > nextMove)
        {
            //set du début du prochain mouvement
            nextMove = Time.time + cooldown;
            //modification de la slideBar sélectionné
            ChangeSlider(1);
        }
        //sinon si le bouton start est pressé
        else if (leadGamePad.startButton.isPressed)
        {
            Destroy(GameObject.FindGameObjectWithTag("Music").gameObject);
            //retour au menu principale
            SceneManager.LoadScene("MainMenu");
        }
    }

    //fonction permettant de changer la slide bar sélectionné
    private void ChangeSlider(int value)
    {
        
        if((currentSlider < optionsSliders.Length - 1 && value >0) || (currentSlider>0 && value <0))
        {
            optionsSliders[currentSlider].gameObject.GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
            currentSlider = currentSlider + value;
            optionsSliders[currentSlider].gameObject.GetComponent<RectTransform>().localScale = new Vector2(1f, 1.5f);
        }

    }

    //fonction permettant d'update la slide bar sélectionné
    private void UpdateSlider(Slider sliderToUpdate, float value)
    {

        if (optionsSliders[currentSlider].value+value>=0 && optionsSliders[currentSlider].value + value <= 10)
        {
            optionsSliders[currentSlider].value = optionsSliders[currentSlider].value + value;
        }
        
    }
}
