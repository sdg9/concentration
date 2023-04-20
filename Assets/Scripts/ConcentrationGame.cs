using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConcentrationGame : MonoBehaviour
{
    public GameObject cardPrefab; // The card prefab to be instantiated

    public int numCards = 20;

    // Array of sprites for the cards
    public Sprite[] cardFaces;

    public float cardScale = .95f;

    // private Transform gameBoard = this;

    private List<GameObject> cards = new List<GameObject>(); // List of all cards in the game
    private GameObject[] selectedCards = new GameObject[2]; // Array to hold the currently selected cards
    private int numPairsFound = 0; // Number of pairs of cards that have been found

    // Use this for initialization
    void Start()
    {
        GenerateCards();
    }

    // Update is called once per frame
    void Update()
    {
        // // Check for mouse click
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Debug.Log("Mouse Clicked");
        //     // Raycast to see if a card was clicked
        //     RaycastHit hit;
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         GameObject card = hit.collider.gameObject;

        //         Debug.Log("Hit " + card.name);
        //         // Check if the card is already face up or if two cards are already selected
        //         if (!card.GetComponent<Card>().isFaceUp && selectedCards[1] == null)
        //         {
        //             // Flip the card face up
        //             card.GetComponent<Card>().Flip();

        //             // Add the card to the selected cards array
        //             if (selectedCards[0] == null)
        //             {
        //                 selectedCards[0] = card;
        //             }
        //             else
        //             {
        //                 selectedCards[1] = card;

        //                 // Check if the two selected cards match
        //                 if (selectedCards[0].GetComponent<Card>().cardType == selectedCards[1].GetComponent<Card>().cardType)
        //                 {
        //                     // The cards match, so remove them from the game
        //                     Destroy(selectedCards[0]);
        //                     Destroy(selectedCards[1]);
        //                     numPairsFound++;

        //                     // Check if all pairs have been found
        //                     if (numPairsFound == (numRows * numCols) / 2)
        //                     {
        //                         Debug.Log("Game Over!");
        //                     }
        //                 }
        //                 else
        //                 {
        //                     // The cards do not match, so flip them face down
        //                     StartCoroutine(FlipCardsFaceDown());
        //                 }

        //                 // Clear the selected cards array
        //                 selectedCards[0] = null;
        //                 selectedCards[1] = null;
        //             }
        //         }
        //     }
        // }
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
        // Delete any existing cards
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        ShuffleCardTypes();


        // clone array of cardFaces
        Sprite[] cardFacesClone = cardFaces.Clone() as Sprite[];
        // randomize order of cardFacesClone
        for (int j = 0; j < cardFacesClone.Length; j++)
        {
            Sprite temp = cardFacesClone[j];
            int randomIndex = UnityEngine.Random.Range(j, cardFacesClone.Length);
            cardFacesClone[j] = cardFacesClone[randomIndex];
            cardFacesClone[randomIndex] = temp;
        }


        for (int i = 0; i < numCards; i++)
        {
            // Instantiate a new card game object keeping scale
            GameObject cardGO = Instantiate(cardPrefab, this.transform);
            // cardGO.transform.localScale = new Vector3(cardScale, cardScale, 1f);

            // Set the card's cardType
            Card card = cardGO.GetComponent<Card>();

            card.cardType = cardTypes[i];
            if (cardTypes[i] < cardFaces.Length)
            {
                card.cardFront = cardFacesClone[cardTypes[i]];
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
        Debug.Log("Card types: " + cardTypesString);

    }

    // Coroutine to flip the selected cards face down after a delay
    IEnumerator FlipCardsFaceDown()
    {
        yield return new WaitForSeconds(1);

        foreach (GameObject card in selectedCards)
        {
            if (card != null)
            {
                // card.GetComponent<Card>().Flip();
            }
        }
    }
}
