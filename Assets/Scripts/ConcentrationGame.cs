using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConcentrationGame : MonoBehaviour
{
    public GameObject cardPrefab; // The card prefab to be instantiated
    public int numRows = 4; // Number of rows of cards
    public int numCols = 6; // Number of columns of cards

    // Array of sprites for the cards
    public Sprite[] cardFaces;

    public float xOffset = 0f;
    public float yOffset = 0f;

    // private Transform gameBoard = this;

    private List<GameObject> cards = new List<GameObject>(); // List of all cards in the game
    private GameObject[] selectedCards = new GameObject[2]; // Array to hold the currently selected cards
    private int numPairsFound = 0; // Number of pairs of cards that have been found

    // Use this for initialization
    void Start()
    {
        // Create the cards
        // for (int row = 0; row < numRows; row++)
        // {
        //     for (int col = 0; col < numCols; col++)
        //     {
        //         GameObject card = Instantiate(cardPrefab, new Vector3(col * 2, 0, row * 2), Quaternion.identity);
        //         card.transform.parent = this.transform;
        //         cards.Add(card);
        //     }
        // }

        // // Shuffle the cards
        // Shuffle(cards);
        GenerateCards();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Clicked");
            // Raycast to see if a card was clicked
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject card = hit.collider.gameObject;

                Debug.Log("Hit " + card.name);
                // Check if the card is already face up or if two cards are already selected
                if (!card.GetComponent<Card>().isFaceUp && selectedCards[1] == null)
                {
                    // Flip the card face up
                    card.GetComponent<Card>().Flip();

                    // Add the card to the selected cards array
                    if (selectedCards[0] == null)
                    {
                        selectedCards[0] = card;
                    }
                    else
                    {
                        selectedCards[1] = card;

                        // Check if the two selected cards match
                        if (selectedCards[0].GetComponent<Card>().cardType == selectedCards[1].GetComponent<Card>().cardType)
                        {
                            // The cards match, so remove them from the game
                            Destroy(selectedCards[0]);
                            Destroy(selectedCards[1]);
                            numPairsFound++;

                            // Check if all pairs have been found
                            if (numPairsFound == (numRows * numCols) / 2)
                            {
                                Debug.Log("Game Over!");
                            }
                        }
                        else
                        {
                            // The cards do not match, so flip them face down
                            StartCoroutine(FlipCardsFaceDown());
                        }

                        // Clear the selected cards array
                        selectedCards[0] = null;
                        selectedCards[1] = null;
                    }
                }
            }
        }
    }

    // Shuffle a list of game objects
    void Shuffle(List<GameObject> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            GameObject card = list[k];
            list[k] = list[n];
            list[n] = card;
        }

        // Set the card types based on the shuffled order
        for (int i = 0; i < list.Count; i += 2)
        {
            list[i].GetComponent<Card>().cardType = i / 2;
            list[i + 1].GetComponent<Card>().cardType = i / 2;
        }
    }

    public void GenerateCards()
    {
        int numCards = numRows * numCols;

        // Shuffle the card types
        ShuffleCardTypes();

        for (int i = 0; i < numCards; i++)
        {
            // Calculate the row and column of this card
            int row = i / numCols;
            int col = i % numCols;

            // Instantiate a new card game object
            GameObject cardGO = Instantiate(cardPrefab);

            // Set the card's parent to the card container
            cardGO.transform.SetParent(this.transform);

            // Position the card based on its row and column
            float x = (col - (numCols - 1) * 0.5f) * xOffset;
            float y = ((numRows - 1) * 0.5f - row) * yOffset;
            cardGO.transform.localPosition = new Vector3(x, y, 0f);

            // Set the card's cardType
            Card card = cardGO.GetComponent<Card>();
            card.cardType = cardTypes[i];
            if (cardTypes[i] < cardFaces.Length)
            {
                card.cardFront = cardFaces[cardTypes[i]];
            }

            // Subscribe to the CardClicked event
            card.CardClicked += OnCardClicked;
        }
    }

    private void OnCardClicked(Card clickedCard)
    {
        // Handle card flip logic and game logic here
        Debug.Log("Card Clicked from CG: " + clickedCard.cardType);
    }

    // Declare the cardTypes array
    int[] cardTypes;


    void ShuffleCardTypes()
    {
        int numCards = numRows * numCols;
        // Create a list of card types
        List<int> cardTypeList = new List<int>();

        // Add each card type to the list twice
        for (int i = 0; i < numCards / 2; i++)
        {
            cardTypeList.Add(i);
            cardTypeList.Add(i);
        }

        // Shuffle the list of card types
        for (int i = 0; i < cardTypeList.Count; i++)
        {
            int temp = cardTypeList[i];
            int randomIndex = UnityEngine.Random.Range(i, cardTypeList.Count);
            cardTypeList[i] = cardTypeList[randomIndex];
            cardTypeList[randomIndex] = temp;
        }

        // Copy the shuffled card types into the cardTypes array
        cardTypes = cardTypeList.ToArray();

        // Log the card types
        string cardTypesString = "";
        for (int i = 0; i < cardTypes.Length; i++)
        {
            cardTypesString += cardTypes[i] + " ";
        }
        Debug.Log(cardTypesString);

    }

    // void ShuffleCardTypes()
    // {
    //     int numCards = numRows * numCols;

    //     // Initialize a temporary array to hold the card types
    //     // int[] tempCardTypes = new int[numCards / 2];
    //     List<int> tempCardTypes = new List<int>();
    //     for (int i = 0; i < cardTypes.Length; i++)
    //     {
    //         tempCardTypes[i] = cardTypes[i];
    //     }

    //     // Shuffle the card types using the Fisher-Yates shuffle algorithm
    //     for (int i = 0; i < tempCardTypes.Count - 1; i++)
    //     {
    //         int j = Random.Range(i, tempCardTypes.Count);
    //         int temp = tempCardTypes[i];
    //         tempCardTypes[i] = tempCardTypes[j];
    //         tempCardTypes[j] = temp;
    //     }

    //     // Copy the shuffled card types back to the original array
    //     // for (int i = 0; i < cardTypes.Length; i++)
    //     // {
    //     //     cardTypes[i] = tempCardTypes[i];
    //     // }

    //     cardTypes = tempCardTypes.ToArray();
    // }





    // void Generate()
    // {
    //     // Generate the pairs of cards
    //     for (int i = 0; i < cardCount / 2; i++)
    //     {
    //         // Choose a random card type that hasn't been used yet
    //         int cardType = Random.Range(0, cardTypes.Count);
    //         while (cardCounts[cardType] >= 2)
    //         {
    //             cardType = Random.Range(0, cardTypes.Count);
    //         }

    //         // Create the first card
    //         GameObject card1 = Instantiate(cardPrefab, new Vector3(x, y, 0), Quaternion.identity);
    //         card1.GetComponent<Card>().cardType = cardType;

    //         // Create the second card
    //         GameObject card2 = Instantiate(cardPrefab, new Vector3(x, y - yOffset, 0), Quaternion.identity);
    //         card2.GetComponent<Card>().cardType = cardType;

    //         // Add the cards to the list of all cards
    //         allCards.Add(card1);
    //         allCards.Add(card2);

    //         // Update the card counts
    //         cardCounts[cardType] += 2;

    //         // Increment the x position
    //         x += xOffset;

    //         // Reset the x position and increment the y position after every row of cards
    //         if (i % cardsPerRow == cardsPerRow - 1)
    //         {
    //             x = startX;
    //             y -= yOffset;
    //         }
    //     }

    // }



    // Coroutine to flip the selected cards face down after a delay
    IEnumerator FlipCardsFaceDown()
    {
        yield return new WaitForSeconds(1);

        foreach (GameObject card in selectedCards)
        {
            if (card != null)
            {
                card.GetComponent<Card>().Flip();
            }
        }
    }
}
