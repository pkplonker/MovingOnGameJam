using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClipUI : MonoBehaviour
{
    Text clipText;

    [SerializeField]
    IntVariable value;

    private void Start()
    {
        clipText = GetComponent<Text>();
        clipText.text = value.Get().ToString();
    }

    // Update is called once per frame
    public void UpdateText()
    {
        clipText.text = value.Get().ToString();
    }
}
