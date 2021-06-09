using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] private float lowHealthThreshold;
    public float healthRestoreRate;
    private float currentHealth;
    
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Cover[] availableCovers;
    [SerializeField] private Transform[] patrolSpots;
    [SerializeField] private GameObject deathEffect;
    
    //private Material material;
    private Transform bestCoverSpot;
    private NavMeshAgent agent;
    
    [SerializeField] private AI.Behaviour_Trees.Scriptable_Tree.Selector topNode;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //material = GetComponentInChildren<MeshRenderer>().material;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        topNode.Evaluate(this);
        if (topNode.nodeState == AI.Behaviour_Trees.Scriptable_Tree.NodeState.FAILURE)
        {
            //SetColor(Color.red);
            agent.isStopped = true;
        }

        if (currentHealth <= maxHealth)
        {
            currentHealth += Time.deltaTime * healthRestoreRate;
        }
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameManager>().ReduceEnemyCount();

        GameObject deathEffectObject = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathEffectObject, 5f);
        
        Destroy(gameObject);
    }
    
    #region Getters/Setters
    /*
    public void SetColor(Color color)
    {
        material.color = color;
    }
    */

    public void SetBestCoverSpot(Transform bestCoverSpot)
    {
        this.bestCoverSpot = bestCoverSpot;
    }

    public Transform GetBestCoverSpot()
    {
        return bestCoverSpot;
    }
    
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth(float value)
    {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }
    
    public EnemyWeapon GetEnemyWeapon()
    {
        return GetComponentInChildren<EnemyWeapon>();
    }
    
    public NavMeshAgent GetAgent()
    {
        return agent;
    }
    
    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }

    public Transform[] GetPatrolSpots()
    {
        return patrolSpots;
    }

    public Cover[] GetAvailableCovers()
    {
        return availableCovers;
    }

    public float GetLowHealthThreshold()
    {
        return lowHealthThreshold;
    }

    #endregion
}
