using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    // The type of the card (determines the artwork on the card)
    public int cardType;

    // Whether the card is currently face up or face down
    public bool isFaceUp = false;

    // The sprite renderer component of the card
    // private SpriteRenderer spriteRenderer;
    // private Image image;

    private Image frontImage;
    private Image backImage;

    // Source image
    // public Sprite cardBack;
    // public Sprite cardFront;

    public Transform cardBackTransform;
    public Transform cardFrontTransform;

    // The sprite for the card's face-up state (set in the Inspector)
    public Sprite cardFront;

    // The sprite for the card's face-down state (set in the Inspector)
    public Sprite cardBack;

    public event Action<Card> CardClicked;


    // Initialization
    private void Awake()
    {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        // image = GetComponent<Image>();

        // find image component in cardBackTransform
        backImage = cardBackTransform.GetComponent<Image>();
        frontImage = cardFrontTransform.GetComponent<Image>();
    }

    void Start()
    {
        // _image = GetComponent<Image>();
        // _image.sprite = cardBack;
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnCardClick);

        frontImage.sprite = cardFront;
        frontImage.color = Color.white;
        backImage.sprite = cardBack;
        // backImage.color = Color.white;
        // Button cardBackButton = cardBackTransform.GetComponent<Button>();
        // cardBackButton.onClick.AddListener(OnCardBackClick);

        // Button cardFrontButton = cardFrontTransform.GetComponent<Button>();
        // cardFrontButton.onClick.AddListener(OnCardFrontClick);
    }

    // card back click
    // private void OnCardBackClick()
    // {
    //     Debug.Log("Card Back Clicked");
    //     Flip();
    // }

    // // card front click
    // private void OnCardFrontClick()
    // {
    //     Debug.Log("Card Front Clicked");
    //     Flip();
    // }


    void OnCardClick()
    {
        // if (!_isFlipped)
        // {
        //     _isFlipped = true;
        //     _image.sprite = cardFace;
        //     // Handle card flip logic here
        // }
        Debug.Log("Card Clicked");
        Flip();
    }

    // Flip the card face up or face down
    public void Flip()
    {
        // transform.DORotate(new Vector3(0, transform.eulerAngles.y + 180, 0), 0.5f, RotateMode.FastBeyond360);
        transform.DORotate(new Vector3(0, transform.eulerAngles.y + 90, 0), 0.25f, RotateMode.FastBeyond360).OnComplete(() =>
        {
            if (isFaceUp)
            {
                // layer frontImage above backImage
                // frontImage.transform.SetAsLastSibling();

                // hide frontImage
                backImage.gameObject.SetActive(true);
                frontImage.gameObject.SetActive(false);
            }
            else
            {
                // backImage.transform.SetAsLastSibling();
                backImage.gameObject.SetActive(false);
                frontImage.gameObject.SetActive(true);
            }
            isFaceUp = !isFaceUp;

            transform.DORotate(new Vector3(0, transform.eulerAngles.y + 90, 0), 0.25f, RotateMode.FastBeyond360);
        });
        // Debug.Log("Is face up: " + isFaceUp);
        // if (isFaceUp)
        // {
        //     // layer frontImage above backImage
        //     frontImage.transform.SetAsLastSibling();

        //     // Face down the card
        //     // spriteRenderer.sprite = faceDownSprite;
        //     // set the color to red
        //     // spriteRenderer.color = Color.red;
        //     // set the image color to red
        //     // image.color = Color.green;
        //     // image.sprite = cardBack;

        //     // flip the card
        //     // transform.DOFlip().SetEase(Ease.InOutBack);

        //     // flip the card using DOTween animation

        //     // transform.DOFlip()
        //     // isFaceUp = false;
        // }
        // else
        // {
        //     backImage.transform.SetAsLastSibling();
        //     // Face up the card
        //     // spriteRenderer.sprite = faceUpSprite;
        //     // set the image color to red
        //     // image.color = Color.red;
        //     // image.color = Color.clear;

        //     // remove color
        //     // image.color = Color.white;

        //     // image.sprite = cardFront;
        //     // isFaceUp = true;
        //     // transform.DORotate(new Vector3(0, 180, 0), 0.5f, RotateMode.FastBeyond360);
        //     // CardClicked?.Invoke(this);
        // }
        // transform.DORotate(new Vector3(0, transform.eulerAngles.y + 90, 0), 0.25f, RotateMode.FastBeyond360);
        // isFaceUp = !isFaceUp;
    }

    // Check if the card matches another card
    public bool Matches(Card otherCard)
    {
        return cardType == otherCard.cardType;
    }
}
