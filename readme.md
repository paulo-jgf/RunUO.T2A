All credits go to people behind RunUO, and to Grimoric. Grimoric's T2A RunUO is the very best ready to go T2A era UO emulator. In this fork I fixed a few minor things, like Magincia Moongate, some messages, a missing skill check for Resist Spells gain, but in the other hand I changed quite some things that made the server less T2A accurate: difficulty cap in stats gains, different difficulty for Healing skill gains, guarded Minoc Town Mines, custom timers for character deletion, stuck menu, corpse decay, few player only made/obtained items are sold by vendors to make the game more enjoyable if there is no live economy, etc.

How to start, as simples as it gets (Windows 7+, and will probably work the same for any RunUO/ServUO starts)
Quick note: RunUO requires Microsoft.NET Framework and may be some other things. If you have trouble with prerequisites please google around.

- Inside RunUO folder execute Compile.bat
- After done, execute RunUO.exe. Follow instructions, have in hand UO installation path (you can edit at RunUO\Scripts\Misc\Datapath.cs)
- Accept Firewall permissions. To connect use razor directed to 127.0.0.1 , port 2593

How to start your world:
- [UOAMVendors    				(Create most vendors, just a few are wrong. Magincia, Cove banker... may fix someday)
- [DoorGen        				(Create most doors. Guard outpost doors are missing, and a few others... may fix someday)
- [TelGen         				(Create teleporters, that connects world regions, dungeons, etc)
- [SignGen        				(Create world signs. Roads, shops, ways, etc)
- [xmlimportmap Data\Spawners\Monsters		(Monsters, wild life, town life, etc)
- [MoonGen                (Create Moongates)

How to let people in the server:
- If you are under a router you will need to create a rule (search for NAT, Virtual Server, etc in you router configuration) that directs 2593 port (TCP protocol) to the local address of the PC running RunUO.
- Open RunUO\Scripts\Misc\ServerList.cs and change Address = null; null value to your IPv4 public address.
- Provide people you want in your public IP address, and it should work.

A little help to get you going with adjustments, if you want to:
- Guarded Regions: RunUO\Data\Regions.xml
- Stats cap: Server\Mobile.cs m_StatCap = 225 (more than one line to change)
- Skills cap: Server\Skills.cs m_Cap = 7000 (more than one line to change)
- Corpses decay timer: Scripts\Items\Misc\ Corpse.cs and DecayedCorpse.cs
  m_DefaultDecayTime
  m_BoneDecayTime
  m_DefaultDecayTime (DecayedCorpse.cs)
