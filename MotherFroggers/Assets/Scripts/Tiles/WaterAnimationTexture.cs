using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimationTexture : MonoBehaviour
{
    [SerializeField] private GameObject _objectWithMaterial = null;
    [SerializeField] private List<Texture2D> _textures= new List<Texture2D>();
    [SerializeField] private float _animationRate = 0.1f;
    private float _timer = 0f;
    private int _index = 0;
    private Material _material;

    // Start is called before the first frame update
    void Start()
    {
        List<Material> materials = new List<Material>();
        _objectWithMaterial.GetComponent<MeshRenderer>().GetMaterials(materials);
        _material = materials[1];
        _material.SetTexture(0, _textures[_index]);
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _timer = _animationRate;
            _index++;
            _index %= _textures.Count;
            _material.mainTexture = _textures[_index];
        }


    }
}
