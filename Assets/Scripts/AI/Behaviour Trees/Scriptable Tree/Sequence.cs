using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour_Trees.Scriptable_Tree
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/Sequence")]
    public class Sequence : Node
    {
        [SerializeField] private List<Node> nodes;

        public override NodeState Evaluate(EnemyAIController ai)
        {
            bool isAnyNodeRunning = false;
            foreach (var node in nodes)
            {
                switch (node.Evaluate(ai))
                {
                    case NodeState.RUNNING:
                        isAnyNodeRunning = true;
                        break;
                    case NodeState.SUCCESS:
                        break;
                    case NodeState.FAILURE:
                        _nodeState = NodeState.FAILURE;
                        return _nodeState;
                    default:
                        break;
                }
            }

            _nodeState = isAnyNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            
            return _nodeState;
        }
    }
}