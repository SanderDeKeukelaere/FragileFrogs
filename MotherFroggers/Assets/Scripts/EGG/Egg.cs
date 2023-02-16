using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject _deathVFXTemplate = null;

    [SerializeField] private bool _isReadyToHatch = false;
    public bool IsReadyToHatch
    {
        get { return _isReadyToHatch; }
    }

    private bool _hatched = false;

    [SerializeField] private int _health = 1;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    [SerializeField] private float _damage = 1.0f;

    [SerializeField] private int _wavesNeeded = 3;
    public int WavesNeeded
    {
        get { return _wavesNeeded; }
        set { _wavesNeeded = value; }
    }

    private int _wavesAlive = 0;

    [SerializeField] private float _chance = 0.33f;
    public float Chance
    {
        get { return _chance; }
        set { _chance = value; }
    }

    [SerializeField] protected List<MyEnums.TileType> _placeableOn = new List<MyEnums.TileType>();
    public List<MyEnums.TileType> PlaceableOn
    {
        get { return _placeableOn; }
    }

    [SerializeField] private List<GameObject> _towers;

    private const string HATCHING = "Hatching";

    private void Start()
    {
        if (_isReadyToHatch)
            ReadyToHatch();
    }

    public void Hatch()
    {
        if (_towers.Count == 0 || _hatched || !_isReadyToHatch)
            return;
        else
        {

            FindObjectOfType<GameManager>().SpawnItemToPlace(_towers[Random.Range(0, _towers.Count)]);

            //GameObject tower = Instantiate(_towers[Random.Range(0, _towers.Count)]);
            //if (tower == null) return null;

            //tower.transform.parent = null;
            _hatched = true;

            Invoke(DESTROY_METHODNAME, 0.1f);

            //return tower;
        }
    }

    public void ReadyToHatch()
    {
        _isReadyToHatch = true;

        Animator animator = transform.GetComponent<Animator>();

        if (animator == null)
            return;

        animator.SetTrigger(HATCHING);
    }

    public void Hit()
    {
        if (_deathVFXTemplate)
            Instantiate(_deathVFXTemplate, transform.position, transform.rotation);

        Kill();
    }

    private void Kill()
    {
        Destroy(gameObject);
    }


    private const string DESTROY_METHODNAME = "Destroy";
    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        BasicEnemy enemy = other.GetComponent<BasicEnemy>();
        if(enemy != null)
        {
            Hit();
            enemy.DoDamage(_damage);
        }
    }

    public void TryHatch()
    {
        if (_isReadyToHatch || _hatched) return;

        ++_wavesAlive;
        if(Random.Range(0.0f, 1.0f) < _chance || _wavesAlive >= _wavesNeeded)
        {
            ReadyToHatch();
        }
    }
}
