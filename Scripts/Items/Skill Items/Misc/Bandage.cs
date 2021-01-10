using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
    public class Bandage : Item, IDyable
	{
		public static int Range = 1;

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public Bandage() : this( 1 )
		{
		}

		[Constructable]
		public Bandage( int amount ) : base( 0xE21 )
		{
			Stackable = true;
			Amount = amount;
		}

		public Bandage( Serial serial ) : base( serial )
		{
		}

		public virtual bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
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

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( GetWorldLocation(), Range ) )
			{
				from.RevealingAction();

				from.SendLocalizedMessage( 500948 ); // Who will you use the bandages on?

				from.Target = new InternalTarget( this );
			}
			else
			{
				from.SendLocalizedMessage( 500295 ); // You are too far away to do that.
			}
		}

		private class InternalTarget : Target
		{
			private Bandage m_Bandage;

			public InternalTarget( Bandage bandage ) : base( Bandage.Range, false, TargetFlags.Beneficial )
			{
				m_Bandage = bandage;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Bandage.Deleted )
					return;

				if ( targeted is Mobile )
				{
					if ( from.InRange( m_Bandage.GetWorldLocation(), Bandage.Range ) )
					{
						if ( BandageContext.BeginHeal( from, (Mobile)targeted ) != null )
						{
							m_Bandage.Consume();
						}
					}
					else
					{
						from.SendLocalizedMessage( 500295 ); // You are too far away to do that.
					}
				}
				else
				{
					from.SendLocalizedMessage( 500970 ); // Bandages can not be used on that.
				}
			}
		}
	}

	public class BandageContext
	{
		private Mobile m_Healer;
		private Mobile m_Patient;
		private int m_Slips;
		private Timer m_Timer;

		public Mobile Healer{ get{ return m_Healer; } }
		public Mobile Patient{ get{ return m_Patient; } }
		public int Slips{ get{ return m_Slips; } set{ m_Slips = value; } }
		public Timer Timer{ get{ return m_Timer; } }

		public void Slip()
		{
			m_Healer.SendLocalizedMessage( 500961 ); // Your fingers slip!
			++m_Slips;
		}

		public BandageContext( Mobile healer, Mobile patient, TimeSpan delay )
		{
			m_Healer = healer;
			m_Patient = patient;

			m_Timer = new InternalTimer( this, delay );
			m_Timer.Start();
		}

		public void StopHeal()
		{
			m_Table.Remove( m_Healer );

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;
		}

		private static Dictionary<Mobile, BandageContext> m_Table = new Dictionary<Mobile, BandageContext>();

		public static BandageContext GetContext( Mobile healer )
		{
			BandageContext bc = null;
			m_Table.TryGetValue( healer, out bc );
			return bc;
		}

		public static SkillName GetPrimarySkill( Mobile m )
		{
			if ( !m.Player && (m.Body.IsMonster || m.Body.IsAnimal) )
				return SkillName.Veterinary;
			else
				return SkillName.Healing;
		}

		public static SkillName GetSecondarySkill( Mobile m )
		{
			if ( !m.Player && (m.Body.IsMonster || m.Body.IsAnimal) )
				return SkillName.AnimalLore;
			else
				return SkillName.Anatomy;
		}

		public void EndHeal()
		{
			StopHeal();

			int healerNumber = -1, patientNumber = -1;
			bool playSound = true;
			bool checkSkills = false;
      double bonus = 0.0;

			SkillName primarySkill = GetPrimarySkill( m_Patient );
			SkillName secondarySkill = GetSecondarySkill( m_Patient );

			if ( !m_Healer.Alive )
			{
				healerNumber = 500962; // You were unable to finish your work before you died.
				patientNumber = -1;
				playSound = false;
			}
			else if ( !m_Healer.InRange( m_Patient, Bandage.Range ) )
			{
				healerNumber = 500963; // You did not stay close enough to heal your target.
				patientNumber = -1;
				playSound = false;
			}
			else if ( !m_Patient.Alive )
			{
				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = (healing - 68.0) / 50.0 - m_Slips * 0.02;

				if ( (checkSkills = healing >= 80.0 && anatomy >= 80.0) && chance > Utility.RandomDouble() )	//TODO: Dbl check doesn't check for faction of the horse here?
				{
					if ( m_Patient.Map == null || !m_Patient.Map.CanFit( m_Patient.Location, 16, false, false ) )
					{
						healerNumber = 501042; // Target can not be resurrected at that location.
						patientNumber = 502391; // Thou can not be resurrected there!
					}
					else
					{
						healerNumber = 500965; // You are able to resurrect your patient.
						patientNumber = -1;
            bonus = 30.0; // Res will give a skill gain bonus

						m_Patient.PlaySound( 0x214 );
						m_Patient.FixedEffect( 0x376A, 10, 16 );

						m_Patient.CloseGump( typeof( ResurrectGump ) );
						m_Patient.SendGump( new ResurrectGump( m_Patient, m_Healer ) );
					}
				}
				else
				{
					healerNumber = 500966; // You are unable to resurrect your patient.
					patientNumber = -1;
				}
			}
			else if ( m_Patient.Poisoned )
			{
				m_Healer.SendLocalizedMessage( 500969 ); // You finish applying the bandages.

				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = (healing - 30.0) / 50.0 - m_Patient.Poison.Level * 0.1 - m_Slips * 0.02;

				if ( (checkSkills = healing >= 60.0 && anatomy >= 60.0) && chance > Utility.RandomDouble() )
				{
					if ( m_Patient.CurePoison( m_Healer ) )
					{
						healerNumber = m_Healer == m_Patient ? -1 : 1010058; // You have cured the target of all poisons.
						patientNumber = 1010059; // You have been cured of all poisons.
            bonus = 20.0; // Cure will give a skill gain bonus
					}
					else
					{
						healerNumber = -1;
						patientNumber = -1;
					}
				}
				else
				{
					healerNumber = 1010060; // You have failed to cure your target!
					patientNumber = -1;
				}
			}
			else if ( m_Patient.Hits == m_Patient.HitsMax )
			{
				healerNumber = 500967; // You heal what little damage your patient had.
				patientNumber = -1;
			}
			else
			{
				checkSkills = true;
				patientNumber = -1;

				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = (healing + 10.0) / 100.0 - m_Slips * 0.02;

				if ( chance > Utility.RandomDouble() )
				{
					healerNumber = 500969; // You finish applying the bandages.

					double min, max;

					min = anatomy / 5.0 + healing / 5.0 + 3.0;
					max = anatomy / 5.0 + healing / 2.0 + 10.0;

					double toHeal = min + Utility.RandomDouble() * (max - min);

					if ( m_Patient.Body.IsMonster || m_Patient.Body.IsAnimal )
						toHeal += m_Patient.HitsMax / 100;

					toHeal -= m_Slips * 4;

					if ( toHeal < 1 )
					{
						toHeal = 1;
						healerNumber = 500968; // You apply the bandages, but they barely help.
					}

					m_Patient.Heal( (int) toHeal, m_Healer, false );
				}
				else
				{
					healerNumber = 500968; // You apply the bandages, but they barely help.
					playSound = false;
				}
			}

			if ( healerNumber != -1 )
				m_Healer.SendLocalizedMessage( healerNumber );

			if ( patientNumber != -1 )
				m_Patient.SendLocalizedMessage( patientNumber );

			if ( playSound )
				m_Patient.PlaySound( 0x57 );

      // This is the only original Healing checkSkill, it is very painfull to gain, original "gaincap value" is 120.0
      // Let's add a bonus to healing poison and ressurecting, and lower the gaincap to 100.0 to make it less hard
			if ( checkSkills )
			{
				m_Healer.CheckSkill( secondarySkill, 0.0, 120.0 );
				//m_Healer.CheckSkill( primarySkill, 0.0, 120.0 );
        m_Healer.CheckSkill( primarySkill, 0.0, 100.0 - bonus);
			}
		}

		private class InternalTimer : Timer
		{
			private BandageContext m_Context;

			public InternalTimer( BandageContext context, TimeSpan delay ) : base( delay )
			{
				m_Context = context;
				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				m_Context.EndHeal();
			}
		}

		public static BandageContext BeginHeal( Mobile healer, Mobile patient )
		{
			if ( !patient.Poisoned && patient.Hits == patient.HitsMax )
			{
				healer.SendLocalizedMessage( 500955 ); // That being is not damaged!
			}
			else if ( !patient.Alive && (patient.Map == null || !patient.Map.CanFit( patient.Location, 16, false, false )) )
			{
				healer.SendLocalizedMessage( 501042 ); // Target cannot be resurrected at that location.
			}
			else if ( healer.CanBeBeneficial( patient, true, true ) )
			{
				healer.DoBeneficial( patient );

				bool onSelf = healer == patient;
				int dex = healer.Dex;

				double seconds;
				double resDelay = patient.Alive ? 0.0 : 5.0;

				if ( onSelf )
				{
					seconds = 9.4 + 0.6 * ((double)(120 - dex) / 10);
				}
				else
				{
					if ( dex >= 100 )
						seconds = 3.0 + resDelay;
					else if ( dex >= 40 )
						seconds = 4.0 + resDelay;
					else
						seconds = 5.0 + resDelay;
				}

				BandageContext context = GetContext( healer );

				if ( context != null )
					context.StopHeal();
				seconds *= 1000;

				context = new BandageContext( healer, patient, TimeSpan.FromMilliseconds( seconds ) );

				m_Table[healer] = context;

				if ( !onSelf )
					patient.SendLocalizedMessage( 1008078, false, healer.Name ); //  : Attempting to heal you.

				healer.SendLocalizedMessage( 500956 ); // You begin applying the bandages.
				return context;
			}

			return null;
		}
	}
}
