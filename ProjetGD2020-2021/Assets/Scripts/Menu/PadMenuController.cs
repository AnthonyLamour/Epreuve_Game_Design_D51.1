using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PadMenuController : MonoBehaviour
{
//variables publiques
    //liste des boutons du menu
    public GameObject[] buttonList;

    //liste des sons utilisés dans le menu
    public AudioClip[] menuSounds;

    //liste des différents audio output
    public AudioMixerGroup[] mixerGroups;

//variables privées
    //panel d'assignation de la manette dirigeant le menu
    private GameObject panelAssign;

    //gamepad dirigeant le menu
    private Gamepad leadGamePad;

    //numéro du bouton actuellement sélectionné
    private int selectedButton;

    //début du prochain mouvement
    private float nextMove;
    //cooldown entre chaque déplacement dans le menu
    private float cooldown;

    //audio source du gestionnaire de menu
    private AudioSource audioSource;

    // Start est appelé à la première activation de l'objet
    void Start()
    {
        //masquage du curseur de la souris
        Cursor.visible = false;
        //lock du curseur au centre de l'écran
        Cursor.lockState=CursorLockMode.Locked;
        //initialisation du cooldown
        cooldown = 0.2f;
        //initialisation du début du prochain mouvement
        nextMove = 0;
        //initialisation panelAssign
        panelAssign = GameObject.FindGameObjectWithTag("PanelAssign");
        //initialisation de audioSource
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update est appelé à chaque frames
    void Update()
    {
        //si il n'y a pas de gamepad dirigeant le menu
        if(leadGamePad == null)
        {
            //si la touche a d'un gamepad est pressée
            if (Gamepad.current.aButton.isPressed)
            {
                //assignation du gamepad actuel au gamepad dirigeant le menu
                leadGamePad = Gamepad.current;
                //désactivation du panel d'assignation
                panelAssign.SetActive(false);
                //changement de la taille du premier bouton
                buttonList[0].GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
                //initialisation du bouton sélectionné
                selectedButton = 0;
                //initialisation du début du prochain mouvement
                nextMove = Time.time + cooldown;
            }
        }
        //sinon
        else
        {
            //si le gamepad bouge vers la droite et que le prochain mouvement est atteint
            if (leadGamePad.leftStick.right.ReadValue() > 0.5 && Time.time>nextMove)
            {
                //set du début du prochain mouvement
                nextMove = Time.time + cooldown;
                //passage au bouton suivant
                NextButton();
            }
            //sinon le gamepad bouge vers la gauche et que le prochain mouvement est atteint
            else if (leadGamePad.leftStick.left.ReadValue() > 0.5 && Time.time > nextMove)
            {
                //set du début du prochain mouvement
                nextMove = Time.time + cooldown;
                //passage au bouton précédent
                PreviousButton();
            }
            //sinon le gamepad bouge vers le haut et que le prochain mouvement est atteint
            else if (leadGamePad.leftStick.up.ReadValue() > 0.5 && Time.time > nextMove)
            {
                //set du début du prochain mouvement
                nextMove = Time.time + cooldown;
                //passage au bouton du dessus
                PreviousLine();
            }
            //sinon le gamepad bouge vers le bas et que le prochain mouvement est atteint
            else if (leadGamePad.leftStick.down.ReadValue() > 0.5 && Time.time > nextMove)
            {
                //set du début du prochain mouvement
                nextMove = Time.time + cooldown;
                //passage au bouton du dessous
                NextLine();
            }
            //sinon la touche a du gamepad est pressée et que le prochain mouvement est atteint
            else if (leadGamePad.aButton.isPressed && Time.time > nextMove)
            {
                //utilisation du bouton
                UseButton();
            }
        }
        
    }

    //fonction permettant de passer au bouton suivant
    private void NextButton()
    {
        //si il y a encore un bouton
        if (selectedButton < buttonList.Length - 1)
        {
            //retour à la normale de la taille du bouton actuellement sélectionné
            buttonList[selectedButton].GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
            //changement du numéro de bouton
            selectedButton++;
            //changement de la taille du nouveau bouton sélectionné
            buttonList[selectedButton].GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
            //set du mixer group de l'audio
            audioSource.outputAudioMixerGroup = mixerGroups[0];
            //lancement du son de menu
            audioSource.clip = menuSounds[0];
            audioSource.Play();
        }
        
    }

    //fonction permettant de passer au bouton précédent
    private void PreviousButton()
    {
        //si il ne s'agit pas du premier bouton
        if (selectedButton > 0)
        {
            //retour à la normale de la taille du bouton actuellement sélectionné
            buttonList[selectedButton].GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
            //changement du numéro de bouton
            selectedButton--;
            //changement de la taille du nouveau bouton sélectionné
            buttonList[selectedButton].GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
            //set du mixer group de l'audio
            audioSource.outputAudioMixerGroup = mixerGroups[0];
            //lancement du son de menu
            audioSource.clip = menuSounds[0];
            audioSource.Play();
        }
    }

    //fonction permettant de passer au bouton du dessous
    private void NextLine()
    {
        //si il reste encore une ligne de bouton complète
        if (selectedButton < buttonList.Length - 2)
        {
            //retour à la normale de la taille du bouton actuellement sélectionné
            buttonList[selectedButton].GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
            //changement du numéro de bouton
            selectedButton += 2;
            //changement de la taille du nouveau bouton sélectionné
            buttonList[selectedButton].GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
            //set du mixer group de l'audio
            audioSource.outputAudioMixerGroup = mixerGroups[0];
            //lancement du son de menu
            audioSource.clip = menuSounds[0];
            audioSource.Play();
        }
        //sinon si il reste encore un bouton
        else if (selectedButton < buttonList.Length - 1)
        {
            //retour à la normale de la taille du bouton actuellement sélectionné
            buttonList[selectedButton].GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
            //changement du numéro de bouton
            selectedButton = buttonList.Length - 1;
            //changement de la taille du nouveau bouton sélectionné
            buttonList[selectedButton].GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
            //set du mixer group de l'audio
            audioSource.outputAudioMixerGroup = mixerGroups[0];
            //lancement du son de menu
            audioSource.clip = menuSounds[0];
            audioSource.Play();
        }
    }

    //fonction permettant de passer au bouton du dessus
    private void PreviousLine()
    {
        //si le bouton n'est pas sur la première ligne
        if (selectedButton > 1)
        {
            //retour à la normale de la taille du bouton actuellement sélectionné
            buttonList[selectedButton].GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
            //changement du numéro de bouton
            selectedButton -= 2;
            //changement de la taille du nouveau bouton sélectionné
            buttonList[selectedButton].GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
            //set du mixer group de l'audio
            audioSource.outputAudioMixerGroup = mixerGroups[0];
            //lancement du son de menu
            audioSource.clip = menuSounds[0];
            audioSource.Play();
        }
        //sinon si le bouton n'est pas le premier
        else if(selectedButton > 0)
        {
            //retour à la normale de la taille du bouton actuellement sélectionné
            buttonList[selectedButton].GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
            //changement du numéro de bouton
            selectedButton = 0;
            //changement de la taille du nouveau bouton sélectionné
            buttonList[selectedButton].GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
            //set du mixer group de l'audio
            audioSource.outputAudioMixerGroup = mixerGroups[0];
            //lancement du son de menu
            audioSource.clip = menuSounds[0];
            audioSource.Play();
        }
    }

    //fonction permettant d'utiliser un bouton
    private void UseButton()
    {
        //set du mixer group de l'audio
        audioSource.outputAudioMixerGroup = mixerGroups[1];
        //lancement du son de menu
        audioSource.clip = menuSounds[1];
        audioSource.Play();
        //début de la coroutine permettant d'attendre la fin du son du bouton avant de l'activer
        StartCoroutine(PlayButton(audioSource.clip.length));
    }

    //coroutine permettant d'attendre la fin du son d'un bouton avant de l'activer
    public IEnumerator PlayButton(float SoundDuration)
    {
        //attente de la fin du son du bouton
        yield return new WaitForSeconds(SoundDuration);
        //déclanchement du onclick du bouton
        buttonList[selectedButton].GetComponent<Button>().onClick.Invoke();
    }
}
