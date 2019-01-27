using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCoil : MonoBehaviour
{
    public Gradient heatGradient;

    private bool on = false;
    private Material material;
    private float heatPercent = 0;
    public float heatSpeed = 0.1f;
    private Renderer renderer;
    public ParticleSystem particle;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = new Material(renderer.material);
        renderer.material.color = heatGradient.Evaluate(0);
    }

    private void Update()
    {
        if (on)
            heatPercent = Mathf.Clamp(heatPercent + Time.deltaTime * heatSpeed, 0, 1.5f);
        else
            heatPercent = Mathf.Clamp(heatPercent - Time.deltaTime * heatSpeed, 0, 1.5f);
        
        renderer.material.color = heatGradient.Evaluate(Mathf.Clamp(heatPercent, 0, 1));
    }

    public void TurnOn()
    {
        on = true;
        particle.Play();
    }

    public void TurnOff()
    {
        on = false;
        particle.Stop();
    }
}
