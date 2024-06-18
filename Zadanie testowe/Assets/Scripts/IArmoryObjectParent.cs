using UnityEngine;

public interface IArmoryObjectParent
{
    public Transform GetCounterTopPoint();

    public void SetArmoryObject(ArmoryObject armoryObject);

    public ArmoryObject GetArmoryObject();

    public void ClearArmoryObject();

    public bool HasArmoryObject();

}
