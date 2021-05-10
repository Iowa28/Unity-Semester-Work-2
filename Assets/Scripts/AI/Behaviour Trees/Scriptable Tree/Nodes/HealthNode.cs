using UnityEngine;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/HealthNode")]
    public class HealthNode : Node
    { 
        private EnemyAIController ai;
        private float threshold;
        
        public override NodeState Evaluate(EnemyAIController aiController)
        {
            ai = aiController;
            threshold = aiController.lowHealthThreshold;
            
            return ai.GetCurrentHealth() <= threshold ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }
}