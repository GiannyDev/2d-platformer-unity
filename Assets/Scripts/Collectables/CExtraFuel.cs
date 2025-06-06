﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CExtraFuel : Collectable
{
    [Header("Settings")] 
    [SerializeField] private float extraFuel = 10f;
    [SerializeField] private float extraFuelTimer = 3f;

    private PlayerJetpack _jetpack;
    
    protected override void Collect()
    {
        ApplyFuel();
    }

    /// <summary>
    /// Apply that bonus
    /// </summary>
    private void ApplyFuel()
    {
        _jetpack = _playerMotor.GetComponent<PlayerJetpack>();
        StartCoroutine(IEExtraFuel());
    }

    /// <summary>
    /// Adds fuel 
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEExtraFuel()
    {
        _jetpack.JetpackFuel = extraFuel;
        _jetpack.FuelLeft = extraFuel;
        UIManager.Instance.UpdateFuel(extraFuel, extraFuel); // ---- ADD THIS
        yield return new WaitForSeconds(extraFuelTimer);
        _jetpack.JetpackFuel = _jetpack.InitialFuel;
        _jetpack.FuelLeft = _jetpack.InitialFuel;  // ---- ADD THIS
        UIManager.Instance.UpdateFuel(_jetpack.FuelLeft, _jetpack.JetpackFuel);  // ---- ADD THIS
    }
}
