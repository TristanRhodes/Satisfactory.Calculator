namespace Satisfactory.Calculator
{
    public static class Recipies
    {
        public static readonly Recipie IronIngot =
            new Recipie(
                RecipieCodes.IronIngot,
                new[] { new ItemQuantity(ItemCodes.IronOre, 1) },
                new[] { new ItemQuantity(ItemCodes.IronIngot, 1) },
                TimeSpan.FromSeconds(2));

        public static readonly Recipie CopperIngot =
            new Recipie(
                RecipieCodes.CopperIngot,
                new[] { new ItemQuantity(ItemCodes.CopperOre, 1) },
                new[] { new ItemQuantity(ItemCodes.CopperIngot, 1) },
                TimeSpan.FromSeconds(2));

        public static readonly Recipie QuartzCrystal =
            new Recipie(
                RecipieCodes.QuartzCrystal,
                new[] { new ItemQuantity(ItemCodes.RawQuartz, 5) },
                new[] { new ItemQuantity(ItemCodes.QuartzCrystal, 3) },
                TimeSpan.FromSeconds(8));

        public static readonly Recipie IronPlate = 
            new Recipie(
                RecipieCodes.IronPlate,
                new ItemQuantity(ItemCodes.IronPlate, 2),
                TimeSpan.FromSeconds(6),
                new ItemQuantity(ItemCodes.IronIngot, 3));

        public static readonly Recipie ReinforcedIronPlate = 
            new Recipie(
                RecipieCodes.ReinforcedIronPlate,
                new ItemQuantity(ItemCodes.ReinforcedIronPlate, 1),
                TimeSpan.FromSeconds(12),
                new[] {
                    new ItemQuantity(ItemCodes.IronPlate, 6),
                    new ItemQuantity(ItemCodes.Screws, 12)
                });

        public static readonly Recipie BoltedReinforcedIronPlate =
            new Recipie(
                RecipieCodes.BoltedReinforcedIronPlate,
                new ItemQuantity(ItemCodes.ReinforcedIronPlate, 3), 
                TimeSpan.FromSeconds(12),
                new[] {
                    new ItemQuantity(ItemCodes.IronPlate, 18),
                    new ItemQuantity(ItemCodes.Screws, 50)
                });

        public static readonly Recipie Wire =
             new Recipie(
                RecipieCodes.Wire,
                new ItemQuantity(ItemCodes.Wire, 9),
                TimeSpan.FromSeconds(24),
                new[] {
                    new ItemQuantity(ItemCodes.IronIngot, 5)
                });

        public static readonly Recipie IronWire =
             new Recipie(
                RecipieCodes.IronWire,
                new ItemQuantity(ItemCodes.Wire, 2),
                TimeSpan.FromSeconds(4),
                new[] {
                    new ItemQuantity(ItemCodes.CopperIngot, 1)
                });

        public static readonly Recipie Cable = new Recipie(
                RecipieCodes.Cable,
                new ItemQuantity(ItemCodes.Cable, 1),
                TimeSpan.FromSeconds(2),
                new[] {
                    new ItemQuantity(ItemCodes.Wire, 2)
                });

        public static readonly Recipie CrystalOscillator = new Recipie(
                RecipieCodes.CrystalOscillator,
                new ItemQuantity(ItemCodes.CrystalOscillator, 2),
                TimeSpan.FromSeconds(120),
                new[] {
                    new ItemQuantity(ItemCodes.QuartzCrystal, 36),
                    new ItemQuantity(ItemCodes.Cable, 28),
                    new ItemQuantity(ItemCodes.ReinforcedIronPlate, 5)
                });
    }
}