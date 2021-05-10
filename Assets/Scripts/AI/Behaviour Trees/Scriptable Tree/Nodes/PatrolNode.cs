using UnityEngine;
using UnityEngine.AI;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/PatrolNode")]
    public class PatrolNode : Node
    {
        private NavMeshAgent agent;
        private EnemyAIController ai;
        private Transform[] patrolSpots;

        private int currentSpot = 0;
        
        public override NodeState Evaluate(EnemyAIController aiController)
        {
            agent = aiController.GetAgent();
            ai = aiController;
            patrolSpots = aiController.patrolSpots;
            
            ai.SetColor(Color.magenta);
            //Debug.Log(Vector2.Distance(ai.transform.position, patrolSpots[currentSpot].position));
            if (Vector2.Distance(ai.transform.position, patrolSpots[currentSpot].position) < 1f)
            {
                currentSpot = Random.Range(0, patrolSpots.Length);
                Debug.Log(currentSpot);
            }

            agent.SetDestination(patrolSpots[currentSpot].position);

            return NodeState.SUCCESS;
        }
    }
}