using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullScript : MonoBehaviour
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
