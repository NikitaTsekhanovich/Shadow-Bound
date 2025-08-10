using UnityEngine;

namespace GameControllers.Controllers.AnimatorsControllers
{
    public class UnitAnimator
    {
        private readonly Animator _animator;
        private readonly float _attackSpeed;
        private readonly float _moveSpeed;

        private static readonly int IsRun = Animator.StringToHash("IsRun");
        private static readonly int IsAttackRightHand = Animator.StringToHash("IsAttackRightHand");
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");
        private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

        public UnitAnimator(Animator animator, float attackSpeed, float moveSpeed)
        {
            _animator = animator;
            _attackSpeed = attackSpeed;
            _moveSpeed = moveSpeed;
            
            _animator.SetFloat(MovementSpeed, _moveSpeed);
            _animator.SetFloat(AttackSpeed, _attackSpeed);
        }
        
        public void Run(bool isRun)
        {
            _animator.SetBool(IsRun, isRun);
        }

        public void AttackOneKatana(bool isAttack)
        {
            _animator.SetBool(IsAttackRightHand, isAttack);
        }

        public void DieAnimation()
        {
            _animator.SetBool(IsDead, true);
        }
    }
}
