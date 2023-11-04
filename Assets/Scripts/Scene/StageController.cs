using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;
using System.Threading;
using UnityEngine.UI;
using System.Collections.Generic;


public class StageController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Image select;
    public ScrollRect scrollrect;
    public delegate void MyAction();
    public static Action<Stage> StageSelect;
    public static event Action<StagePlayer> StageUnlock;
    public static MyAction StageDraw;
    private CancellationTokenSource token;
    private int SelectSize;
    private float First;
    private float TargetPos;
    private float LastDropPos;
    public CanvasScaler scaler;
    public float Ratio;
    public List<Stage> Stages = new List<Stage>();
    /// <summary>
    /// ����âĭ ũ�� �̰ɷ� lerp�Ἥ ���� ĭ�� ����
    /// </summary>

    private void OnDestroy()
    {
        token.Cancel();
    }

    private void Start()
    {
        SelectSize = Screen.width;
        Ratio = SelectSize / scaler.referenceResolution.x;
        First = GetComponent<RectTransform>().position.x;
        TargetPos = First;
        token = new CancellationTokenSource();
        for(int i=0; i<GameManager.Instance.StageP.Count; i++)
        {
            StageUnlock(GameManager.Instance.StageP[i]);
        }
        StageDraw();
    }

    private void DeSelect()
    {
        select.gameObject.SetActive(false);
        for (int i = 0; i < Stages.Count; i++)
        {
            Stages[i].active = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DeSelect();
        scrollrect.OnBeginDrag(eventData);
        if (token.IsCancellationRequested.Equals(false))
        {
            token.Cancel();
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        DeSelect();
        scrollrect.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DeSelect();
        token = new CancellationTokenSource();
        LastDropPos = GetComponent<RectTransform>().position.x;
        TargetPosSetting();
        Centering().Forget();
    }
    /// <summary>
    /// �ڿ� �� �Ѿ�� ���ߴ� ���(TargetPos)������ �߸��Ǿ��ִ°�츦 �����ϱ� ���� �Լ�
    /// </summary>
    public void TargetPosSetting()
    {
        if (LastDropPos - TargetPos > 0)
            TargetPos = ((int)((LastDropPos - TargetPos + SelectSize / 2) / SelectSize)) * SelectSize + TargetPos;
        else
            TargetPos = ((int)((LastDropPos - TargetPos - SelectSize / 2) / SelectSize)) * SelectSize + TargetPos;
        if (TargetPos > First)
            TargetPos = First;
        else if (TargetPos <= First - (GetComponent<RectTransform>().sizeDelta.x - transform.parent.GetComponent<RectTransform>().sizeDelta.x) * Ratio)
            TargetPos = First - (GetComponent<RectTransform>().sizeDelta.x - transform.parent.GetComponent<RectTransform>().sizeDelta.x) * Ratio;
    }

    async UniTask Centering()
    {
        LastDropPos = GetComponent<RectTransform>().position.x;
        while (true)
        {
            if (GameManager.Instance.GameStop == true)
                await UniTask.WaitUntil(() => !GameManager.Instance.GameStop);
            await UniTask.Delay(20, cancellationToken: token.Token);
            LastDropPos = Mathf.Lerp(LastDropPos, TargetPos, 0.3f);
            Debug.Log(LastDropPos);
            if (Mathf.Abs(TargetPos - LastDropPos) < 10)
            {
                LastDropPos = TargetPos;
                token.Cancel();
            }
            GetComponent<RectTransform>().position = new Vector3(LastDropPos, GetComponent<RectTransform>().position.y, GetComponent<RectTransform>().position.z);
        }
    }

    public void LeftMove()
    {
        DeSelect();
        if (token.IsCancellationRequested.Equals(false))
        {
            token.Cancel();
        }
        token = new CancellationTokenSource();
        TargetPos += SelectSize;
        if (TargetPos > First)
            TargetPos = First;
        Centering().Forget();
    }

    public void RightMove()
    {
        DeSelect();
        if (token.IsCancellationRequested.Equals(false))
        {
            token.Cancel();
        }
        token = new CancellationTokenSource();
        TargetPos -= SelectSize;
        Debug.Log(TargetPos);
        if (TargetPos <= First - (GetComponent<RectTransform>().sizeDelta.x - transform.parent.GetComponent<RectTransform>().sizeDelta.x) * Ratio)
            TargetPos = First - (GetComponent<RectTransform>().sizeDelta.x - transform.parent.GetComponent<RectTransform>().sizeDelta.x) * Ratio;
        Centering().Forget();
    }

    private void OnDisable()
    {
        StageUnlock = null;
        StageDraw = null;
        StageSelect = null;
    }
}
