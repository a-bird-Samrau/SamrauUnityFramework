using UnityEngine;
using Behaviour = SamrauFramework.Core.Behaviour;

namespace SamrauFramework.Player
{
    public class PlayerStart : Core.Behaviour
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