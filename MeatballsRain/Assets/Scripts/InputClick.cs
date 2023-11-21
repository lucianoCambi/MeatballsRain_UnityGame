using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputClick : MonoBehaviour
{

    public TextMeshProUGUI text;
    private TouchScreenKeyboard keyboard;

    public void OnClick() {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}
