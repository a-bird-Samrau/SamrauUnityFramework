using Player;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace UI
{
    public class HeadUpDisplay : Behaviour
    {
        [SerializeField] private GameObject _crosshair;

        private IPlayableCharacter _playableCharacter;
        
        public void Construct(IPlayableCharacter playableCharacter)
        {
            playableCharacter.Pointer.Pointing += OnPointing;
            playableCharacter.Pointer.UnPointed += OnUnPointed;
            
            _playableCharacter = playableCharacter;
        }

        private void OnPointing(GameObject target)
        {
            _crosshair.Activate();
        }
        
        private void OnUnPointed()
        {
            _crosshair.Deactivate();
        }

        public void Clear()
        {
            _playableCharacter.Pointer.Pointing -= OnPointing;
            _playableCharacter.Pointer.UnPointed -= OnUnPointed;

            _playableCharacter = null;
        }
    }
}