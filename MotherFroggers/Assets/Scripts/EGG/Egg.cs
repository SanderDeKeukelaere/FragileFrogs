using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject _attackVFXTemplate = null;

    bool _readyToHatch = false;

    const string _hatching = "Hatching";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


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
}
