using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public float fadeSpeed = 10.0f;
    private Material material;
    private float opacity;
    private bool isFadingIn;
    // Start is called before the first frame update
    void Start()
    {
        isFadingIn = true;
        material = GetComponent<Renderer>().material;
        opacity = material.color.a;
        material.color = new Color(material.color.r, material.color.g, material.color.b, 0);
        Debug.Log("opa" + opacity);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingIn)
        {
            FadeInObject();
        }
    }

    void FadeInObject()
    {
        Debug.Log(material.color.a + "apo2" + opacity);
        Color smoothColor = new (
            material.color.r,
            material.color.g,
            material.color.b,
            Mathf.Lerp(material.color.a, opacity, fadeSpeed * Time.deltaTime)
            );
        material.color = smoothColor;
        if (material.color.a == 1)
        {
            isFadingIn = false;
        }
    }
}
