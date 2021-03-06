﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drunkeness : MonoBehaviour
{
    public Material material;

    private float offsetScale;
    private float warpScale;
    private float drugScale;

    public void SetVariables(float offsetScale, float warpScale)
    {
        this.offsetScale = offsetScale;
        this.warpScale = warpScale;
    }

    public void SetDrugScale(float drugScale)
    {
        this.drugScale = drugScale;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_OffsetScale", offsetScale);
        material.SetFloat("_WarpScale", warpScale);
        material.SetFloat("_DrugScale", drugScale);
        Graphics.Blit(source, destination, material);
    }
}

