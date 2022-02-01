using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AIOTools.Items
{
	public class AIOT6 : ModItem
	{
		private int mode = 1;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("AIOT Mk.VI (Hallowed Tier)");
			Tooltip.SetDefault("Combination pickaxe, axe, and hammer\nRight click to switch modes");
		}

		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.damage = 36;
			item.knockBack = 6.0f;
			item.useAnimation = 20;
			item.pick = 200;
			item.axe = 25;
			item.melee = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.value = Item.sellPrice(gold: 4);
			item.rare = ItemRarityID.LightRed;
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
					item.useTime = 14;
					item.pick = 0;
					item.axe = 0;
					item.hammer = 80;
					break;

				case 1: // not hammertime
					item.useTime = 7;
					item.pick = 200;
					item.axe = 25;
					item.hammer = 0;
					break;
			}

		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}