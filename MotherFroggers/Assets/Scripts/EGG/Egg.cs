using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject _attackVFXTemplate = null;

    private bool _readyToHatch = false;
    public bool readyToHatch
    {
        get { return _readyToHatch; }
    }

    private bool _hatched = false;

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


    public GameObject Hatch()
    {
        if (_towers.Count == 0 || _hatched || !_readyToHatch)
            return null;
        else
        {
            GameObject tower = Instantiate(_towers[Random.Range(0, _towers.Count - 1)]);
            if (tower == null)
                return null;
            tower.transform.parent = null;

            _hatched = true;
            Invoke(KILL_METHODNAME, 0.2f);

            return tower;
        }
    }

    public void ReadyToHatch()
    {
        Animator animator = transform.GetComponent<Animator>();

        if (animator == null)
            return;

        animator.SetTrigger(_hatching);
    }

    public void Hit()
    {
        if (_attackVFXTemplate)
            Instantiate(_attackVFXTemplate, transform.position, transform.rotation);

        Kill();
    }

    private void Kill()
    {
        Destroy(gameObject);
    }


    private const string KILL_METHODNAME = "Destroy";
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
