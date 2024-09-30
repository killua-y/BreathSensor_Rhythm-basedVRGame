using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    private Vector2 Offset = new Vector2(0, 50);
    private Vector2 RandomOffset = new Vector2(50, 30);

    [System.Serializable]
    public class EffectPool
    {
        public string effectName;
        public GameObject effectPrefab;
        public int poolSize;
    }

    public List<EffectPool> effectPools;
    private Dictionary<string, Queue<GameObject>> effectPoolDictionary;

    // Start is called before the first frame update
    void Start()
    {

        InitializeEffectPools();
    }

    private void InitializeEffectPools()
    {
        effectPoolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    public void PlayEffect(string effectName, Vector3 position)
    {
        if (!effectPoolDictionary.ContainsKey(effectName))
        {
            InitializeEffectPool(effectName);
        }

        GameObject effectToPlay;

        if (effectPoolDictionary[effectName].Count > 0)
        {
            effectToPlay = effectPoolDictionary[effectName].Dequeue();
        }
        else
        {
            // Instantiate a new effect if the pool is empty
            effectToPlay = InstantiateNewEffect(effectName);
        }

        // ����offse�ʹ���λ���޸�λ��
        effectToPlay.transform.position = position;
        effectToPlay.SetActive(true);

        StartCoroutine(ReturnEffectToPool(effectName, effectToPlay, effectToPlay.GetComponent<ParticleSystem>().main.duration));
    }

    public void PlayEffect(string effectName, Vector3 position, Quaternion rotation)
    {
        if (!effectPoolDictionary.ContainsKey(effectName))
        {
            InitializeEffectPool(effectName);
        }

        GameObject effectToPlay;

        if (effectPoolDictionary[effectName].Count > 0)
        {
            effectToPlay = effectPoolDictionary[effectName].Dequeue();
        }
        else
        {
            // Instantiate a new effect if the pool is empty
            effectToPlay = InstantiateNewEffect(effectName);
        }

        // ����offse�ʹ���λ���޸�λ��
        effectToPlay.transform.position = position;
        effectToPlay.transform.rotation = rotation;
        effectToPlay.SetActive(true);

        StartCoroutine(ReturnEffectToPool(effectName, effectToPlay, effectToPlay.GetComponent<ParticleSystem>().main.duration));
    }

    private void InitializeEffectPool(string effectName)
    {
        EffectPool pool = effectPools.Find(pool => pool.effectName == effectName);

        if (pool == null)
        {
            Debug.LogWarning("EffectManager: Effect " + effectName + " doesn't exist in the pool.");
            return;
        }

        Queue<GameObject> effectPool = new Queue<GameObject>();

        //for (int i = 0; i < pool.poolSize; i++)
        //{
        //    GameObject effectObject = Instantiate(pool.effectPrefab);
        //    effectObject.SetActive(false);
        //    effectPool.Enqueue(effectObject);
        //}

        effectPoolDictionary.Add(pool.effectName, effectPool);
    }

    private GameObject InstantiateNewEffect(string effectName)
    {
        EffectPool pool = effectPools.Find(pool => pool.effectName == effectName);

        if (pool == null)
        {
            Debug.LogWarning("EffectManager: Effect " + effectName + " doesn't exist in the pool.");
            return null;
        }

        GameObject newEffect = Instantiate(pool.effectPrefab);
        newEffect.SetActive(false);
        effectPoolDictionary[effectName].Enqueue(newEffect);
        return newEffect;
    }

    private IEnumerator ReturnEffectToPool(string effectName, GameObject effectObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        effectObject.SetActive(false);
        effectObject.transform.rotation = Quaternion.identity;
        effectPoolDictionary[effectName].Enqueue(effectObject);
    }
}