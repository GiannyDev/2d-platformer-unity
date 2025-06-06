﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCling : PlayerStates
{
    [Header("Settings")] 
    [SerializeField] private float fallFactor = 0.5f;
    
    protected override void GetInput()
    {
        if (_horizontalInput <= -0.1f || _horizontalInput >= 0.1f)
        {
            WallCling();
        }
    }

    public override void ExecuteState()
    {
        ExitWallCling();
    }

    private void WallCling()
    {
        if (_playerController.Conditions.IsCollidingBelow || _playerController.Force.y >= 0)
        {
            return;
        }

        if (_playerController.Conditions.IsCollidingLeft && _horizontalInput <= -0.1f ||
            _playerController.Conditions.IsCollidingRight && _horizontalInput >= 0.1f)
        {
            _playerController.SetWallClingMultiplier(fallFactor);
            _playerController.Conditions.IsWallClinging = true;
        }
    }

    private void ExitWallCling()
    {
        if (_playerController.Conditions.IsWallClinging)
        {
            // Exit wall cling for right direction <------ NEW CONDITION
            if (_playerController.Conditions.IsCollidingRight == false && _playerController.FacingRight)
            {
                _playerController.SetWallClingMultiplier(0f);
                _playerController.Conditions.IsWallClinging = false;
            }

            // Exit wall cling for left direction <------ NEW CONDITION
            if (_playerController.Conditions.IsCollidingLeft == false && _playerController.FacingRight == false)
            {
                _playerController.SetWallClingMultiplier(0f);
                _playerController.Conditions.IsWallClinging = false;
            }
            
            
            if (_playerController.Conditions.IsCollidingBelow || _playerController.Force.y >= 0)
            {
                _playerController.SetWallClingMultiplier(0f);
                _playerController.Conditions.IsWallClinging = false;
            }

            if (_playerController.FacingRight)
            {
                if (_horizontalInput <= -0.1f || _horizontalInput < 0.1f)
                {
                    _playerController.SetWallClingMultiplier(0f);
                    _playerController.Conditions.IsWallClinging = false;
                }
            }
            else
            {
                if (_horizontalInput >= 0.1f || _horizontalInput > -0.1f)
                {
                    _playerController.SetWallClingMultiplier(0f);
                    _playerController.Conditions.IsWallClinging = false;
                }
            }
        }
    }
}
