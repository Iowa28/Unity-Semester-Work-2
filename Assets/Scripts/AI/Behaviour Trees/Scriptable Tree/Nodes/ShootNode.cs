using UnityEngine;
using UnityEngine.AI;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/ShootNode")]
    public class ShootNode : Node
    {
        private Vector3 currentVelocity;
        [SerializeField]
        private float smoothTime;
        [SerializeField] 
        private float checkRange;
        private EnemyWeapon enemyWeapon;

        public override NodeState Evaluate(EnemyAIController ai)
        {
            NavMeshAgent agent = ai.GetAgent();
            Transform target = ai.GetPlayerTransform();
            if (enemyWeapon == null)
            {
                enemyWeapon = ai.GetEnemyWeapon();
            }

            agent.isStopped = true;
            //ai.SetColor(Color.green);
            Vector3 direction = target.position - ai.transform.position;
            Vector3 currentDirection = Vector3.SmoothDamp(ai.transform.forward, direction, ref currentVelocity, smoothTime);
            Quaternion rotation = Quaternion.LookRotation(currentDirection, Vector3.up);
            ai.transform.rotation = rotation;

            if (IsWallAhead(currentDirection, ai.transform.position))
            {
                return NodeState.FAILURE;
            }
            enemyWeapon.Shoot(currentDirection, ai.transform.position);

            return NodeState.RUNNING;
        }

        private bool IsWallAhead(Vector3 direction, Vector3 position)
        {
            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, checkRange))
            {
                string tag = hit.transform.gameObject.tag;
                if (tag == "Wall")
                {
                    return true;
                }
            }
            return false;
        }

    }
}