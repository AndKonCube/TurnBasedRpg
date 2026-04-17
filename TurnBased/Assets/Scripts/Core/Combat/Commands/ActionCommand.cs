using System.Collections.Generic;
using UnityEngine;

public abstract class ActionCommand : MonoBehaviour
{
   public CombatUnit source;
   public List<CombatUnit> targets;
    
    public virtual void Execute(){}

    public ActionCommand(CombatUnit source,List<CombatUnit> targets)
    {
        this.source = source;
        this.targets = targets;    
    }
}
