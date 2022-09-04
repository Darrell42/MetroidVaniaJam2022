using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/basic")]
public class BaseSkill : ScriptableObject
{

    [Header("Properties")]
    public string IDname;

    public string description;

    public string message;

    [Header("Descriptions")]
    [Multiline(2)]
    public string Remarcs ="IDname should be unique to the skills";

}
