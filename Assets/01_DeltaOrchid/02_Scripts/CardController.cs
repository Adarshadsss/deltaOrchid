using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public int cardID;

    [SerializeField] GameObject front;
    [SerializeField] GameObject back;
    [SerializeField] Image frontImage;

    bool isFlipped = false;
    bool isMatched = false;
    bool isAnimating = false;

    float flipDuration = 0.25f;

    public void SetSprite(Sprite sprite)
    {
        frontImage.sprite = sprite;
    }

    public void OnClick()
    {
        if (isFlipped || isMatched || isAnimating)
            return;

        StartCoroutine(FlipCard(true));

        MatchSystem.Instance.RegisterCard(this);
    }

    public void FlipBack()
    {
        if (isMatched) return;
        if (!gameObject.activeInHierarchy) return;

        StartCoroutine(FlipCard(false));
    }

    public void SetMatched()
    {
        isMatched = true;
    }

    IEnumerator FlipCard(bool showFront)
    {
        isAnimating = true;

        Quaternion startRot = transform.localRotation;
        Quaternion midRot = Quaternion.Euler(0f, 90f, 0f);
        Quaternion endRot = Quaternion.Euler(0f, showFront ? 0f : 180f, 0f);

        float time = 0f;

        // rotate to 90°
        while (time < flipDuration)
        {
            time += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(startRot, midRot, time / flipDuration);
            yield return null;
        }

        // swap images at halfway
        front.SetActive(showFront);
        back.SetActive(!showFront);

        if (showFront)
            AudioManager.Instance.PlayFlip();

        time = 0f;

        // rotate to final angle
        while (time < flipDuration)
        {
            time += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(midRot, endRot, time / flipDuration);
            yield return null;
        }

        isFlipped = showFront;
        isAnimating = false;
        transform.localRotation = Quaternion.Euler(0f, 0f , 0f );

    }

    // used for the 2 second preview at game start
    public void ShowFrontInstant()
    {
        front.SetActive(true);
        back.SetActive(false);
        transform.localRotation = Quaternion.identity;
        isFlipped = true;
    }

    public void HideInstant()
    {
        front.SetActive(false);
        back.SetActive(true);
        transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        isFlipped = false;
    }
}