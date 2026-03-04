using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public int cardID;

    [SerializeField] GameObject front;
    [SerializeField] GameObject back;

    [SerializeField] Image frontImage;

    bool isFlipped;
    bool isMatched;

    public void SetSprite(Sprite sprite)
    {
        frontImage.sprite = sprite;
    }

    public void OnClick()
    {
        if (isFlipped || isMatched)
            return;

        Flip();
        MatchSystem.Instance.RegisterCard(this);
    }

    void Flip()
    {
        isFlipped = !isFlipped;

        front.SetActive(isFlipped);
        back.SetActive(!isFlipped);

        AudioManager.Instance.PlayFlip();
    }

    public void FlipBack()
    {
        isFlipped = false;

        front.SetActive(false);
        back.SetActive(true);
    }

    public void SetMatched()
    {
        isMatched = true;
    }
}
