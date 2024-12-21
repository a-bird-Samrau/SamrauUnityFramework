using UnityEngine;
using Behaviour = Core.Behaviour;

[RequireComponent(typeof(BoxCollider))]
public class Trigger : Behaviour
{
    private Collider _collider;

    protected override void Start()
    {
        base.Start();

        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }
}