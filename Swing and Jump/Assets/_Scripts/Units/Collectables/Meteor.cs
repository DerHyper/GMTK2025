using System;
using TMPro;
using UnityEngine;

public class Meteor : Collectable
{
    private float _teleGraphHeight;
    bool telegraphSend = false;
    [SerializeField]
    private float minTelegraphDistance = 10;
    public GameObject TelegraphPrefab;
    private GameObject spawnedTelegraph;

    public override void Collect()
    {
        CollectableManager.Instance.CollectMeteor();
    }

    private void Awake()
    {
        _teleGraphHeight = GameObject.FindGameObjectWithTag("TelegraphMarker").transform.position.y;
    }

    private new void Update()
    {
        base.Update();
        float distance = DistanceFromTelegraph();
        if (!telegraphSend && distance < minTelegraphDistance)
        {
            telegraphSend = true;
            Vector3 pos = new(gameObject.transform.position.x, _teleGraphHeight);
            spawnedTelegraph = Instantiate(TelegraphPrefab, pos, Quaternion.identity);
        }

        if (telegraphSend && distance > 1 && spawnedTelegraph != null)
        {
            spawnedTelegraph.GetComponentInChildren<TMP_Text>().text = ((int)distance).ToString();
        }

        if (telegraphSend && distance < 1 && spawnedTelegraph != null)
        {
            Destroy(spawnedTelegraph);
        }
    }

    private float DistanceFromTelegraph()
    {
        return Math.Abs(gameObject.transform.position.y - _teleGraphHeight);
    }

    void OnDestroy()
    {
        if (spawnedTelegraph != null)
        {
            Destroy(spawnedTelegraph);
        }
    }
}
