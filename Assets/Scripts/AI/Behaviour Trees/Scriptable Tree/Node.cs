using UnityEngine;

namespace AI.Behaviour_Trees.Scriptable_Tree
{
    public abstract class Node : ScriptableObject
    {
        protected NodeState _nodeState;
        public NodeState nodeState => _nodeState;
        
        public abstract NodeState Evaluate(EnemyAIController ai);
    }
    
    public enum NodeState
    {
        RUNNING, SUCCESS, FAILURE
    }
}