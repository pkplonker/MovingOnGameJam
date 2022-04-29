using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprintUI : MonoBehaviour
{
    // Start is called before the first frame update
    Slider slider;

    [SerializeField]
    FloatVariable sprintUI;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    public void UpdateSlider()
    {
        slider.value = sprintUI.Get();
    }
}
