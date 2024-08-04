using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpriteGlowEffect : MonoBehaviour
{
    private Image image;
    private Material originalMaterial;
    public Color glowColor = Color.yellow;
    public float glowIntensity = 2.0f;

    private Material glowMaterial;

    void Start()
    {
        image = GetComponent<Image>();
        originalMaterial = image.material;

        // Create a new material with the glow shader
        glowMaterial = new Material(Shader.Find("Custom/GlowShader"));
        glowMaterial.SetColor("_GlowColor", glowColor);
        glowMaterial.SetFloat("_GlowIntensity", glowIntensity);
    }

    public void EnableGlow()
    {
        if (glowMaterial != null)
        {
            image.material = glowMaterial;
        }
    }

    public void DisableGlow()
    {
        if (originalMaterial != null)
        {
            image.material = originalMaterial;
        }
    }
}
