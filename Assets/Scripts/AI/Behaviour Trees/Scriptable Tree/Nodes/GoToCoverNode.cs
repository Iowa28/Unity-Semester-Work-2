using UnityEngine;
using UnityEngine.AI;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/GoToCoverNode")]
    public class GoToCoverNode : Node
    {
        public override NodeState Evaluate(EnemyAIController ai)
        {
            NavMeshAgent agent = ai.GetAgent();
            Transform coverSpot = ai.GetBestCoverSpot();
            
            if (coverSpot == null)
            {
                Debug.Log("I can't find any cover...");
                return NodeState.FAILURE;
            }

            //ai.SetColor(Color.blue);
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