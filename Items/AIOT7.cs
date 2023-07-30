using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AIOTools.Items
{
	public class AIOT7 : ModItem
	{
		private int mode = 1;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("AIOT Mk.VII (Golem Tier)");
			// Tooltip.SetDefault("Combination pickaxe, axe, and hammer\nRight click to switch modes");
		}

		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 40;
			Item.damage = 50;
			Item.knockBack = 6.5f;
			Item.useAnimation = 18;
			Item.pick = 210;
			Item.axe = 25;
			Item.tileBoost = 2;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Lime;
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
					Item.useTime = 12;
					Item.pick = 0;
					Item.axe = 0;
					Item.hammer = 90;
					break;

				case 1: // not hammertime
					Item.useTime = 6;
					Item.pick = 210;
					Item.axe = 25;
					Item.hammer = 0;
					break;
			}

		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("AIOT6").Type);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
			recipe.AddIngredient(ItemID.Picksaw, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}