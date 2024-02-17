using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Content;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;



public class TeleportNavMesh : MonoBehaviour
{
    [SerializeField] private Material VisualMat;
    [SerializeField] Vector3 generatedmeshOffset = new(0,0.05f,0);

    private GameObject MeshVis;
    // Start is called before the first frame update
    void Start()
    {
        MeshVis = new("NavMesh Visual");
        MeshRenderer renderer = MeshVis.AddComponent<MeshRenderer>();
        
        MeshFilter filter = MeshVis.AddComponent<MeshFilter>();

        Mesh navMesh = new Mesh();


        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
        navMesh.SetVertices(triangulation.vertices);
        navMesh.SetIndices(triangulation.indices, MeshTopology.Triangles, 0);

        renderer.sharedMaterial = VisualMat;
        filter.mesh = navMesh;
        MeshVis.AddComponent<MeshCollider>();
        MeshVis.AddComponent<TeleportationArea>();
        MeshVis.GetComponent<TeleportationArea>().interactionLayers = InteractionLayerMask.GetMask("Teleport");
    }

    // Update is called once per frame
    void Update()
    {
        MeshVis.SetActive(true);
        MeshVis.transform.position = generatedmeshOffset;
    }
}
