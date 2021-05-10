using UnityEngine;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/RangeNode")]
    public class RangeNode : Node
    {
        [SerializeField] private float range;
        private Transform target;
        private Transform origin;
        
        public override NodeState Evaluate(EnemyAIController aiController)
        {
            target = aiController.playerTransform;
            origin = aiController.transform;
            
            float distance = Vector3.Distance(target.position, origin.position);
            return distance <= range ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }
}