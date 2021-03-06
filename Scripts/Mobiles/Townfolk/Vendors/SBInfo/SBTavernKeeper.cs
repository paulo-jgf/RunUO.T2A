using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBTavernKeeper : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBTavernKeeper()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{ // BeverageBuyInfo naming error
				Add( new BeverageBuyInfo( "Bottle of ale", typeof( BeverageBottle ), BeverageType.Ale, 7, 20, 0x99F, 0 ) );
				Add( new BeverageBuyInfo( "Bottle of wine", typeof( BeverageBottle ), BeverageType.Wine, 7, 20, 0x9C7, 0 ) );
				Add( new BeverageBuyInfo( "Bottle of liquor", typeof( BeverageBottle ), BeverageType.Liquor, 7, 20, 0x99B, 0 ) );
				Add( new BeverageBuyInfo( "Jug of cider", typeof( Jug ), BeverageType.Cider, 13, 20, 0x9C8, 0 ) );
				Add( new BeverageBuyInfo( "Milk", typeof( Pitcher ), BeverageType.Milk, 7, 20, 0x9F0, 0 ) );
				Add( new BeverageBuyInfo( "Pitcher of ale", typeof( Pitcher ), BeverageType.Ale, 11, 20, 0x1F95, 0 ) );
				Add( new BeverageBuyInfo( "Pitcher of cider", typeof( Pitcher ), BeverageType.Cider, 11, 20, 0x1F97, 0 ) );
				Add( new BeverageBuyInfo( "Pitcher of liquor", typeof( Pitcher ), BeverageType.Liquor, 11, 20, 0x1F99, 0 ) );
				Add( new BeverageBuyInfo( "Pitcher of wine", typeof( Pitcher ), BeverageType.Wine, 11, 20, 0x1F9B, 0 ) );
				Add( new BeverageBuyInfo( "Pitcher of water", typeof( Pitcher ), BeverageType.Water, 11, 20, 0x1F9D, 0 ) );

				Add( new GenericBuyInfo( "Loaf of bread", typeof( BreadLoaf ), 6, 10, 0x103B, 0 ) );
				Add( new GenericBuyInfo( "Wheel of cheese", typeof( CheeseWheel ), 21, 10, 0x97E, 0 ) );
				Add( new GenericBuyInfo( "Cooked bird", typeof( CookedBird ), 17, 20, 0x9B7, 0 ) );
				Add( new GenericBuyInfo( "Cooked leg of lamb", typeof( LambLeg ), 8, 20, 0x160A, 0 ) );
				Add( new GenericBuyInfo( "Chicken leg", typeof( ChickenLeg ), 5, 20, 0x1608, 0 ) );
				Add( new GenericBuyInfo( "Cut of ribs", typeof( Ribs ), 7, 20, 0x9F2, 0 ) );

				Add( new GenericBuyInfo( "Bowl of carrots", typeof( WoodenBowlOfCarrots ), 3, 20, 0x15F9, 0 ) );
				Add( new GenericBuyInfo( "Bowl of corn", typeof( WoodenBowlOfCorn ), 3, 20, 0x15FA, 0 ) );
				Add( new GenericBuyInfo( "Bowl of lettuce", typeof( WoodenBowlOfLettuce ), 3, 20, 0x15FB, 0 ) );
				Add( new GenericBuyInfo( "Bowl of peas", typeof( WoodenBowlOfPeas ), 3, 20, 0x15FC, 0 ) );
				Add( new GenericBuyInfo( "Pewter bowl", typeof( EmptyPewterBowl ), 2, 20, 0x15FD, 0 ) );
				Add( new GenericBuyInfo( "Bowl of corn", typeof( PewterBowlOfCorn ), 3, 20, 0x15FE, 0 ) );
				Add( new GenericBuyInfo( "Bowl of lettuce", typeof( PewterBowlOfLettuce ), 3, 20, 0x15FF, 0 ) );
				Add( new GenericBuyInfo( "Bowl of peas", typeof( PewterBowlOfPeas ), 3, 20, 0x1600, 0 ) );
				Add( new GenericBuyInfo( "Bowl of potatos", typeof( PewterBowlOfPotatos ), 3, 20, 0x1601, 0 ) );
				Add( new GenericBuyInfo( "Bowl pf stew", typeof( WoodenBowlOfStew ), 3, 20, 0x1604, 0 ) );
				Add( new GenericBuyInfo( "Bowl of tomato soup", typeof( WoodenBowlOfTomatoSoup ), 3, 20, 0x1606, 0 ) );

				Add( new GenericBuyInfo( "Baked pie", typeof( ApplePie ), 7, 20, 0x1041, 0 ) );

				Add( new GenericBuyInfo( "A chessboard", typeof( Chessboard ), 2, 20, 0xFA6, 0 ) );
				Add( new GenericBuyInfo( "A checker board", typeof( CheckerBoard ), 2, 20, 0xFA6, 0 ) );
				Add( new GenericBuyInfo( "Backgammon game", typeof( Backgammon ), 2, 20, 0xE1C, 0 ) );
				Add( new GenericBuyInfo( "Dice and cup", typeof( Dices ), 2, 20, 0xFA7, 0 ) );
				Add( new GenericBuyInfo( "A contract of employment", typeof( ContractOfEmployment ), 1252, 20, 0x14F0, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( WoodenBowlOfCarrots ), 1 );
				Add( typeof( WoodenBowlOfCorn ), 1 );
				Add( typeof( WoodenBowlOfLettuce ), 1 );
				Add( typeof( WoodenBowlOfPeas ), 1 );
				Add( typeof( EmptyPewterBowl ), 1 );
				Add( typeof( PewterBowlOfCorn ), 1 );
				Add( typeof( PewterBowlOfLettuce ), 1 );
				Add( typeof( PewterBowlOfPeas ), 1 );
				Add( typeof( PewterBowlOfPotatos ), 1 );
				Add( typeof( WoodenBowlOfStew ), 1 );
				Add( typeof( WoodenBowlOfTomatoSoup ), 1 );
				Add( typeof( BeverageBottle ), 3 );
				Add( typeof( Jug ), 6 );
				Add( typeof( Pitcher ), 5 );
				Add( typeof( GlassMug ), 1 );
				Add( typeof( BreadLoaf ), 3 );
				Add( typeof( CheeseWheel ), 12 );
				Add( typeof( Ribs ), 6 );
				Add( typeof( Peach ), 1 );
				Add( typeof( Pear ), 1 );
				Add( typeof( Grapes ), 1 );
				Add( typeof( Apple ), 1 );
				Add( typeof( Banana ), 1 );
				Add( typeof( Candle ), 3 );
				Add( typeof( Chessboard ), 1 );
				Add( typeof( CheckerBoard ), 1 );
				Add( typeof( Backgammon ), 1 );
				Add( typeof( Dices ), 1 );
				Add( typeof( ContractOfEmployment ), 626 );
			}
		}
	}
}