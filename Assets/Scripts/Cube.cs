using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Clicker))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private MeshRenderer _renderer;
    private Clicker _clicker;
    private Rigidbody _rigidbody;

    public event Action<Cube> Removed;

    public float SeparationChance { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();

        _clicker = GetComponent<Clicker>();

        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _clicker.Clicked += OnClicked;
    }

    private void OnDisable()
    {
        _clicker.Clicked -= OnClicked;
    }

    private List<Rigidbody> GetExplodableObjects(float exsplosionRadius)
    {
        Collider [] hits = Physics.OverlapSphere(transform.position, exsplosionRadius);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        return cubes;
    }

    public void Explode(float explosionForce, float exsplosionRadius)
    {
        
        foreach (Rigidbody exploDableObject in GetExplodableObjects(exsplosionRadius))
        {
            exploDableObject.AddExplosionForce(explosionForce, transform.position, exsplosionRadius);
        }
    }

    public void AddForse(float explosionForce, float exsplosionRadius) 
    {
        _rigidbody.AddExplosionForce(explosionForce, transform.position, exsplosionRadius);
    }
    public void Init(float chanse, Color color)
    {
        SeparationChance = chanse;

        _renderer.material.color = color;
    }

    private void OnClicked()
    {
        Removed?.Invoke(this);

        Destroy(gameObject);
    }
}