using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BlurEffect : MonoBehaviour
{
    [SerializeField] Material effectMaterial;

    [Range(0, 5)]
    [SerializeField] int blurIterations;

    [Range(0, 5)]
    [SerializeField] int downresCount;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        int width = source.width >> downresCount;
        int height = source.height >> downresCount;

        RenderTexture tex1 = RenderTexture.GetTemporary(width, height);
        Graphics.Blit(source, tex1);

        for (int i = 0; i < blurIterations; i++)
        {
            RenderTexture tex2 = RenderTexture.GetTemporary(width, height);
            Graphics.Blit(tex1, tex2, effectMaterial);
            RenderTexture.ReleaseTemporary(tex1);

            tex1 = tex2;
        }

        Graphics.Blit(tex1, destination);
        RenderTexture.ReleaseTemporary(tex1);
    }
}