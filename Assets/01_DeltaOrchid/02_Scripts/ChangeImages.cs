using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImages : MonoBehaviour
{

    public Sprite _winsprite;
    public Sprite _lostsprite;
    public Image _image;
    public void ChangeGamewinImage()
    {
        _image.sprite = _winsprite;
    }
    public void ChangeGameLostImage()
    {
        _image.sprite = _lostsprite;

    }
}
