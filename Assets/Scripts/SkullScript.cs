using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullScript : MonoBehaviour
{
    public GameObject keyCards;
    public GameObject PCSymbols;
    public GameObject skulls;
    public GameObject chests;

    //card stuff
    int CARD_INDEX = 6;

    int TextureWidth = 1024;
    int TextureHeight = 1024;

    int codeVal;
    public List<int> codeValPrev = new List<int>();

    public List<int> incorrectCards = new List<int> { 0, 1, 2, 3, 4, 5 }; //the cards that don't work
    public List<int> RandCardOrder = new List<int> { 0, 1, 2, 3, 4, 5 }; //the cards that are assigned to a chest

    public List<int> cardSymbol = new List<int> { 100, 100, 100, 100, 100, 100 }; //stores symbol values for associated cards
    public List<GameObject> correctCards = new List<GameObject> { }; //stores card order

    //skull stuff
    public List<int> RandSkullOrder = new List<int> { 0, 1, 2, 3 };

    public bool owl = false;
    public bool parrot = false;
    public bool mallard = false;
    public bool spoonbill = false;

    //end of level stuff
    public ElevatorDoors Elevator;

    public bool levelDone = false;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeCards();
        Debug.Log("CardsRandomized");

        RandomizeSkulls();
        Debug.Log("SkullsRandomized");
    }

    // Update is called once per frame
    void Update()
    {
        if (PCSymbols.gameObject.activeInHierarchy)
        {
            //activate email symbols
            for (int i = 0; i < 4; i++)
            {
                PCSymbols.transform.GetChild(i).GetChild(cardSymbol[i]-1).gameObject.SetActive(true);
            }
        }

        if (owl == true && parrot == true && mallard == true && spoonbill == true && levelDone == false)
        {
            openElevator();
        }
    }

    public void RandomizeCards()
    {
        for (int i = 0; i < 4; i++)
        {
            incorrectCards.RemoveAt(Random.Range(0, incorrectCards.Count));
        }
        for (int i = 0; i < incorrectCards.Count; i++)
        {
            RandCardOrder.Remove(incorrectCards[i]);
        }

        CARD_INDEX = 6;
        int CHILD_INDEX = 2;

        CHILD_INDEX = 1;

        for (int i = 0; i < CARD_INDEX; i++)
        {
            Debug.Log("i: " + i);
            Mesh mesh = keyCards.gameObject.transform.GetChild(i).GetChild(CHILD_INDEX).GetComponent<MeshFilter>().mesh;

            Vector3[] vertices = mesh.vertices;
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = mesh.triangles;

            int CodeStartX = (TextureWidth / 20);
            int CodeStartY = 430;
            int CodeWidth = (TextureWidth / 10);
            int CodeHeight = 164;

            Vector2 Coords = RandomCodeCoords(CodeStartX, CodeStartY);

            //moving the uvs
            uv[0] = ConvertPixelsToUVCoordinates((int)Coords.x, (int)Coords.y + CodeHeight, TextureWidth, TextureHeight);
            uv[1] = ConvertPixelsToUVCoordinates((int)Coords.x + CodeWidth, (int)Coords.y + CodeHeight, TextureWidth, TextureHeight);
            uv[2] = ConvertPixelsToUVCoordinates((int)Coords.x, (int)Coords.y, TextureWidth, TextureHeight);
            uv[3] = ConvertPixelsToUVCoordinates((int)Coords.x + CodeWidth, (int)Coords.y, TextureWidth, TextureHeight);

            //applying the defined attributes to the mesh
            mesh.uv = uv;
            keyCards.gameObject.transform.GetChild(i).GetChild(CHILD_INDEX).GetComponent<MeshFilter>().mesh = mesh;

            if (incorrectCards.Contains(i) == false)
            {
                //pick random order for chest card locations
                int randLoc = Random.Range(0, RandCardOrder.Count);

                //changes the card name to be identifiable by the chests
                string cardName = "chestCard_" + i;
                keyCards.transform.GetChild(RandCardOrder[randLoc]).name = cardName;
                correctCards.Add(keyCards.transform.GetChild(RandCardOrder[randLoc]).gameObject);

                //stores code value for card
                //cardSymbol[RandCardOrder[randLoc]] = codeVal;

                //removes chest from location pool
                RandCardOrder.RemoveAt(randLoc);
            }
        }

        //iterates through the values
        for (int i = 0; i < codeValPrev.Count; i++)
        {
            //iterates through each child
            for (int j = 0; j < codeValPrev.Count; j++)
            {
                string cardVal = keyCards.transform.GetChild(j).name;

                if (cardVal == "chestCard_" + i)
                {
                    cardSymbol.Add(codeValPrev[keyCards.transform.GetChild(j).GetSiblingIndex()]);
                }
            }
        }

        //clears the placeholder values
        cardSymbol.RemoveAll(clearSymbolList);
    }

    bool clearSymbolList(int i)
    {
        if (i > 18)
        {
            return true;
        }
        else
            return false;
    }

    public void RandomizeSkulls()
    {
        for (int i = 0; i < 4; i++)
        {
            chests.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<ChestInteraction>().chestContents = skulls.transform.GetChild(0).GetChild(i).gameObject.name;
        }

        for (int i = 0; i < 4; i++)
        {
            int randSkull = Random.Range(0, RandSkullOrder.Count);

            skulls.gameObject.transform.GetChild(i).GetChild(RandSkullOrder[randSkull]).gameObject.SetActive(true);

            RandSkullOrder.RemoveAt(randSkull);
        }
    }

    public Vector2 RandomCodeCoords(int x, int y)
    {
        bool loop = true;

        int xOut = x;
        int yOut = y;

        while (loop == true)
        {
            //takes input coordinates and picks a random column and row relative to them
            int xRand = Random.Range(0, 8);
            int yRand = Random.Range(0, 7);
            yRand = (yRand % 2);

            xOut = x + (xRand * (TextureWidth / 10));
            yOut = y - (yRand * ((TextureHeight / 10) * 2));

            //logs the number of the random symbol
            codeVal = (xRand + 1) + (9 * yRand);

            if (codeValPrev.Contains(codeVal) != true)
            {
                Debug.Log("codeVal: " + codeVal);
                codeValPrev.Add(codeVal);
                loop = false;
            }
        }
        return new Vector2(xOut, yOut);
    }

    public Vector2 ConvertPixelsToUVCoordinates(int x, int y, int textureWidth, int textureHeight)
    {
        return new Vector2((float)x / textureWidth, (float)y / textureHeight);
    }

    void openElevator()
    {
        Debug.Log("openElevator");
        Elevator.TriggerDoors();
        //ElevatorDoorL.SetBool("OpenDoor", true);
        //ElevatorDoorR.SetBool("OpenDoor", true);
        levelDone = true;
    }
}
