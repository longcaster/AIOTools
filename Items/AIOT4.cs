using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AIOTools.Items
{
	public class AIOT4 : ModItem
	{
		private int mode = 1;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("AIOT Mk.IV (Hellstone Tier)");
			// Tooltip.SetDefault("Combination pickaxe, axe, and hammer\nRight click to switch modes");
		}

		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 40;
			Item.damage = 24;
			Item.knockBack = 5.0f;
			Item.useAnimation = 22;
			Item.pick = 100;
			Item.axe = 20;
			Item.tileBoost = 1;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.value = Item.sellPrice(silver: 50);
			Item.rare = ItemRarityID.Orange;
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
					Item.useTime = 14;
					Item.pick = 0;
					Item.axe = 0;
					Item.hammer = 70;
					break;

				case 1: // not hammertime
					Item.useTime = 17;
					Item.pick = 100;
					Item.axe = 20;
					Item.hammer = 0;
					break;
			}

		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("AIOT3").Type);
			recipe.AddIngredient(ItemID.HellstoneBar, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}