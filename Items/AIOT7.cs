using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AIOTools.Items
{
	public class AIOT7 : ModItem
	{
		private int mode = 1;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("AIOT Mk.VII (Golem Tier)");
			Tooltip.SetDefault("Combination pickaxe, axe, and hammer\nRight click to switch modes");
		}

		public override void SetDefaults()
		{
			item.width = 36;
			item.height = 40;
			item.damage = 50;
			item.knockBack = 6.5f;
			item.useAnimation = 18;
			item.pick = 210;
			item.axe = 25;
			item.tileBoost = 2;
			item.melee = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.value = Item.sellPrice(gold: 5);
			item.rare = ItemRarityID.Lime;
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
					CombatText.NewText(player.getRect(), Color.LightGreen, "Pickaxe and axe mode activated!");
				}
				else if (mode == 1)
				{
					mode = 0; // hammer mode
					CombatText.NewText(player.getRect(), Color.LightYellow, "Hammer mode activated!");
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
					item.useTime = 12;
					item.pick = 0;
					item.axe = 0;
					item.hammer = 90;
					break;

				case 1: // not hammertime
					item.useTime = 6;
					item.pick = 210;
					item.axe = 25;
					item.hammer = 0;
					break;
			}

		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AIOT6"));
			recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
			recipe.AddIngredient(ItemID.Picksaw, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}