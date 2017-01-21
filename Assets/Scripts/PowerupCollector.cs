using UnityEngine;
using UnityEngine.AI;

public class PowerupCollector : MonoBehaviour
{
    private GameObject m_Target;
    public ParticleSystem m_BonusCollected;
    private NavMeshAgent m_NavMeshAgent;

    // Use this for initialization
    private void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        var items = FindObjectsOfType<BonusBehaviour>();
        var closestDistance = float.MaxValue;
        BonusBehaviour closestItem = null;
        //look for the closest item:
        foreach (var item in items)
        {
            var distance = (item.transform.position - transform.position).magnitude;
            if (!(distance < closestDistance)) continue;
            closestDistance = distance;
            closestItem = item;
        }
        if (closestItem == null || closestItem == m_Target) return;
        m_Target = closestItem.gameObject;
        m_NavMeshAgent.SetDestination(m_Target.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if colliding with an item
        var item = collision.collider.GetComponent<BonusBehaviour>();

        if (item == null) return;
        m_BonusCollected.Stop();
        m_BonusCollected.Play();
    }
}