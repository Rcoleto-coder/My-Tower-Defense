
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;
    public GameObject ui;

    public Text upgradeCost;
    public Text sellCost;
    public Button upgradeButton;

    public void SetTarget(Node target)
    {
       this.target = target;
       transform.position = target.GetBuildPosition();

        if (!target.isUpgraded) 
        {
            // Changes the text of the upgrade button to the cost of the upgrade
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }else
        {
            upgradeCost.text = "MAX LVL";
            upgradeButton.interactable = false;
        }

        sellCost.text = "$" + target.turretBlueprint.GetSellAmount();
               
       ui.SetActive(true);
    }

    public void Hide()
    {
          ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

}
