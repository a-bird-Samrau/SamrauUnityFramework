using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Player
{
    public class PlayerStart : Behaviour
    {
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            var cachedTransform = transform;

            Gizmos.matrix = cachedTransform.localToWorldMatrix;
            
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(0.5f, 2f, 0.5f));
            Gizmos.DrawRay(Vector3.zero, Vector3.forward);
        }
    }
}