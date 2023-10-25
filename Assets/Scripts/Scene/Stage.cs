using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    private static StageController stagecontroller;
    public int StageBuildIndex;
    private bool active = false;
    public bool Locked = true;
    // Start is called before the first frame update
    private void Awake()
    {
        StageController.StageSelect += this.ReceiveActive;
        StageController.StageUnlock += this.ReceiveUnlocked;
        this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f); //���ɸ� �̹���
    }
    private void Start()
    {
        stagecontroller = this.transform.parent.GetComponent<StageController>();

    }
    public void Select()
    {
        if (Locked == true ||GameManager.Instance.GameStop)
            return;
        else if (active == false)
        {
            StageController.StageSelect(this);
            StageController.StageDraw();
        }
        else
            SceneManager.LoadScene(StageBuildIndex);
    }

    public void ReceiveActive(Stage stage)
    {
        if (Locked == true)
            return;
        if (stage == this)
            active = true;
        else
            active = false;
    }

    public void ReceiveUnlocked(int stage)
    {
        if (stage == StageBuildIndex)
        {
            Locked = false;
            StageController.StageDraw += this.Draw;
        }
    }

    public void Draw()
    {
        if (Locked == true)
            return;
        if (active == true)
        {
            this.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f); // ù ���ý� �ִϸ��̼ǵ� ������
        }
        else
        {
            this.GetComponent<Image>().color = new Color(1f, 1f, 1f); // ���� �ȵ� ���� �ִϸ��̼ǵ�
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        stagecontroller.OnBeginDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        stagecontroller.OnEndDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        stagecontroller.OnDrag(eventData);
    }
}
