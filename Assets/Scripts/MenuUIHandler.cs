using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// This namespace will only be included when compiling within the Unity Editor
#if UNITY_EDITOR
    using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        // add code here to handle when a color is selected
        MainManager.instance.TeamColor = color;
    }
    
    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;

        // pre-select the saved color in the MainManager (if there is one) when the menu screen is launched.
        ColorPicker.SelectColor(MainManager.instance.TeamColor);
    }

    // quick testing functionality : save the color from the application instantly
    public void SaveColorClicked()
    {
        MainManager.instance.SaveColor();
    }

    // // quick testing functionality : load the color from the application instantly
    public void LoadColorClicked()
    {
        MainManager.instance.LoadColor();
        ColorPicker.SelectColor(MainManager.instance.TeamColor);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    // Conditionnal compiling
    public void Exit()
    {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // Only works in the built application, not while testing in Editor mode
#endif
        // save the user’s last selected color when the application exits.
        MainManager.instance.SaveColor();
    }
}
