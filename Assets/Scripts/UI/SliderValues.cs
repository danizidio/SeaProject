using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValues : MonoBehaviour
{
    Slider _slider;

    [SerializeField] TMP_Text _text;
    void Awake()
    {
        _slider = this.GetComponent<Slider>();
    }

    public void ChangingValuesText()
    {
        _text.text = _slider.value.ToString();
    }

}
