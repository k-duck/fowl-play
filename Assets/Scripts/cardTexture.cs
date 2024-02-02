using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardTexture : MonoBehaviour
{
    int CARD_INDEX = 6;

    public Vector2 KeyCard;

    int TextureWidth = 1024;
    int TextureHeight = 1024;

    int codeVal;
    List<int> codeValPrev = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        CARD_INDEX = 6;
        int CHILD_INDEX = 2;

        //identify the card that contains the correct symbol(s)
        KeyCard.x = Random.Range(0, CARD_INDEX);

        //Set identified card to correct
        gameObject.transform.GetChild((int)KeyCard.x).GetComponent<CardScript>().isCorrectCard = true;
        //Set the Sibling Index
        gameObject.transform.GetChild((int)KeyCard.x).SetSiblingIndex(0);

        //Output the Sibling Index to the console
        //Debug.Log("Sibling Index : " + gameObject.transform.GetChild((int)KeyCard.x).GetSiblingIndex());

        //floor 1 only needs the one child; update to detect scene if needed in future.
        CHILD_INDEX = 1;
        //gameObject.transform.GetChild(CHILD_INDEX).GetComponent<MeshRenderer>();

        for (int i = 0; i < CARD_INDEX; i++)
        {
            Mesh mesh = gameObject.transform.GetChild(i).GetChild(CHILD_INDEX).GetComponent<MeshFilter>().mesh;
            //Debug.Log(gameObject.transform.GetChild(i).GetChild(CHILD_INDEX).name.ToString());

            Vector3[] vertices = mesh.vertices;
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = mesh.triangles;

            //Debug.Log("Before: " + "0: " + uv[0] + ", 1: " + uv[1] + ", 2: " + uv[2] + ", 3: " + uv[3]);

            int CodeStartX = (TextureWidth / 20);
            int CodeStartY = 430;
            int CodeWidth = (TextureWidth / 10);
            int CodeHeight = 164;

            Vector2 Coords = RandomCodeCoords(CodeStartX, CodeStartY);

            if (i == 0)
            {
                KeyCard.y = codeVal;
                //Debug.Log("Y = " + KeyCard.y);
            }

            //moving the uvs
            uv[0] = ConvertPixelsToUVCoordinates((int)Coords.x, (int)Coords.y + CodeHeight, TextureWidth, TextureHeight);
            uv[1] = ConvertPixelsToUVCoordinates((int)Coords.x + CodeWidth, (int)Coords.y + CodeHeight, TextureWidth, TextureHeight);
            uv[2] = ConvertPixelsToUVCoordinates((int)Coords.x, (int)Coords.y, TextureWidth, TextureHeight);
            uv[3] = ConvertPixelsToUVCoordinates((int)Coords.x + CodeWidth, (int)Coords.y, TextureWidth, TextureHeight);

            //Debug.Log("After: " + "0: " + uv[0] + ", 1: " + uv[1] + ", 2: " + uv[2] + ", 3: " + uv[3]);

            //applying the defined attributes to the mesh
            mesh.uv = uv;
            gameObject.transform.GetChild(i).GetChild(CHILD_INDEX).GetComponent<MeshFilter>().mesh = mesh;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 RandomCodeCoords(int x, int y)
    {
        //old code (just in case? idk)
        /*
        while (codeVal == codeValPrev || codeVal == KeyCard.y)
        {
            //takes input coordinates and picks a random column and row relative to them
            int xRand = Random.Range(0, 8);
            int yRand = Random.Range(0, 1);

            x = x + (xRand * (TextureWidth / 10));
            y = y - (yRand * (TextureHeight / 10));

            //logs the number of the random symbol
            codeVal = (xRand + 1) + (9 * yRand);
        }

        codeValPrev = codeVal;
        return new Vector2(x, y);
        */

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

            //Debug.Log("xRand: " + xRand + ", yRand: " + yRand + ", x: " + x + ", y: " + y);

            //logs the number of the random symbol
            codeVal = (xRand + 1) + (9 * yRand);

            if (codeValPrev.Contains(codeVal) != true)
            {
                codeValPrev.Add(codeVal);
                loop = false;
            }
        }

        //output the list to console
        /*
        for (int i = 0; i < codeValPrev.Count; i++)
        {
            if (codeValPrev[i] == codeVal)
                Debug.Log("Current Num " + i + ": " + codeVal);
            Debug.Log("List Num " + i + ": " + codeValPrev[i].ToString());
        }
        */

        return new Vector2(xOut, yOut);
    }

    public Vector2 ConvertPixelsToUVCoordinates(int x, int y, int textureWidth, int textureHeight)
    {
        return new Vector2((float)x / textureWidth, (float)y / textureHeight);
    }
}