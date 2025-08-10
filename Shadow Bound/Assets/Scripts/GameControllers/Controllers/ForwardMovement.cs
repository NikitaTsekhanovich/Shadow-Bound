using UnityEngine;

namespace GameControllers.Controllers
{
    public class ForwardMovement
    {
        private readonly Vector3 _direction;
        private readonly Rigidbody _rigidbody;
        private readonly float _speed;
        
        public ForwardMovement(
            Vector3 direction,
            Rigidbody rigidbody,
            float speed)
        {
            _direction = direction;
            _rigidbody = rigidbody;
            _speed = speed;
        }

        public void Run()
        {
            _rigidbody.linearVelocity = _direction * _speed;
        }

        public void Stop()
        {
            _rigidbody.linearVelocity = Vector3.zero;
        }
    }
}
