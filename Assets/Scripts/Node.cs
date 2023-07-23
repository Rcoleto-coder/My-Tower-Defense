using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;

    [HideInInspector]
    public TurretBlueprint turretBlueprint;

    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;

    

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        // Prevents building on top of UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild())
        {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (!buildManager.IsMoneyEnough())
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.money -= blueprint.cost;

        // Build a turret
        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5.0f);

        Debug.Log("Turret build!");
    }

    public void UpgradeTurret()
    {
        /*if (!buildManager.IsMoneyEnoughToUpgrade())
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }*/
        if (PlayerStats.money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.money -= turretBlueprint.upgradeCost;

        // Get rid of the old turret
        Destroy(turret);

        // Build a new turret
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5.0f);

        isUpgraded = true;

        Debug.Log("Turret upgraded!");

    }

    public void SellTurret()
    {
        PlayerStats.money += turretBlueprint.GetSellAmount();

        // Spawn effect
        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5.0f);

        Destroy(turret);
        turretBlueprint = null;
    }

    private void OnMouseEnter()
    {
        // Prevents building on top of UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild())
        {
            return;
        }

        if(buildManager.IsMoneyEnough())
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
        
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
