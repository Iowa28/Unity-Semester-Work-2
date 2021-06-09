using UnityEngine;
using UnityEngine.AI;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/PatrolNode")]
    public class PatrolNode : Node
    {
        //TODO: current spot doesn't change for each bot
        private int currentSpot;
        
        public override NodeState Evaluate(EnemyAIController ai)
        {
            NavMeshAgent agent = ai.GetAgent();
            Transform[] patrolSpots = ai.GetPatrolSpots();

            if (ai.GetCurrentHealth() < ai.maxHealth)
            {
                return NodeState.FAILURE;
            }
            
            //ai.SetColor(Color.magenta);
            if (currentSpot < patrolSpots.Length && Vector2.Distance(ai.transform.position, patrolSpots[currentSpot].position) < 1f)
            {
                int newSpot = Random.Range(0, patrolSpots.Length);
                currentSpot = newSpot;
                
                if (newSpot < patrolSpots.Length)
                {
                    
                }
            }

            if (currentSpot < patrolSpots.Length)
            {
                agent.SetDestination(patrolSpots[currentSpot].position);   
            }

            return NodeState.SUCCESS;
        }
    }
}