using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drunkeness : MonoBehaviour
{
    public Material material;

    private float offsetScale;
    private float warpScale;

    public void SetVariables(float offsetScale, float warpScale)
    {
        this.offsetScale = offsetScale;
        this.warpScale = warpScale;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_OffsetScale", offsetScale);
        material.SetFloat("_WarpScale", warpScale);
        Graphics.Blit(source, destination, material);
    }
}

