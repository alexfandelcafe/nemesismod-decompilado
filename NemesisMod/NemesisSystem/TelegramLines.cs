using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x02000071 RID: 113
	internal static class TelegramLines
	{
		// Token: 0x060012A9 RID: 4777 RVA: 0x00127040 File Offset: 0x00125240
		private static Dictionary<string, string[]> P([TupleElementNames(new string[]
		{
			"key",
			"lines"
		})] params ValueTuple<string, string[]>[] pools)
		{
			return pools.ToDictionary(([TupleElementNames(new string[]
			{
				"key",
				"lines"
			})] ValueTuple<string, string[]> p) => p.Item1, ([TupleElementNames(new string[]
			{
				"key",
				"lines"
			})] ValueTuple<string, string[]> p) => p.Item2);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00127094 File Offset: 0x00125294
		public static string Pick(string situation, string factionId, Personality? temper, Random rng, IDictionary<string, string> tokens = null, bool leaderOut = false)
		{
			Dictionary<string, string[]> dictionary;
			if (!TelegramLines.Lines.TryGetValue(situation, out dictionary))
			{
				return null;
			}
			List<string> list = new List<string>();
			string[] collection;
			if (!string.IsNullOrEmpty(factionId) && dictionary.TryGetValue(factionId, out collection))
			{
				list.AddRange(collection);
				list.AddRange(collection);
			}
			string[] collection2;
			if (temper != null && dictionary.TryGetValue(temper.Value.ToString(), out collection2))
			{
				list.AddRange(collection2);
			}
			string[] collection3;
			if (dictionary.TryGetValue("", out collection3))
			{
				list.AddRange(collection3);
			}
			string text = (!string.IsNullOrEmpty(factionId) && TelegramLines.LeaderNameOf != null) ? TelegramLines.LeaderNameOf(factionId) : null;
			if (leaderOut || (text == null && TelegramLines.LeaderNameOf != null && !string.IsNullOrEmpty(factionId)))
			{
				list.RemoveAll((string l) => l.Contains("COLM") || l.Contains("MR BRONTE") || l.Contains("THE GENERAL") || l.Contains("FAVORES") || l.Contains("{LEADER}"));
			}
			if (list.Count == 0)
			{
				return null;
			}
			List<string> list2 = (from l in list
			where !TelegramLines._recentSet.Contains(l)
			select l).ToList<string>();
			string text2 = ((list2.Count > 0) ? list2 : list)[rng.Next((list2.Count > 0) ? list2.Count : list.Count)];
			TelegramLines._recent.AddLast(text2);
			TelegramLines._recentSet.Add(text2);
			while (TelegramLines._recent.Count > 80)
			{
				TelegramLines._recentSet.Remove(TelegramLines._recent.First.Value);
				TelegramLines._recent.RemoveFirst();
			}
			if (tokens != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in tokens)
				{
					text2 = text2.Replace("{" + keyValuePair.Key.ToUpperInvariant() + "}", (keyValuePair.Value ?? "").ToUpperInvariant());
				}
			}
			if (text != null)
			{
				string[] array = text.Split(new char[]
				{
					' '
				});
				string text3 = ((array.Length > 1 && array[0].EndsWith(".")) ? array[array.Length - 1] : array[0]).ToUpperInvariant();
				if (factionId == "odriscoll" && !text.Contains("Colm"))
				{
					text2 = text2.Replace("COLM", text3);
				}
				if (factionId == "lemoyneraiders")
				{
					text2 = text2.Replace("THE GENERAL", text3);
				}
				if (factionId == "bronte" && !text.Contains("Bronte"))
				{
					text2 = text2.Replace("MR BRONTE'S", text3 + "'S").Replace("MR BRONTE", text3);
				}
			}
			return text2;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00127380 File Offset: 0x00125580
		// Note: this type is marked as 'beforefieldinit'.
		static TelegramLines()
		{
			Dictionary<string, Dictionary<string, string[]>> dictionary = new Dictionary<string, Dictionary<string, string[]>>();
			dictionary["peace_yes"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"MAYBE. WE WILL SEE. STAY CLEAR OF ME.",
					"ALRIGHT. I WILL NOT COME LOOKING. DO NOT GIVE ME CAUSE.",
					"READ YOUR WIRE TWICE. I AM TIRED TOO. LET IT LIE.",
					"FINE. BUT IF I SEE YOU ON MY ROAD ALL BETS ARE OFF.",
					"I WILL THINK ON IT. THAT IS MORE THAN YOU DESERVE.",
					"NOT FORGIVEN. BUT LET IT REST FOR NOW.",
					"AGREED STOP KEEP YOUR DISTANCE STOP",
					"VERY WELL. YOU KEEP TO YOUR ROADS AND I KEEP TO MINE.",
					"I WILL HOLSTER IT. FOR NOW. DO NOT MAKE ME DRAW IT AGAIN.",
					"ACCEPTED STOP DO NOT WRITE AGAIN STOP",
					"A TRUCE THEN. I WILL NOT SHAKE YOUR HAND ON IT.",
					"ENOUGH BLOOD SPILT ALREADY. ALRIGHT.",
					"MY WIFE SAYS TAKE IT. SO I TAKE IT. STAY AWAY.",
					"LET IT BE BURIED. DO NOT DIG IT UP."
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"ALRIGHT. NO MORE TROUBLE. PLEASE.",
					"YES. YES. THANK YOU. I WANT NO MORE OF IT.",
					"AGREED. I NEVER WANTED ANY OF THIS.",
					"THANK GOD. I HAVE NOT SLEPT A NIGHT SINCE.",
					"YES STOP A THOUSAND TIMES YES STOP",
					"BLESS YOU. I WILL NOT SO MUCH AS SAY YOUR NAME.",
					"AGREED. I HAVE PUT MY GUN IN A DRAWER. I MEAN IT.",
					"I WILL KEEP TO MY HOUSE. YOU WILL NOT SEE ME. THANK YOU."
				}),
				new ValueTuple<string, string[]>("Errante", new string[]
				{
					"FINE BY ME. ROAD IS LONG ENOUGH FOR THE BOTH OF US.",
					"SURE. I WAS HEADED WEST ANYHOW.",
					"SUITS ME. LIFE IS SHORT. MINE ANYWAY.",
					"ALREADY FORGOT YOUR FACE. KEEP IT THAT WAY.",
					"NO HARD FEELINGS. WELL. SOME. THEY WILL PASS.",
					"DONE. I NEVER CARRY A GRUDGE FURTHER THAN THE NEXT TOWN.",
					"ALRIGHT PARTNER. TOO OLD TO BE CHASING GHOSTS.",
					"FINE. MIGHT EVEN BUY YOU A DRINK ONE DAY. MIGHT NOT."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"PEACE IS CHEAPER THAN BULLETS. AGREED.",
					"FINE. A DEAD MAN EARNS NOTHING. NEITHER DO YOU.",
					"AGREED. FEUDING IS BAD FOR BUSINESS.",
					"PEACE IT IS. CARTRIDGES COST MONEY."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"FOR NOW. IF YOU TOUCH MY PEOPLE IT ENDS.",
					"I WILL HOLD TO IT IF YOU DO. MY WORD IS GOOD.",
					"AGREED. BUT MY FRIENDS ANSWER FOR THEMSELVES.",
					"I WILL TELL MY PEOPLE TO LET IT BE. THAT IS ALL I PROMISE."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"NOTED. DO NOT MISTAKE IT FOR FRIENDSHIP.",
					"VERY WELL. BUSINESS IS BETTER WITHOUT YOU IN IT.",
					"ACCEPTED. TERMS END THE DAY YOU CROSS ME.",
					"AGREED. I HAVE LITTLE USE FOR YOU EITHER WAY."
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"I WILL ALLOW IT. REMEMBER WHO ALLOWED IT.",
					"VERY WELL. YOU CAME TO ME. THAT IS ENOUGH."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"ALRIGHT. BUT I AM STILL SORE ABOUT IT.",
					"FINE. DO NOT PUSH YOUR LUCK."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"ALRIGHT BOYO. BUT COLM DOES NOT HEAR OF THIS.",
					"FINE. KEEP AWAY FROM ME AND I WILL KEEP AWAY FROM YOU. GOD SAVE IRELAND.",
					"GRAND SO. WE NEVER MET. AND IF THE LADS ASK I SPAT ON YOUR WIRE.",
					"AYE ALRIGHT. BUT YOU ARE STILL A VAN DER LINDE AND I STILL HATE THE SMELL OF YE."
				}),
				new ValueTuple<string, string[]>("lemoyneraiders", new string[]
				{
					"A TRUCE THEN. LIKE APPOMATTOX. DO NOT EXPECT ME TO LIKE IT.",
					"FINE. LEMOYNE IS BIG ENOUGH. STAY ON YOUR SIDE OF IT.",
					"TERMS ACCEPTED. A GENTLEMAN KEEPS HIS WORD. SEE THAT YOU DO.",
					"VERY WELL SIR. THE WAR IS OVER BETWEEN US. THAT ONE AT LEAST."
				}),
				new ValueTuple<string, string[]>("wapiti", new string[]
				{
					"WE WANT ONLY TO BE LEFT IN PEACE. SO BE IT.",
					"MY PEOPLE HAVE SEEN ENOUGH BLOOD. AGREED.",
					"I WILL TELL THE ELDERS. THEY WILL BE GLAD OF IT.",
					"YOUR WORDS ARE ACCEPTED. LET YOUR DEEDS MATCH THEM."
				}),
				new ValueTuple<string, string[]>("klan", new string[]
				{
					"WE WILL LET IT PASS. THIS TIME.",
					"THE ORDER IS MERCIFUL. DO NOT TEST IT TWICE."
				}),
				new ValueTuple<string, string[]>("bronte", new string[]
				{
					"MR BRONTE APPRECIATES A MAN WHO KNOWS HIS PLACE. AGREED.",
					"FINE. BUSINESS IS BUSINESS. STAY OUT OF SAINT DENIS AFFAIRS."
				}),
				new ValueTuple<string, string[]>("murfree", new string[]
				{
					"MAMMY SAYS LEAVE YOU BE. FOR NOW.",
					"AINT COMIN DOWN THE HOLLER FOR YOU. YOU STAY OUT OF IT."
				}),
				new ValueTuple<string, string[]>("skinners", new string[]
				{
					"STAY OUT OF THE TALL TREES AND YOU KEEP YOUR HIDE.",
					"FINE. WE GOT OTHER GAME TO HUNT."
				})
			});
			dictionary["peace_no"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"NOTHING IS FORGOTTEN.",
					"NO.",
					"NOT WHILE I BREATHE.",
					"A WIRE DOES NOT WASH IT OFF.",
					"YOU KNOW WHAT YOU DID. NO PEACE.",
					"TOO LATE FOR THAT FRIEND.",
					"PEACE IS FOR THE DEAD. YOU WILL HAVE YOURS SOON.",
					"I KEPT YOUR WIRE. I WILL PUT IT ON YOUR GRAVE.",
					"NO STOP NEVER STOP",
					"YOU WANT PEACE. SHOULD HAVE THOUGHT OF THAT BEFORE.",
					"THE ANSWER IS NO. IT WILL STAY NO.",
					"READ IT. LAUGHED. NO.",
					"SOME THINGS ONLY END ONE WAY.",
					"ASK AGAIN AND I DOUBLE THE DEBT.",
					"YOU WILL GET YOUR PEACE IN A PINE BOX.",
					"I HAVE A LONG MEMORY AND A SHORT TEMPER. NO."
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"YOU THINK A WIRE MAKES IT RIGHT? IT DOES NOT.",
					"ASK ME TO MY FACE. ON YOUR KNEES.",
					"MY NAME IS WORTH MORE THAN YOUR PEACE.",
					"I DO NOT MAKE TERMS WITH MEN WHO SHAMED ME.",
					"EVERY MAN IN THE COUNTY SAW WHAT YOU DID. NO.",
					"PEACE WOULD MAKE ME A LAUGHINGSTOCK. NEVER."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"GO TO HELL.",
					"GO TO HELL STOP AND STAY THERE STOP",
					"PEACE? I WILL PEACE YOUR SKULL IN.",
					"SEND ANOTHER AND I WILL FEED IT TO YOU.",
					"TORE YOUR WIRE UP AND STAMPED ON IT.",
					"YOU WANT PEACE COME GET IT. BRING A GUN.",
					"NO. NO. AND DAMN YOU.",
					"THE CLERK HAD TO DUCK WHEN I READ IT. NO."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"NO. THERE IS A DEBT AND IT WILL BE PAID.",
					"DECLINED.",
					"THE ACCOUNT STAYS OPEN.",
					"NO. YOU KNOW HOW THIS ENDS."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"NOT AFTER WHAT YOU DID TO OURS.",
					"MY BROTHERS WOULD SPIT ON ME. NO.",
					"I DO NOT MAKE PEACE BEHIND MY PEOPLE'S BACKS.",
					"THEY WOULD NEVER FORGIVE ME. NO."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"PEACE COSTS. YOU HAVE NOT OFFERED ENOUGH.",
					"WORDS ARE FREE. MY GRUDGE IS NOT."
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"I CANNOT. THEY WOULD KNOW. LEAVE ME ALONE.",
					"NO. I AM SORRY. NO."
				}),
				new ValueTuple<string, string[]>("Errante", new string[]
				{
					"NOT THIS TIME PARTNER.",
					"SOME THINGS EVEN I CANNOT LET GO."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"PEACE WITH A VAN DER LINDE DOG? COLM WOULD HANG ME. SO WOULD I.",
					"FECK YOUR PEACE.",
					"THE IRISH REMEMBER. FOR CENTURIES.",
					"YE WANT PEACE GO ASK THE ENGLISH HOW THAT WENT.",
					"NOT A CHANCE BOYO. SEE YE AT YER WAKE.",
					"COLM WOULD HAVE MY HIDE AND HE WOULD BE RIGHT. NO."
				}),
				new ValueTuple<string, string[]>("lemoyneraiders", new string[]
				{
					"WE DID NOT SURRENDER IN SIXTY-FIVE. WE WILL NOT NOW.",
					"NO PEACE WITH CARPETBAGGERS. THE CAUSE LIVES.",
					"THE SOUTH DOES NOT TREAT WITH TURNCOATS.",
					"NO TERMS. WE ARE NOT DONE FIGHTING. NOT FOR THIRTY YEARS NOW."
				}),
				new ValueTuple<string, string[]>("wapiti", new string[]
				{
					"YOU BROUGHT THE FIGHT TO US. YOU DO NOT GET TO END IT WITH PAPER.",
					"WE HAVE SIGNED PAPERS BEFORE. THEY MEANT NOTHING. NO."
				}),
				new ValueTuple<string, string[]>("klan", new string[]
				{
					"WE ARE EVERYWHERE. THERE IS NO PEACE FOR YOU.",
					"THE ORDER DOES NOT FORGIVE. IT DOES NOT FORGET."
				}),
				new ValueTuple<string, string[]>("bronte", new string[]
				{
					"MR BRONTE DOES NOT MAKE PEACE WITH DEBTORS. HE COLLECTS.",
					"IN SAINT DENIS AN INSULT IS PAID IN FULL. NO."
				}),
				new ValueTuple<string, string[]>("murfree", new string[]
				{
					"AINT NO PEACE IN THE HOLLER. NEVER WAS.",
					"WE GONNA HAVE YOU YET."
				}),
				new ValueTuple<string, string[]>("skinners", new string[]
				{
					"WE DO NOT TRADE WORDS WITH MEAT.",
					"NO. WE ALREADY PICKED OUT A TREE FOR YOU."
				})
			});
			dictionary["sorry_yes"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"TOOK GUTS TO WRITE THAT. I WILL GIVE YOU THAT.",
					"APOLOGY NOTED. IT HELPS SOME.",
					"DID NOT EXPECT THAT. ALRIGHT.",
					"WORDS ARE CHEAP BUT THESE ONES I KEPT.",
					"WELL. A MAN CAN CHANGE. MAYBE.",
					"ACCEPTED STOP DO NOT MAKE ME SORRY I DID STOP",
					"I READ IT THREE TIMES. ALRIGHT. IT IS ENOUGH.",
					"NOT MANY MEN SAY SORRY. I WILL REMEMBER THAT.",
					"FAIR ENOUGH. WE BOTH HAVE THINGS TO ANSWER FOR.",
					"ALRIGHT. LET IT COOL."
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"THANK YOU. I ACCEPT. PLEASE DO NOT COME BACK.",
					"OH THANK THE LORD.",
					"YES. ACCEPTED. ALL OF IT. THANK YOU.",
					"I ACCEPT. I WAS SO AFRAID YOU WOULD COME."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"I ACCEPT. A MAN WHO OWNS HIS WRONGS IS WORTH SOMETHING.",
					"ACCEPTED. I WILL TELL MY PEOPLE YOU WROTE."
				}),
				new ValueTuple<string, string[]>("Errante", new string[]
				{
					"WATER UNDER THE BRIDGE. MOSTLY.",
					"FORGIVEN. LIFE IS TOO SHORT AND THE ROAD TOO LONG."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"ACCEPTED. A FEW DOLLARS WOULD HAVE SAID IT BETTER."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"ACCEPTED. NOTHING MORE TO SAY."
				})
			});
			dictionary["sorry_no"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"SORRY DOES NOT BRING ANYTHING BACK.",
					"KEEP YOUR SORRY.",
					"TELL IT TO THE UNDERTAKER.",
					"TOO LITTLE. TOO LATE.",
					"SORRY STOP NOT YET YOU ARE NOT STOP",
					"YOU WILL BE SORRIER.",
					"I DO NOT BELIEVE A WORD OF IT.",
					"SAVE IT FOR THE PREACHER."
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"AN APOLOGY BY WIRE. HOW BRAVE.",
					"YOU WILL APOLOGISE IN PERSON. WITH YOUR HAT IN YOUR HAND.",
					"A DOLLAR'S WORTH OF SORRY. I AM WORTH MORE.",
					"YOU SHAMED ME BEFORE A CROWD. SAY IT BEFORE ONE."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"SORRY? YOU WILL BE.",
					"SHOVE IT.",
					"SORRY MY EYE.",
					"WRITE THAT AGAIN AND I COME OVER THERE."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"IRRELEVANT.",
					"IMMATERIAL."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"SAY SORRY TO THE ONES YOU HURT. NOT ME."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"SORRY? THAT IS WHAT DUTCH SAID TOO. LIAR.",
					"SORRY IS A WORD FOR CONFESSION. YE ARE NO PRIEST AND I AM NO FOOL."
				}),
				new ValueTuple<string, string[]>("lemoyneraiders", new string[]
				{
					"SHERMAN WAS SORRY TOO I EXPECT. NO.",
					"A SOUTHERN MAN DOES NOT TAKE SORRY FROM YOUR SORT."
				}),
				new ValueTuple<string, string[]>("wapiti", new string[]
				{
					"SORRY IS A SMALL WORD FOR WHAT WAS DONE."
				}),
				new ValueTuple<string, string[]>("bronte", new string[]
				{
					"IN MY FAMILY SORRY IS SAID WITH MONEY."
				})
			});
			dictionary["threat_scared"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"UNDERSTOOD.",
					"I HEAR YOU. I WILL KEEP AWAY.",
					"NO NEED FOR THAT. YOU WILL NOT SEE ME.",
					"MESSAGE RECEIVED. LOUD AND CLEAR.",
					"ALRIGHT. NO NEED FOR ALL THAT.",
					"YOU HAVE MADE YOUR POINT. I AM DONE.",
					"I WILL STAY OUT OF YOUR WAY STOP",
					"TAKEN TO HEART. NO TROUBLE FROM ME."
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"I WANT NO MORE TROUBLE. I SWEAR IT.",
					"PLEASE. I HAVE DONE NOTHING. LEAVE ME BE.",
					"I AM LEAVING THE COUNTY. YOU WILL NOT SEE ME.",
					"DO NOT HURT ME. I BEG YOU. STOP",
					"I HAVE NOT LEFT MY ROOM SINCE YOUR WIRE CAME.",
					"PLEASE STOP PLEASE STOP PLEASE STOP",
					"I WILL DO ANYTHING. JUST DO NOT COME.",
					"MY HANDS ARE SHAKING AS I WRITE THIS. I AM SORRY. LEAVE ME BE."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"UNDERSTOOD. NO PROFIT IN A GRAVE.",
					"FINE. I HAVE BETTER WAYS TO EARN THAN DYING.",
					"UNDERSTOOD. NOT WORTH IT AT ANY PRICE.",
					"YOU WIN. I HAVE MONEY TO SPEND AND I MEAN TO LIVE TO SPEND IT."
				}),
				new ValueTuple<string, string[]>("Errante", new string[]
				{
					"TAKING THE NEXT TRAIN OUT. NO OFFENCE.",
					"ALREADY SADDLED. SO LONG."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"ALRIGHT ALRIGHT. JAYSUS. I HEAR YE.",
					"EASY NOW. EASY. I WANT NO MORE OF IT. MOTHER OF GOD."
				}),
				new ValueTuple<string, string[]>("lemoyneraiders", new string[]
				{
					"UNDERSTOOD. I WILL KEEP OFF YOUR ROAD."
				}),
				new ValueTuple<string, string[]>("bronte", new string[]
				{
					"UNDERSTOOD. I WILL ASK MR BRONTE FOR OTHER WORK."
				}),
				new ValueTuple<string, string[]>("murfree", new string[]
				{
					"AINT COMIN NEAR YOU. SWEAR ON MAMMY."
				})
			});
			dictionary["threat_angry"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"COME AND TRY IT.",
					"BIG WORDS FROM A MAN WHO HIDES BEHIND A WIRE.",
					"YOU KNOW WHERE I AM. I WILL BE WAITING.",
					"THREATEN ME AGAIN AND I COME TO YOU.",
					"I HAVE BURIED BETTER MEN THAN YOU.",
					"A WIRE IS A POOR SUBSTITUTE FOR A GUN. BRING THE GUN.",
					"I AM NOT AFRAID OF YOU. COME FIND OUT.",
					"PROMISES PROMISES.",
					"YOU HAD BETTER MEAN EVERY WORD.",
					"I WILL KEEP THIS WIRE. FOR THE INQUEST."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"COME AND TRY IT.",
					"YOU AND WHAT ARMY.",
					"I WILL PUT YOU IN THE GROUND MYSELF STOP SOON STOP",
					"SAY IT TO MY FACE COWARD.",
					"WHY WAIT. NAME THE PLACE.",
					"I BROKE THE CLERK'S PENCIL READING IT. YOU ARE NEXT.",
					"THREATEN ME? I WILL CUT YOUR HEART OUT.",
					"COME ON THEN COME ON."
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"A COWARD'S THREAT. PAID FOR BY THE WORD.",
					"NOBODY THREATENS ME. NOBODY.",
					"I WILL FRAME YOUR TELEGRAM. AND YOUR HEAD.",
					"MEN HAVE TRIED TO FRIGHTEN ME BEFORE. THEY ARE ALL DEAD.",
					"I SHOWED YOUR WIRE AROUND THE SALOON. THEY LAUGHED.",
					"YOU DARE."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"THREATEN ME AND YOU THREATEN ALL OF US. REMEMBER THAT.",
					"MY FRIENDS HAVE READ YOUR WIRE. THEY ARE NOT AMUSED."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"YE WANT A FIGHT? THE BOYS AND ME WILL BRING IT.",
					"COME AHEAD THEN. COLM SENDS HIS REGARDS.",
					"THREATS FROM DUTCH'S LAPDOG. I AM SHAKING SO I AM.",
					"COME UP INTO THE HILLS AND SAY THAT. WE WILL BE WAITING."
				}),
				new ValueTuple<string, string[]>("lemoyneraiders", new string[]
				{
					"WE FACED UNION CANNON. YOU ARE NOTHING.",
					"THE GENERAL WILL HEAR OF THIS.",
					"WE STOOD AT SHILOH. YOU DO NOT FRIGHTEN US.",
					"THREATS ARE FOR YANKEE NEWSPAPERS. COME AND FIGHT."
				}),
				new ValueTuple<string, string[]>("wapiti", new string[]
				{
					"WE HAVE BEEN THREATENED BY GENERALS. YOU ARE NOT ONE.",
					"WE DO NOT RUN FROM WORDS."
				}),
				new ValueTuple<string, string[]>("klan", new string[]
				{
					"WE KNOW WHERE YOU SLEEP.",
					"THE ORDER HAS A LONG ARM AND A LONGER ROPE."
				}),
				new ValueTuple<string, string[]>("bronte", new string[]
				{
					"YOU THREATEN A MAN UNDER MR BRONTE'S PROTECTION. BAD BUSINESS.",
					"SAINT DENIS HAS A RIVER. MEN GO IN IT."
				}),
				new ValueTuple<string, string[]>("murfree", new string[]
				{
					"COME ON DOWN THE HOLLER. WE BEEN WAITING.",
					"WE GOT A PLACE FOR YOU IN THE CELLAR."
				}),
				new ValueTuple<string, string[]>("skinners", new string[]
				{
					"COME UP IN THE TREES. WE WILL SKIN YOU SLOW.",
					"BIG TALK FROM A HIDE NOT YET TANNED."
				})
			});
			dictionary["threat_cold"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"NOTED.",
					"I WILL REMEMBER YOU SAID THAT.",
					"IS THAT ALL.",
					"RECEIVED.",
					"UNDERSTOOD. WE WILL SEE.",
					"YOUR CONCERN IS NOTED."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"NOTED. FILED. NOT FORGOTTEN.",
					"YOU ARE WASTING A DOLLAR A WORD.",
					"ADDED TO YOUR ACCOUNT.",
					"I DO NOT FRIGHTEN. CARRY ON."
				})
			});
			dictionary["family_scared"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"LEAVE THEM OUT OF THIS. PLEASE. I WILL DO WHAT YOU WANT.",
					"NOT MY FAMILY. I BEG YOU. I WILL STAY AWAY.",
					"THEY HAVE DONE NOTHING. TAKE IT OUT ON ME. NOT THEM.",
					"ALRIGHT. YOU WIN. JUST STAY AWAY FROM MY HOUSE.",
					"PLEASE. THEY ARE ALL I HAVE.",
					"I WILL NEVER CROSS YOU AGAIN. JUST LEAVE MY HOUSE BE.",
					"WHATEVER YOU WANT. NOT THEM. NOT THEM.",
					"I HAVE SENT THEM AWAY. I BEG YOU DO NOT FOLLOW."
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"PLEASE GOD NO. I WILL LEAVE. I WILL LEAVE TONIGHT.",
					"I AM BEGGING YOU. ON MY CHILDREN.",
					"I WEPT READING IT. PLEASE. I WILL GO ANYWHERE.",
					"I WILL DO ANYTHING STOP ANYTHING STOP"
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"NAME A PRICE. JUST LEAVE THEM BE.",
					"I WILL PAY. WHATEVER YOU ASK. KEEP AWAY FROM THEM."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"YOU WIN. FOR THEIR SAKE ONLY."
				}),
				new ValueTuple<string, string[]>("Errante", new string[]
				{
					"THEY DO NOT EVEN KNOW WHERE I AM. LEAVE THEM BE."
				})
			});
			dictionary["family_rage"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"YOU JUST SIGNED YOUR DEATH WARRANT.",
					"GO NEAR THEM AND I WILL NOT STOP UNTIL YOU ARE DEAD.",
					"NOW IT IS PERSONAL. NOW IT IS FOREVER.",
					"THERE IS NOWHERE YOU CAN HIDE NOW.",
					"A MAN WHO THREATENS CHILDREN IS NO MAN. I WILL END YOU.",
					"YOU CROSSED A LINE NO MAN COMES BACK FROM.",
					"I WILL HUNT YOU TO THE ENDS OF THE EARTH.",
					"SAY THEIR NAMES AGAIN AND I WILL KILL YOU WHERE YOU STAND.",
					"THAT WAS THE LAST MISTAKE YOU WILL EVER MAKE.",
					"YOU WILL NOT LIVE TO SEE THE YEAR OUT."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"MY KIN ARE ALL I HAVE. YOU HAVE MADE AN ENEMY FOR LIFE.",
					"TOUCH ONE HAIR. ONE.",
					"EVERY FRIEND I HAVE WILL KNOW WHAT YOU WROTE.",
					"I WILL STAND AT THEIR DOOR NIGHT AND DAY. AND THEN I WILL COME FOR YOU."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"I AM COMING FOR YOU. RIGHT NOW. STOP",
					"YOU ARE DEAD. YOU HEAR ME. DEAD.",
					"I WILL TEAR YOU APART WITH MY HANDS.",
					"DEAD DEAD DEAD STOP"
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"YOU HAVE NO HONOUR. I WILL TEACH YOU SOME BEFORE YOU DIE.",
					"ONLY THE LOWEST DOG GOES AFTER A MAN'S FAMILY."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"YOU HAVE CHOSEN HOW YOU DIE."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"THREATEN AN IRISHMAN'S MOTHER. YE ARE A DEAD MAN WALKING.",
					"MY MAM WILL SAY A ROSARY FOR YE. YE WILL NEED IT."
				}),
				new ValueTuple<string, string[]>("lemoyneraiders", new string[]
				{
					"YANKEE SAVAGERY. LIKE THEY BURNED OUR HOMES. NEVER AGAIN.",
					"A SOUTHERN MAN DEFENDS HIS HEARTH. YOU WILL LEARN THAT."
				}),
				new ValueTuple<string, string[]>("wapiti", new string[]
				{
					"THE SOLDIERS SAID THE SAME TO MY FATHER. YOU WILL NOT FIND US WEAK.",
					"OUR CHILDREN ARE OUR FUTURE. YOU WILL NOT COME NEAR THEM."
				}),
				new ValueTuple<string, string[]>("bronte", new string[]
				{
					"FAMILY IS SACRED. YOU HAVE INSULTED MINE. NOW YOU PAY."
				})
			});
			dictionary["coming_scared"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"PLEASE DO NOT. I WILL LEAVE TOWN.",
					"I AM ALREADY GONE.",
					"DO NOT COME. I BEG YOU.",
					"BY THE TIME YOU GET HERE I WILL BE IN MEXICO.",
					"THERE IS NOTHING HERE FOR YOU. I AM LEAVING.",
					"TURN BACK. PLEASE. I WILL NOT TROUBLE YOU.",
					"I HAVE PACKED MY BAG STOP",
					"PLEASE DO NOT COME STOP"
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"NO NO NO. PLEASE. I AM NOBODY.",
					"I HAVE BOUGHT A TICKET. YOU WILL NOT FIND ME.",
					"I WILL HIDE. I WILL HIDE WHERE YOU NEVER LOOK.",
					"LORD HAVE MERCY. PLEASE."
				})
			});
			dictionary["coming_ready"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"GOOD. SAVES ME LOOKING.",
					"I WILL BE WAITING.",
					"DOOR IS OPEN. COFFEE IS ON. GUN IS LOADED.",
					"BRING A SHOVEL. FOR YOURSELF.",
					"COME ALONE IF YOU HAVE THE NERVE.",
					"FINALLY.",
					"I WILL KEEP A CHAIR FOR YOU. AND A BOX.",
					"COME SOON. I GROW TIRED OF WAITING.",
					"THE UNDERTAKER IS OUT OF PINE. HURRY BEFORE HE RESTOCKS.",
					"I WILL MEET YOU IN THE STREET AT NOON.",
					"HERE I AM.",
					"CLEANED MY GUN. COME AHEAD."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"I WILL BE WAITING.",
					"I HAVE BEEN EXPECTING YOU.",
					"VERY WELL.",
					"GOOD. LET US FINISH IT."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"WHY WAIT? I AM COMING TO YOU.",
					"HURRY UP THEN.",
					"I WILL MEET YOU HALFWAY.",
					"COME ON. COME ON."
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"I WILL WEAR MY GOOD COAT FOR YOUR FUNERAL.",
					"COME. THE WHOLE TOWN WILL WATCH YOU FALL."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"MY FRIENDS WILL BE WAITING TOO."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"THERE IS A PRICE ON YOU. I WILL COLLECT."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"THE BOYS ARE LOOKING FORWARD TO IT.",
					"COME AHEAD. WE WILL HAVE A WAKE AFTER. YOURS.",
					"WE WILL HAVE THE WHISKEY READY. FOR AFTER.",
					"COME UP TO US BOYO. NOBODY COMES BACK DOWN."
				}),
				new ValueTuple<string, string[]>("lemoyneraiders", new string[]
				{
					"WE WILL HOLD THE LINE. BRING WHAT YOU HAVE.",
					"WE HAVE DUG IN BEFORE. WE WILL AGAIN."
				}),
				new ValueTuple<string, string[]>("wapiti", new string[]
				{
					"WE WILL BE READY."
				}),
				new ValueTuple<string, string[]>("murfree", new string[]
				{
					"COME ON DOWN. WE BEEN SHARPENIN."
				}),
				new ValueTuple<string, string[]>("skinners", new string[]
				{
					"WE WILL HEAR YOU COMING A MILE OFF."
				})
			});
			dictionary["taunt_rage"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"LAUGH WHILE YOU CAN.",
					"YOU WILL EAT THOSE WORDS. AND YOUR TEETH.",
					"EVERY WORD OF THIS I WILL PAY BACK IN LEAD.",
					"KEEP TALKING. IT ONLY MAKES IT SWEETER.",
					"FUNNY MAN. I KNOW A FUNNY END FOR YOU.",
					"YOU WILL NOT BE LAUGHING LONG.",
					"I READ IT ALOUD. NOBODY LAUGHED.",
					"YOUR JOKE COST YOU MORE THAN A DOLLAR."
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"NOBODY MOCKS ME. NOBODY.",
					"I WILL MAKE YOU CRAWL FOR THIS ONE.",
					"YOU WILL BEG MY PARDON IN THE MUD.",
					"YOU HAVE MADE A FOOL OF YOURSELF. NOT ME."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"I WILL KILL YOU. I WILL KILL YOU. I WILL KILL YOU.",
					"SAY THAT TO MY FACE.",
					"I PUT MY FIST THROUGH THE WINDOW READING IT.",
					"LAUGH NOW. SCREAM LATER."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"YE THINK YER FUNNY. VAN DER LINDE BOYS ALWAYS DO. UNTIL THEY DO NOT.",
					"AN IRISHMAN NEVER FORGETS AN INSULT. NEVER."
				}),
				new ValueTuple<string, string[]>("lemoyneraiders", new string[]
				{
					"MOCK A SOUTHERN MAN AT YOUR PERIL."
				}),
				new ValueTuple<string, string[]>("bronte", new string[]
				{
					"YOU MAKE JOKES. I MAKE ARRANGEMENTS."
				})
			});
			dictionary["taunt_shrug"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"A WHOLE DOLLAR FOR THAT. YOU MUST BE BORED.",
					"IS THAT THE BEST YOU HAVE.",
					"NOTED. YAWN.",
					"WASTE OF A TELEGRAM.",
					"THE CLERK LAUGHED. I DID NOT.",
					"TRY HARDER.",
					"MY MOTHER INSULTS ME BETTER.",
					"YOU NEED A HOBBY."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"CHILDISH.",
					"PITIFUL."
				}),
				new ValueTuple<string, string[]>("Errante", new string[]
				{
					"HA. GOOD ONE.",
					"HA. I WILL TELL THAT ONE IN THE NEXT TOWN."
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"PLEASE JUST LEAVE ME ALONE.",
					"I DO NOT WANT ANY TROUBLE. PLEASE."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"YOU PAID GOOD MONEY TO SAY THAT. FOOL."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"WORDS. NOTHING MORE."
				})
			});
			dictionary["money_thanks"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"GOT IT. DOES NOT SQUARE US.",
					"RECEIVED. SOMEWHAT SOFTER ON YOU.",
					"MONEY TALKS. I AM LISTENING.",
					"TEN DOLLARS. A START.",
					"RECEIVED STOP NOT FORGOTTEN STOP",
					"THE MONEY CAME. I WILL THINK BETTER OF YOU. A LITTLE.",
					"CASHED IT. WE WILL SEE.",
					"SURPRISING. ALRIGHT."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"RECEIVED WITH THANKS. MORE WOULD NOT HURT.",
					"NOW WE ARE TALKING. KEEP IT COMING.",
					"TEN? MAKE IT FIFTY AND WE ARE FRIENDS.",
					"AH. NOW YOU ARE A MAN AFTER MY OWN HEART.",
					"SPENT IT ALREADY. SEND MORE.",
					"MONEY HEALS MOST WOUNDS. KEEP SENDING."
				}),
				new ValueTuple<string, string[]>("Errante", new string[]
				{
					"WELL NOW. DRINKS ARE ON YOU THEN.",
					"OBLIGED. THAT IS A WEEK OF BEANS."
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"THANK YOU. TRULY. I WANT NO TROUBLE.",
					"BLESS YOU. I WILL NOT FORGET IT."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"WILL SHARE IT WITH MY PEOPLE. THAT IS ALL."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"RECEIVED. IT CHANGES LITTLE."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"KEPT IT. STILL MAD."
				})
			});
			dictionary["money_insult"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"KEEP YOUR MONEY. I AM NOT FOR SALE.",
					"BLOOD MONEY. SENT IT BACK.",
					"YOU CANNOT BUY ME.",
					"RETURNED. SPEND IT ON YOUR FUNERAL.",
					"NOT ONE CENT OF YOURS WILL I KEEP.",
					"SENT BACK UNOPENED. NEARLY."
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"I DO NOT TAKE ALMS FROM THE LIKES OF YOU.",
					"AN INSULT. RETURNED IN FULL.",
					"DO YOU THINK ME A BEGGAR.",
					"TEN DOLLARS FOR MY HONOUR. YOU INSULT US BOTH.",
					"I AM A MAN OF STANDING. NOT A STREET URCHIN.",
					"TAKE YOUR CHARITY BACK. AND CHOKE ON IT."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"MY PEOPLE ARE NOT FOR SALE AND NEITHER AM I.",
					"THEY WOULD CALL ME BOUGHT. SENT BACK."
				})
			});
			dictionary["payoff_yes"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"TWENTY-FIVE DOLLARS BUYS A LOT OF FORGETTING. DONE.",
					"A FAIR PRICE. WE ARE CLOSER TO SQUARE.",
					"DEAL. DO NOT MAKE ME REGRET IT.",
					"RECEIVED STOP WE ARE NEARLY EVEN STOP",
					"THE MONEY DOES SOME TALKING. ALRIGHT.",
					"TAKEN. I WILL LET IT REST."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"NOW THAT IS AN APOLOGY. ALL IS FORGIVEN. NEARLY.",
					"PLEASURE DOING BUSINESS. WRITE ANY TIME.",
					"YOU SPEAK MY LANGUAGE FRIEND.",
					"TWENTY-FIVE. HANDSOME. WE ARE FRIENDS NOW.",
					"MONEY ON THE BARREL HEAD. THE BEST KIND OF PEACE.",
					"SQUARE. IF YOU WANT TO BE SQUARER YOU KNOW HOW."
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"YES. THANK YOU. WE ARE DONE. WE ARE DONE.",
					"THANK YOU. I DID NOT EVEN NEED IT. I WANTED IT OVER."
				}),
				new ValueTuple<string, string[]>("Errante", new string[]
				{
					"THAT BUYS A HORSE AND A LONG ROAD. DONE."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"ACCEPTABLE."
				})
			});
			dictionary["payoff_no"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"NOT ENOUGH MONEY IN THE WORLD.",
					"SENT IT BACK. I WANT YOU NOT YOUR CASH.",
					"KEEP IT. BUY A COFFIN.",
					"MONEY DOES NOT SETTLE THIS."
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"YOU THINK MY HONOUR HAS A PRICE. IT DOES NOT.",
					"TWENTY-FIVE DOLLARS. IS THAT WHAT YOU THINK I AM WORTH."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"THEY WOULD CALL ME JUDAS. KEEP YOUR SILVER.",
					"I DO NOT SELL OUT MY FRIENDS FOR ANY PRICE."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"I WILL SHOVE IT DOWN YOUR THROAT.",
					"STICK YOUR MONEY. I WANT BLOOD."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"INSUFFICIENT. AND IRRELEVANT."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"NOT ENOUGH. NOT NEAR ENOUGH."
				})
			});
			dictionary["boss_tell"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"{BOSS} RIDES OUT OF {CAMP}. YOU DID NOT HEAR IT FROM ME.",
					"{CAMP}. THAT IS WHERE {BOSS} SLEEPS. LEAVE ME OUT OF IT.",
					"TRY {CAMP}. {BOSS} IS THERE MOST NIGHTS. BURN THIS WIRE.",
					"{BOSS} - {CAMP}. NOW WE ARE SQUARE.",
					"LOOK FOR {BOSS} AT {CAMP}. AND FORGET MY NAME.",
					"{CAMP} STOP {BOSS} STOP NOTHING MORE STOP",
					"{BOSS} KEEPS HIS FIRE AT {CAMP}. I NEVER SAID IT.",
					"YOU WANT {BOSS}. {CAMP}. DO NOT WRITE AGAIN."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"FOR A FRIEND OF MY POCKET - {BOSS} IS AT {CAMP}.",
					"{CAMP}. THAT ONE IS FREE. THE NEXT ONE COSTS.",
					"{BOSS} OWES ME WAGES ANYHOW. {CAMP}.",
					"{CAMP}. IF THERE IS A REWARD I WANT MY CUT."
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"{CAMP}. PLEASE. HE CANNOT KNOW IT WAS ME.",
					"{BOSS} IS AT {CAMP}. I AM LEAVING THE STATE.",
					"{CAMP}. NOW PLEASE LEAVE ME OUT OF IT.",
					"HE IS AT {CAMP}. GOD FORGIVE ME."
				}),
				new ValueTuple<string, string[]>("Errante", new string[]
				{
					"{BOSS} - {CAMP}. I WAS MOVING ON ANYHOW."
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"{BOSS} PASSED ME OVER ONCE TOO OFTEN. {CAMP}."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"{BOSS} HOLDS {CAMP}. IF COLM FINDS OUT I AM A DEAD MAN. BURN THIS.",
					"{CAMP}. MAY THE SAINTS FORGIVE ME. {BOSS} WILL NOT."
				}),
				new ValueTuple<string, string[]>("lemoyneraiders", new string[]
				{
					"{BOSS} HAS HIS HEADQUARTERS AT {CAMP}. I AM DONE WITH THE CAUSE."
				}),
				new ValueTuple<string, string[]>("bronte", new string[]
				{
					"{BOSS} DOES HIS BUSINESS FROM {CAMP}. IF THE FAMILY ASKS I AM IN CHICAGO."
				}),
				new ValueTuple<string, string[]>("murfree", new string[]
				{
					"{BOSS} HOLED UP AT {CAMP}. DONT TELL MAMMY."
				}),
				new ValueTuple<string, string[]>("skinners", new string[]
				{
					"{BOSS} CAMPS AT {CAMP}. HOPE HE SKINS YOU FIRST."
				})
			});
			dictionary["boss_no"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"I AM NO RAT.",
					"NICE TRY.",
					"ASK HIM YOURSELF. IF YOU CAN FIND HIM.",
					"YOU MUST THINK ME A FOOL.",
					"I DO NOT KNOW AND I WOULD NOT SAY.",
					"FIND HIM YOURSELF.",
					"THAT IS NOT FOR SALE.",
					"A RAT DOES NOT LIVE LONG. NEITHER WILL YOU."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"I WOULD DIE FIRST.",
					"{BOSS} TAKES CARE OF HIS OWN. I DO NOT SELL HIM.",
					"NEVER. NOT FOR ALL THE GOLD IN THE WEST.",
					"{BOSS} IS FAMILY. NO."
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"YOU INSULT ME BY ASKING.",
					"I AM NO STOOL PIGEON."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"GO TO HELL. AND TAKE YOUR QUESTIONS.",
					"ASK AGAIN AND I WILL TELL {BOSS} YOU ASKED."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"NOTHING TO SAY TO YOU ON THAT."
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"I CANNOT. HE WOULD KILL ME. I AM SORRY."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"YOU HAVE NOT OFFERED ENOUGH FOR THAT."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"AN O'DRISCOLL DOES NOT SQUEAL. NOT TO DUTCH'S LOT.",
					"COLM HANGS RATS. I SEEN IT. NO."
				}),
				new ValueTuple<string, string[]>("lemoyneraiders", new string[]
				{
					"WE DID NOT GIVE UP OUR OFFICERS TO THE BLUECOATS. NOT TO YOU EITHER.",
					"A SOLDIER DOES NOT BETRAY HIS COMMANDER."
				}),
				new ValueTuple<string, string[]>("wapiti", new string[]
				{
					"YOU WILL NOT LEARN THAT FROM ME.",
					"I DO NOT BETRAY MY PEOPLE."
				}),
				new ValueTuple<string, string[]>("klan", new string[]
				{
					"THE ORDER KEEPS ITS SECRETS. AND ITS OATHS."
				}),
				new ValueTuple<string, string[]>("bronte", new string[]
				{
					"IN MY FAMILY WE DO NOT TALK. EVER."
				}),
				new ValueTuple<string, string[]>("murfree", new string[]
				{
					"AINT TELLIN YOU NOTHIN."
				}),
				new ValueTuple<string, string[]>("skinners", new string[]
				{
					"FIND HIM IN THE TREES. HE WILL FIND YOU FIRST."
				})
			});
			dictionary["distrust_yes"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"...HOW DO YOU KNOW THAT.",
					"I HAD MY DOUBTS ABOUT {BOSS}. NOW I HAVE MORE.",
					"IF THAT IS TRUE {BOSS} WILL ANSWER FOR IT.",
					"I WILL BE WATCHING HIM. DO NOT WRITE AGAIN.",
					"THAT EXPLAINS A GREAT DEAL.",
					"I ALWAYS WONDERED WHERE THE SHARES WENT.",
					"{BOSS} HAS BEEN ACTING STRANGE. NOW I KNOW WHY.",
					"WHO TOLD YOU THIS. NO MATTER. I BELIEVE IT."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"WHAT IS HE TAKING. HOW MUCH. I KNEW IT.",
					"SKIMMING OFF MY SHARE. HE WILL PAY IT BACK DOUBLE."
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"PASSED OVER. I KNEW IT. {BOSS} WILL REGRET IT.",
					"{BOSS} THINKS HE IS BETTER THAN ME. WE WILL SEE."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"I WILL GUT HIM IF IT IS TRUE.",
					"I WILL HAVE IT OUT WITH HIM TONIGHT."
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"OH GOD. WHAT DO I DO NOW."
				}),
				new ValueTuple<string, string[]>("Errante", new string[]
				{
					"TIME I MOVED ON THEN."
				})
			});
			dictionary["distrust_no"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"NICE TRY. {BOSS} IS STRAIGHT.",
					"YOU WANT US AT EACH OTHER'S THROATS. NO.",
					"LIES. I KNOW WHAT YOU ARE DOING.",
					"{BOSS} WILL LAUGH WHEN I SHOW HIM THIS.",
					"YOU MUST BE DESPERATE.",
					"I KNOW A SNAKE WHEN I READ ONE."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"{BOSS} PULLED ME OUT OF THE GUTTER. I WOULD TRUST HIM WITH MY LIFE.",
					"WE STAND TOGETHER. YOUR WIRE CHANGES NOTHING.",
					"I HAVE RIDDEN WITH {BOSS} FOR YEARS. YOU ARE A LIAR.",
					"SHOWED {BOSS} YOUR WIRE. HE SENDS HIS REGARDS."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"TRANSPARENT. TRY HARDER.",
					"CRUDE."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"YOU LYING DOG."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"DUTCH TRIED THAT TRICK ON COLM. DID NOT WORK THEN EITHER.",
					"TURN AN IRISHMAN ON HIS OWN? YE DO NOT KNOW US."
				}),
				new ValueTuple<string, string[]>("lemoyneraiders", new string[]
				{
					"BROTHERS IN ARMS DO NOT TURN ON EACH OTHER."
				}),
				new ValueTuple<string, string[]>("bronte", new string[]
				{
					"THE FAMILY HAS NO TRAITORS. ONLY CORPSES."
				})
			});
			dictionary["kin_rage"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"YOU WROTE TO GLOAT. I WILL SEE YOU IN HELL.",
					"{KIN}. YOU SAY HIS NAME AGAIN AND I CUT OUT YOUR TONGUE.",
					"I BURIED MY {REL}. SOON I BURY YOU NEXT TO HIM.",
					"EVERY NIGHT I SEE HIM. EVERY NIGHT I SEE YOU DYING.",
					"NOTHING YOU WRITE WILL MAKE IT QUICK.",
					"{KIN} DESERVED BETTER. YOU DESERVE WORSE. YOU WILL GET IT.",
					"YOU KILLED MY {REL} AND NOW YOU MOCK HIM. YOU ARE DEAD.",
					"I CARVED YOUR NAME ON A BULLET THE DAY WE BURIED {KIN}.",
					"I SWORE OVER HIS GRAVE. I KEEP MY OATHS.",
					"THE LAST THING YOU HEAR WILL BE HIS NAME."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"HE WAS WORTH TEN OF YOU. I WILL AVENGE HIM.",
					"FAMILY IS EVERYTHING. YOU TOOK MINE.",
					"{KIN} WOULD HAVE DONE THE SAME FOR ME. I AM COMING.",
					"WE ALL SWORE ON {KIN}. ALL OF US."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"I AM COMING. I AM COMING. I AM COMING.",
					"YOU FILTHY DOG. I WILL GUT YOU."
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"YOU WILL NOT DISHONOUR HIS MEMORY AND LIVE."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"{KIN} IS ON YOUR ACCOUNT. IT WILL BE SETTLED."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"THE IRISH BURY THEIR DEAD AND THEN THEY GET EVEN.",
					"WE WAKED {KIN} FOR THREE DAYS. YOU WILL NOT GET ONE."
				}),
				new ValueTuple<string, string[]>("lemoyneraiders", new string[]
				{
					"{KIN} SURVIVED THE WAR TO BE KILLED BY YOU. I WILL NOT REST."
				}),
				new ValueTuple<string, string[]>("bronte", new string[]
				{
					"YOU KILLED FAMILY. IN SAINT DENIS THAT MEANS ONE THING."
				})
			});
			dictionary["kin_grief"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"WHY WOULD YOU WRITE THAT. WHY.",
					"HE HAD A FAMILY. DID YOU KNOW THAT.",
					"LEAVE ME TO GRIEVE. PLEASE.",
					"I HAVE NOTHING LEFT. WHAT MORE DO YOU WANT.",
					"MY {REL} IS IN THE GROUND. IS THAT NOT ENOUGH.",
					"I STILL SET A PLACE FOR HIM. LEAVE ME BE."
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"PLEASE. AM I NEXT. PLEASE DO NOT.",
					"I WILL NOT COME FOR YOU. I SWEAR. JUST STOP.",
					"I CANNOT STOP SHAKING. PLEASE NO MORE.",
					"I AM NOT LIKE {KIN}. PLEASE."
				}),
				new ValueTuple<string, string[]>("Errante", new string[]
				{
					"HE KNEW WHAT LIFE HE CHOSE. SO DO I. LEAVE IT.",
					"WE ALL END UP IN THE DIRT. HE GOT THERE FIRST. LEAVE IT."
				})
			});
			dictionary["regards_warm"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"GOOD TO HEAR FROM YOU FRIEND. COME BY WHEN YOU ARE NEAR.",
					"THANK YOU. YOU ARE A GOOD SORT WHATEVER THEY SAY.",
					"WARMED MY HEART. DOOR IS ALWAYS OPEN.",
					"WELL I NEVER. A TELEGRAM. FIRST ONE IN YEARS. THANK YOU.",
					"KIND OF YOU TO THINK OF ME. STAY SAFE.",
					"YOU MADE MY WEEK. THERE IS A PLATE FOR YOU HERE ANY NIGHT.",
					"SHOWED YOUR WIRE TO EVERYBODY. THEY ALL SEND REGARDS BACK.",
					"GOD KEEP YOU ON THE ROAD FRIEND.",
					"WHAT A THING TO GET ON A TUESDAY. THANK YOU KINDLY.",
					"SAME TO YOU AND MORE. COME SEE US SOON."
				})
			});
			dictionary["wish_yes"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"SAME TO YOU. I SUPPOSE.",
					"DID NOT EXPECT THAT. ALRIGHT. LOOK AFTER YOURSELF TOO.",
					"KIND WORDS. I WILL TAKE THEM.",
					"FUNNY OLD WORLD. THANK YOU STOP",
					"NO HARD FEELINGS THEN. MOSTLY.",
					"READ IT THREE TIMES. STILL DO NOT KNOW WHAT TO MAKE OF YOU. THANKS."
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"THANK YOU. TRULY. I WANT NO TROUBLE.",
					"BLESS YOU FOR WRITING. I SLEPT BETTER."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"DECENT OF YOU. I WILL REMEMBER IT.",
					"A MAN WHO WRITES THAT IS NOT ALL BAD. THANKS."
				}),
				new ValueTuple<string, string[]>("Errante", new string[]
				{
					"AND YOU. ROAD IS LONG. KEEP YOUR HEAD DOWN.",
					"SURE. NO HARD FEELINGS. WELL. FEWER."
				})
			});
			dictionary["wish_no"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"WORDS ARE CHEAP.",
					"I DO NOT WANT YOUR WELL WISHES.",
					"RECEIVED. CHANGES NOTHING.",
					"KEEP YOUR KINDNESS. I KEEP MY GRUDGE.",
					"NICE WORDS FROM A MAN LIKE YOU. NO."
				})
			});
			dictionary["wish_scorn"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"GETTING SOFT ARE WE.",
					"YOU WRITE LIKE A MAN WHO IS AFRAID.",
					"LOOK AFTER MYSELF. HA. LOOK AFTER YOURSELF.",
					"IS THAT A WHITE FLAG I SEE STOP"
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"I NEED NOTHING FROM YOU. NOT EVEN THAT.",
					"DO NOT PITY ME. I AM NOT A DOG."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"SAVE YOUR INK. SAVE YOUR SKIN WHILE YOU ARE AT IT.",
					"SOFT WORDS. I WILL GIVE YOU HARD ONES WHEN WE MEET."
				})
			});
			dictionary["condole_yes"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"THANK YOU. {KIN} WAS A GOOD MAN WHATEVER THEY SAY.",
					"DID NOT THINK YOU HAD IT IN YOU. THANK YOU FOR {KIN}.",
					"IT MEANS SOMETHING. MORE THAN I CAN SAY BY WIRE.",
					"BURIED HIM LAST WEEK. YOUR WIRE CAME THE SAME DAY. THANK YOU.",
					"HE WOULD HAVE LAUGHED AT YOU WRITING. I DO NOT."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"{KIN} WAS FAMILY. THANK YOU FOR SAYING SO.",
					"I WILL NOT FORGET YOU WROTE."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"THE BOYS DRANK TO {KIN}. I DRANK TO YOUR WIRE TOO. JUST THE ONCE."
				})
			});
			dictionary["condole_cold"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"NOTED.",
					"{KIN} KNEW THE RISKS. STILL. NOTED.",
					"RECEIVED. PEOPLE DIE.",
					"KIND OF YOU. IT CHANGES NOTHING. BUT KIND."
				})
			});
			dictionary["gift_yes"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"{LEADER} TOOK YOUR MONEY. HE SAYS YOU KNOW YOUR MANNERS.",
					"RECEIVED. THE BOSS WAS PLEASED. FOR NOW.",
					"{LEADER} LAUGHED AND POCKETED IT. YOU ARE LESS OF A NUISANCE TO HIM TODAY.",
					"GIFT RECEIVED STOP NOTED STOP",
					"THE BOSS SAYS A MAN WHO PAYS RESPECT IS A MAN WHO CAN BE TALKED TO."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"MONEY TALKS. YOURS SAID THE RIGHT THING.",
					"MORE WHERE THAT CAME FROM AND WE MIGHT BE FRIENDS."
				}),
				new ValueTuple<string, string[]>("bronte", new string[]
				{
					"MR {LEADER} APPRECIATES A GESTURE. HE WILL REMEMBER IT.",
					"THE FAMILY ACCEPTS. BUSINESS IS BUSINESS."
				})
			});
			dictionary["gift_yes_self"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"ACEPTÉ TU DINERO. TIENES MODALES.",
					"RECIBIDO. ESTOY SATISFECHO. POR AHORA.",
					"ME LO GUARDÉ. HOY ME RESULTAS MENOS MOLESTO.",
					"UN HOMBRE QUE MUESTRA RESPETO ES UN HOMBRE CON QUIEN PUEDO HABLAR."
				})
			});
			dictionary["gift_back_self"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"TE LO DEVUELVO. NO ESTOY EN VENTA.",
					"QUÉDATE TU DINERO. NO ACEPTO CARIDAD DE GENTE COMO TÚ."
				})
			});
			dictionary["gift_scorn_self"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"TE BURLAS DE MÍ. AUN ASÍ ME QUEDÉ EL DINERO.",
					"UN INSULTO. GRACIAS POR LAS BALAS QUE COMPRÓ. NO ENVÍES MÁS."
				})
			});
			dictionary["gift_back"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"{LEADER} SENDS IT BACK. HE IS NOT FOR SALE.",
					"KEEP YOUR MONEY. THE BOSS DOES NOT TAKE CHARITY FROM YOUR KIND.",
					"RETURNED. YOU CANNOT BUY YOUR WAY OUT OF THIS."
				})
			});
			dictionary["gift_scorn"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"{LEADER} THINKS YOU ARE MOCKING HIM. HE KEPT THE MONEY THOUGH.",
					"AN INSULT. THE BOSS SAYS THANKS FOR THE BULLETS IT BOUGHT.",
					"HE TOOK IT AND SPAT. DO NOT SEND MORE."
				})
			});
			dictionary["checkin_warm"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"GOOD TO HEAR FROM YOU. THINGS ARE FINE. COME BY.",
					"ALL WELL. WIFE SAYS HELLO.",
					"FINE THANKS. WRITE AGAIN.",
					"THINGS ARE QUIET. QUIET IS GOOD. YOU KEEP SAFE.",
					"NICE TO BE THOUGHT OF. ALL WELL HERE STOP"
				})
			});
			dictionary["checkin_plain"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"FINE. YOU.",
					"ALL WELL ENOUGH.",
					"NOTHING TO REPORT. THANKS FOR ASKING.",
					"GETTING BY. SAME AS EVERYONE.",
					"DID NOT EXPECT A WIRE. ALL FINE."
				})
			});
			dictionary["pgift_yes"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"THAT IS TOO MUCH. THANK YOU.",
					"BOUGHT SHOES FOR THE BOY. THANK YOU KINDLY.",
					"YOU DID NOT HAVE TO. GLAD YOU DID.",
					"RECEIVED WITH THANKS. I OWE YOU.",
					"A GOOD DEED. I WILL REMEMBER IT."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"NOW THAT IS A TELEGRAM. THANK YOU.",
					"MUCH OBLIGED. ANY TIME YOU FEEL GENEROUS."
				})
			});
			dictionary["pgift_back"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"I DO NOT TAKE CHARITY. BUT THANK YOU.",
					"SENDING IT BACK. KIND THOUGHT THOUGH.",
					"KEEP IT. I GET BY."
				})
			});
			dictionary["pcondole_yes"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"THANK YOU. {KIN} WAS A GOOD FRIEND.",
					"STILL CANNOT BELIEVE IT. THANK YOU FOR WRITING.",
					"FEW PEOPLE WROTE. YOU DID. THANK YOU.",
					"WE BURIED {KIN} ON SUNDAY. KIND OF YOU."
				})
			});
			dictionary["pcondole_rage"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"YOU HAVE SOME NERVE. I KNOW WHAT YOU DID.",
					"SORRY. YOU KILLED HIM AND YOU ARE SORRY.",
					"DO NOT WRITE TO ME AGAIN.",
					"I KNOW IT WAS YOU. EVERYONE KNOWS."
				})
			});
			dictionary["regards_plain"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"KIND OF YOU. ALL WELL HERE.",
					"THANKS. SAME TO YOU.",
					"WELL ENOUGH. WEATHER COULD BE BETTER.",
					"RECEIVED. KEEP WELL.",
					"OBLIGED. NOTHING NEW HERE.",
					"GOOD TO HEAR. ALL QUIET THIS END.",
					"THANK YOU. CROPS ARE SLOW. OTHERWISE FINE.",
					"MUCH OBLIGED. TAKE CARE OF YOURSELF."
				})
			});
			dictionary["regards_wary"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"THANK YOU. I THINK.",
					"WHY ARE YOU WRITING TO ME.",
					"I AM FINE. PLEASE DO NOT VISIT.",
					"UNDERSTOOD. ALL QUIET HERE. VERY QUIET.",
					"RECEIVED. NO NEED TO WRITE AGAIN.",
					"I HAVE SAID NOTHING TO ANYBODY. ALL WELL.",
					"THANK YOU. I WOULD RATHER YOU DID NOT.",
					"WELL. ALRIGHT. GOOD DAY."
				})
			});
			dictionary["sorry_ok"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"WATER UNDER THE BRIDGE.",
					"APOLOGY ACCEPTED. TAKES A BIG MAN.",
					"LET US FORGET IT.",
					"I APPRECIATE THAT. TRULY.",
					"ALL FORGIVEN. COME BY SOMETIME.",
					"THANK YOU FOR WRITING. IT MEANS A GOOD DEAL.",
					"WE ARE SQUARE FRIEND.",
					"ALREADY FORGOTTEN. NEARLY."
				})
			});
			dictionary["sorry_cold"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"WORDS ARE CHEAP. BUT I READ THEM.",
					"WE WILL SEE.",
					"IT WILL TAKE MORE THAN A WIRE.",
					"NOTED.",
					"I HEAR YOU. I AM STILL SORE.",
					"MAYBE IN TIME.",
					"IT IS A START. ONLY A START.",
					"WE SHALL SEE HOW YOU CONDUCT YOURSELF."
				})
			});
			dictionary["warn_quiet"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"I SAW NOTHING. I SWEAR.",
					"MY LIPS ARE SEALED.",
					"I DO NOT KNOW WHAT YOU MEAN. AND I NEVER WILL.",
					"NOT A WORD. ON MY MOTHER.",
					"I WAS NOT EVEN THERE THAT DAY.",
					"I AM DEAF AND BLIND MISTER. ALWAYS HAVE BEEN.",
					"NOTHING TO TELL. NOTHING TO TELL ANYBODY.",
					"I UNDERSTAND. NOT ONE WORD."
				})
			});
			dictionary["warn_defy"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"DO NOT THREATEN ME.",
					"I WILL SAY WHAT I PLEASE.",
					"THE SHERIFF WILL HEAR ABOUT THIS WIRE TOO.",
					"SHAME ON YOU.",
					"I AM AN HONEST MAN. I TELL THE TRUTH.",
					"THREATS BY TELEGRAM. YOU SHOULD BE ASHAMED.",
					"THE MARSHAL WILL WANT TO READ THIS.",
					"YOU DO NOT FRIGHTEN ME MISTER."
				})
			});
			dictionary["warn_late"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"TOO LATE FOR THAT. THE LAW ALREADY KNOWS.",
					"I ALREADY TOLD THEM. I AM SORRY. NO I AM NOT.",
					"THE SHERIFF HAS MY STATEMENT. NOTHING TO BE DONE.",
					"TOLD THE LAW THE DAY IT HAPPENED. TOO LATE."
				})
			});
			dictionary["thanks_warm"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"IT IS I WHO OWE YOU. NEVER FORGET IT.",
					"ANY TIME FRIEND. ANY TIME.",
					"YOU HELPED ME WHEN NOBODY WOULD. I REMEMBER.",
					"YOU ARE ALWAYS WELCOME AT MY TABLE.",
					"I PRAY FOR YOU EVERY SUNDAY. THAT IS THE TRUTH.",
					"NO THANKS NEEDED BETWEEN US.",
					"IF YOU EVER NEED ANYTHING YOU KNOW WHERE I AM.",
					"TOLD MY WHOLE FAMILY WHAT YOU DID. YOU ARE A HERO HERE."
				})
			});
			dictionary["thanks_plain"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"THANK YOU FOR THE THANKS. HA.",
					"NO NEED. BUT KIND.",
					"YOU ARE WELCOME.",
					"THINK NOTHING OF IT.",
					"OBLIGED TO YOU TOO.",
					"WAS NOTHING. GOOD DAY TO YOU."
				})
			});
			dictionary["drink_yes"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"I WILL DRINK TO YOU TONIGHT. NEXT ONE IS MINE.",
					"THAT IS A ROUND I WILL REMEMBER. MUCH OBLIGED.",
					"WHISKEY TASTES BETTER FREE. THANK YOU.",
					"RAISED A GLASS. THE WHOLE BAR DID. YOU ARE POPULAR HERE NOW.",
					"BOUGHT THE GOOD STUFF. NOT THE ROTGUT. TO YOUR HEALTH.",
					"A BEER AND A WHISKEY ON YOU. A FINE EVENING.",
					"DRANK IT SLOW. SAVOURED EVERY DROP. THANKS FRIEND.",
					"TO YOU. AND TO NEXT TIME WE DRINK TOGETHER."
				})
			});
			dictionary["drink_no"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"I DO NOT DRINK. GAVE IT TO THE CHURCH.",
					"SENT IT BACK. I DO NOT TAKE MONEY FROM YOUR KIND.",
					"KIND BUT NO THANK YOU.",
					"SIGNED THE PLEDGE LAST YEAR. RETURNED WITH THANKS.",
					"MY WIFE WOULD SKIN ME. SENT BACK.",
					"NO THANK YOU. I PAY FOR MY OWN."
				})
			});
			dictionary["loan_yes"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"TEN DOLLARS ENCLOSED. PAY IT BACK WHEN YOU CAN.",
					"HERE. DO NOT DRINK IT ALL.",
					"FOR A FRIEND. TEN ENCLOSED. NO HURRY.",
					"I AM NOT RICH BUT HERE IS WHAT I CAN SPARE.",
					"SENT WHAT I HAD UNDER THE MATTRESS. GOOD LUCK.",
					"TAKE IT. YOU DID RIGHT BY ME ONCE.",
					"HERE IS TEN. MIND YOU DO NOT GAMBLE IT.",
					"A LOAN. NOT A GIFT. BUT TAKE YOUR TIME."
				})
			});
			dictionary["loan_no"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"I HAVE NOTHING TO SPARE. SORRY.",
					"TIMES ARE HARD HERE TOO.",
					"ASK SOMEBODY ELSE.",
					"NO. AND DO NOT ASK AGAIN.",
					"THE BANK HAS MONEY. I DO NOT.",
					"WISH I COULD. CANNOT.",
					"BARELY FEEDING MY OWN. SORRY FRIEND.",
					"I DO NOT LEND. NOT TO ANYONE."
				})
			});
			dictionary["asking_none"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"NOBODY HAS ASKED. YOU ARE SAFE HERE FOR NOW.",
					"QUIET. NO STRANGERS ASKING.",
					"NOT THAT I HEARD. I WILL KEEP AN EAR OUT.",
					"NOBODY. TOWN IS DULL AS DISHWATER.",
					"NO ONE HAS SAID YOUR NAME. NOT IN MY HEARING.",
					"ALL QUIET. ASKED THE BARKEEP TOO. NOTHING."
				})
			});
			dictionary["asking_name"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"{MAN} WAS ASKING AFTER YOU IN {TOWN}. WATCH YOURSELF.",
					"A MAN CALLED {MAN} WANTS TO KNOW WHERE YOU ARE. I TOLD HIM NOTHING.",
					"{MAN} HAS BEEN DRINKING IN {TOWN} AND CURSING YOUR NAME.",
					"BE CAREFUL. {MAN} IS LOOKING FOR YOU ROUND {TOWN}.",
					"{MAN} STOP {TOWN} STOP ASKING FOR YOU STOP ARMED STOP",
					"{MAN} SHOWED THE BARKEEP IN {TOWN} A DOLLAR FOR NEWS OF YOU.",
					"STAY CLEAR OF {TOWN}. {MAN} IS THERE AND HE MEANS YOU HARM.",
					"{MAN} ASKED ME BY NAME. IN {TOWN}. I SAID I NEVER HEARD OF YOU."
				})
			});
			dictionary["lookout_yes"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"I WILL KEEP MY EYES OPEN FOR YOU.",
					"YOU CAN COUNT ON ME. I WILL WIRE IF I HEAR ANYTHING.",
					"CONSIDER IT DONE FRIEND.",
					"I WILL WATCH THE ROAD AND THE SALOON. YOU WILL HEAR.",
					"AGREED. NOBODY COMES INTO TOWN I DO NOT SEE.",
					"I OWE YOU THAT MUCH. I WILL WATCH."
				})
			});
			dictionary["lookout_no"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"I WANT NO PART OF YOUR TROUBLE.",
					"I MIND MY OWN BUSINESS. YOU SHOULD TOO.",
					"NO. I HAVE A FAMILY TO THINK OF.",
					"I AM NO SPY. SORRY.",
					"TOO DANGEROUS FOR ME. GOOD LUCK.",
					"ASK SOMEONE WITH LESS TO LOSE."
				})
			});
			dictionary["in_threat"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"I HAVE NOT FORGOTTEN YOU. NOT FOR ONE DAY.",
					"SLEEP WELL WHILE YOU CAN.",
					"I KNOW WHICH STATIONS YOU USE.",
					"YOUR DAYS ARE COUNTED. I AM COUNTING THEM.",
					"EVERY TOWN YOU RIDE INTO I HAVE A FRIEND IN.",
					"LOOK OVER YOUR SHOULDER. ALWAYS.",
					"I SAW YOU LAST WEEK. YOU DID NOT SEE ME.",
					"THINKING OF YOU STOP NOT KINDLY STOP",
					"I AM CLOSER THAN YOU THINK.",
					"ONE DAY YOU WILL TURN A CORNER AND I WILL BE THERE.",
					"I HAVE BEEN SAVING A BULLET. IT HAS YOUR NAME ON IT.",
					"THE DEBT STILL STANDS. IT IS DUE SOON."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"WHERE ARE YOU HIDING COWARD.",
					"COME OUT AND FIGHT.",
					"I AM SICK OF WAITING. SHOW YOUR FACE.",
					"COWARD COWARD COWARD STOP"
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"PEOPLE STILL TALK ABOUT WHAT YOU DID. SOON THEY WILL TALK ABOUT WHAT I DID TO YOU.",
					"I WILL HAVE MY NAME BACK. AND YOUR HIDE WITH IT."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"SOON.",
					"PATIENCE IS A VIRTUE. I HAVE PLENTY."
				}),
				new ValueTuple<string, string[]>("Leal", new string[]
				{
					"MY FRIENDS ARE LOOKING TOO. ALL OF THEM."
				}),
				new ValueTuple<string, string[]>("Codicioso", new string[]
				{
					"THERE IS MONEY ON YOUR HEAD. I MEAN TO COLLECT."
				}),
				new ValueTuple<string, string[]>("Errante", new string[]
				{
					"THOUGHT I WOULD LET IT GO. TURNS OUT I CANNOT."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"COLM SAYS HELLO. SO DO I. SEE YOU SOON BOYO.",
					"THE BOYS DRINK TO YOUR FUNERAL EVERY NIGHT. SOON IT WILL BE REAL."
				}),
				new ValueTuple<string, string[]>("lemoyneraiders", new string[]
				{
					"LEMOYNE IS NOT SAFE FOR YOU. NOWHERE IS.",
					"WE HAVE MEN IN EVERY PARISH. YOU WILL NOT SLIP US."
				}),
				new ValueTuple<string, string[]>("klan", new string[]
				{
					"WE RIDE AT NIGHT. LISTEN FOR US."
				}),
				new ValueTuple<string, string[]>("bronte", new string[]
				{
					"SAINT DENIS HAS MANY ALLEYS. MR BRONTE OWNS THEM ALL."
				}),
				new ValueTuple<string, string[]>("murfree", new string[]
				{
					"WE BEEN WATCHIN YOU FROM THE TREES."
				}),
				new ValueTuple<string, string[]>("skinners", new string[]
				{
					"WE GOT YOUR SCENT."
				})
			});
			dictionary["in_plea"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"PLEASE LEAVE ME BE. I HAVE DONE WITH ALL OF IT.",
					"I AM OUT. I SWEAR. DO NOT COME FOR ME.",
					"I WANT TO LIVE. PLEASE. I WILL NOT CROSS YOU AGAIN.",
					"I HAVE TAKEN HONEST WORK. PLEASE LET ME KEEP IT.",
					"I DREAM OF YOU EVERY NIGHT. PLEASE MAKE IT STOP.",
					"WHATEVER I DID I AM SORRY STOP PLEASE STOP"
				}),
				new ValueTuple<string, string[]>("Cobarde", new string[]
				{
					"I CANNOT EAT. I CANNOT SLEEP. PLEASE FORGIVE ME.",
					"I HAVE A LITTLE MONEY. IT IS YOURS. JUST LET ME BE."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"I AM DONE WITH THE BOYS. I SWEAR IT ON ME MAM. LEAVE ME BE."
				})
			});
			dictionary["in_regards"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"THINKING OF YOU. HOPE THE ROAD IS KIND.",
					"ALL WELL HERE. COME BY IF YOU ARE NEAR. SUPPER IS ON ME.",
					"JUST WANTED YOU TO KNOW SOMEBODY HERE IS GLAD YOU EXIST.",
					"THE CHILDREN ASK AFTER YOU. SAFE TRAVELS.",
					"SPRING HAS COME. THOUGHT OF YOU. STAY WELL.",
					"WE SAID GRACE FOR YOU SUNDAY. GOD KEEP YOU.",
					"THE PIE IS BETTER THIS YEAR. COME TRY SOME.",
					"NO NEWS. JUST HELLO FROM AN OLD FRIEND."
				})
			});
			dictionary["in_tip"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"THOUGHT YOU SHOULD KNOW. {MAN} IS IN {TOWN} ASKING QUESTIONS ABOUT YOU.",
					"KEEPING MY EYES OPEN LIKE YOU ASKED. {MAN} WAS SEEN ROUND {TOWN}.",
					"A FRIEND WARNS YOU. {MAN}. {TOWN}. ARMED.",
					"{MAN} PASSED THROUGH {TOWN} WITH A HARD LOOK. HE SPOKE YOUR NAME.",
					"{MAN} BOUGHT CARTRIDGES IN {TOWN} TODAY. A GREAT MANY. HE ASKED AFTER YOU.",
					"WATCH {TOWN}. {MAN} HAS TAKEN A ROOM THERE.",
					"{MAN} IS DRINKING IN {TOWN} AND TALKING BIG ABOUT YOU. BE CAREFUL.",
					"SAW {MAN} RIDE INTO {TOWN} AT DAWN. THOUGHT YOU OUGHT TO KNOW."
				})
			});
			dictionary["in_gloat"] = TelegramLines.P(new ValueTuple<string, string[]>[]
			{
				new ValueTuple<string, string[]>("", new string[]
				{
					"HOW DOES THE DIRT TASTE. I CAN STILL HEAR YOU FALL.",
					"THAT WAS THE FIRST TIME. IT WILL NOT BE THE LAST.",
					"TELL EVERYONE WHO PUT YOU DOWN.",
					"STILL SORE. GOOD.",
					"YOU WENT DOWN LIKE A SACK OF FEED.",
					"NEXT TIME I WILL MAKE SURE YOU STAY DOWN."
				}),
				new ValueTuple<string, string[]>("Orgulloso", new string[]
				{
					"EVERY SALOON FROM HERE TO SAINT DENIS KNOWS MY NAME NOW. THANK YOU FOR THAT.",
					"THEY SING SONGS ABOUT ME NOW. ABOUT THE DAY I BEAT YOU."
				}),
				new ValueTuple<string, string[]>("Impulsivo", new string[]
				{
					"THAT FELT GOOD. LET US DO IT AGAIN."
				}),
				new ValueTuple<string, string[]>("Frío", new string[]
				{
					"YOU WERE SLOW. NEXT TIME BE QUICKER. OR DO NOT."
				}),
				new ValueTuple<string, string[]>("odriscoll", new string[]
				{
					"THE BOYS STILL LAUGH ABOUT IT. ONE OF DUTCH'S FINEST. FACE IN THE MUD."
				})
			});
			TelegramLines.Lines = dictionary;
			TelegramLines._recent = new LinkedList<string>();
			TelegramLines._recentSet = new HashSet<string>();
		}

		// Token: 0x04000C50 RID: 3152
		private static readonly Dictionary<string, Dictionary<string, string[]>> Lines;

		// Token: 0x04000C51 RID: 3153
		private static readonly LinkedList<string> _recent;

		// Token: 0x04000C52 RID: 3154
		private static readonly HashSet<string> _recentSet;

		// Token: 0x04000C53 RID: 3155
		public static Func<string, string> LeaderNameOf;
	}
}
