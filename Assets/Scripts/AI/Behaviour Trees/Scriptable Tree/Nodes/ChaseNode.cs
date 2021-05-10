using UnityEngine;
using UnityEngine.AI;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/ChaseNode")]
    public class ChaseNode : Node
    {
        private Transform target;
        private NavMeshAgent agent;
        private EnemyAIController ai;

        public override NodeState Evaluate(EnemyAIController aiController)
        {
            target = aiController.playerTransform;
            agent = aiController.GetAgent();
            ai = aiController;
            
            ai.SetColor(Color.yellow);
            float distance = Vector3.Distance(target.position, agent.transform.position);
            if (distance > .2f)
            {
                //Debug.Log("I follow you");
                agent.isStopped = false;
                agent.SetDestination(target.position);
                return NodeState.RUNNING;
            }
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }
}