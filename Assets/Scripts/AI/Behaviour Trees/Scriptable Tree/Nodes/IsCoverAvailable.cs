using UnityEngine;

namespace AI.Behaviour_Trees.Scriptable_Tree.Nodes
{
    [CreateAssetMenu(menuName = "ScriptableTree/Nodes/IsCoverAvailable")]
    public class IsCoverAvailable : Node
    {
        [SerializeField] 
        private float minAngle;
        private Transform target;
        
        public override NodeState Evaluate(EnemyAIController ai)
        {
            Cover[] availableCovers = ai.GetAvailableCovers();
            target = ai.GetPlayerTransform();
            
            Transform bestSpot = FindBestCoverSpot(ai, availableCovers);
            ai.SetBestCoverSpot(bestSpot);
            return bestSpot != null ? NodeState.SUCCESS : NodeState.FAILURE;
        }
        
        private Transform FindBestCoverSpot(EnemyAIController ai, Cover[] covers)
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

            foreach (Cover cover in covers)
            {
                Transform bestSpotInCover = FindBestSpotInCover(cover, ref minAngle);
                
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
            foreach (Transform spot in availableSpots)
            {
                Vector3 direction = target.position - spot.position;
            
                if (CheckIfSpotIsValid(spot))
                {
                    float angle = Vector3.Angle(spot.forward, direction);
                    if (angle < minAngle)
                    {
                        minAngle = angle;
                        bestSpot = spot;
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