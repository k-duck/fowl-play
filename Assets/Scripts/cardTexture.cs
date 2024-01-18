using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardTexture : MonoBehaviour
{
    int codeVal;

    int TextureWidth = 1024;
    int TextureHeight = 1024;

    // Start is called before the first frame update
    void Start()
    {
        int CHILD_INDEX = 2;

        //floor 1 only needs the one child
        CHILD_INDEX = 1;
        //gameObject.transform.GetChild(CHILD_INDEX).GetComponent<MeshRenderer>();

        Mesh mesh = gameObject.transform.GetChild(CHILD_INDEX).GetComponent<MeshFilter>().mesh;
        Debug.Log(gameObject.transform.GetChild(CHILD_INDEX).name.ToString());

        Vector3[] vertices = mesh.vertices;
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = mesh.triangles;

        //Debug.Log("Before: " + "0: " + uv[0] + ", 1: " + uv[1] + ", 2: " + uv[2] + ", 3: " + uv[3]);

        int CodeStartX = (TextureWidth/20);
        int CodeStartY = 430;
        int CodeWidth = (TextureWidth/10);
        int CodeHeight = 164;

        Vector2 Coords = RandomCodeCoords(CodeStartX, CodeStartY);
        
        //moving the uvs
        uv[0] = ConvertPixelsToUVCoordinates((int)Coords.x, (int)Coords.y + CodeHeight, TextureWidth, TextureHeight);
        uv[1] = ConvertPixelsToUVCoordinates((int)Coords.x + CodeWidth, (int)Coords.y + CodeHeight, TextureWidth, TextureHeight);
        uv[2] = ConvertPixelsToUVCoordinates((int)Coords.x, (int)Coords.y, TextureWidth, TextureHeight);
        uv[3] = ConvertPixelsToUVCoordinates((int)Coords.x + CodeWidth, (int)Coords.y, TextureWidth, TextureHeight);

        //Debug.Log("After: " + "0: " + uv[0] + ", 1: " + uv[1] + ", 2: " + uv[2] + ", 3: " + uv[3]);

        //applying the defined attributes to the mesh
        mesh.uv = uv;
        gameObject.transform.GetChild(CHILD_INDEX).GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 RandomCodeCoords(int x, int y)
    {
        //takes input coordinates and picks a random column and row relative to them
        int xRand = Random.Range(0, 8);
        int yRand = Random.Range(0, 1);

        codeVal = (xRand + 1) + (9 * yRand);

        x = x + (xRand * (TextureWidth/10));
        y = y - (yRand * (TextureHeight/10));
        return new Vector2(x, y);
    }

    public Vector2 ConvertPixelsToUVCoordinates(int x, int y, int textureWidth, int textureHeight)
    {
        return new Vector2((float)x / textureWidth, (float)y / textureHeight);
    }
}
