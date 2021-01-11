using System;
using Server.Items;

namespace Server.Misc
{
	public class RegenRates
	{
		[CallPriority( 10 )]
		public static void Configure()
		{
			Mobile.DefaultHitsRate = TimeSpan.FromSeconds( 11.0 );
			Mobile.DefaultStamRate = TimeSpan.FromSeconds(  7.0 );
			Mobile.DefaultManaRate = TimeSpan.FromSeconds(  7.0 );

			Mobile.ManaRegenRateHandler = new RegenRateHandler( Mobile_ManaRegenRate );
		}

		private static void CheckBonusSkill( Mobile m, int cur, int max, SkillName skill )
		{
			if ( !m.Alive )
				return;

			double n = (double)cur / max;
			double v = Math.Sqrt( m.Skills[skill].Value * 0.005 );

			n *= (1.0 - v);
			n += v;

			m.CheckSkill( skill, n );
		}

		private static TimeSpan Mobile_ManaRegenRate( Mobile from )
		{
			if ( from.Skills == null )
				return Mobile.DefaultManaRate;

			if ( !from.Meditating )
				CheckBonusSkill( from, from.Mana, from.ManaMax, SkillName.Meditation );

			double rate;
			double armorPenalty = GetArmorOffset( from );
			double medPoints = (from.Int + from.Skills[SkillName.Meditation].Value) * 0.5;

			if ( medPoints <= 0 )
				rate = 7.0;
			else if ( medPoints <= 100 )
				rate = 7.0 - (239*medPoints/2400) + (19*medPoints*medPoints/48000);
			else if ( medPoints < 120 )
				rate = 1.0;
			else
				rate = 0.75;

			rate += armorPenalty;

            if ( from.Meditating )
				rate *= 0.5;

			if ( rate < 0.5 )
				rate = 0.5;
			else if ( rate > 7.0 )
				rate = 7.0;

			return TimeSpan.FromSeconds( rate );
		}

		public static double GetArmorOffset( Mobile from )
		{
			double rating = 0.0;

			rating += GetArmorMeditationValue( from.ShieldArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.NeckArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.HandArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.HeadArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.ArmsArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.LegsArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.ChestArmor as BaseArmor );

			return rating / 4;
		}

		private static double GetArmorMeditationValue( BaseArmor ar )
		{
			if ( ar == null )
				return 0.0;

			switch ( ar.MeditationAllowance )
			{
				default:
				case ArmorMeditationAllowance.None: return ar.BaseArmorRatingScaled;
				case ArmorMeditationAllowance.Half: return ar.BaseArmorRatingScaled / 2.0;
				case ArmorMeditationAllowance.All:  return 0.0;
			}
		}
	}
}