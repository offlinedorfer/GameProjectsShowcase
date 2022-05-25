using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterConditions : MonoBehaviour
{
    public float a_skiing;
    public float a_Shooting;

    public Slider speedSlider;

    // Start is called before the first frame update
    void Awake()
    {
        a_skiing /= 20;
    }
}
