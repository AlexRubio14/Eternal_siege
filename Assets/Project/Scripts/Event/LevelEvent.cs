using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class LevelEvent : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private float maxActiveTime;
    [SerializeField] private float maxDesactiveTime;
    private float desactiveTime;
    private float activeTime;

    [Header("Player")]
    private int currentPlayersIn;

    [Header("UI")]
    [SerializeField] private string eventMesage;
    private TextMeshProUGUI _text;
    protected GameObject arrow;

    protected Transform[] corner;

    private enum eventState { Desactive, Active, Starting };
    private eventState state;
    protected void Initialize()
    {
        desactiveTime = 0;
        activeTime = 0;
        currentPlayersIn = 0;
        state = eventState.Desactive;
    }

    protected virtual void Update()
    {
        switch (state) 
        {
            case eventState.Desactive:
                DesactiveEvent();
                break;
            case eventState.Starting:
                ActiveEvent();
                DesactiveEvent();
                break;
            case eventState.Active:
                EventUpdate();
                break;
        }
    }

    private void ActiveEvent()
    {
        activeTime += Time.deltaTime * (currentPlayersIn / PlayersManager.instance.GetPlayersList().Count);
        transform.GetChild(0).transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, activeTime/maxActiveTime);
        if (activeTime > maxActiveTime)
        {
            state = eventState.Active;
            this._text.gameObject.SetActive(true);
            _text.text = eventMesage;
        }
    }

    protected virtual void EventUpdate()
    {

    }

    private void DesactiveEvent()
    {
        desactiveTime += Time.deltaTime;
        if (desactiveTime > maxDesactiveTime)
        {
            Destroy(gameObject);
            this._text.gameObject.SetActive(true);
            arrow.SetActive(false);
            _text.text = "The event time has expired";
        }
    }

    private void OnTriggerEnter(Collider othern)
    {
        if(othern.CompareTag("Player") && othern is CapsuleCollider)
        {
            state = eventState.Starting;
            currentPlayersIn++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other is CapsuleCollider)
        {
            currentPlayersIn--;
            if(currentPlayersIn <= 0) 
            {
                state = eventState.Desactive;
            }
        }
    }

    public void SetText(TextMeshProUGUI _text)
    {
        this._text = _text;
        this._text.gameObject.SetActive(true);
        this._text.text = "An Event has Appeared";
    }

    public void SetArrow(GameObject _arrow)
    {
        arrow = _arrow;
    }

    public void SetCorner(Transform[] _corner)
    {
        corner = new Transform[_corner.Length];
        for (int i = 0; i < _corner.Length; i++) 
        {
            corner[i] = _corner[i];
        }
    }
}
