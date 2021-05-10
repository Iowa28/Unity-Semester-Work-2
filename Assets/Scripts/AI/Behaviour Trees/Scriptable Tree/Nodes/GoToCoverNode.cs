using UnityEngine;
using UnityEngine.AI;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/GoToCoverNode")]
    public class GoToCoverNode : Node
    {
        private NavMeshAgent agent;
        private EnemyAIController ai;

        public override NodeState Evaluate(EnemyAIController aiController)
        {
            agent = aiController.GetAgent();
            ai = aiController;
            
            Transform coverSpot = ai.GetBestCoverSpot();
            if (coverSpot == null)
                return NodeState.FAILURE;
        
            ai.SetColor(Color.blue);
            float distance = Vector3.Distance(coverSpot.position, agent.transform.position);
            if(distance > 0.2f)
            {
                agent.isStopped = false;
                agent.SetDestination(coverSpot.position);
                return NodeState.RUNNING;
            }
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }
}