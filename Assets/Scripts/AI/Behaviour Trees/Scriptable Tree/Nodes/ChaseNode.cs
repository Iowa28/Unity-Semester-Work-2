using UnityEngine;
using UnityEngine.AI;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/ChaseNode")]
    public class ChaseNode : Node
    {
        public override NodeState Evaluate(EnemyAIController ai)
        {
            Transform target = ai.GetPlayerTransform();
            NavMeshAgent agent = ai.GetAgent();
            
            //ai.SetColor(Color.yellow);
            float distance = Vector3.Distance(target.position, agent.transform.position);
            if (distance > .2f)
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
                return NodeState.RUNNING;
            }
            agent.isStopped = true;
            agent.SetDestination(Vector3.zero);
            return NodeState.SUCCESS;
        }
    }
}