using UnityEngine;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/IsCoveredNode")]
    public class IsCoveredNode : Node
    {
        public override NodeState Evaluate(EnemyAIController ai)
        {
            Transform target = ai.GetPlayerTransform();
            Transform origin = ai.transform;
            
            RaycastHit hit;
            if (Physics.Raycast(origin.position, target.position - origin.position, out hit))
            {
                //Player doesn't see the bot
                if (hit.collider.transform != target)
                {
                    return NodeState.SUCCESS;
                }
            }
            return NodeState.FAILURE;
        }
    }
}