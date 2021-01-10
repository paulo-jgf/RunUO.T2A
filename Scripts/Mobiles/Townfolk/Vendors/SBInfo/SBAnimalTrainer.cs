using System.Collections.Generic;

namespace Server.Mobiles
{
    public class SBAnimalTrainer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBAnimalTrainer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
        // To discover the animal's ItemID (argument that comes before hue, ex do is 217) create one in game
        // type [props and get it's BodyValue Attribute. Rideable Llama is 220.
				Add( new AnimalBuyInfo( 1, "A cat", typeof( Cat ), 132, 10, 201, 0 ) );
				Add( new AnimalBuyInfo( 1, "A dog", typeof( Dog ), 170, 10, 217, 0 ) );
				Add( new AnimalBuyInfo( 1, "A horse", typeof( Horse ), 550, 10, 204, 0 ) );
				Add( new AnimalBuyInfo( 1, "A pack horse", typeof( PackHorse ), 631, 10, 291, 0 ) );
				Add( new AnimalBuyInfo( 1, "A pack llama", typeof( PackLlama ), 565, 10, 292, 0 ) );
				Add( new AnimalBuyInfo( 1, "A raven", typeof( Raven ), 156, 10, 205, 0 ) );
				Add( new AnimalBuyInfo( 1, "A rabbit", typeof( Rabbit ), 106, 10, 205, 0 ) );
				Add( new AnimalBuyInfo( 1, "An eagle", typeof( Eagle ), 402, 10, 5, 0 ) );

        Add( new AnimalBuyInfo( 1, "A Llama", typeof( RidableLlama ), 500, 10, 220, 0 ) );

//				Add( new AnimalBuyInfo( 1, typeof( BrownBear ), 855, 10, 167, 0 ) );
//				Add( new AnimalBuyInfo( 1, typeof( GrizzlyBear ), 1767, 10, 212, 0 ) );
//				Add( new AnimalBuyInfo( 1, typeof( Panther ), 1271, 10, 214, 0 ) );
//				Add( new AnimalBuyInfo( 1, typeof( TimberWolf ), 768, 10, 225, 0 ) );
//				Add( new AnimalBuyInfo( 1, typeof( Rat ), 107, 10, 238, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
}
