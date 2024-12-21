using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Player.FirstPersonCharacter
{
	public class FirstPersonBob : Behaviour
	{
		[SerializeField] private Transform _transform;

		[SerializeField] private bool _allowTilt;
		[SerializeField] private float _tiltIntensity;
		
		[SerializeField] private bool _invertHorizontalTilt;
		[SerializeField] private bool _invertVerticalTilt;
		
		[SerializeField] private float _shakeSpeed;
		[SerializeField] private float _shakeIntensity;

		private Quaternion _originRotation;

		private bool _isShakeCleaning;

		private float _vTilt;
		private float _hTilt;
		
		private float _vShake;

		private Vector3 _velocity;

		protected override void Start()
		{
			base.Start();

			_originRotation = _transform.localRotation;
		}

		protected override void Update()
		{
			base.Update();

			var deltaTime = Time.deltaTime;

			if (_allowTilt)
			{
				var invertHorizontalAmount = _invertHorizontalTilt ? -1 : 1;
				var invertVerticalAmount = _invertVerticalTilt ? -1 : 1;
				
				_vTilt = Mathf.Lerp(_vTilt, IsBobing ? _velocity.z * invertVerticalAmount: 0f, deltaTime * 2);
				_hTilt = Mathf.Lerp(_hTilt, IsBobing ? _velocity.x * invertHorizontalAmount: 0f, deltaTime * 2);
			}

			if (IsBobing)
			{
				_vShake = Mathf.Sin(Time.time * _shakeSpeed) * _velocity.magnitude * _shakeIntensity;
			}
			else
			{
				if (_isShakeCleaning)
				{
					_vShake = Mathf.Lerp(_vShake, 0f, deltaTime * 8);

					if (_vShake < 0.02f)
					{
						_vShake = 0f;
						
						_isShakeCleaning = false;
					}
				}
			}

			_transform.localRotation =
				_originRotation * Quaternion.AngleAxis(_hTilt * _tiltIntensity, Vector3.forward) *
				Quaternion.AngleAxis(_vTilt * _tiltIntensity + _vShake, Vector3.right);
		}

		public void Bob(Vector3 velocity)
		{
			_velocity = velocity;
			
			IsBobing = true;
		}

		public void StopBobing()
		{
			_velocity = Vector3.zero;

			_isShakeCleaning = true;
			IsBobing = false;
		}

		public bool IsBobing { get; private set; }
	}
}