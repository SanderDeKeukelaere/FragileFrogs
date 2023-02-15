using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _attackVFXTemplate = null;

    private bool _readyToHatch = false;

    [SerializeField] private int _health = 1;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    [SerializeField] private int _wavesNeeded = 3;
    public int WavesNeeded
    {
        get { return _wavesNeeded; }
        set { _wavesNeeded = value; }
    }

    [SerializeField] private float _chance = 0.33f;
    public float Chance
    {
        get { return _chance; }
        set { _chance = value; }
    }

    [SerializeField] private List<GameObject> _towers;

    const string _hatching = "Hatching";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Test"))
            WaveEnded();
    }

    public void WaveEnded()
    {
        if (_readyToHatch)
            return;

        if(--_wavesNeeded <= 0)
            ReadyToHatch();
        else if(Random.Range(0.0f, 1.0f) <= _chance)
        {
            ReadyToHatch();
        }
    }

    public GameObject Hatch()
    {
        if (_towers.Count == 0)
            return null;
        else
        {
            return Instantiate(_towers[Random.Range(0, _towers.Count - 1)]);
        }
    }

    void ReadyToHatch()
    {
        _readyToHatch = true;

        Animator animator = transform.GetComponent<Animator>();

        if (animator == null)
            return;

        animator.SetTrigger(_hatching);
    }

    public void Hit()
    {
        if(--_health <= 0)
            Kill();
    }

    private void Kill()
    {
        if (_attackVFXTemplate)
            Instantiate(_attackVFXTemplate, transform.position, transform.rotation);


        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Hit();

        BasicEnemy enemy = other.GetComponent<BasicEnemy>();
        if(enemy)
            enemy.DoDamage(1);
    }
}
