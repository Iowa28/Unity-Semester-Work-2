using UnityEngine;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/IsCoveredNode")]
    public class IsCoveredNode : Node
    {
        private Transform target;
        private Transform origin;
        
        public override NodeState Evaluate(EnemyAIController aiController)
        {
            target = aiController.playerTransform;
            origin = aiController.transform;
            
            RaycastHit hit;
            if (Physics.Raycast(origin.position, target.position - origin.position, out hit))
            {
                if (hit.collider.transform != target)
                {
                    return NodeState.SUCCESS;
                }
            }
            return NodeState.FAILURE;
        }
    }
}