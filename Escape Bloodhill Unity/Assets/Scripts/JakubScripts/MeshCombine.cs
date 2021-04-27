using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombine : MonoBehaviour
{
    void Start()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter mesh in meshFilters)
        {
            Debug.Log(mesh);
        }
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;

        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            //combine[i].transform = transform.localToWorldMatrix * meshFilters[i].transform.localToWorldMatrix;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        gameObject.AddComponent<MeshFilter>();
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine, false, true);
        transform.gameObject.SetActive(true);

        transform.position = position;
        transform.rotation = rotation;
    }
}
