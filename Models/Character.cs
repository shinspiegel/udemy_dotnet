namespace dotnet_rpg.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int HitPoints { get; set; } = 0;
        public int Strength { get; set; } = 0;
        public int Defense { get; set; } = 0;
        public int Intelligence { get; set; } = 0;
        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}