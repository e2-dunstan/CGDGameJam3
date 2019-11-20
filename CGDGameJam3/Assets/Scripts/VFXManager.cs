using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    public ParticleSystem torchPS;
    public List<PartSys> torchPSList;

    public class PartSys
    {
        public GameObject instance;
        public GameObject target;
        public ParticleSystem effect;
    }

    //Make script a singleton
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateParticleSystemForObject(ParticleSystem _pEffect, List<PartSys> _list)
    {
        PartSys pSys = new PartSys();

        pSys.effect = Instantiate(_pEffect, transform);
        pSys.instance = pSys.effect.gameObject;
        pSys.target = pSys.instance;
        _list.Add(pSys);
        _list[_list.Count - 1].instance.SetActive(false);
    }

    public bool PlayParticleSystemOnGameObject(GameObject _target, List<PartSys> _list)
    {
        //  Check if there are already any of this system with this target
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i].target == _target)
            {
                _list[i].instance.transform.position = _list[i].target.transform.position;
                if (!_list[i].effect.isPlaying)
                {
                    StopParticleSystemOnGameObject(_list[i].target, _list);
                }
                return false;
            }
        }
        //Apply the first available system to the target
        for (int i = 0; i < _list.Count; i++)
        {
            if (!_list[i].instance.activeSelf)
            {
                _list[i].target = _target;
                _list[i].instance.SetActive(true);
                _list[i].instance.transform.position = _list[i].target.transform.position;
                _list[i].effect.Play();
                return true;
            }
        }
        return false;
    }
    public void StopParticleSystemOnGameObject(GameObject _target, List<PartSys> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i].instance.activeSelf)
            {
                if (_list[i].target == _target)
                {
                    _list[i].effect.Stop();
                    _list[i].target = _list[i].instance;
                    _list[i].instance.transform.position = Vector3.zero;
                    _list[i].instance.SetActive(false);
                }
            }
        }
    }
}
