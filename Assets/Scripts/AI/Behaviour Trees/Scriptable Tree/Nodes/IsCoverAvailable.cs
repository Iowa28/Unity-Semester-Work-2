using UnityEngine;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/IsCoverAvailable")]
    public class IsCoverAvailable : Node
    {
        private Cover[] availableCovers;
        private Transform target; //player
        private EnemyAIController ai;
        [SerializeField] private float minAngle;
        
        public override NodeState Evaluate(EnemyAIController aiController)
        {
            availableCovers = aiController.availableCovers;
            target = aiController.playerTransform;
            ai = aiController;
            
            Transform bestSpot = FindBestCoverSpot();
            ai.SetBestCoverSpot(bestSpot);
            return bestSpot != null ? NodeState.SUCCESS : NodeState.FAILURE;
        }
        
        private Transform FindBestCoverSpot()
        {
            if (ai.GetBestCoverSpot() != null)
            {
                if (CheckIfSpotIsValid(ai.GetBestCoverSpot()))
                {
                    return ai.GetBestCoverSpot();
                }
            }
            
            Transform bestSpot = null;
            float bestDistance = 0;
            
            for (int i = 0; i < availableCovers.Length; i++)
            {
                Transform bestSpotInCover = FindBestSpotInCover(availableCovers[i], ref minAngle);
                
                if (bestSpotInCover != null)
                {
                    float distance = Vector3.Distance(ai.transform.position, bestSpotInCover.position);

                    if (distance < bestDistance || bestDistance == 0)
                    {
                        //Debug.Log( "best distance: " + distance + " , cover index: " + i);
                        bestDistance = distance;
                        bestSpot = bestSpotInCover;
                    }
                }
            }

            return bestSpot;
        }
        
        private Transform FindBestSpotInCover(Cover cover, ref float minAngle)
        {
            Transform[] availableSpots = cover.GetCoverSpots();
            Transform bestSpot = null;
            for (int i = 0; i < availableSpots.Length; i++)
            {
                Vector3 direction = target.position - availableSpots[i].position;
            
                if (CheckIfSpotIsValid(availableSpots[i]))
                {
                    float angle = Vector3.Angle(availableSpots[i].forward, direction);
                    if (angle < minAngle)
                    {
                        minAngle = angle;
                        bestSpot = availableSpots[i];
                    }
                }
            }

            return bestSpot;
        }
        
        private bool CheckIfSpotIsValid(Transform spot)
        {
            RaycastHit hit;
            Vector3 direction = target.position - spot.position;
            if (Physics.Raycast(spot.position, direction, out hit))
            {
                if (hit.collider.transform != target)
                {
                    return true;
                }
            }

            return false;
        }
    }
}