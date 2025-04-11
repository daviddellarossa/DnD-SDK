using System.Collections.Generic;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Species;
using DnD.Code.Scripts.Tools;

namespace Tests.Infrastructure.SaveManager
{
    public class CharacterStatsTestData
    {
        public Background Background;
        public Class Class;
        public Spex Spex;
        public SubClass SubClass;
        public string CharacterName;
        public List<Tool> Tools;
    }
}