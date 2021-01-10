using System;
using System.Collections.Generic;
using Server.Network;

namespace Server.Misc
{
    public class AttackMessage
	{
		private const string AggressorFormat = "You are attacking {0}!";
		private const string AggressedFormat = "{0} is attacking you!";
    // Format inspired on Old UO, using both aggressor and aggressed names
    private const string AggressionOldFormat = "{0} is attacking {1}!";
		private const int Hue = 0x22;

		private static TimeSpan Delay = TimeSpan.FromMinutes( 1.0 );

		public static void Initialize()
		{
			EventSink.AggressiveAction += new AggressiveActionEventHandler( EventSink_AggressiveAction );
		}

		public static void EventSink_AggressiveAction( AggressiveActionEventArgs e )
		{
			Mobile aggressor = e.Aggressor;
			Mobile aggressed = e.Aggressed;

      // Show combat message only for PVP combat
			//if ( !aggressor.Player || !aggressed.Player )
			//	return;

			if ( !CheckAggressions( aggressor, aggressed ) )
			{
        //Default RunUO shows only local messages for attacking
				//aggressor.LocalOverheadMessage( MessageType.Regular, Hue, true, String.Format( AggressorFormat, aggressed.Name ) );
				//aggressed.LocalOverheadMessage( MessageType.Regular, Hue, true, String.Format( AggressedFormat, aggressor.Name ) );

        // Old style UO is Public overhead on attacker!
        aggressor.PublicOverheadMessage( MessageType.Regular, Hue, true, String.Format( AggressionOldFormat, aggressor.Name, aggressed.Name ) );
			}
		}

		public static bool CheckAggressions( Mobile m1, Mobile m2 )
		{
			List<AggressorInfo> list = m1.Aggressors;

			for ( int i = 0; i < list.Count; ++i )
			{
				AggressorInfo info = list[i];

				if ( info.Attacker == m2 && DateTime.Now < info.LastCombatTime + Delay )
					return true;
			}

			list = m2.Aggressors;

			for ( int i = 0; i < list.Count; ++i )
			{
				AggressorInfo info = list[i];

				if ( info.Attacker == m1 && DateTime.Now < info.LastCombatTime + Delay )
					return true;
			}

			return false;
		}
	}
}
