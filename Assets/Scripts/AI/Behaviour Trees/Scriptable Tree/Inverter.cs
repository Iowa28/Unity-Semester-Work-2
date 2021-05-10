using UnityEngine;

namespace AI.Behaviour_Trees.Scriptable_Tree
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/Inverter")]
    public class Inverter : Node
    {
        [SerializeField]
        protected Node node;

        public override NodeState Evaluate(EnemyAIController ai)
        {
            switch (node.Evaluate(ai))
            {
                case NodeState.RUNNING:
                    _nodeState = NodeState.RUNNING;
                    break;
                case NodeState.SUCCESS:
                    _nodeState = NodeState.FAILURE;
                    break;
                case NodeState.FAILURE:
                    _nodeState = NodeState.SUCCESS;
                    break;
                default:
                    break;
                
            }

            return _nodeState;
        }
    }
}