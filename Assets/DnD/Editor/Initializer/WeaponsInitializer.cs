using DnD.Code.Scripts.Weapons;

namespace DnD.Editor.Initializer
{
    public static class WeaponsInitializer
    {
        public static readonly string WeaponsPath = $"{Common.FolderPath}/Weapons";

        public static Weapon[] GetAllWeapons()
        {
            return Common.GetAllScriptableObjects<Weapon>(WeaponsPath);
        }
    }
}