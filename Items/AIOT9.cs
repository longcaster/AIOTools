using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AIOTools.Items
{
	public class AIOT9 : ModItem
	{
		private int mode = 1;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("AIOT Ultimate (Post-ML Tier)");
			// Tooltip.SetDefault("Combination pickaxe, axe, and hammer\nRight click to switch modes");
		}

		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 40;
			Item.damage = 150;
			Item.knockBack = 7.0f;
			Item.useAnimation = 14;
			Item.pick = 250;
			Item.axe = 30;
			Item.tileBoost = 5;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.value = Item.sellPrice(gold: 20);
			Item.rare = ItemRarityID.Purple;
			SetUpItem();
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			// change mode
			if (player.altFunctionUse == 2)
			{
				if (mode == 0)
				{
					mode = 1; // normal mode
					CombatText.NewText(player.getRect(), Color.LightGreen, "Digging mode activated!");
				}
				else if (mode == 1)
				{
					mode = 0; // hammer mode
					CombatText.NewText(player.getRect(), Color.LightYellow, "Hammer mode activated!");
				}
				SetUpItem();
				SoundEngine.PlaySound(SoundID.Item37, player.Center);
				return true;
			}
			return true;
		}

		private void SetUpItem()
		{
			switch(mode)
			{
				case 0: // HAMMERTIME
					Item.useTime = 5;
					Item.pick = 0;
					Item.axe = 0;
					Item.hammer = 100;
					break;

				case 1: // not hammertime
					Item.useTime = 4;
					Item.pick = 250;
					Item.axe = 30;
					Item.hammer = 0;
					break;
			}

		}

		public override void AddRecipes()
		{
			ModLoader.TryGetMod("CalamityMod", out Mod calamity);

			if (calamity != null)
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(Mod.Find<ModItem>("AIOT8").Type);
				recipe.AddIngredient(calamity.Find<ModItem>("UeliaceBar").Type, 8);
				recipe.AddTile(TileID.LunarCraftingStation);
				recipe.Register();
			}
            else
            {
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(Mod.Find<ModItem>("AIOT8").Type);
				recipe.AddIngredient(ItemID.LunarBar, 50);
				recipe.AddTile(TileID.LunarCraftingStation);
				recipe.Register();
			}
		}
	}
}