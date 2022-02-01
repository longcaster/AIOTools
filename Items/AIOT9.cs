using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AIOTools.Items
{
	public class AIOT9 : ModItem
	{
		private int mode = 1;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("AIOT Ultimate (Post-ML Tier)");
			Tooltip.SetDefault("Combination pickaxe, axe, and hammer\nRight click to switch modes");
		}

		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.damage = 150;
			item.knockBack = 7.0f;
			item.useAnimation = 14;
			item.pick = 250;
			item.axe = 30;
			item.tileBoost = 5;
			item.melee = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.value = Item.sellPrice(gold: 20);
			item.rare = ItemRarityID.Purple;
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
					mode = 1; // hammer mode
				}
				else if (mode == 1)
                {
					mode = 0; // normal mode
				}
				SetUpItem();
				Main.PlaySound(SoundID.Item37, player.Center);
				return true;
			}
			return true;
		}

		private void SetUpItem()
		{
			switch(mode)
			{
				case 0: // HAMMERTIME
					item.useTime = 5;
					item.pick = 0;
					item.axe = 0;
					item.hammer = 100;
					break;

				case 1: // not hammertime
					item.useTime = 4;
					item.pick = 250;
					item.axe = 30;
					item.hammer = 0;
					break;
			}

		}

		public override void AddRecipes()
		{
			Mod Calamity = ModLoader.GetMod("CalamityMod");

			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AIOT8"));
			recipe.AddIngredient(ItemID.LunarBar, 50);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();

			if (Calamity != null)
			{
				recipe = new ModRecipe(mod);
				recipe.AddIngredient(mod.ItemType("AIOT8"));
				recipe.AddIngredient(Calamity.ItemType("UeliaceBar"), 8);
				recipe.AddTile(TileID.LunarCraftingStation);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
	}
}