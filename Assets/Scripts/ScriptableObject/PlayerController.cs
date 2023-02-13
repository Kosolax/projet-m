using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputEventSO inputEventSO;

    private void OnEnable()
    {
        if (this.inputEventSO != null)
        {
            this.inputEventSO.MoveEvent += (vector2) => Move(vector2);
            this.inputEventSO.JumpEvent += () => Jump();
        }
    }

    private void OnDisable()
    {
        if (this.inputEventSO != null)
        {
            this.inputEventSO.MoveEvent -= (vector2) => Move(vector2);
            this.inputEventSO.JumpEvent -= () => Jump();
        }
    }

    private void Move(Vector2 vector2)
    {
        Debug.Log("ok x" + vector2.x + ", y" + vector2.y);
    }

    private void Jump()
    {
        Debug.Log("jump");
    }
}