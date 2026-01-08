using UnityEngine;
using UnityEngine.InputSystem; // Добавьте это пространство имен

public class InputHandler2D : MonoBehaviour
{
    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                FallingObject fObject = hit.collider.GetComponent<FallingObject>();
                if (fObject != null)
                {
                    fObject.OnClick();
                }
            }
        }
    }
}