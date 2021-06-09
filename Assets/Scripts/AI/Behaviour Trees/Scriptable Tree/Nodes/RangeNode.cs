using UnityEngine;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/RangeNode")]
    public class RangeNode : Node
    {
        [SerializeField] private float range;
        
        public override NodeState Evaluate(EnemyAIController aiController)
        {
            Transform target = aiController.GetPlayerTransform();
            Transform origin = aiController.transform;
            
            float distance = Vector3.Distance(target.position, origin.position);
            return distance <= range ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }
}