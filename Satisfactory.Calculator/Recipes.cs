namespace Satisfactory.Calculator
{
    public static class Recipes
    {
        public static readonly Recipe IronOre =
            new Recipe(
                RecipeCodes.IronOre,
                Array.Empty<ItemQuantity>(),
                new[] { new ItemQuantity(ItemCodes.IronOre, 1) },
                TimeSpan.FromSeconds(1));

        public static readonly Recipe CopperOre =
            new Recipe(
                RecipeCodes.CopperOre,
                Array.Empty<ItemQuantity>(),
                new[] { new ItemQuantity(ItemCodes.CopperOre, 1) },
                TimeSpan.FromSeconds(1));

        public static readonly Recipe RawQuartz =
            new Recipe(
                RecipeCodes.RawQuartz,
                Array.Empty<ItemQuantity>(),
                new[] { new ItemQuantity(ItemCodes.RawQuartz, 1) },
                TimeSpan.FromSeconds(0.5));

        public static readonly Recipe IronIngot =
            new Recipe(
                RecipeCodes.IronIngot,
                new[] { new ItemQuantity(ItemCodes.IronOre, 1) },
                new[] { new ItemQuantity(ItemCodes.IronIngot, 1) },
                TimeSpan.FromSeconds(2));

        public static readonly Recipe CopperIngot =
            new Recipe(
                RecipeCodes.CopperIngot,
                new[] { new ItemQuantity(ItemCodes.CopperOre, 1) },
                new[] { new ItemQuantity(ItemCodes.CopperIngot, 1) },
                TimeSpan.FromSeconds(2));

        public static readonly Recipe QuartzCrystal =
            new Recipe(
                RecipeCodes.QuartzCrystal,
                new[] { new ItemQuantity(ItemCodes.RawQuartz, 5) },
                new[] { new ItemQuantity(ItemCodes.QuartzCrystal, 3) },
                TimeSpan.FromSeconds(8));

        public static readonly Recipe IronPlate = 
            new Recipe(
                RecipeCodes.IronPlate,
                new ItemQuantity(ItemCodes.IronPlate, 2),
                TimeSpan.FromSeconds(6),
                new ItemQuantity(ItemCodes.IronIngot, 3));

        public static readonly Recipe IronRods =
            new Recipe(
                RecipeCodes.IronRods,
                new[] { new ItemQuantity(ItemCodes.IronIngot, 1) },
                new[] { new ItemQuantity(ItemCodes.IronRods, 1) },
                TimeSpan.FromSeconds(4));

        public static readonly Recipe Screws =
            new Recipe(
                RecipeCodes.Screws,
                new[] { new ItemQuantity(ItemCodes.IronRods, 1) },
                new[] { new ItemQuantity(ItemCodes.Screws, 4) },
                TimeSpan.FromSeconds(6));

        public static readonly Recipe CastScrews =
            new Recipe(
                RecipeCodes.CastScrews,
                new[] { new ItemQuantity(ItemCodes.IronIngot, 5) },
                new[] { new ItemQuantity(ItemCodes.Screws, 20) },
                TimeSpan.FromSeconds(24));

        public static readonly Recipe ReinforcedIronPlate = 
            new Recipe(
                RecipeCodes.ReinforcedIronPlate,
                new ItemQuantity(ItemCodes.ReinforcedIronPlate, 1),
                TimeSpan.FromSeconds(12),
                new[] {
                    new ItemQuantity(ItemCodes.IronPlate, 6),
                    new ItemQuantity(ItemCodes.Screws, 12)
                });

        public static readonly Recipe BoltedReinforcedIronPlate =
            new Recipe(
                RecipeCodes.BoltedReinforcedIronPlate,
                new ItemQuantity(ItemCodes.ReinforcedIronPlate, 3), 
                TimeSpan.FromSeconds(12),
                new[] {
                    new ItemQuantity(ItemCodes.IronPlate, 18),
                    new ItemQuantity(ItemCodes.Screws, 50)
                });

        public static readonly Recipe Wire =
             new Recipe(
                RecipeCodes.Wire,
                new ItemQuantity(ItemCodes.Wire, 2),
                TimeSpan.FromSeconds(4),
                new[] {
                    new ItemQuantity(ItemCodes.CopperIngot, 1)
                });

        public static readonly Recipe IronWire =
             new Recipe(
                RecipeCodes.IronWire,
                new ItemQuantity(ItemCodes.Wire, 9),
                TimeSpan.FromSeconds(24),
                new[] {
                    new ItemQuantity(ItemCodes.IronIngot, 5)
                });

        public static readonly Recipe Cable = new Recipe(
                RecipeCodes.Cable,
                new ItemQuantity(ItemCodes.Cable, 1),
                TimeSpan.FromSeconds(2),
                new[] {
                    new ItemQuantity(ItemCodes.Wire, 2)
                });

        public static readonly Recipe CrystalOscillator = new Recipe(
                RecipeCodes.CrystalOscillator,
                new ItemQuantity(ItemCodes.CrystalOscillator, 2),
                TimeSpan.FromSeconds(120),
                new[] {
                    new ItemQuantity(ItemCodes.QuartzCrystal, 36),
                    new ItemQuantity(ItemCodes.Cable, 28),
                    new ItemQuantity(ItemCodes.ReinforcedIronPlate, 5)
                });
    }
}