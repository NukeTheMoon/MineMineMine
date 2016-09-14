using TeamUtility.IO;
using UnityEngine;

public class ReticleControls : MonoBehaviour
{
    public float MoveSpeed;

    private void Update()
    {
        float horizontalMovement = SceneReference.InputMappingManager.GetMoveReticleHorizontal() * MoveSpeed * Time.deltaTime;
        float verticalMovement = SceneReference.InputMappingManager.GetMoveReticleVertical() * MoveSpeed * Time.deltaTime;
        transform.Translate(horizontalMovement, 0, verticalMovement);
        if (SceneReference.InputMappingManager.GetReticleConfirm())
        {
            SceneReference.RespawnManager.Respawn();
        }
    }

}
