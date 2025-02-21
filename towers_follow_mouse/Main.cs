﻿using MelonLoader;
using Harmony;
using Assets.Scripts.Unity.UI_New.InGame.Races;
using Assets.Scripts.Simulation.Towers.Weapons;
using Assets.Scripts.Simulation;
using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Unity.UI_New.Main;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Models.Towers;

using Assets.Scripts.Unity;



using Assets.Scripts.Simulation.Towers;

using Assets.Scripts.Utils;

using Il2CppSystem.Collections;
using Assets.Scripts.Unity.UI_New.Popups;
using UnityEngine;
using System;

namespace towers_follow_mouse
{
    public class Main : MelonMod
    {

        static float offset = 2.3f;
        static bool follow = true;
        static System.Random random = new System.Random();

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            Console.WriteLine("towers follow mouse loaded");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            bool inAGame = InGame.instance != null && InGame.instance.bridge != null;


            if (inAGame && follow)
            {
                var v3 = UnityEngine.Input.mousePosition;
                //v3.z = offset;
                v3 = InGame.instance.sceneCamera.ScreenToWorldPoint(v3);
                var towers = InGame.instance.bridge.GetAllTowers();
                foreach (var tower in towers)
                {
                    float x = v3.x;//tower.position.x + change1;
                    float y = v3.y * -offset;//tower.tower.Node.position.Y
                    tower.tower.PositionTower(new Assets.Scripts.Simulation.SMath.Vector2(x, y));
                }
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                //Il2CppSystem.Action<string> deb = (Il2CppSystem.Action<string>)delegate (string s)
                //{
                //    offset = float.Parse(s);
                //};
                //PopupScreen.instance.ShowSetNamePopup("offset",
                //"offset", deb, "2.3");
                follow = !follow;

            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                follow = false;
                var towers = InGame.instance.bridge.GetAllTowers();
                foreach (var tower in towers)
                {
                    float x = tower.position.x + (float)((random.NextDouble() > 0.5 ? 1 : -1) * (20 + (random.NextDouble() * 40)));
                    float y = tower.tower.Node.position.Y + (float)((random.NextDouble() > 0.5 ? 1 : -1) * (20 + (random.NextDouble() * 40)));
                    tower.tower.PositionTower(new Assets.Scripts.Simulation.SMath.Vector2(x, y));
                }

            }

        }





    }

}