using System;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    // The type of the card (determines the artwork on the card)
    public int cardType;

    // Whether the card is currently face up or face down
    public bool isFaceUp = false;

    // The sprite renderer component of the card
    private SpriteRenderer spriteRenderer;
    private Image image;

    // Source image
    public Sprite cardBack;
    public Sprite cardFront;

    // The sprite for the card's face-up state (set in the Inspector)
    public Sprite faceUpSprite;

    // The sprite for the card's face-down state (set in the Inspector)
    public Sprite faceDownSprite;

    public event Action<Card> CardClicked;


    // Initialization
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    void Start()
    {
        // _image = GetComponent<Image>();
        // _image.sprite = cardBack;
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnCardClick);
    }

    void OnCardClick()
    {
        // if (!_isFlipped)
        // {
        //     _isFlipped = true;
        //     _image.sprite = cardFace;
        //     // Handle card flip logic here
        // }
        Debug.Log("Card Clicked");
        // isFaceUp = true;
        Flip();
    }

    // Flip the card face up or face down
    public void Flip()
    {
        if (isFaceUp)
        {
            // Face down the card
            // spriteRenderer.sprite = faceDownSprite;
            // set the color to red
            // spriteRenderer.color = Color.red;
            // set the image color to red
            image.color = Color.green;
            image.sprite = cardBack;
            isFaceUp = false;
        }
        else
        {
            // Face up the card
            // spriteRenderer.sprite = faceUpSprite;
            // set the image color to red
            // image.color = Color.red;
            // image.color = Color.clear;

            // remove color
            image.color = Color.white;

            image.sprite = cardFront;
            isFaceUp = true;
            CardClicked?.Invoke(this);
        }
    }

    // Check if the card matches another card
    public bool Matches(Card otherCard)
    {
        return cardType == otherCard.cardType;
    }
}
