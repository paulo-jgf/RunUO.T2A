using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a sewer rat corpse" )]
	public class Sewerrat : BaseCreature
	{
		[Constructable]
		public Sewerrat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a sewer rat";
			Body = 238;
			BaseSoundID = 0xCC;

			SetStr( 9 );
			SetDex( 25 );
			SetInt( 6, 10 );

			SetHits( 6 );
			SetMana( 0 );

			SetDamage( 1, 2 );

			SetSkill( SkillName.MagicResist, 5.0 );
			SetSkill( SkillName.Tactics, 5.0 );
			SetSkill( SkillName.Wrestling, 5.0 );

			Fame = 300;
			Karma = -300;

			VirtualArmor = 6;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -0.9;

      // Fixing the amount of Gold on rats - erradication campaign to help new players
      PackItem( new Gold( 5 ) );
		}

    /* Original loot is too much for a rat
		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}*/

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Eggs | FoodType.FruitsAndVegies; } }

		public Sewerrat(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
