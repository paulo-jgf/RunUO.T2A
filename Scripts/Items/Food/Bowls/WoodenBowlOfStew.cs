namespace Server.Items
{
    public class WoodenBowlOfStew : Food
    {
        [Constructable]
        public WoodenBowlOfStew() : base( 0x1604 )
        {
            Stackable = false;
            Weight = 2.0;
            FillFactor = 2;
        }

        public override bool Eat( Mobile from )
        {
            if ( !base.Eat( from ) )
                return false;

            from.AddToBackpack( new EmptyWoodenTub() );
            return true;
        }

        public WoodenBowlOfStew( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 0 ); // version
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();
        }
    }
}