using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NemesisSystem
{
	// Token: 0x02000004 RID: 4
	internal static class ApiCheck
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		internal static List<string> Missing(Assembly mod)
		{
			int num;
			return ApiCheck.Missing(mod, out num);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002075 File Offset: 0x00000275
		internal static List<string> Missing(Assembly mod, out int checkedApi)
		{
			return ApiCheck.Missing(mod, out checkedApi, null, null);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002080 File Offset: 0x00000280
		internal static List<string> Missing(Assembly mod, out int checkedApi, ICollection<string> found, ICollection<string> sigs = null)
		{
			checkedApi = 0;
			SortedSet<string> sortedSet = new SortedSet<string>();
			HashSet<int> hashSet = new HashSet<int>();
			Module manifestModule = mod.ManifestModule;
			Type[] array;
			try
			{
				array = mod.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				array = (from t in ex.Types
				where t != null
				select t).ToArray<Type>();
			}
			foreach (Type type in array)
			{
				foreach (MethodBase methodBase in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Cast<MethodBase>().Concat(type.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)))
				{
					byte[] array3;
					try
					{
						MethodBody methodBody = methodBase.GetMethodBody();
						array3 = ((methodBody != null) ? methodBody.GetILAsByteArray() : null);
					}
					catch
					{
						continue;
					}
					if (array3 != null)
					{
						int num = 0;
						while (num + 4 < array3.Length)
						{
							byte b = array3[num];
							if (b == 40 || b == 111 || b == 115 || (b == 254 && array3[num + 1] == 6))
							{
								int num2 = (b == 254) ? (num + 2) : (num + 1);
								if (num2 + 4 <= array3.Length)
								{
									int num3 = BitConverter.ToInt32(array3, num2);
									if (num3 >> 24 == 10 && hashSet.Add(num3))
									{
										try
										{
											MemberInfo memberInfo = manifestModule.ResolveMember(num3);
											Type declaringType = memberInfo.DeclaringType;
											bool flag;
											if (declaringType == null)
											{
												flag = false;
											}
											else
											{
												string @namespace = declaringType.Namespace;
												flag = ((@namespace != null) ? new bool?(@namespace.StartsWith("RDR2")) : null).GetValueOrDefault();
											}
											if (flag)
											{
												checkedApi++;
												if (found != null)
												{
													found.Add(memberInfo.DeclaringType.Name + "." + memberInfo.Name);
												}
												if (sigs != null)
												{
													string fullName = memberInfo.DeclaringType.FullName;
													string str = ".";
													MemberInfo memberInfo2 = memberInfo;
													sigs.Add(fullName + str + ((memberInfo2 != null) ? memberInfo2.ToString() : null));
												}
											}
										}
										catch (MissingMemberException ex2)
										{
											if (ex2.Message.Contains("RDR2."))
											{
												sortedSet.Add(ApiCheck.Strip(ex2.Message));
											}
										}
										catch
										{
										}
									}
								}
							}
							num++;
						}
					}
				}
			}
			return sortedSet.ToList<string>();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000235C File Offset: 0x0000055C
		private static string Strip(string message)
		{
			int num = message.IndexOf('\'');
			int num2 = message.LastIndexOf('\'');
			if (num < 0 || num2 <= num)
			{
				return message;
			}
			return message.Substring(num + 1, num2 - num - 1);
		}
	}
}
