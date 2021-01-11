INICIAR SHARD DO ZERO

- Abrir o Compile, se não houver um arquivo RunUO.exe
- Rodar o RunUO.exe, ele deve localizar automaticamente os arquivos do UO. Se não digitar o caminho da instalação do UO
- Se o último passo der errado, é possível editar o caminho dos arquivos do UO aqui: RunUO\Scripts\Misc\Datapath.cs
- Liberar Firewall do Windows, quando aparecer a janela.
- Conectar com o razor no endereço local 127.0.0.1

OBS: É interessante saber que personagens são instâncias da classe Mobile, assim como demais criaturas,
     mas personagens são player = true, algo assim. Como sou noob em RunUO registro isso para a posteridade.

------------------------------------------------------------------------------------------
SPAWN DO MUNDO (C:\RunUO\Scripts\Commands)

- [UOAMVendors    				(Cria vendors)
- [DoorGen        				(Cria portas)
- [TelGen         				(Cria conexões entre areas - entrar caverna, esgoto, etc
- [SignGen        				(Cria placas de lojas, estradas, etc)
- [xmlimportmap Data\Spawners\Monsters		(Cria monstros e bichos)
- [MoonGen                                      (Cria Moongates)


OBS: XML Spawner é muito louco, não sei quase nada sobre (https://github.com/SpagMonster/XMLSpawner/blob/master/XMLSpawner%20Orb%202.2/Instructions%20for%20importing%20maps.txt)
É possível adicionar Spawners no mundo com interface, e usar o campo de escrita para buscar as criaturas

Ajustar guardas: RunUO\Data\Regions.xml

------------------------------------------------------------------------------------------
ARRUMAR CONEXÃO PARA OUTRAS PESSOAS

- O último passo deve ser feito toda vez que o IP externo mudar. O segundo passo deve ser feito
  toda vez que o IP interno mudar.

- Configurar PortForward no Roteador, redirecionar a porta do jogo para o IP interno correto

- Seguir estas instruções em Inglês (não necessário):
Go into your RunUO directory where the RunUO.exe file is. Once there right click the file, 
go into properties and set this file to "Run As Administrator" don't apply yet! Under that setting there is a 
box for "Change setting for all users" Click that and make sure you run this executable as admin for ALL users... 

- Editar o arquivo Scripts\Misc\ServerList.cs
  Mudar:
  public static readonly string Address = null;
  para (o IP é um exemplo, use ali o seu IP ou hostname):
  public static readonly string Address = "177.43.17.244";

- Creditos: http://www.runuo.com/community/threads/cant-connect.529708/page-2
	    LocoDino
------------------------------------------------------------------------------------------
AJUSTES DE JOGO

-Stats cap: Server\Mobile.cs
 m_StatCap = 225

-Skills cap: Server\Skills.cs
 m_Cap = 7000
faça busca por public Skills(
Substituir em todos os campos que tiverem m_Cap = 7000

- Ganho de skills (global) e stats (global), e cap individual de stats: Scripts\Misc\SkillCheck.cs
  Delay para subir stats: m_StatGainDelay
  Perto desse há o delay para pets m_PetStatGainDelay
  Stats cap está na mesma função, buscar por < 125 dentro da função CanRaise(
  Tudo isso está dentro da mesma função
  Nesse mesmo arquivo há a possibilidade de ligar ou desligar os anti macros nativos,
  é uma lista de booleanos logo no começo.
 
- Stuck Menu: por default o jogo só deixa usar uma vez por dia. A informação de uso fica guardada
  em um state do Mobile: Server\Mobile.cs
  para mudar isso buscar CanUseStuckMenu() dentro do Mobile.cs

- Tempo que o corpo fica depois de morto: mexer em Scripts\Items\Misc\ Corpse.cs e DecayedCorpse.cs
  m_DefaultDecayTime
  m_BoneDecayTime
  m_DefaultDecayTime (DecayedCorpse.cs)
	
OBS: Se um dia quiser fazer arquivos de config para o RunUO como no ServUO, que tem uma pasta Config
Usar como exemplo o Server\Movile.cs e ver o m_StatCap no começo da função DefaultMobileInit()

------------------------------------------------------------------------------------------
GIT
git update-index --assume-unchanged ./Scripts/Misc/ServerList.cs
Para não ficar upando o seu IP
------------------------------------------------------------------------------------------

COMANDOS ADMIN
Todos começam com [ exemplo: [add
Admin Commands

- Como descobrir itemID?
Type [props or [get itemid then target the item you want to know about.
You can also search to find them with Fiddler.


Add - Creates the variable object
Admin - Lists the Accounts with Administrator rights currently logged in.
Animate - Causes the Target to perform the designated animation.
APN - Toggles On/Off Auto Page Notify
AutoPageNotify - Toggles On/Off Auto Page Notify
B - Broadcast the message to all accounts currently logged in
Ban - Bans the targetted player
Bank - Opens the target's bank box
BC - Broadcast the message to all accounts currently logged in
BCAST - Broadcast the message to all accounts currently logged in
Cast - Auto-Casts the designated spell
Client - Displays Target Client Version information
Delete - Deletes targetted object
Dismount - Dismounts from steed
DocGen - Generates Documents
Dupe - Duplicates the targetted object
DupeInBag - Duplicates the targetted object in bag
Firewall - Displays Firewall information
Get - Displays Property information
GetSkill - Displays skill information
Go - Opens 'Go' gump menu
Grab - Optional player loot package
GuildProps - Displays target's guild information
Help - Displays Help gump
Hide - Hides target
Immortal - Causes the target to become invulnerable
IncX - Adds/Subtracts from the targets X coordinate
IncXYZ - Adds/Subtracts from the targets X/Y/Z coordinates
IncY - Adds/Subtracts from the targets Y coordinate
IncZ - Adds/Subtracts from the targets Z coordinate
Invul - Causes the target to become invulnerable
Kick - Disconnects Target
Kill - Kills the Target
Mortal - Causes the target to become vulnerable / mortal
MOTD - Edits/Displays the Message of the Day
Move - Moves target object/player to selected position
NewX - Sets the target object/players X coordinate
NewXYZ - Sets the target object/players XYZ coordinates
NewY - Sets the target object/players Y coordinate
NewZ - Sets the target object/players Z coordinate
NoInvul - Causes the target to become vulnerable / mortal
Objlist - Generates an html document listing all objects currently spawned on the shard
Pages - Displays GM pages
Props - Displays Property information
Recompile - Recompiles Scripts
Remove - Deletes targetted object
Res - Resurrects Target Player (would be nice if someone made npcs ress-able as well)
Resurrect - Resurrects Target Player (would be nice if someone made npcs ress-able as well)
S - Sends variable message to all currently logged in staff
Save - Causes the world to save
Set - Sets variable property to target
SetSkill - Sets variable skill on target
ShaveBeard - Shaves targets beard
ShaveHair - Shaves targets hair
SignGen - Generates all world signs using signs.cfg
Skills - Displays a skill menu
SM - Sends variable message to all currently logged in staff
SMSG - Sends variable message to all currently logged in staff
Stuck - Used by players when physically unable to move due to map/script flaw
Tele - Teleports user to target location
Teleport - Teleports user to target location
TelGen - Generates standard world teleporters
Tile - Tiles variable object in a bounding box
Unhide - Unhides target
Who - Displays a list of all currently logged in users
Wholist - Displays a list of all currently logged in users
Wipe - Destroys all objects in a bounding box
WipeItems - Destroys all items (in a bounding box)
WipeNPCs - Kills all NPCs (in a bounding box)
WipMultis - Destroys all multis (in a bounding box)

Set locked true  (Tranca uma porta GM)
allspells        (Completa spellbook) 
hide
unhide

------------------------------------------------------------------------------------------

RunUO Q&A
Q: How can I log in to my RunUO shard?
A: Go to your Ultima Online folder, search for a file called login.cfg! Open it. Now locate this line of code. LoginServer=x.x.x.x,xxxx Remove all of it and type this in there LoginServer=127.0.0.1,2593. You can now connect to your server
Q: Where can I change my RunUO Server name?
A: Go to Scripts\Misc\ServerList.cs and there you will find "RunUO Test center". Modify that to whatever you wish your shard name to be.
OR
1.Browse to RunUO\Scripts\ServerList.cs and open it up in Notepad (or your favorite text editor).
2.You'll see the following line: public static readonly string servername = "Name of shard Here"
3.Simply change "Name of shard Here" to whatever you'd like your shard to be called and save
Q: What program I should use for scripting C+?
A: I really recommend you to use Notepad++ text editor. You can get Notepad++
OR

Visual Studio Express
Q: Where can I download RunUO/Ultima Online scripts?
A: http://www.runuo.net/scripts/ has a nice selection of scripts you can add to your shard
Q: How To Make Your RunUO Server public [Tutorial]
How To Make Your RunUO Server public - external Connection:
A: If you want to make it public; go to into Scripts/Misc/serverlist.cs Edit with whatever you want(its a text file), then you just look for this:
public class ServerList
{
public static readonly string Address = null;
public static readonly string ServerName = null;
public static readonly bool AutoDetect = true;
public static void Initialize()
{
AND CHANGE IT the first null should be your IP address in hiphens "127.0.0.123" thats an example the next red null should be changed to the name you want your shard in hiphens "example MY TEST SHARD" so it would look like this:
public class ServerList
{
public static readonly string Address = "127.0.0.123";
public static readonly string ServerName = "MY TEST SHARD";
public static readonly bool AutoDetect = true;
public static void Initialize()
{
It is public now, boot up and run UO Server
Q: How to I set up my shard to auto create player accounts?

A: You should run RunUO.exe at least once from the command line to create the owner account. Then look for this line of code and set the Auto Account Creation to "true"

public class AccountHandler
{
private static int MaxAccountsPerIP = 1;
private static bool AutoAccountCreation = true;
private static bool RestrictDeletion = !TestCenter.Enabled;
Q: How do I disable UO Era Expansions?

A: One of the first thing many users like to do is to disable certain features added to Ultima Online brought fourth by other expansions.

These values are controlled thru /Scripts/Misc/Currentexpansion.cs

To control what Expansion era Ultima Online you want present search for the following line.

private static readonly Expansion Expansion = Expansion.ML;

Leaving the value at default will give you all the features leading up to and including Mondain's Legacy.

For Samurai Empire UO replace the line with the following:
private static readonly Expansion Expansion = Expansion.SE;
For Age of Shadows UO replace the line with the following:
private static readonly Expansion Expansion = Expansion.AOS;
For Lord Blackthorns Revenge UO replace the line with the following:
private static readonly Expansion Expansion = Expansion.LBR;
For UO:R UO replace the line with the following:
private static readonly Expansion Expansion = Expansion.None;
Q: How do I Disable the UO Young Player System?

A: To disable the Young Player System open up /Scripts/Misc/CharacterCreation.Cs

find the line:
bool young = true;
replace it with:
bool young=false;
then find the line:
if ( pm.AccessLevel == AccessLevel.Player && ((Account)pm.Account).Young )
young = pm.Young = true;
replace it with:
if ( pm.AccessLevel == AccessLevel.Player && ((Account)pm.Account).Young )
young = pm.Young = false;

------------------------------------------------------------------------------------------

RunUO Contructor Guide
Original post by Ezekiel

This guide will cover the basic in-game commands necessary for constructing items with basic & advanced syntax input.

Adding Items
To add an item in the game you must use the proper syntax, and the correct item name in order for the server to recognize your command. This can be done in many ways, simple and advanced. Here we will cover how to add items in both ways, one at a time.

Add Commands
[addmenu - Using this syntax alone will bring up an alternate version of the "add menu" in which you can input search text to find items by name and their proper syntax for addition. This is very useful for finding the add-name of an item you wish to add but do not know the proper syntax of the item by memory.

[m add - This command cannot be used alone, it is a "multiplier" command which allows you to retain your target cursor after the first addition, so that you may continuously "click-add" items in repetition, instead of having to re-type or ctrl-q the command syntax.

[adddoor - Using this command will prompt a gump which allows you to add a variety of doors, which come complete with door functions.

Now lets use these commands in an example, both basic & advanced syntax additions.

Add

[add sandals - This will add a pair of sandals at the location you click.

[addtopack sandals - This will add a pair of sandals into the container or player's backpack of your choice.

Now lets try the same commands with additional parameters added for additional settings.

[add sandals set hue 1 loottype blessed weight 11 - This will add a pair of sandals with the hue 1 (black), which are blessed & weigh 11 stones.

[addtopack sandals set hue 1 loottype blessed weight 11 - This will add a pair of black sandals, which are blessed & weigh 11 stones, into the container or player's backpack of your choice.

[m add sandals set hue 1 loottype blessed weight 11 - Same effect as above, except you will not lose your target cursor and can add multiples of the item without having to re-write your command line.

[m addtopack sandals set hue 1 loottype blessed weight 11 - Same effect as above, except you will not lose your target cursor and can add multiples of the item without having to re-write your command line.

Addmenu

This is a very nifty command which will help you find items you do not know how to properly spell or what their add-names are. This is a rather basic command. To utilize this command, all you need to do is type [addmenu <what you are looking for>

So, if you were trying to add a potted plant but are unsure of the proper name syntax or type, you could utilize this command like this.

[addmenu potted - This command will open the add-menu gump and match all item names with the term "potted" in them. You can refine this search query to shorter or longer text matching at your discretion.

[addmenu pot [addmenu pottedplant [addmenu plant [addmenu tree

Etc, and so forth. Tinkering with it for a short time should give you the full feel of how it works, its basically your dictionary for finding item-names (not statics).

Now that we've covered the basics of item construction, let's move on to more complex construction which will save your countless hours of time & clicking your mouse all over the place. There are only a couple of commands in this category, but setting additional parameters to them will increase your array of capabilities tremendously.

Advanced Construction Commands
[area - Invokes settings to everything caught within the bounding box of the effect. Very powerful & useful tool for invoking properties on a large area of things at once.

[tile - A large-scale adding tool. Generates items in a bounding box grid, at one item per tile for the full bounding box you determine.

Let's cover the [tile command first, since it is actually an item generation command, where-as [area is not and is mostly a support command to help finish your construction properties, but does not actually add items itself. Here are some examples of executing the [tile command in both basic & advanced syntax, under the same general concepts as listed above.

[tile sandals - This will generate sandals on every tile (one per tile) for the entire bounding box area you provide. Upon using the command, you will be prompted to target the first location of the bounding box, and then the second. Your first target is "corner 1" and the second target is "corner 2". Once completed, the system will generate the item you specified on each tile within these bounds. Be cautious with this command, attempting to add items within a very large bounds may cause server lag or a potential crash. It is not recommended to tile items in a large radius unless you know exactly what you are doing.

[tile sandals set hue 1 loottype blessed weight 11 - This command will net you the same results as above, except now all of the sandals you create will be of a black hue, blessed loottype, and weigh 11 stones.

The "add" & "tile" commands work with statics as well, I am simply using items as a demonstration purpose. You can just as easily use [tile static 1313 set hue 1 movable true, if you wanted to add black cobblestone tiles in an area which were movable for players to pick up.

Lastly, we will cover the area command. This command is mostly a support modifier, as it does not add items itself, it only invokes properties onto items caught within the bounds. This is most useful in the scenario where you are building construction in an area, and need to invoke additional properties onto a large amount of items which are not part of their default settings. A good example of this is hue-ing floor tiles to a different color of that of their default. Now let's use some examples.

[area set hue 1 - Using this command will prompt you to connect 2 locations for your bounding box, exactly the same as the [tile command. Once specified, everything caught within the radius will be set to hue 1, black.

Sometimes you may have a large array of items/statics within a certain area, stacked on top of each-other, or just arranged in a certain fashion where you cannot tag them separately from other items you do not wish to be tagged within the bounds. Adding additional parameters to this will allow you to exclude items from the tagging. Let's try an example.

[area set hue 1 where item itemid = 1313 - After specifying your bounding box, this command will hue all items 1 (black) within the bounding box, but only if their itemID matches the parameters entered above, 1313. So, in this instance you are able to hue all floor tiles (1313) black, but anything else caught within the bounds will not be affected. This command can be used in many various ways with these parameters to achieve all kinds of different results.

------------------------------------------------------------------------------------------