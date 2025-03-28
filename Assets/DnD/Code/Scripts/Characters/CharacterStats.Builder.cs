using System;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Helpers;
using DnD.Code.Scripts.Helpers.NameHelper;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Species;
using UnityEngine;

namespace DnD.Code.Scripts.Characters
{
    public partial class CharacterStats
    {
        public class Builder
        {
            public static readonly string CharacterStatsPath = "Assets/CharacterStats.asset";
            private string _name;
            private Class _class;
            private SubClass _subClass;
            private Spex _spex;
            private Background _background;
            private int _level = 1;
            private int _xp = 0;
            
            public Builder SetName(string name)
            {
                this._name = name;
                return this;
            }

            public Builder SetClass(Class @class)
            {
                this._class = @class;
                return this;
            }
            
            public Builder SetSubClass(SubClass subClass)
            {
                this._subClass = subClass;
                
                return this;
            }

            public Builder SetBackground(Background background)
            {
                this._background = background;
                return this;
            }
            
            public Builder SetSpex(Spex spex)
            {
                this._spex = spex;
                return this;
            }

            public CharacterStats Build()
            {
                if (!CheckAll())
                {
                    Debug.LogError("Cannot continue with the CharacterStats creation.");
                    return null;
                }
                
                FileSystemHelper.EnsureFolderExists(PathHelper.CharacterStatsPath);

                var fileName = $"{NameHelper.Naming.CharacterStats}.{this._name}.{Guid.NewGuid().ToString()}";
                var characterStats = ScriptableObjectHelper.CreateScriptableObject<CharacterStats>(fileName, PathHelper.CharacterStatsPath);
                
                characterStats.name = this._name;
                characterStats.@class = this._class;
                characterStats.subClass = this._subClass;
                characterStats.spex = this._spex;
                characterStats.background = this._background;
                characterStats.level = _level;
                characterStats.xp = _xp;
                
                
                return characterStats;
            }

            private bool CheckName()
            {
                if (string.IsNullOrEmpty(this._name))
                {
                    Debug.LogError("Name cannot be null or empty.");
                    return false;
                }

                return true;
            }
            
            private bool CheckClass()
            {
                if (this._class is null)
                {
                    Debug.LogError("Class cannot be null");
                    return false;
                }
                
                return true;
            }
            
            private bool CheckSubClass()
            {
                if (_class.SubClasses.Count == 0)
                {
                    return true; // Class does not offer sub-classes
                }
                
                if (this._subClass is null)
                {
                    Debug.LogError("SubClass cannot be null");
                    return false;
                }
                
                if (!_class.SubClasses.Contains(this._subClass))
                {
                    Debug.LogError($"Class {_class.name} does not have a subclass with {this._subClass.name}");
                    return false;
                    
                }
                
                return true;
            }
            
            private bool CheckBackground()
            {
                if (this._background is null)
                {
                    Debug.LogError("Background cannot be null");
                    return false;
                }
                
                return true;
            }

            private bool CheckSpex()
            {
                if (this._spex is null)
                {
                    Debug.LogError("Spex cannot be null");
                    return false;
                }
                
                return true;
            }

            private bool CheckAll()
            {
                return CheckName()
                    && CheckClass()
                    && CheckSubClass()
                    && CheckBackground()
                    && CheckSpex();
            }
        }
    }
}