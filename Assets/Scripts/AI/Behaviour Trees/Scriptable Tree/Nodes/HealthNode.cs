using UnityEngine;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/HealthNode")]
    public class HealthNode : Node
    {
        public override NodeState Evaluate(EnemyAIController ai)
        {
            float threshold = ai.GetLowHealthThreshold();
            return ai.GetCurrentHealth() <= threshold ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }
}