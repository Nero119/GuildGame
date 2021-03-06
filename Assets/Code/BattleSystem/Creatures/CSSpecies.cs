﻿using UnityEngine;
using System.Collections;
using System;

namespace GG.BattleSystem.CreatureSystem
{
    [Serializable]
    public class CSSpecies
    {
        #region variables
        [SerializeField]
        private string _name;
        [SerializeField]
        private string _desript;
        [SerializeField]
        private Sprite _icon;
        #endregion
        #region Setters and getters
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string descript
        {
            get { return _desript; }
            set { _desript = value; }
        }
        public Sprite icon
        {
            get { return _icon; }
            set { _icon = value; }
        }
        #endregion
        public CSSpecies()
        {
            _name = " ";
            _desript = " ";
            _icon = new Sprite();
        }

    }
}