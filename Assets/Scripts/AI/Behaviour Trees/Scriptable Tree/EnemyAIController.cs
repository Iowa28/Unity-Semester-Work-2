using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    public float maxHealth;
    public float lowHealthThreshold;
    public float healthRestoreRate;

    public Transform playerTransform;
    public Cover[] availableCovers;
    public Transform[] patrolSpots;
    
    private Material material;
    private Transform bestCoverSpot;
    private NavMeshAgent agent;
    
    [SerializeField] private AI.Behaviour_Trees.Scriptable_Tree.Selector topNode;

    public float currentHealth;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        material = GetComponentInChildren<MeshRenderer>().material;
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
            SetColor(Color.red);
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
            Destroy(gameObject);
        }
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }
    
    public void SetColor(Color color)
    {
        material.color = color;
    }

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
}
