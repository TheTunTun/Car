using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarCustomization : MonoBehaviour
{
    
    public static bool GameIsPaused = false;
    // Start is called before the first frame update
    [SerializeField] private GameObject carControlUI;
    [SerializeField] private GameObject customizeMenuUI;
    [SerializeField] private MeshRenderer paint;
    [SerializeField] private AudioManagerScript audioManager;
    [SerializeField] private Text ColorLabel;

    [SerializeField] private Button customizeButton;
    [SerializeField] private Button resumeButton;



    public Action customize;
    public Action resumeGame;
    private enum colorState
    {
        defaultColor,
        red,
        blue,
        green,
        white,
        yellow,
    }

    private int currentColorIndex = 0;
    private Color defaultColor;
   
    
    
    void Start()
    {
        defaultColor = paint.material.color;
        ChangePaintColor(0);
        customize += OpenCustomizeMenu;
        resumeGame += CloseCustomizeMenu;

        customizeButton.onClick.AddListener(customize.Invoke);
        resumeButton.onClick.AddListener(resumeGame.Invoke);
    }

    
    // Update is called once per frame
    void Update()
    {
       
    }

    void OpenCustomizeMenu()
    {
        
        GameIsPaused = true;
        carControlUI.SetActive(false);
        customizeMenuUI.SetActive(true);
        StartCoroutine(ExecuteAfterTime(.2f));
        
    }
    void CloseCustomizeMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        carControlUI.SetActive(true);
        customizeMenuUI.SetActive(false);
    }

    public void IncreasePaintColorIndux()
    {
        currentColorIndex++;
        if(currentColorIndex >= System.Enum.GetValues(typeof(colorState)).Length) { currentColorIndex = 0; }
        ChangePaintColor(currentColorIndex);
    }
    public void DecreasePaintColorIndux()
    {
        Debug.Log("decrease");
        currentColorIndex--;
        if (currentColorIndex < 0) { currentColorIndex = System.Enum.GetValues(typeof(colorState)).Length - 1; }
        ChangePaintColor(currentColorIndex);

    }

    void ChangePaintColor(int Index)
    {
        switch ((colorState)Index)
        {
            case colorState.defaultColor:
                paint.material.SetColor("_BaseColor", defaultColor);
                ColorLabel.text = colorState.defaultColor.ToString();
                break;
            case colorState.red:
                paint.material.SetColor("_BaseColor", Color.red);
                ColorLabel.text = colorState.red.ToString();
                break;
            case colorState.blue:
                paint.material.SetColor("_BaseColor", Color.blue);
                ColorLabel.text = colorState.blue.ToString();
                break;
            case colorState.green:
                paint.material.SetColor("_BaseColor", Color.green);
                ColorLabel.text = colorState.green.ToString();
                break;
            case colorState.white:
                paint.material.SetColor("_BaseColor", Color.white);
                ColorLabel.text = colorState.white.ToString();
                break;
            case colorState.yellow:
                paint.material.SetColor("_BaseColor", Color.yellow);
                ColorLabel.text = colorState.yellow.ToString();
                break;
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        Time.timeScale = 0f;
    }
}
