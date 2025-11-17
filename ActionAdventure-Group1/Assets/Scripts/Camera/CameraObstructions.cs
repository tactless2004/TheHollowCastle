/************************************************************
* COPYRIGHT:  2025
* PROJECT: The Hollow Castle
* FILE NAME: CameraObstructions.cs
* DESCRIPTION: Lowers the transparency of objects between the player and camera.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/16 | Leyton McKinney | Init
*
************************************************************/

using UnityEngine;
using System.Collections.Generic; // List 


public class CameraObstructions : MonoBehaviour
{
    [Header("THIS CODE SHOULD NOT BE USED IN PRODUCTION, IT DOES NOT BEHAVE AS EXPECTED.")]
    [SerializeField] private Transform target;
    [SerializeField] private float fadeSpeed = 4.0f;

    private List<Renderer> currentObstructions = new List<Renderer>();

    private void Update()
    {
        HandleObstructions();
    }

    private void HandleObstructions()
    {
        foreach (Renderer r in currentObstructions)
            FadeToOpaque(r);

        currentObstructions.Clear();

        Vector3 direction = target.position - transform.position;
        Ray ray = new Ray(transform.position, direction);

        if(Physics.Raycast(ray, out RaycastHit hit, direction.magnitude))
        {
            if (hit.collider.TryGetComponent(out Renderer r))
            {
                currentObstructions.Add(r);
                FadeToTransparent(r);
            }
        }
    }

    // Fade methods heavily inspired by: https://stackoverflow.com/questions/69003736/unity-material-color-a-code-is-not-working
    void FadeToTransparent(Renderer r)
    {
        foreach (Material mat in r.materials)
        {
            mat.SetFloat("_Mode", 2); // Fade mode
            mat.SetOverrideTag("RenderType", "Transparent");

            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);

            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.renderQueue = 3000;

            Color c = mat.color;
            c.a = Mathf.Lerp(c.a, 0.0f, Time.deltaTime * fadeSpeed);
            mat.color = c;
        }
    }

    void FadeToOpaque(Renderer r)
    {
        foreach (Material mat in r.materials)
        {
            Color c = mat.color;
            c.a = Mathf.Lerp(c.a, 1f, Time.deltaTime * fadeSpeed);
            mat.color = c;

            if (c.a > 0.98f)
            {
                // Return to opaque mode
                mat.SetFloat("_Mode", 0);
                mat.SetOverrideTag("RenderType", "Opaque");

                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);

                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.EnableKeyword("_ALPHATEST_ON");
                mat.renderQueue = -1;
            }
        }
    }
}
