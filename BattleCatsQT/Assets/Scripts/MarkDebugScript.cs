using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MarkDebugScript : MonoBehaviour, IPointerDownHandler {

    GameObject WorldObject;

    public RectTransform theButton;

    public Canvas canvas;
    public Camera gameCam;

    Vector2 pointerOffset;

    RectTransform canvasRect; 



    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log(eventData.pressPosition);
        Debug.Log(gameObject.transform.localPosition);
        Debug.Log(RectTransformUtility.ScreenPointToLocalPointInRectangle(theButton, eventData.position, eventData.pressEventCamera, out pointerOffset));
        Debug.Log(pointerOffset);
        gameObject.transform.localPosition = pointerOffset;

    }

    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }
}
