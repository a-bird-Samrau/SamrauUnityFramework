using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Placeables
{
    public class ItemReceiverIndicator : Behaviour
    {
        [SerializeField] private ItemReceiverCellIndicator _cellIndicatorPrefab;
        [SerializeField] private float _betweenCellsOffset;
        
        [SerializeField] private ItemReceiver _target;

        private ItemReceiverCellIndicator[] _cellIndicators;

        protected override void OnEnable()
        {
            base.OnEnable();

            var cachedTransform = transform;
            var cells = _target.GetCells();

            var height = 0f;
            
            _cellIndicators = new ItemReceiverCellIndicator[cells.Length];

            for (var i = 0; i < _cellIndicators.Length; i++)
            {
                var cellIndicator = Instantiate(_cellIndicatorPrefab, cachedTransform);
                var cellIndicatorTransform = cellIndicator.transform;

                cellIndicatorTransform.localPosition = new Vector3(0f, height, 0f);
                height -= _betweenCellsOffset;

                cellIndicator.Construct(cells[i]);
                _cellIndicators[i] = cellIndicator;
            }

            _target.Received += OnReceived;
        }

        private void OnReceived()
        {
            
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            foreach (var cellIndicator in _cellIndicators)
            {
                cellIndicator.Clear();
                Destroy(cellIndicator.gameObject);
            }

            _cellIndicators = null;
            _target.Received -= OnReceived;
        }
    }
}