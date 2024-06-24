using UnityEngine;
using System;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Clicker))]

public class Cube : MonoBehaviour
{
    public event Action<Cube> Removed;

    private MeshRenderer _renderer;

    private Clicker _clicker;

    public float SeparationChance { get; private set; } 

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();

        _clicker = GetComponent<Clicker>();
    }

    private void OnEnable()
    {
        _clicker.Clicked += OnClicked;
    }

    private void OnDisable()
    {
        _clicker.Clicked -= OnClicked;
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void SetChance(float chance) 
    {
        SeparationChance = chance;
    }

    private void OnClicked()
    {
        Removed?.Invoke(this);

        Destroy(gameObject);
    }
}