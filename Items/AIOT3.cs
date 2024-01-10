using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AIOTools.Items
{
	public class AIOT3 : ModItem
	{
		private int mode = 1;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("AIOT Mk.III (Evil Tier)");
			// Tooltip.SetDefault("Combination pickaxe, axe, and hammer\nRight click to switch modes");
		}

		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 40;
            Item.scale = 1.25f;
            Item.damage = 18;
			Item.knockBack = 5.0f;
			Item.useAnimation = 22;
			Item.pick = 65;
			Item.axe = 16;
			Item.tileBoost = 1;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ItemRarityID.Blue;
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
					Item.useTime = 17;
					Item.pick = 0;
					Item.axe = 0;
					Item.hammer = 60;
					break;

				case 1: // not hammertime
					Item.useTime = 14;
					Item.pick = 65;
					Item.axe = 16;
					Item.hammer = 0;
					break;
			}

		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("AIOT2").Type);
			recipe.AddIngredient(ItemID.DemoniteBar, 12);
			recipe.AddIngredient(ItemID.ShadowScale, 6);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();

			recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("AIOT2").Type);
			recipe.AddIngredient(ItemID.CrimtaneBar, 12);
			recipe.AddIngredient(ItemID.TissueSample, 6);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}