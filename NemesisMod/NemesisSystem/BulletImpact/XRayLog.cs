using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x020000A2 RID: 162
	internal sealed class XRayLog
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x00149E19 File Offset: 0x00148019
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x00149E21 File Offset: 0x00148021
		public List<XRayEntry> Entries { get; private set; } = new List<XRayEntry>();

		// Token: 0x06001493 RID: 5267 RVA: 0x00149E2C File Offset: 0x0014802C
		public XRayLog(string path, int maxEntries)
		{
			this._path = path;
			this._maxEntries = Math.Max(1, maxEntries);
			this.Entries = XRayLog.Clean(SafeFile.Read<List<XRayEntry>>(path, null));
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00149E7B File Offset: 0x0014807B
		private static List<XRayEntry> Clean(List<XRayEntry> list)
		{
			if (list != null)
			{
				return (from x in list
				where x != null && x.Bones != null
				select x).ToList<XRayEntry>();
			}
			return new List<XRayEntry>();
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00149EB0 File Offset: 0x001480B0
		public void Add(XRayEntry e)
		{
			XRayEntry xrayEntry = this.Entries.LastOrDefault<XRayEntry>();
			e.Id = ((xrayEntry != null) ? xrayEntry.Id : 0) + 1;
			e.ShootoutId = ((xrayEntry == null) ? 1 : (((e.Time - xrayEntry.Time).TotalSeconds > 90.0) ? (xrayEntry.ShootoutId + 1) : xrayEntry.ShootoutId));
			this.Entries.Add(e);
			int num = this.Entries.Count((XRayEntry x) => x.Animal == e.Animal);
			if (num > this._maxEntries)
			{
				int drop = num - this._maxEntries;
				this.Entries.RemoveAll(delegate(XRayEntry x)
				{
					if (x.Animal == e.Animal)
					{
						int drop = drop;
						drop--;
						return drop > 0;
					}
					return false;
				});
			}
			this._dirty = true;
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00149FA9 File Offset: 0x001481A9
		public void MarkDirty()
		{
			this._dirty = true;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00149FB4 File Offset: 0x001481B4
		public void Replace(XRayEntry old, XRayEntry now)
		{
			int num = this.Entries.IndexOf(old);
			if (num < 0)
			{
				return;
			}
			this.Entries[num] = now;
			this._dirty = true;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00149FE7 File Offset: 0x001481E7
		public void Reload()
		{
			this.Entries = XRayLog.Clean(SafeFile.Read<List<XRayEntry>>(this._path, null));
			this._dirty = false;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0014A007 File Offset: 0x00148207
		public void Clear()
		{
			this.Entries.Clear();
			this._dirty = true;
			this.SaveIfDirty(true);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0014A024 File Offset: 0x00148224
		public void SaveInBackground()
		{
			if (!this._dirty)
			{
				return;
			}
			List<XRayEntry> snap = this.Entries.ToList<XRayEntry>();
			SafeFile.WriteLater(this._path, () => JsonConvert.SerializeObject(snap), true, delegate(Exception err)
			{
				if (err != null)
				{
					this._dirty = true;
				}
			});
			this._dirty = false;
			this._lastSave = DateTime.Now;
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0014A090 File Offset: 0x00148290
		public void SaveIfDirty(bool force = false)
		{
			if (!this._dirty || (!force && ((DateTime.Now - this._lastSave).TotalSeconds < 10.0 || XRayLog.Hold)))
			{
				return;
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				if (force)
				{
					if (SafeFile.WaitWrites(3000))
					{
						SafeFile.Write(this._path, JsonConvert.SerializeObject(this.Entries), true);
					}
					else
					{
						List<XRayEntry> snap = this.Entries.ToList<XRayEntry>();
						SafeFile.WriteLater(this._path, () => JsonConvert.SerializeObject(snap), true, delegate(Exception err)
						{
							if (err != null)
							{
								this._dirty = true;
							}
						});
					}
				}
				else
				{
					List<XRayEntry> snap = this.Entries.ToList<XRayEntry>();
					SafeFile.WriteLater(this._path, () => JsonConvert.SerializeObject(snap), true, delegate(Exception err)
					{
						if (err != null)
						{
							this._dirty = true;
						}
					});
				}
				this._dirty = false;
			}
			catch
			{
			}
			finally
			{
				this._lastSave = DateTime.Now;
				XRayLog.LastSaveMs = stopwatch.ElapsedMilliseconds;
				XRayLog.SaveCount++;
			}
		}

		// Token: 0x04000E86 RID: 3718
		private const int ShootoutGapSeconds = 90;

		// Token: 0x04000E87 RID: 3719
		private readonly string _path;

		// Token: 0x04000E88 RID: 3720
		private readonly int _maxEntries;

		// Token: 0x04000E89 RID: 3721
		private bool _dirty;

		// Token: 0x04000E8A RID: 3722
		private DateTime _lastSave = DateTime.MinValue;

		// Token: 0x04000E8C RID: 3724
		internal static long LastSaveMs;

		// Token: 0x04000E8D RID: 3725
		internal static int SaveCount;

		// Token: 0x04000E8E RID: 3726
		internal static bool Hold;
	}
}
