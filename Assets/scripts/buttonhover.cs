using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonhover : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private Button pb;
    public Sprite Hover;
    public Sprite notHover;

    void Start()
    {
        pb = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pb.image.sprite = Hover;
        //Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse Exit");
        pb.image.sprite = notHover;
        //Change Image back to default?
    }
}