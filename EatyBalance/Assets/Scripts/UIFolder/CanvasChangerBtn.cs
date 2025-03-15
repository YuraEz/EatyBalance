using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CanvasChangerBtn : MonoBehaviour
{
    [SerializeField] private string screenName;

    [SerializeField] private List<Animator> onDisableEffects;


    [SerializeField] private float delay;

    private Button button;
    private CVManager uiManager;
    private bool isClicked;

    private void Start()
    {
        uiManager = CVManager.Instance;
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (isClicked) return;

            isClicked = true;

            if (onDisableEffects.Count > 0) foreach (Animator anim in onDisableEffects)
            {
                anim.SetTrigger("change");
            }

            Invoke(nameof(ChangeScreen), delay);
        });
    }

    private void ChangeScreen()
    {
        uiManager.ChangeScreen(screenName);
        isClicked = false;
    }

}
