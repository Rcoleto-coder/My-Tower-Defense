using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserbeamer;

    BuildManager buildManager;

    private void Start()
    {
          buildManager = BuildManager.instance;
     }

    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret Purchased");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
          Debug.Log("Missile Launcher Purchased");
          buildManager.SelectTurretToBuild(missileLauncher);
    }
    public void SelectLaserbeamer()
    {
        Debug.Log("Laser Beamer Purchased");
        buildManager.SelectTurretToBuild(laserbeamer);

    }
}
