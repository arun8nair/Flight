using System.Collections;
using UnityEngine;

public class ApplyMaterial : MonoBehaviour
{
    public Material[] materials;
    void Start()
    {
        int materialType = Random.Range(0, materials.Length - 1);
        GetComponent<Renderer>().material = materials[materialType];
    }
}
