using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildManager : MonoBehaviour
{
    // This makes the BuildManager a singleton
    public static BuildManager instance;
    
    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public GameObject buildEffect;
    public GameObject sellEffect;
    public NodeUI nodeUI;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }


    public bool IsMoneyEnough()
    {
        return PlayerStats.money >= turretToBuild.cost;
    }
    public bool IsMoneyEnoughToUpgrade()
    {
        return PlayerStats.money >= turretToBuild.upgradeCost;
    }

    public bool CanBuild() 
    {
        return turretToBuild != null; 
    }
    
    public void SelectNode (Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
       
        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
       selectedNode = null;
       nodeUI.Hide();
       
    }
 
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;

        DeselectNode();

    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    } 
    

}
