using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CVManager : MonoBehaviour
{
    public static CVManager Instance { get; private set; }


    [SerializeField] private string startScreen;
    [SerializeField] private UICV[] screens;

    [Space]
    private UICV curScreen;

    private void Awake()
    {
        Instance = this;

        foreach (UICV screen in screens)
        {
            screen.Init();
        }

        ChangeScreen(startScreen);
    }

    private void Update()
    {
        if (curScreen)
        {
            curScreen.UpdateScreen();

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                Cursor.lockState = curScreen.CursorLockMode;
            }
        }
    }

    private UICV GetScreen(string screenName)
    {
        foreach (UICV screen in screens)
        {
            if (screen.ScreenName == screenName) return screen;
        }

        throw new System.Exception($"{screenName} экрана не существует!");
    }

    public void ChangeScreen(string screenName)
    {
        UICV nextScreen = GetScreen(screenName);

        if (curScreen && nextScreen != curScreen)
        {
            curScreen.HideScreen();
        }

        curScreen = nextScreen;
        curScreen.ShowScreen();
        Cursor.lockState = curScreen.CursorLockMode;
    }
}
