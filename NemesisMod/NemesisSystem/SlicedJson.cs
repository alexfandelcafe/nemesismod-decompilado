using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NemesisSystem
{
	// Token: 0x0200006C RID: 108
	public sealed class SlicedJson
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x001228E1 File Offset: 0x00120AE1
		public object Root { get; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x001228E9 File Offset: 0x00120AE9
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x001228F1 File Offset: 0x00120AF1
		public bool Done { get; private set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x001228FA File Offset: 0x00120AFA
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x00122902 File Offset: 0x00120B02
		public int Steps { get; private set; }

		// Token: 0x0600128D RID: 4749 RVA: 0x0012290C File Offset: 0x00120B0C
		public SlicedJson(object root)
		{
			this.Root = root;
			this._w = new JsonTextWriter(this._sw)
			{
				Formatting = this._ser.Formatting
			};
			this._steps = this.Walk(root).GetEnumerator();
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00122980 File Offset: 0x00120B80
		public bool Step()
		{
			if (this.Done)
			{
				return true;
			}
			int steps = this.Steps;
			this.Steps = steps + 1;
			if (!this._steps.MoveNext())
			{
				this._w.Flush();
				this.Done = true;
			}
			return this.Done;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x001229CC File Offset: 0x00120BCC
		public string Finish()
		{
			while (!this.Step())
			{
			}
			return this.Text;
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x001229DC File Offset: 0x00120BDC
		public string Text
		{
			get
			{
				if (!this.Done)
				{
					return null;
				}
				return this._sw.ToString();
			}
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x001229F3 File Offset: 0x00120BF3
		private IEnumerable<bool> Walk(object root)
		{
			if (root == null)
			{
				this._w.WriteNull();
				yield break;
			}
			IList list = root as IList;
			if (list != null && !(root is Array))
			{
				JsonArrayContract jsonArrayContract = this._ser.ContractResolver.ResolveContract(root.GetType()) as JsonArrayContract;
				if (jsonArrayContract != null && SlicedJson.Plain(jsonArrayContract))
				{
					object[] array = list.Cast<object>().ToArray<object>();
					this._w.WriteStartArray();
					foreach (object value in array)
					{
						this._ser.Serialize(this._w, value);
						yield return true;
					}
					object[] array2 = null;
					this._w.WriteEndArray();
					yield break;
				}
			}
			JsonObjectContract jsonObjectContract = this._ser.ContractResolver.ResolveContract(root.GetType()) as JsonObjectContract;
			if (jsonObjectContract == null || !SlicedJson.Plain(jsonObjectContract))
			{
				this._ser.Serialize(this._w, root);
				yield break;
			}
			this._w.WriteStartObject();
			foreach (JsonProperty jsonProperty in jsonObjectContract.Properties)
			{
				if (!jsonProperty.Ignored && jsonProperty.Readable && (jsonProperty.ShouldSerialize == null || jsonProperty.ShouldSerialize(root)) && (jsonProperty.GetIsSpecified == null || jsonProperty.GetIsSpecified(root)))
				{
					object value2 = jsonProperty.ValueProvider.GetValue(root);
					this._w.WritePropertyName(jsonProperty.PropertyName);
					if (value2 == null)
					{
						this._w.WriteNull();
						yield return true;
					}
					else
					{
						if (jsonProperty.Converter == null || !jsonProperty.Converter.CanWrite)
						{
							IList list2 = value2 as IList;
							if (list2 == null || value2 is Array || list2.Count <= 8)
							{
								goto IL_3CC;
							}
							JsonArrayContract jsonArrayContract2 = this._ser.ContractResolver.ResolveContract(value2.GetType()) as JsonArrayContract;
							if (jsonArrayContract2 == null || !SlicedJson.Plain(jsonArrayContract2))
							{
								goto IL_3CC;
							}
							object[] array2 = list2.Cast<object>().ToArray<object>();
							this._w.WriteStartArray();
							int num;
							for (int i = 0; i < array2.Length; i = num + 1)
							{
								this._ser.Serialize(this._w, array2[i]);
								if ((i + 1) % 8 == 0)
								{
									yield return true;
								}
								num = i;
							}
							this._w.WriteEndArray();
							array2 = null;
							IL_3DF:
							yield return true;
							continue;
							IL_3CC:
							this._ser.Serialize(this._w, value2);
							goto IL_3DF;
						}
						jsonProperty.Converter.WriteJson(this._w, value2, this._ser);
						yield return true;
					}
				}
			}
			IEnumerator<JsonProperty> enumerator = null;
			this._w.WriteEndObject();
			yield break;
			yield break;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00122A0C File Offset: 0x00120C0C
		private static bool Plain(JsonObjectContract c)
		{
			if (c.Converter != null || c.ExtensionDataGetter != null || c.IsReference.GetValueOrDefault() || c.ItemConverter != null)
			{
				return false;
			}
			if (c.OnSerializingCallbacks.Count > 0 || c.OnSerializedCallbacks.Count > 0)
			{
				return false;
			}
			foreach (JsonProperty jsonProperty in c.Properties)
			{
				if (!jsonProperty.Ignored && (jsonProperty.NullValueHandling != null || jsonProperty.DefaultValueHandling != null || jsonProperty.ReferenceLoopHandling != null || jsonProperty.TypeNameHandling != null || jsonProperty.IsReference != null || jsonProperty.ItemConverter != null || jsonProperty.ItemIsReference != null || jsonProperty.ItemTypeNameHandling != null || jsonProperty.ItemReferenceLoopHandling != null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00122B44 File Offset: 0x00120D44
		private static bool Plain(JsonArrayContract c)
		{
			return c.Converter == null && !c.IsReference.GetValueOrDefault() && c.ItemConverter == null && !c.ItemIsReference.GetValueOrDefault() && c.ItemTypeNameHandling == null && c.OnSerializingCallbacks.Count == 0 && c.OnSerializedCallbacks.Count == 0;
		}

		// Token: 0x04000C3A RID: 3130
		private const int ItemsPerStep = 8;

		// Token: 0x04000C3B RID: 3131
		private readonly JsonSerializer _ser = JsonSerializer.CreateDefault();

		// Token: 0x04000C3C RID: 3132
		private readonly StringWriter _sw = new StringWriter(new StringBuilder(65536), CultureInfo.InvariantCulture);

		// Token: 0x04000C3D RID: 3133
		private readonly JsonTextWriter _w;

		// Token: 0x04000C3E RID: 3134
		private readonly IEnumerator<bool> _steps;
	}
}
