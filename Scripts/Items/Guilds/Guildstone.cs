using System;
using Server.Gumps;
using Server.Guilds;
using Server.Network;
using Server.Multis;

namespace Server.Items
{
    public class Guildstone : Item, IAddon, IChopable
	{
		private Guild m_Guild;
		private string m_GuildName;
		private string m_GuildAbbrev;

		[CommandProperty( AccessLevel.GameMaster )]
		public string GuildName
		{
			get { return m_GuildName; }
			set { m_GuildName = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string GuildAbbrev
		{
			get { return m_GuildAbbrev; }
			set { m_GuildAbbrev = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Guild Guild
		{
			get
			{
				return m_Guild;
			}
		}

		public override int LabelNumber { get { return 1041429; } } // a guildstone

		public Guildstone( Guild g ) : this( g, g.Name, g.Abbreviation )
		{
		}

		public Guildstone( Guild g, string guildName, string abbrev ) : base( 0xED4 )
		{
			m_Guild = g;
			m_GuildName = guildName;
			m_GuildAbbrev = abbrev;

			Movable = false;
		}

		public Guildstone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			if( m_Guild != null && !m_Guild.Disbanded )
			{
				m_GuildName = m_Guild.Name;
				m_GuildAbbrev = m_Guild.Abbreviation;
			}

			writer.Write( (int)3 ); // version

			writer.Write( m_BeforeChangeover );

			writer.Write( m_GuildName );
			writer.Write( m_GuildAbbrev );

			writer.Write( m_Guild );
		}

		private bool m_BeforeChangeover;
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch( version )
			{
				case 3:
				{
					m_BeforeChangeover = reader.ReadBool();
					goto case 2;
				}
				case 2:
				{
					m_GuildName = reader.ReadString();
					m_GuildAbbrev = reader.ReadString();

					goto case 1;
				}
				case 1:
				{
					m_Guild = reader.ReadGuild() as Guild;

					goto case 0;
				}
				case 0:
				{
					break;
				}
			}

			if( version <= 2 )
				m_BeforeChangeover = true;

			if( m_Guild == null )
				this.Delete();
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			if( m_Guild != null && !m_Guild.Disbanded )
			{
				string name;

				if( (name = m_Guild.Name) == null || (name = name.Trim()).Length <= 0 )
					name = "(unnamed)";

				this.LabelTo( from, name );
			}
			else if( m_GuildName != null )
			{
				this.LabelTo( from, m_GuildName );
			}
		}

		public override void OnAfterDelete()
		{
			if( m_Guild != null && !m_Guild.Disbanded )
				m_Guild.Disband();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( m_Guild == null || m_Guild.Disbanded )
			{
				Delete();
			}
			else if( !from.InRange( GetWorldLocation(), 2 ) )
			{
				from.SendLocalizedMessage( 500446 ); // That is too far away.
			}
			else if( m_Guild.Accepted.Contains( from ) )
			{
				m_Guild.Accepted.Remove( from );
				m_Guild.AddMember( from );

				GuildGump.EnsureClosed( from );
				from.SendGump( new GuildGump( from, m_Guild ) );
			}
			else if( from.AccessLevel < AccessLevel.GameMaster && !m_Guild.IsMember( from ) )
			{
				from.Send( new MessageLocalized( Serial, ItemID, MessageType.Regular, 0x3B2, 3, 501158, "", "" ) ); // You are not a member ...
			}
			else
			{
				GuildGump.EnsureClosed( from );
				from.SendGump( new GuildGump( from, m_Guild ) );
			}
		}

		#region IAddon Members
		public Item Deed
		{
			get { return new GuildstoneDeed( m_Guild, m_GuildName, m_GuildAbbrev ); }
		}

		public bool CouldFit( IPoint3D p, Map map )
		{
			return map.CanFit( p.X, p.Y, p.Z, this.ItemData.Height );
		}

		#endregion

		#region IChopable Members

		public void OnChop( Mobile from )
		{
		}

		#endregion
	}

	[Flipable( 0x14F0, 0x14EF )]
	public class GuildstoneDeed : Item
	{
		public override int LabelNumber { get { return 1041233; } } // deed to a guildstone

		private Guild m_Guild;
		private string m_GuildName;
		private string m_GuildAbbrev;

		[CommandProperty( AccessLevel.GameMaster )]
		public string GuildName
		{
			get { return m_GuildName; }
			set { m_GuildName = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string GuildAbbrev
		{
			get { return m_GuildAbbrev; }
			set { m_GuildAbbrev = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Guild Guild
		{
			get
			{
				return m_Guild;
			}
		}

		[Constructable]
		public GuildstoneDeed() : this( null, null )
		{
		}

		[Constructable]
		public GuildstoneDeed( string guildName, string abbrev ) : this( null, guildName, abbrev )
		{
		}

		public GuildstoneDeed( Guild g, string guildName, string abbrev ) : base( 0x14F0 )
		{
			m_Guild = g;
			m_GuildName = guildName;
			m_GuildAbbrev = abbrev;

			Weight = 1.0;
		}

		public GuildstoneDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			if( m_Guild != null && !m_Guild.Disbanded )
			{
				m_GuildName = m_Guild.Name;
				m_GuildAbbrev = m_Guild.Abbreviation;
			}

			writer.Write( (int)1 ); // version

			writer.Write( m_GuildName );
			writer.Write( m_GuildAbbrev );

			writer.Write( m_Guild );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_GuildName = reader.ReadString();
					m_GuildAbbrev = reader.ReadString();

					m_Guild = reader.ReadGuild() as Guild;

					break;
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( IsChildOf( from.Backpack ) )
			{
				BaseHouse house = BaseHouse.FindHouseAt( from );

				if( house != null && house.IsOwner( from ) )
				{
					from.SendLocalizedMessage( 1062838 ); // Where would you like to place this decoration?
					from.BeginTarget( -1, true, Targeting.TargetFlags.None, new TargetStateCallback( Placement_OnTarget ), null );
				}
				else
				{
					from.SendLocalizedMessage( 502092 ); // You must be in your house to do this.
				}
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		public void Placement_OnTarget( Mobile from, object targeted, object state )
		{
			IPoint3D p = targeted as IPoint3D;

			if( p == null || Deleted )
				return;

			Point3D loc = new Point3D( p );

			BaseHouse house = BaseHouse.FindHouseAt( loc, from.Map, 16 );

			if( IsChildOf( from.Backpack ) )
			{
				if( house != null && house.IsOwner( from ) )
				{
					Item addon = new Guildstone( m_Guild, m_GuildName, m_GuildAbbrev );

					addon.MoveToWorld( loc, from.Map );

					house.Addons.Add( addon );
					Delete();
				}
				else
				{
					from.SendLocalizedMessage( 1042036 ); // That location is not in your house.
				}
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}
	}
}
