namespace Server.Items
{
    public class WizardsHat : BaseHat
    {
        public override int InitMinHits{ get{ return 20; } }
        public override int InitMaxHits{ get{ return 30; } }

        [Constructable]
        public WizardsHat() : this( 0 )
        {
        }

        [Constructable]
        public WizardsHat( int hue ) : base( 0x1718, hue )
        {
            Weight = 1.0;
        }

        public WizardsHat( Serial serial ) : base( serial )
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