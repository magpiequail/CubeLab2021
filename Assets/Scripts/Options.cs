using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class Options : MonoBehaviour
{
    public static int input; //0 = keyboard, 1 = mouse
    ToggleGroup inputToggleGroup;
    //Dropdown d;
    InteractionButton interaction;
    Toggle[] toggleArray;

    public Slider masterVolumeSilder;
    public Slider voiceVolumeSilder;
    public Slider SFXVolumeSilder;
    public Slider BGMVolumeSilder;

    public Image masterSpeakerImage;
    public Image voiceSpeakerImage;
    public Image SFXSpeakerImage;
    public Image BGMSpeakerImage;
    public Sprite soundOn;
    public Sprite soundOff;
    public GameObject initialSelection;
    
    public AudioMixer masterMixer;

    public Toggle currentOption
    {
        get { return inputToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    private void Awake()
    {
        //d = GetComponentInChildren<Dropdown>();
        interaction = FindObjectOfType<InteractionButton>();
        
        //volumeSilder = GetComponentInChildren<Slider>();
        inputToggleGroup = GetComponentInChildren<ToggleGroup>();
        toggleArray = inputToggleGroup.GetComponentsInChildren<Toggle>();


    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(initialSelection);
    }

    // Start is called before the first frame update
    void Start()
    {
        //this was used for dropdown 
        /*d.onValueChanged.AddListener(delegate
        {
            ChangeInput(d);
        });
        if(PlayerPrefs.GetInt("OptionValue") == 0)
        {
            d.value = 0;
            input = 0;
        }
        else if(PlayerPrefs.GetInt("OptionValue") == 1)
        {
            d.value = 1;
            input = 1;
        }*/
        ChangeInputToggle(PlayerPrefs.GetInt("OptionValue"));

        if(PlayerPrefs.GetInt("VolumeSaved") !=1)
        {
            masterVolumeSilder.value = 0.0f;
            voiceVolumeSilder.value = 0.0f;
            SFXVolumeSilder.value = 0.0f;
            BGMVolumeSilder.value = 0.0f;
        }
        else if (PlayerPrefs.GetInt("VolumeSaved") == 1)
        {

            masterVolumeSilder.value = PlayerPrefs.GetFloat("MasterVolume");
            voiceVolumeSilder.value = PlayerPrefs.GetFloat("VoiceVolume");
            SFXVolumeSilder.value = PlayerPrefs.GetFloat("SFXVolume");
            BGMVolumeSilder.value = PlayerPrefs.GetFloat("BGMVolume");
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        masterMixer.SetFloat("MasterVolume", masterVolumeSilder.value);
        masterMixer.SetFloat("VoiceVolume", voiceVolumeSilder.value);
        masterMixer.SetFloat("SFXVolume", SFXVolumeSilder.value);
        masterMixer.SetFloat("BGMVolume", BGMVolumeSilder.value);
        //AudioListener.volume = volumeSilder.value;
        if (masterVolumeSilder.value == masterVolumeSilder.minValue)
        {
            masterSpeakerImage.sprite = soundOff;
        }
        else
        {
            masterSpeakerImage.sprite = soundOn;
        }
        if (voiceVolumeSilder.value == voiceVolumeSilder.minValue)
        {
            voiceSpeakerImage.sprite = soundOff;
        }
        else
        {
            voiceSpeakerImage.sprite = soundOn;
        }
        if (SFXVolumeSilder.value == SFXVolumeSilder.minValue)
        {
            SFXSpeakerImage.sprite = soundOff;
        }
        else
        {
            SFXSpeakerImage.sprite = soundOn;
        }
        if (BGMVolumeSilder.value == BGMVolumeSilder.minValue)
        {
            BGMSpeakerImage.sprite = soundOff;
        }
        else
        {
            BGMSpeakerImage.sprite = soundOn;
        }
    }

    public void ChangeInputToggle(int i)
    {
        toggleArray[i].isOn = true;
    }

    public void ChangeInput(Dropdown d)
    {
        if(d.value == 0)
        {
            input = 0;
        }
        else if(d.value == 1)
        {
            input = 1;
        }
        if (interaction)
        {
            interaction.ChangeButtonState();
        }
        PlayerPrefs.SetInt("OptionValue", input);
    }

    public void ChangeToKeyboard()
    {
        input = 0;
        PlayerPrefs.SetInt("OptionValue", input);
        Debug.Log(input);
    }
    public void ChangeToMouse()
    {
        input = 1;
        PlayerPrefs.SetInt("OptionValue", input);
        Debug.Log(input);
    }


    public void GetCurrentInputOption()
    {
        if (PlayerPrefs.GetInt("OptionValue") == 0)
        {
            input = 0;
        }
        else if(PlayerPrefs.GetInt("OptionValue") == 1)
        {
            input = 1;
        }
    }
    public void SaveCurrentOption()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSilder.value);
        PlayerPrefs.SetFloat("VoiceVolume", voiceVolumeSilder.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolumeSilder.value);
        PlayerPrefs.SetFloat("BGMVolume", BGMVolumeSilder.value);
        PlayerPrefs.SetInt("VolumeSaved", 1);
        PlayerPrefs.SetInt("SavedOption", 1);
    }
    private void OnDisable()
    {
        SaveCurrentOption();
    }
}
