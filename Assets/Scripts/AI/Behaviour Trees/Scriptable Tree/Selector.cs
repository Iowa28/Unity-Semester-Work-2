using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour_Trees.Scriptable_Tree
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/Selector")]
    public class Selector : Node
    {
        [SerializeField] private List<Node> nodes = new List<Node>();

        public override NodeState Evaluate(EnemyAIController ai)
        {
            foreach (var node in nodes)
            {
                switch (node.Evaluate(ai))
                {
                    case NodeState.RUNNING:
                        _nodeState = NodeState.RUNNING;
                        return _nodeState;
                    case NodeState.FAILURE:
                        break;
                    case NodeState.SUCCESS:
                        _nodeState = NodeState.SUCCESS;
                        return _nodeState;
                    default:
                        break;
                }
            }

            _nodeState = NodeState.FAILURE;
            return _nodeState;
        }
    }
}