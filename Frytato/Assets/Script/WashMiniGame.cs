using UnityEngine;

public class WashMiniGame : MiniGame
{
    public override void Interact()
    {
        base.Interact();
        CameraManager.Instance.ChangeCamera(CameraType.WashCamera);
        Debug.Log("Wash Mini Game Started");
    }
}
