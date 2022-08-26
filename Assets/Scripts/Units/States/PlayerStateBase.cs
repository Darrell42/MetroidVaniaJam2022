using UnityEngine;

public abstract class PlayerStateBase
{
    public abstract void EnterState(PlayerUnit player);

    public abstract void Update(PlayerUnit player);
    public abstract void FixedUpdate(PlayerUnit player);

    public abstract void OnCollisionEnter(PlayerUnit player, Collision collision);

    public abstract void OnCollisionExit(PlayerUnit player, Collision collision);
    public abstract void OnTriggerEnter(PlayerUnit player, Collider other);
    public abstract void OnTriggerExit(PlayerUnit player, Collider other);
}
