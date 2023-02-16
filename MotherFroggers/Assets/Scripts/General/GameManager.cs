using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private WaveManager _waveManager = null;
    private ItemManager _itemManager = null;
    private TileManager _tileManager = null;
    private ClickManager _clickManager = null;

    private AudioSource _audioBuildSource = null;
    private AudioSource _audioWaveSource = null;

    [SerializeField] private float _fadeSpeed = 0.5f;

    [SerializeField] private GameObject _startButton = null;

    [SerializeField] private AudioClip _buildMusic = null;
    [SerializeField] private AudioClip _waveMusic = null;

    [SerializeField] private List<GameObject> _initialItemsToPlace = new List<GameObject>();
    [SerializeField] private List<GameObject> _itemsToPlacePerWave = new List<GameObject>();

    [SerializeField] private List<Egg> _eggs = new List<Egg>();

    private List<GameObject> _remainingItemsToPlace = new List<GameObject>();
    public int RemainingItemsToPlace
    {
        get { return _remainingItemsToPlace.Count; }
    }
    public int RemainingEggsToPlace
    {
        get
        {
            int eggCount = 0;
            foreach (GameObject obj in _remainingItemsToPlace)
            {
                if (obj.GetComponent<Egg>()) ++eggCount;
            }
            return eggCount;
        }
    }

    [SerializeField] private Transform _itemSpawnSocket = null;

    bool _isInWave = true;
    public bool IsInWave { get { return _isInWave; } }

    private Motherfrogger _motherfrogger;

    private void Start()
    {
        _motherfrogger = FindAnyObjectByType<Motherfrogger>();
        _waveManager = GetComponent<WaveManager>();
        _itemManager = GetComponent<ItemManager>();
        _tileManager = GetComponent<TileManager>();
        _clickManager = GetComponent<ClickManager>();

        var audioSources = GetComponents<AudioSource>();
        _audioBuildSource = audioSources[0];
        _audioWaveSource = audioSources[1];

        _audioBuildSource.clip = _buildMusic;
        _audioWaveSource.clip = _waveMusic;

        _startButton.SetActive(true);

        _audioBuildSource.Play();

        _isInWave = false;

        //Initialize item to place and spawn the first one
        _remainingItemsToPlace = _initialItemsToPlace;

        SpawnNextItemToPlace();
    }

    public void NextWave()
    {
        //Temporarely disable clicks in the click manager
        if (_clickManager != null)
            _clickManager.HandleClicks = false;

        //Start wave if all items are placed
        if (_isInWave == false && _remainingItemsToPlace.Count == 0 && _itemManager.CurrentItem == null)
        {
            _waveManager.StartWave();

            _startButton.SetActive(false);

            _audioWaveSource.Play();

            _isInWave = true;
        }

        //Check if valid placement tile is selected
        if (_itemManager.CurrentItem != null && _isInWave == false)
        {
            //Try to confirm the location of the current item
            if (_itemManager.ConfirmItemPlacement())
            {
                //Remove the confirmed item
                _remainingItemsToPlace.RemoveAt(0);

                //Spawn the next item to place if there are items left
                if (_remainingItemsToPlace.Count > 0)
                    SpawnNextItemToPlace();
            }
        }
    }

    private void Update()
    {
        if(_isInWave && _waveManager.HasWaveEnded() && BasicEnemy.Enemies.Count == 0 && _motherfrogger.CurrentHp > 0)
        {
            _startButton.SetActive(true);

            _audioBuildSource.Play();

            _isInWave = false;

            _remainingItemsToPlace = new List<GameObject>(_itemsToPlacePerWave);


            for(int i = _eggs.Count - 1; i >= 0; i--)
            {
                if (_eggs[i] == null || _eggs[i].TryHatch())
                {
                    _eggs.RemoveAt(i);
                }
            }

            SpawnNextItemToPlace();
        }

        if(_isInWave)
        {
            _audioBuildSource.volume -= _fadeSpeed * Time.deltaTime;
            if(_audioBuildSource.volume < 0)
            {
                _audioBuildSource.volume = 0.0f;
                _audioBuildSource.Stop();
            }

            _audioWaveSource.volume += _fadeSpeed * Time.deltaTime;
            if (_audioWaveSource.volume > 1.0f) _audioWaveSource.volume = 1.0f;
        }
        else
        {
            _audioWaveSource.volume -= _fadeSpeed * Time.deltaTime;
            if (_audioWaveSource.volume < 0)
            {
                _audioWaveSource.volume = 0.0f;
                _audioWaveSource.Stop();
            }

            _audioBuildSource.volume += _fadeSpeed * Time.deltaTime;
            if (_audioBuildSource.volume > 1.0f) _audioBuildSource.volume = 1.0f;
        }
    }

    public void SpawnItemToPlace(GameObject newPlacementObject)
    {
        _remainingItemsToPlace.Add(newPlacementObject);
        SpawnNextItemToPlace();
    }

    private void SpawnNextItemToPlace()
    {
        //Only continue if there are items left to place and if there is an item & tile manager
        if (_remainingItemsToPlace.Count == 0 || _itemManager == null || _tileManager == null) return;

        //Spawn the next item
        GameObject spawnedItem = Instantiate(_remainingItemsToPlace[0], _itemSpawnSocket.position, _itemSpawnSocket.rotation);

        //Set the spawned item as current item of the item manager
        _itemManager.CurrentItem = spawnedItem;

        if(spawnedItem.GetComponent<Egg>())
        {
            _eggs.Add(spawnedItem.GetComponent<Egg>());
        }

        //Make tiles clickable depending on type of item to place
        if (_itemManager.CurrentItem.GetComponent<BaseTower>())
        {
            List<MyEnums.TileType> itemPlaceableOn = _itemManager.CurrentItem.GetComponent<BaseTower>().PlaceableOn;
            foreach (MyEnums.TileType tileType in itemPlaceableOn)
            {
                _tileManager.SetClickableTilesOfType(tileType);
            }
        }
        if (_itemManager.CurrentItem.GetComponent<Egg>())
        {
            List<MyEnums.TileType> itemPlaceableOn = _itemManager.CurrentItem.GetComponent<Egg>().PlaceableOn;
            foreach (MyEnums.TileType tileType in itemPlaceableOn)
            {
                _tileManager.SetClickableTilesOfType(tileType);
            }
        }
    }
}
