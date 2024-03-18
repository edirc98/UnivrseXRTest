using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
public class HandPrecense : MonoBehaviour
{
    #region PUBLIC ATRIBUTES
    [Header("Atributes")]
    public bool showController = false; 
    [Header("Models Prefabs")]
    public List<GameObject> controllerPrefabs;
    public GameObject handPrefab; 
    #endregion
    #region PROTECTED ATRIBUTES
    [SerializeField]
    protected InputDeviceCharacteristics _desiredCharacteristics;
    #endregion
    #region PRIVATE ATRIBUTES
    private List<InputDevice> _devices;
    private InputDevice _targetDevice;

    private GameObject _spawnedController;
    private GameObject _spawnedHand;
    private Animator _handAnimator; 
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        tryInitHandPresence();
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!_targetDevice.isValid)
        {
            tryInitHandPresence();
        }
        else
        {
            if (showController)
            {
                _spawnedController.SetActive(true);
                _spawnedHand.SetActive(false);
            }
            else
            {
                _spawnedController.SetActive(false);
                _spawnedHand.SetActive(true);
                UpdateHandAnims();
            }
        }

    }

    #region FUNCTIONS
    private void tryInitHandPresence()
    {
        _devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(_desiredCharacteristics, _devices);
        if (_devices.Count > 0)
        {
            _targetDevice = _devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == _targetDevice.name);
            if (prefab)
            {
                _spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("No matching controller found! Adding default Controller");
                _spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
            _spawnedHand = Instantiate(handPrefab, transform);
            _handAnimator = _spawnedHand.GetComponent<Animator>();
        }
    }
    private void UpdateHandAnims()
    {
        if (_targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            _handAnimator.SetFloat("Trigger", triggerValue);
        }
        else _handAnimator.SetFloat("Trigger", 0);
        if (_targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            _handAnimator.SetFloat("Grip", gripValue);
        }
        else _handAnimator.SetFloat("Grip", 0);
    }
    #endregion
}
