using UnityEngine;
using UnityEngine.AI;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/ShootNode")]
    public class ShootNode : Node
    {
        private NavMeshAgent agent;
        private EnemyAIController ai;
        private Transform target;

        private Vector3 currentVelocity;
        [SerializeField]
        private float smoothDamp;
        [SerializeField] 
        private float checkRange;
        private EnemyWeapon enemyWeapon;

        public override NodeState Evaluate(EnemyAIController aiController)
        {
            agent = aiController.GetAgent();
            ai = aiController;
            target = aiController.playerTransform;
            enemyWeapon = aiController.GetEnemyWeapon();
            
            agent.isStopped = true;
            ai.SetColor(Color.green);
            Vector3 direction = target.position - ai.transform.position;
            Vector3 currentDirection = Vector3.SmoothDamp(ai.transform.forward, direction, ref currentVelocity, smoothDamp);
            Quaternion rotation = Quaternion.LookRotation(currentDirection, Vector3.up);
            ai.transform.rotation = rotation;

            string tag = checkRaycast(currentDirection, ai.transform.position);
            if (tag == "Wall")
            {
                return NodeState.FAILURE;
            }
            
            enemyWeapon.Shoot(currentDirection, ai.transform.position);

            return NodeState.RUNNING;
        }

        private string checkRaycast(Vector3 direction, Vector3 position)
        {
            string tag = "";
            
            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, checkRange))
            {
                tag = hit.transform.gameObject.tag;
            }

            return tag;
        }

    }
}