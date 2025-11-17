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
* 2025/11/17 | Leyton McKinney | Use URP materials fields.
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

        RaycastHit[] hits = Physics.RaycastAll(ray, direction.magnitude);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == target.gameObject) continue;
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
            Color c = mat.color;

            if (c.a > 0.0f && mat.GetFloat("_Mode") != 2)
            {
                SetTransparentMode(mat);
            }

            if (c.a > 0.0f)
            {
                c.a = Mathf.MoveTowards(c.a, 0.0f, Time.deltaTime * fadeSpeed);
                mat.color = c;
            }
        }
    }

    void FadeToOpaque(Renderer r)
    {
        foreach (Material mat in r.materials)
        {
            Color c = mat.color;

            c.a = Mathf.MoveTowards(c.a, 1.0f, Time.deltaTime * fadeSpeed);
            mat.color = c;

            if (c.a > 0.98f)
                SetOpaqueMode(mat);
        }
    }

    // Helper methods to make a material transparent or opaque.
    private void SetTransparentMode(Material mat)
    {
        if (mat.HasProperty("_Surface"))
        {
            mat.SetFloat("_Surface", 1);
        }

        if (mat.HasProperty("_Blend"))
        {
            mat.SetFloat("_Blend", 0);
        }

        if(mat.HasProperty("_AlphaClip"))
        {
            mat.SetFloat("_AlphaClip", 0);
        }

        // Enable alpha blending
        mat.SetOverrideTag("RenderType", "Transparent");
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

        // Enable transparency keywords for URP
        mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
    }

    private void SetOpaqueMode(Material mat)
    {
        // URP Standard Lit shader opaque
        if (mat.HasProperty("_Surface"))
        {
            mat.SetFloat("_Surface", 0); // 0 = Opaque
        }

        if (mat.HasProperty("_AlphaClip"))
        {
            mat.SetFloat("_AlphaClip", 0);
        }

        mat.SetOverrideTag("RenderType", "");
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        mat.SetInt("_ZWrite", 1);
        mat.renderQueue = 3000;

        // Disable transparency keywords
        mat.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
    }

}
