using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x0200006E RID: 110
	internal static class StoryCast
	{
		// Token: 0x06001299 RID: 4761 RVA: 0x00122D48 File Offset: 0x00120F48
		public static bool TryGet(string modelLower, out StoryCast.Person p)
		{
			return StoryCast.People.TryGetValue(modelLower ?? "", out p);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00122D60 File Offset: 0x00120F60
		// Note: this type is marked as 'beforefieldinit'.
		static StoryCast()
		{
			Dictionary<string, StoryCast.Person> dictionary = new Dictionary<string, StoryCast.Person>();
			dictionary["cs_dutch"] = new StoryCast.Person("Dutch van der Linde", "Orgulloso", "siempre tiene un plan", "vdl");
			dictionary["cs_hoseamatthews"] = new StoryCast.Person("Hosea Matthews", "Leal", "la conciencia de la banda", "vdl");
			dictionary["cs_johnmarston"] = new StoryCast.Person("John Marston", "Errante", "inquieto, marcado por cicatrices", "vdl");
			dictionary["cs_javierescuella"] = new StoryCast.Person("Javier Escuella", "Leal", "seguiría a Dutch a cualquier parte", "vdl");
			dictionary["cs_charlessmith"] = new StoryCast.Person("Charles Smith", "Leal", "rastreador callado y honrado", "vdl");
			dictionary["cs_micahbell"] = new StoryCast.Person("Micah Bell", "Impulsivo", "una serpiente", "vdl");
			dictionary["cs_billwilliamson"] = new StoryCast.Person("Bill Williamson", "Impulsivo", "grandullón y furioso", "vdl");
			dictionary["cs_lenny"] = new StoryCast.Person("Lenny Summers", "Leal", "joven y despierto", "vdl");
			dictionary["cs_sean"] = new StoryCast.Person("Sean MacGuire", "Impulsivo", "bocazas, pero de gran corazón", "vdl");
			dictionary["cs_kieran"] = new StoryCast.Person("Kieran Duffy", "Cobarde", "antiguo O'Driscoll", "vdl");
			dictionary["cs_uncle"] = new StoryCast.Person("Uncle", "Cobarde", "lumbago y una botella", "vdl");
			dictionary["cs_mrpearson"] = new StoryCast.Person("Simon Pearson", "Orgulloso", "cocinero del campamento, exmarino", "vdl");
			dictionary["cs_leostrauss"] = new StoryCast.Person("Leopold Strauss", "Codicioso", "el prestamista", "vdl");
			dictionary["cs_josiahtrelawny"] = new StoryCast.Person("Josiah Trelawny", "Errante", "estafador de guante blanco", "vdl");
			dictionary["cs_revswanson"] = new StoryCast.Person("Reverend Swanson", "Cobarde", "un predicador borracho", "vdl");
			dictionary["cs_abigailroberts"] = new StoryCast.Person("Abigail Roberts", "Orgulloso", "feroz", "vdl");
			dictionary["cs_jackmarston"] = new StoryCast.Person("Jack Marston", "Brave", "un chico aficionado a la lectura", "vdl");
			dictionary["cs_jackmarston_teen"] = new StoryCast.Person("Jack Marston", "Errante", "hijo de su padre", "vdl");
			dictionary["cs_karen"] = new StoryCast.Person("Karen Jones", "Impulsivo", "bebe fuerte", "vdl");
			dictionary["cs_tilly"] = new StoryCast.Person("Tilly Jackson", "Leal", "lengua afilada", "vdl");
			dictionary["cs_marybeth"] = new StoryCast.Person("Mary-Beth Gaskill", "Leal", "una soñadora", "vdl");
			dictionary["cs_susangrimshaw"] = new StoryCast.Person("Susan Grimshaw", "Frío", "lleva las riendas del campamento", "vdl");
			dictionary["cs_mollyoshea"] = new StoryCast.Person("Molly O'Shea", "Orgulloso", "la chica de Dutch", "vdl");
			dictionary["cs_mrsadler"] = new StoryCast.Person("Sadie Adler", "Impulsivo", "una viuda vengativa", "vdl");
			dictionary["cs_cleet"] = new StoryCast.Person("Cleet", "Impulsivo", "Micah's man", "vdl");
			dictionary["cs_joe"] = new StoryCast.Person("Joe", "Impulsivo", "Micah's man", "vdl");
			dictionary["cs_colmodriscoll"] = new StoryCast.Person("Colm O'Driscoll", "Frío", "the O'Driscoll boss", "odriscoll");
			dictionary["cs_nial_whelan"] = new StoryCast.Person("Nial Whelan", "Frío", "an O'Driscoll", "odriscoll");
			dictionary["cs_dusterinformant_01"] = new StoryCast.Person("O'Driscoll informant", "Cobarde", null, "odriscoll");
			dictionary["cs_miltonandrews"] = new StoryCast.Person("Andrew Milton", "Frío", "Pinkerton agent", null);
			dictionary["cs_edgarross"] = new StoryCast.Person("Edgar Ross", "Frío", "Pinkerton agent", null);
			dictionary["cs_leviticuscornwall"] = new StoryCast.Person("Leviticus Cornwall", "Codicioso", "railroad tycoon", null);
			dictionary["cs_bronte"] = new StoryCast.Person("Angelo Bronte", "Codicioso", "Saint Denis boss", null);
			dictionary["cs_guidomartelli"] = new StoryCast.Person("Guido Martelli", "Codicioso", "Bronte's man", null);
			dictionary["cs_valsheriff"] = new StoryCast.Person("Sheriff Malloy", "Orgulloso", "Valentine sheriff", null);
			dictionary["cs_mud2bigguy"] = new StoryCast.Person("Tommy", "Impulsivo", "Valentine saloon brawler", null);
			dictionary["cs_sherifffreeman"] = new StoryCast.Person("Sheriff Freeman", "Orgulloso", "Strawberry sheriff", null);
			dictionary["cs_sheriffowens"] = new StoryCast.Person("Sheriff Owens", "Orgulloso", "the law", null);
			dictionary["cs_leighgray"] = new StoryCast.Person("Sheriff Leigh Gray", "Orgulloso", "Rhodes sheriff", null);
			dictionary["cs_colfavours"] = new StoryCast.Person("Colonel Favours", "Frío", "Fort Wallace", "army");
			dictionary["cs_captainmonroe"] = new StoryCast.Person("Captain Monroe", "Leal", "an honest officer", "army");
			dictionary["cs_pinkertongoon"] = new StoryCast.Person("Pinkerton agent", "Frío", null, null);
			dictionary["cs_catherinebraithwaite"] = new StoryCast.Person("Catherine Braithwaite", "Orgulloso", "the Braithwaite matriarch", null);
			dictionary["cs_geraldbraithwaite"] = new StoryCast.Person("Gerald Braithwaite", "Impulsivo", null, null);
			dictionary["cs_garethbraithwaite"] = new StoryCast.Person("Gareth Braithwaite", "Impulsivo", null, null);
			dictionary["cs_bartholomewbraithwaite"] = new StoryCast.Person("Bartholomew Braithwaite", "Impulsivo", null, null);
			dictionary["cs_penelopebraithwaite"] = new StoryCast.Person("Penelope Braithwaite", "Leal", "loves a Gray", null);
			dictionary["cs_tavishgray"] = new StoryCast.Person("Tavish Gray", "Impulsivo", "short temper", null);
			dictionary["cs_beaugray"] = new StoryCast.Person("Beau Gray", "Leal", "loves a Braithwaite", null);
			dictionary["cs_jockgray"] = new StoryCast.Person("Jock Gray", "Impulsivo", null, null);
			dictionary["cs_iangray"] = new StoryCast.Person("Ian Gray", "Impulsivo", null, null);
			dictionary["cs_scottgray"] = new StoryCast.Person("Scott Gray", "Impulsivo", null, null);
			dictionary["cs_adamgray"] = new StoryCast.Person("Adam Gray", "Impulsivo", null, null);
			dictionary["cs_eagleflies"] = new StoryCast.Person("Eagle Flies", "Impulsivo", "Rains Fall's son", "wapiti");
			dictionary["cs_rainsfall"] = new StoryCast.Person("Rains Fall", "Leal", "Wapiti chief", "wapiti");
			dictionary["cs_paytah"] = new StoryCast.Person("Paytah", "Leal", null, "wapiti");
			dictionary["cs_wapitiboy"] = new StoryCast.Person("Wapiti boy", null, null, "wapiti");
			dictionary["cs_hercule"] = new StoryCast.Person("Hercule Fontaine", "Orgulloso", "Saint Denis gentleman", null);
			dictionary["cs_mrsfellows"] = new StoryCast.Person("Mrs. Fellows", null, null, null);
			dictionary["cs_doroetheawicklow"] = new StoryCast.Person("Dorothea Wicklow", null, null, null);
			dictionary["cs_thomasdown"] = new StoryCast.Person("Thomas Downes", "Leal", null, null);
			dictionary["cs_edithdown"] = new StoryCast.Person("Edith Downes", "Orgulloso", null, null);
			dictionary["cs_archiedown"] = new StoryCast.Person("Archie Downes", "Cobarde", null, null);
			dictionary["cs_albertmason"] = new StoryCast.Person("Albert Mason", "Errante", "photographer", null);
			dictionary["cs_professorbell"] = new StoryCast.Person("Professor Bell", "Orgulloso", null, null);
			dictionary["cs_marshall_thurwell"] = new StoryCast.Person("Marshal Thurwell", "Orgulloso", null, null);
			dictionary["cs_hamish"] = new StoryCast.Person("Hamish Sinclair", "Leal", "one-legged hunter", null);
			dictionary["cs_jimcalloway"] = new StoryCast.Person("Jim Calloway", "Orgulloso", "old gunslinger", null);
			dictionary["cs_hobartcrawley"] = new StoryCast.Person("Hobart Crawley", "Codicioso", null, null);
			dictionary["cs_agnesdowd"] = new StoryCast.Person("Agnes Dowd", null, null, null);
			dictionary["cs_obediahhinton"] = new StoryCast.Person("Obediah Hinton", "Frío", null, null);
			dictionary["cs_slavecatcher"] = new StoryCast.Person("the slave catcher", "Frío", null, null);
			dictionary["cs_lucanapoli"] = new StoryCast.Person("Luca Napoli", null, null, null);
			dictionary["cs_henrilemiux"] = new StoryCast.Person("Henri Lemieux", "Orgulloso", null, null);
			dictionary["cs_frenchartist"] = new StoryCast.Person("Charles Châtenay", "Orgulloso", null, null);
			dictionary["cs_cavehermit"] = new StoryCast.Person("the hermit", "Errante", null, null);
			dictionary["cs_herbalist"] = new StoryCast.Person("the herbalist", "Errante", null, null);
			dictionary["cs_taxidermist"] = new StoryCast.Person("the taxidermist", null, null, null);
			dictionary["cs_drmalcolmmacintosh"] = new StoryCast.Person("Dr. Malcolm MacIntosh", null, null, null);
			dictionary["cs_angusgeddes"] = new StoryCast.Person("Angus Geddes", null, null, null);
			dictionary["cs_davidgeddes"] = new StoryCast.Person("David Geddes", "Orgulloso", null, null);
			dictionary["cs_duncangeddes"] = new StoryCast.Person("Duncan Geddes", null, null, null);
			dictionary["cs_mrsgeddes"] = new StoryCast.Person("Mrs. Geddes", null, null, null);
			dictionary["cs_exconfedsleader_01"] = new StoryCast.Person("Lemoyne Raider leader", "Impulsivo", null, "lemoyneraiders");
			dictionary["cs_famousgunslinger_01"] = new StoryCast.Person("a famous gunslinger", "Orgulloso", null, null);
			dictionary["cs_famousgunslinger_02"] = new StoryCast.Person("a famous gunslinger", "Orgulloso", null, null);
			dictionary["cs_famousgunslinger_03"] = new StoryCast.Person("a famous gunslinger", "Orgulloso", null, null);
			dictionary["cs_famousgunslinger_04"] = new StoryCast.Person("a famous gunslinger", "Orgulloso", null, null);
			dictionary["cs_famousgunslinger_05"] = new StoryCast.Person("a famous gunslinger", "Orgulloso", null, null);
			dictionary["cs_famousgunslinger_06"] = new StoryCast.Person("a famous gunslinger", "Orgulloso", null, null);
			dictionary["cs_mrwayne"] = new StoryCast.Person("Mr. Wayne", null, null, null);
			dictionary["cs_mradler"] = new StoryCast.Person("Jake Adler", "Leal", null, null);
			dictionary["cs_wrobel"] = new StoryCast.Person("Wrobel", "Errante", null, null);
			dictionary["cs_hestonjameson"] = new StoryCast.Person("Heston Jameson", null, null, null);
			dictionary["cs_archibaldjameson"] = new StoryCast.Person("Archibald Jameson", "Leal", null, null);
			dictionary["cs_gilbertknightly"] = new StoryCast.Person("Gilbert Knightly", null, null, null);
			dictionary["cs_braithwaitebutler"] = new StoryCast.Person("the Braithwaites' butler", null, null, null);
			dictionary["cs_abe"] = new StoryCast.Person("Abe", null, null, null);
			dictionary["cs_aberdeenpigfarmer"] = new StoryCast.Person("Bray Aberdeen", null, null, null);
			dictionary["cs_aberdeensister"] = new StoryCast.Person("Tammy Aberdeen", null, null, null);
			dictionary["cs_acrobat"] = new StoryCast.Person("an acrobat", null, null, null);
			dictionary["cs_albertcakeesquire"] = new StoryCast.Person("Albert Cakes", null, null, null);
			dictionary["cs_andershelgerson"] = new StoryCast.Person("Anders Helgerson", null, null, null);
			dictionary["cs_angel"] = new StoryCast.Person("Angel", null, null, null);
			dictionary["cs_angryhusband"] = new StoryCast.Person("an angry husband", null, null, null);
			dictionary["cs_ansel_atherton"] = new StoryCast.Person("Ansel Atherton", null, null, null);
			dictionary["cs_antonyforemen"] = new StoryCast.Person("Anthony Foreman", null, null, null);
			dictionary["cs_archerfordham"] = new StoryCast.Person("Archer Fordham", null, null, null);
			dictionary["cs_artappraiser"] = new StoryCast.Person("Professor Shiftacre", null, null, null);
			dictionary["cs_asbdeputy_01"] = new StoryCast.Person("Annesburg deputy", null, null, null);
			dictionary["cs_ashton"] = new StoryCast.Person("Ashton", null, null, null);
			dictionary["cs_balloonoperator"] = new StoryCast.Person("Arturo Bullard", null, null, null);
			dictionary["cs_bandbassist"] = new StoryCast.Person("the band's bassist", null, null, null);
			dictionary["cs_banddrummer"] = new StoryCast.Person("the band's drummer", null, null, null);
			dictionary["cs_bandpianist"] = new StoryCast.Person("the band's pianist", null, null, null);
			dictionary["cs_bandsinger"] = new StoryCast.Person("the band's singer", null, null, null);
			dictionary["cs_baptiste"] = new StoryCast.Person("Baptiste", null, null, null);
			dictionary["cs_bathingladies_01"] = new StoryCast.Person("a bathing lady", null, null, null);
			dictionary["cs_beatenupcaptain"] = new StoryCast.Person("a beaten-up captain", null, null, null);
			dictionary["cs_bivcoachdriver"] = new StoryCast.Person("the coach driver", null, null, null);
			dictionary["cs_blwphotographer"] = new StoryCast.Person("the Blackwater photographer", null, null, null);
			dictionary["cs_blwwitness"] = new StoryCast.Person("Jimmy Brooks", null, null, null);
			dictionary["cs_braithwaitemaid"] = new StoryCast.Person("the Braithwaites' maid", null, null, null);
			dictionary["cs_braithwaiteservant"] = new StoryCast.Person("the Braithwaites' servant", null, null, null);
			dictionary["cs_brendacrawley"] = new StoryCast.Person("Brenda Crawley", null, null, null);
			dictionary["cs_brontesbutler"] = new StoryCast.Person("Bronte's butler", null, null, null);
			dictionary["cs_brotherdorkins"] = new StoryCast.Person("Brother Dorkins", null, null, null);
			dictionary["cs_brynntildon"] = new StoryCast.Person("Brynn Tildon", null, null, null);
			dictionary["cs_bubba"] = new StoryCast.Person("Bubba", null, null, null);
			dictionary["cs_cabaretmc"] = new StoryCast.Person("the cabaret MC", null, null, null);
			dictionary["cs_cajun"] = new StoryCast.Person("a Cajun", null, null, null);
			dictionary["cs_cancan_01"] = new StoryCast.Person("a Petit Flâneur dancer", null, null, null);
			dictionary["cs_cancan_02"] = new StoryCast.Person("a Petit Flâneur dancer", null, null, null);
			dictionary["cs_cancan_03"] = new StoryCast.Person("a Petit Flâneur dancer", null, null, null);
			dictionary["cs_cancan_04"] = new StoryCast.Person("a Petit Flâneur dancer", null, null, null);
			dictionary["cs_cancanman_01"] = new StoryCast.Person("a can-can dancer", null, null, null);
			dictionary["cs_cassidy"] = new StoryCast.Person("Blind Man Cassidy", null, null, null);
			dictionary["cs_cattlerustler"] = new StoryCast.Person("a cattle rustler", null, null, null);
			dictionary["cs_chainprisoner_01"] = new StoryCast.Person("Sampson Black", null, null, null);
			dictionary["cs_chainprisoner_02"] = new StoryCast.Person("Wendell White", null, null, null);
			dictionary["cs_chelonianmaster"] = new StoryCast.Person("the Chelonian Master", null, null, null);
			dictionary["cs_cigcardguy"] = new StoryCast.Person("Phineas T. Ramsbottom", null, null, null);
			dictionary["cs_clay"] = new StoryCast.Person("Clay Davies", null, null, null);
			dictionary["cs_clive"] = new StoryCast.Person("Clive Davies", null, null, null);
			dictionary["cs_cooper"] = new StoryCast.Person("Cooper", null, null, null);
			dictionary["cs_cornwalltrainconductor"] = new StoryCast.Person("the Cornwall train conductor", null, null, null);
			dictionary["cs_crackpotinventor"] = new StoryCast.Person("Marko Dragic", null, null, null);
			dictionary["cs_crackpotrobot"] = new StoryCast.Person("the robot", null, null, null);
			dictionary["cs_creepyoldlady"] = new StoryCast.Person("an old woman", null, null, null);
			dictionary["cs_creolecaptain"] = new StoryCast.Person("Thomas", null, null, null);
			dictionary["cs_creoledoctor"] = new StoryCast.Person("the Creole doctor", null, null, null);
			dictionary["cs_creoleguy"] = new StoryCast.Person("a Creole man", null, null, null);
			dictionary["cs_dalemaroney"] = new StoryCast.Person("Dale Maroney", null, null, null);
			dictionary["cs_daveycallender"] = new StoryCast.Person("Davey Callander", null, null, null);
			dictionary["cs_desmond"] = new StoryCast.Person("Desmond Blythe", null, null, null);
			dictionary["cs_didsbury"] = new StoryCast.Person("Didsbury", null, null, null);
			dictionary["cs_dinoboneslady"] = new StoryCast.Person("Deborah MacGuiness", null, null, null);
			dictionary["cs_disguisedduster_01"] = new StoryCast.Person("a man in disguise", null, null, null);
			dictionary["cs_disguisedduster_02"] = new StoryCast.Person("a man in disguise", null, null, null);
			dictionary["cs_disguisedduster_03"] = new StoryCast.Person("a man in disguise", null, null, null);
			dictionary["cs_drhiggins"] = new StoryCast.Person("Dr. Lane Higgins", null, null, null);
			dictionary["cs_edith_john"] = new StoryCast.Person("Edith Downes", null, null, null);
			dictionary["cs_edmundlowry"] = new StoryCast.Person("Edmund Lowry Jr.", null, null, null);
			dictionary["cs_escapeartist"] = new StoryCast.Person("Benjamin Lazarus", null, null, null);
			dictionary["cs_escapeartistassistant"] = new StoryCast.Person("Benjamin Lazarus's assistant", null, null, null);
			dictionary["cs_evelynmiller"] = new StoryCast.Person("Evelyn Miller", null, null, null);
			dictionary["cs_exconfedinformant"] = new StoryCast.Person("a Lemoyne Raider informant", null, null, null);
			dictionary["cs_exoticcollector"] = new StoryCast.Person("Algernon Wasp", null, null, null);
			dictionary["cs_featherstonchambers"] = new StoryCast.Person("Featherston Chambers", null, null, null);
			dictionary["cs_featsofstrength"] = new StoryCast.Person("Hortensia", null, null, null);
			dictionary["cs_fightref"] = new StoryCast.Person("the fight announcer", null, null, null);
			dictionary["cs_fire_breather"] = new StoryCast.Person("Antoinette Sanseverino", null, null, null);
			dictionary["cs_fishcollector"] = new StoryCast.Person("Jeremy Gill", null, null, null);
			dictionary["cs_forgivenhusband_01"] = new StoryCast.Person("a husband", null, null, null);
			dictionary["cs_forgivenwife_01"] = new StoryCast.Person("a wife", null, null, null);
			dictionary["cs_formyartbigwoman"] = new StoryCast.Person("an artist's model", null, null, null);
			dictionary["cs_francis_sinclair"] = new StoryCast.Person("Francis Sinclair", null, null, null);
			dictionary["cs_frenchman_01"] = new StoryCast.Person("a Frenchman", null, null, null);
			dictionary["cs_fussar"] = new StoryCast.Person("Colonel Alberto Fussar", null, null, null);
			dictionary["cs_gavin"] = new StoryCast.Person("Nigel", null, null, null);
			dictionary["cs_genstoryfemale"] = new StoryCast.Person("a woman", null, null, null);
			dictionary["cs_genstorymale"] = new StoryCast.Person("a man", null, null, null);
			dictionary["cs_germandaughter"] = new StoryCast.Person("a settler's daughter", null, null, null);
			dictionary["cs_germanfather"] = new StoryCast.Person("a German settler", null, null, null);
			dictionary["cs_germanmother"] = new StoryCast.Person("a settler's wife", null, null, null);
			dictionary["cs_germanson"] = new StoryCast.Person("a settler's son", null, null, null);
			dictionary["cs_gloria"] = new StoryCast.Person("Gloria", null, null, null);
			dictionary["cs_grizzledjon"] = new StoryCast.Person("Jon", null, null, null);
			dictionary["cs_hectorfellowes"] = new StoryCast.Person("Hector Fellowes Jr.", null, null, null);
			dictionary["cs_jamie"] = new StoryCast.Person("Jamie Gillis", null, null, null);
			dictionary["cs_janson"] = new StoryCast.Person("Janson", null, null, null);
			dictionary["cs_jeb"] = new StoryCast.Person("Jeb", null, null, null);
			dictionary["cs_joebutler"] = new StoryCast.Person("Joe Butler", null, null, null);
			dictionary["cs_johnthebaptisingmadman"] = new StoryCast.Person("John the Mad Preacher", null, null, null);
			dictionary["cs_johnweathers"] = new StoryCast.Person("J. John Weathers", null, null, null);
			dictionary["cs_jules"] = new StoryCast.Person("Jules", null, null, null);
			dictionary["cs_karensjohn_01"] = new StoryCast.Person("Karen's mark", null, null, null);
			dictionary["cs_laramie"] = new StoryCast.Person("the Laramie gang leader", null, null, null);
			dictionary["cs_lemiuxassistant"] = new StoryCast.Person("Jean-Marc Mercier", null, null, null);
			dictionary["cs_leon"] = new StoryCast.Person("Leon Fuentes", null, null, null);
			dictionary["cs_levisimon"] = new StoryCast.Person("Levi Simon", null, null, null);
			dictionary["cs_lillianpowell"] = new StoryCast.Person("Lillian Powell", null, null, null);
			dictionary["cs_lillymillet"] = new StoryCast.Person("Lilly Millet", null, null, null);
			dictionary["cs_londonderryson"] = new StoryCast.Person("Mrs. Londonderry's son", null, null, null);
			dictionary["cs_magnifico"] = new StoryCast.Person("Magnifico", null, null, null);
			dictionary["cs_mamawatson"] = new StoryCast.Person("Mama Watson", null, null, null);
			dictionary["cs_marylinton"] = new StoryCast.Person("Mary Linton", null, null, null);
			dictionary["cs_meditatingmonk"] = new StoryCast.Person("the meditating monk", null, null, null);
			dictionary["cs_meredith"] = new StoryCast.Person("Meredith Buckley", null, null, null);
			dictionary["cs_meredithsmother"] = new StoryCast.Person("Etta Buckley", null, null, null);
			dictionary["cs_micahsnemesis"] = new StoryCast.Person("an O'Driscoll in the cells", null, null, null);
			dictionary["cs_mickey"] = new StoryCast.Person("Mickey", null, null, null);
			dictionary["cs_missmarjorie"] = new StoryCast.Person("Miss Marjorie", null, null, null);
			dictionary["cs_mixedracekid"] = new StoryCast.Person("a boy", null, null, null);
			dictionary["cs_moira"] = new StoryCast.Person("Moira Calthorpe", null, null, null);
			dictionary["cs_mrdevon"] = new StoryCast.Person("Mr. Devon", null, null, null);
			dictionary["cs_mrlinton"] = new StoryCast.Person("Mr. Gillis", null, null, null);
			dictionary["cs_mrs_calhoun"] = new StoryCast.Person("Olive Calhoon", null, null, null);
			dictionary["cs_mrs_sinclair"] = new StoryCast.Person("Mrs. Sinclair", null, null, null);
			dictionary["cs_mrslondonderry"] = new StoryCast.Person("Mrs. Londonderry", null, null, null);
			dictionary["cs_mrsweathers"] = new StoryCast.Person("Mrs. Weathers", null, null, null);
			dictionary["cs_mysteriousstranger"] = new StoryCast.Person("the strange man", null, null, null);
			dictionary["cs_nbxdrunk"] = new StoryCast.Person("a drunk", null, null, null);
			dictionary["cs_nbxexecuted"] = new StoryCast.Person("a condemned man", null, null, null);
			dictionary["cs_nbxpolicechiefformal"] = new StoryCast.Person("the police chief", null, null, null);
			dictionary["cs_nbxreceptionist_01"] = new StoryCast.Person("the receptionist", null, null, null);
			dictionary["cs_nicholastimmins"] = new StoryCast.Person("Nicholas Timmins", null, null, null);
			dictionary["cs_nils"] = new StoryCast.Person("Nils", null, null, null);
			dictionary["cs_norrisforsythe"] = new StoryCast.Person("Norris Forsythe", null, null, null);
			dictionary["cs_oddfellowspinhead"] = new StoryCast.Person("an Oddfellow", null, null, null);
			dictionary["cs_odprostitute"] = new StoryCast.Person("a working girl", null, null, null);
			dictionary["cs_operasinger"] = new StoryCast.Person("the opera singer", null, null, null);
			dictionary["cs_poisonwellshaman"] = new StoryCast.Person("the Butcher Creek shaman", null, null, null);
			dictionary["cs_poorjoe"] = new StoryCast.Person("Poor Joe", null, null, null);
			dictionary["cs_priest_wedding"] = new StoryCast.Person("the priest", null, null, null);
			dictionary["cs_princessisabeau"] = new StoryCast.Person("Princess Isabeau", null, null, null);
			dictionary["cs_ramon_cortez"] = new StoryCast.Person("Ramon Cortez", null, null, null);
			dictionary["cs_reverendfortheringham"] = new StoryCast.Person("Reverend Peter Fotheringham", null, null, null);
			dictionary["cs_rhodeputy_01"] = new StoryCast.Person("Rhodes deputy", null, null, null);
			dictionary["cs_rhodeputy_02"] = new StoryCast.Person("Rhodes deputy", null, null, null);
			dictionary["cs_rhodesassistant"] = new StoryCast.Person("the Rhodes clerk", null, null, null);
			dictionary["cs_rhodeskidnapvictim"] = new StoryCast.Person("a kidnap victim", null, null, null);
			dictionary["cs_rhodessaloonbouncer"] = new StoryCast.Person("the Rhodes saloon bouncer", null, null, null);
			dictionary["cs_ringmaster"] = new StoryCast.Person("Haywood Margaret", null, null, null);
			dictionary["cs_rockyseven_widow"] = new StoryCast.Person("Charlotte Balfour", null, null, null);
			dictionary["cs_samaritan"] = new StoryCast.Person("a good samaritan", null, null, null);
			dictionary["cs_sd_streetkid_01"] = new StoryCast.Person("a street kid", null, null, null);
			dictionary["cs_sd_streetkid_01a"] = new StoryCast.Person("a street kid", null, null, null);
			dictionary["cs_sd_streetkid_01b"] = new StoryCast.Person("a street kid", null, null, null);
			dictionary["cs_sd_streetkid_02"] = new StoryCast.Person("a street kid", null, null, null);
			dictionary["cs_sddoctor_01"] = new StoryCast.Person("a Saint Denis doctor", null, null, null);
			dictionary["cs_sdpriest"] = new StoryCast.Person("the Saint Denis priest", null, null, null);
			dictionary["cs_sdsaloondrunk_01"] = new StoryCast.Person("a saloon drunk", null, null, null);
			dictionary["cs_sdstreetkidthief"] = new StoryCast.Person("a street thief", null, null, null);
			dictionary["cs_sistercalderon"] = new StoryCast.Person("Sister Calderón", null, null, null);
			dictionary["cs_soothsayer"] = new StoryCast.Person("the soothsayer", null, null, null);
			dictionary["cs_strawberryoutlaw_01"] = new StoryCast.Person("a Strawberry outlaw", null, null, null);
			dictionary["cs_strawberryoutlaw_02"] = new StoryCast.Person("a Strawberry outlaw", null, null, null);
			dictionary["cs_strdeputy_01"] = new StoryCast.Person("Strawberry deputy", null, null, null);
			dictionary["cs_strdeputy_02"] = new StoryCast.Person("Strawberry deputy", null, null, null);
			dictionary["cs_strsheriff_01"] = new StoryCast.Person("Sheriff Hanley", null, null, null);
			dictionary["cs_sunworshipper"] = new StoryCast.Person("Bob Brownlee", null, null, null);
			dictionary["cs_swampfreak"] = new StoryCast.Person("a swamp dweller", null, null, null);
			dictionary["cs_swampweirdosonny"] = new StoryCast.Person("Sonny", null, null, null);
			dictionary["cs_sworddancer"] = new StoryCast.Person("the Mysterious Maya", null, null, null);
			dictionary["cs_theodorelevin"] = new StoryCast.Person("Theodore Levin", null, null, null);
			dictionary["cs_tigerhandler"] = new StoryCast.Person("the tiger handler", null, null, null);
			dictionary["cs_timothydonahue"] = new StoryCast.Person("Timothy Donahue", null, null, null);
			dictionary["cs_tinyhermit"] = new StoryCast.Person("a hermit", null, null, null);
			dictionary["cs_tomdickens"] = new StoryCast.Person("Tom Dickens", null, null, null);
			dictionary["cs_towncrier"] = new StoryCast.Person("the Armadillo town crier", null, null, null);
			dictionary["cs_treasurehunter"] = new StoryCast.Person("a treasure hunter", null, null, null);
			dictionary["cs_twinbrother_01"] = new StoryCast.Person("one of the twins", null, null, null);
			dictionary["cs_twinbrother_02"] = new StoryCast.Person("one of the twins", null, null, null);
			dictionary["cs_twingroupie_01"] = new StoryCast.Person("Helen", null, null, null);
			dictionary["cs_twingroupie_02"] = new StoryCast.Person("a groupie", null, null, null);
			dictionary["cs_unidusterjail_01"] = new StoryCast.Person("a prisoner", null, null, null);
			dictionary["cs_valauctionboss_01"] = new StoryCast.Person("Morris Peyton", null, null, null);
			dictionary["cs_valdeputy_01"] = new StoryCast.Person("Valentine deputy", null, null, null);
			dictionary["cs_valprayingman"] = new StoryCast.Person("a praying man", null, null, null);
			dictionary["cs_valprostitute_01"] = new StoryCast.Person("a working girl", null, null, null);
			dictionary["cs_valprostitute_02"] = new StoryCast.Person("a working girl", null, null, null);
			dictionary["cs_vampire"] = new StoryCast.Person("the Saint Denis vampire", null, null, null);
			dictionary["cs_vht_bathgirl"] = new StoryCast.Person("a bath girl", null, null, null);
			dictionary["cs_warvet"] = new StoryCast.Person("a war veteran", null, null, null);
			dictionary["cs_watson_01"] = new StoryCast.Person("a Watson boy", null, null, null);
			dictionary["cs_watson_02"] = new StoryCast.Person("a Watson boy", null, null, null);
			dictionary["cs_watson_03"] = new StoryCast.Person("a Watson boy", null, null, null);
			dictionary["cs_welshfighter"] = new StoryCast.Person("Simon of Wales", null, null, null);
			dictionary["cs_wintonholmes"] = new StoryCast.Person("Winton Holmes", null, null, null);
			StoryCast.People = dictionary;
		}

		// Token: 0x04000C45 RID: 3141
		private const string VdL = "vdl";

		// Token: 0x04000C46 RID: 3142
		public static readonly Dictionary<string, StoryCast.Person> People;

		// Token: 0x02000452 RID: 1106
		public sealed class Person
		{
			// Token: 0x0600258D RID: 9613 RVA: 0x0018184F File Offset: 0x0017FA4F
			public Person(string name, string temper = null, string note = null, string gang = null)
			{
				this.Name = name;
				this.Temper = temper;
				this.Note = note;
				this.Gang = gang;
			}

			// Token: 0x04001FC2 RID: 8130
			public string Name;

			// Token: 0x04001FC3 RID: 8131
			public string Temper;

			// Token: 0x04001FC4 RID: 8132
			public string Note;

			// Token: 0x04001FC5 RID: 8133
			public string Gang;
		}
	}
}
