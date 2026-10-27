using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x020000A4 RID: 164
	internal sealed class XRayRenderer
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0014A1DC File Offset: 0x001483DC
		public int LastStepCap
		{
			get
			{
				return this._mp;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0014A1E4 File Offset: 0x001483E4
		// (set) Token: 0x060014A0 RID: 5280 RVA: 0x0014A1EC File Offset: 0x001483EC
		public XRayQuality ActiveLevel { get; private set; } = XRayQuality.Medium;

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0014A1F5 File Offset: 0x001483F5
		// (set) Token: 0x060014A2 RID: 5282 RVA: 0x0014A1FD File Offset: 0x001483FD
		public int LastRects { get; private set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0014A206 File Offset: 0x00148406
		// (set) Token: 0x060014A4 RID: 5284 RVA: 0x0014A20E File Offset: 0x0014840E
		public int LastDropped { get; private set; }

		// Token: 0x060014A5 RID: 5285 RVA: 0x0014A218 File Offset: 0x00148418
		public void ResetView()
		{
			this.Yaw = 0f;
			this.Pitch = 8f;
			this.Zoom = 1f;
			this.PanX = (this.PanY = 0f);
			this._fit = -1f;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0014A268 File Offset: 0x00148468
		public bool SetHeld(Keys key, bool down)
		{
			if (key <= Keys.A)
			{
				if (key != Keys.Left)
				{
					if (key == Keys.Right)
					{
						goto IL_44;
					}
					if (key != Keys.A)
					{
						return false;
					}
				}
				this._left = down;
				return true;
			}
			if (key != Keys.D)
			{
				if (key == Keys.S)
				{
					this._down = down;
					return true;
				}
				switch (key)
				{
				case Keys.W:
					this._up = down;
					return true;
				case Keys.X:
					this._zoomOut = down;
					return true;
				case Keys.Y:
					return false;
				case Keys.Z:
					this._zoomIn = down;
					return true;
				default:
					return false;
				}
			}
			IL_44:
			this._right = down;
			return true;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0014A2E8 File Offset: 0x001484E8
		public void ReleaseAll()
		{
			this._left = (this._right = (this._up = (this._down = (this._zoomIn = (this._zoomOut = false)))));
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0014A329 File Offset: 0x00148529
		public void Orbit(float dYaw, float dPitch)
		{
			this.Yaw += dYaw;
			this.Pitch = Math.Max(-80f, Math.Min(80f, this.Pitch + dPitch));
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0014A35B File Offset: 0x0014855B
		public void ZoomBy(float factor)
		{
			this.Zoom = Math.Max(0.6f, Math.Min(5f, this.Zoom * factor));
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0014A380 File Offset: 0x00148580
		public void Build(Anatomy.Body body, [TupleElementNames(new string[]
		{
			"path",
			"thin"
		})] IList<ValueTuple<PathResult, bool>> paths, ISet<string> hitKeys, XRayQuality preference)
		{
			this._preference = preference;
			this._buildId++;
			foreach (XRayQuality xrayQuality in new XRayQuality[]
			{
				XRayQuality.Low,
				XRayQuality.Medium,
				XRayQuality.High
			})
			{
				this._prims = new List<XRayRenderer.Prim>();
				this.BuildLevel(body, paths, hitKeys, xrayQuality);
				this._levels[xrayQuality] = this._prims;
			}
			this.ActiveLevel = ((preference == XRayQuality.Auto) ? XRayQuality.Medium : preference);
			this._prims = this._levels[this.ActiveLevel];
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0014A414 File Offset: 0x00148614
		private void BuildLevel(Anatomy.Body body, [TupleElementNames(new string[]
		{
			"path",
			"thin"
		})] IList<ValueTuple<PathResult, bool>> paths, ISet<string> hitKeys, XRayQuality q)
		{
			this._fit = -1f;
			Dictionary<int, V3> bones = body.Bones;
			this._framePoints = bones.Values.ToList<V3>();
			this._center = new V3(this._framePoints.Average((V3 p) => p.X), this._framePoints.Average((V3 p) => p.Y), (this._framePoints.Min((V3 p) => p.Z) + this._framePoints.Max((V3 p) => p.Z) + 0.15f) / 2f);
			this._focus = ((paths.Count > 0) ? paths[0].Item1.Entry : this._center);
			V3 v = (paths.Count > 0) ? (paths[0].Item1.End - paths[0].Item1.Start) : new V3(1f, 0f, 0f);
			V3 v2 = new V3(v.X, v.Y, 0f).Normalized();
			if (v2.Length() < 0.5f)
			{
				v2 = new V3(1f, 0f, 0f);
			}
			this._baseYaw = (float)(Math.Atan2((double)(-(double)v2.Y), (double)v2.X) * 180.0 / 3.141592653589793);
			V3 up = body.Up;
			V3 fwd = body.Fwd;
			V3 right = body.Right;
			bool flag = q == XRayQuality.High;
			bool flag2 = q == XRayQuality.Low;
			if (body.Animal)
			{
				this.BuildAnimal(body, q);
			}
			else
			{
				if (!flag2)
				{
					foreach (Anatomy.Sphere sphere in body.Organs)
					{
						XRayRenderer.Kind kind = XRayRenderer.Kind.Dot;
						XRayRenderer.Style style = XRayRenderer.Style.Organ;
						V3 c = sphere.C;
						float r = sphere.R * 0.8f;
						string key = sphere.Key;
						this.Add(kind, style, c, default(V3), r, key, 2).Color = XRayRenderer.OrganColor(sphere.Key);
					}
				}
				List<V3> list = Anatomy.SpineChain.Where(new Func<int, bool>(body.Has)).Select(new Func<int, V3>(body.Get)).ToList<V3>();
				int num = 0;
				while (num + 1 < list.Count)
				{
					float r;
					float frac;
					float num2;
					body.TorsoCoords(list[num], out frac, out r, out num2);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, list[num], list[num + 1], 0f, XRayRenderer.SpineKey(frac), 0);
					num++;
				}
				foreach (V3 v3 in XRayRenderer.Resample(list, flag2 ? 0.045f : 0.026f))
				{
					float r;
					float num2;
					float num3;
					body.TorsoCoords(v3, out num3, out num2, out r);
					XRayRenderer.Kind kind2 = XRayRenderer.Kind.Dot;
					XRayRenderer.Style style2 = XRayRenderer.Style.Bone;
					V3 a = v3;
					r = ((num3 > 1f) ? 0.012f : 0.016f);
					string key = XRayRenderer.SpineKey(num3);
					this.Add(kind2, style2, a, default(V3), r, key, 1);
					if (!flag2)
					{
						this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v3, v3 - fwd * 0.035f - up * 0.01f, 0f, XRayRenderer.SpineKey(num3), 3);
					}
				}
				V3 v4 = body.Has(21030) ? body.Get(21030) : (body.HeadCenter - up * 0.07f);
				V3 v5 = body.Has(14283) ? (v4 - body.Get(14283)).Normalized() : up;
				if (v5.Length() < 0.5f)
				{
					v5 = up;
				}
				V3 v6 = fwd;
				if (body.Has(58559))
				{
					V3 a2 = body.Get(58559) - v4;
					V3 v7 = a2 - v5 * V3.Dot(a2, v5);
					if (v7.Length() > 0.01f)
					{
						v6 = v7.Normalized();
					}
				}
				V3 hr = V3.Cross(v6, v5).Normalized();
				V3 v8 = v4 + v5 * 0.07f + v6 * 0.02f;
				this.BuildSkull(v8, v6, hr, v5, q);
				this._framePoints.Add(v8 + v5 * 0.09f);
				this._framePoints.Add(v8 + v6 * 0.09f - v5 * 0.15f);
				int num4 = flag ? 10 : ((q == XRayQuality.Medium) ? 8 : 5);
				for (int i = 0; i < num4; i++)
				{
					float num5 = 0.9f - (float)i * (0.4f / (float)Math.Max(1, num4 - 1));
					float num6 = Math.Max(0f, 1f - (float)Math.Pow((double)((num5 - 0.68f) / 0.26f), 2.0));
					float num7 = 0.075f + 0.055f * num6;
					float num8 = 0.085f + 0.015f * num6;
					foreach (float num9 in new float[]
					{
						-1f,
						1f
					})
					{
						List<V3> list2 = new List<V3>();
						int num10 = flag ? 5 : 4;
						for (int k = 0; k <= num10; k++)
						{
							double num11 = -1.4507963267948965 + (double)k * 2.7074333882308137 / (double)num10;
							float right2 = num9 * num7 * (float)Math.Cos(num11);
							float fwd2 = num8 * (float)Math.Sin(num11) - 0.01f;
							float s = -0.045f * (float)((num11 + 1.5707963267948966) / 3.141592653589793);
							list2.Add(body.TorsoPoint(num5, right2, fwd2) + up * s);
						}
						string key2 = (num9 < 0f) ? "ribs_l" : "ribs_r";
						int num12 = 0;
						while (num12 + 1 < list2.Count)
						{
							this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, list2[num12], list2[num12 + 1], 0f, key2, 1);
							num12++;
						}
						if (!flag2 && num5 > 0.55f)
						{
							this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Cartilage, list2[list2.Count - 1], body.TorsoPoint(num5 - 0.04f, num9 * 0.012f, 0.105f), 0f, null, 3);
						}
					}
				}
				this.Add(XRayRenderer.Kind.Bone, XRayRenderer.Style.Bone, body.TorsoPoint(0.93f, 0f, 0.1f), body.TorsoPoint(0.58f, 0f, 0.108f), 0.012f, "sternum", 1);
				Func<int[], bool> <>9__4;
				foreach (ValueTuple<int, int, int, int, int[][], float, string> valueTuple in new ValueTuple<int, int, int, int, int[][], float, string>[]
				{
					new ValueTuple<int, int, int, int, int[][], float, string>(30226, 37873, 53675, 34606, Anatomy.LFingers, -1f, "_l"),
					new ValueTuple<int, int, int, int, int[][], float, string>(54802, 46065, 54187, 22798, Anatomy.RFingers, 1f, "_r")
				})
				{
					int item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					int item3 = valueTuple.Item3;
					int item4 = valueTuple.Item4;
					int[][] item5 = valueTuple.Item5;
					float item6 = valueTuple.Item6;
					string item7 = valueTuple.Item7;
					if (body.Has(item2))
					{
						V3 v9 = body.TorsoPoint(0.95f, item6 * 0.02f, 0.08f);
						this.Add(XRayRenderer.Kind.Bone, XRayRenderer.Style.Bone, body.Has(item) ? body.Get(item) : v9, body.Get(item2), 0.008f, "clavicle" + item7, 0);
					}
					if (!flag2)
					{
						V3 v10 = body.TorsoPoint(0.93f, item6 * 0.05f, -0.085f);
						V3 v11 = body.TorsoPoint(0.93f, item6 * 0.15f, -0.06f);
						V3 v12 = body.TorsoPoint(0.68f, item6 * 0.08f, -0.09f);
						this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v10, v11, 0f, "scapula" + item7, 2);
						this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v11, v12, 0f, "scapula" + item7, 2);
						this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v12, v10, 0f, "scapula" + item7, 2);
					}
					if (body.Has(item2) && body.Has(item3))
					{
						this.Add(XRayRenderer.Kind.Bone, XRayRenderer.Style.Bone, body.Get(item2), body.Get(item3), 0.012f, "humerus" + item7, 0);
						XRayRenderer.Kind kind3 = XRayRenderer.Kind.Dot;
						XRayRenderer.Style style3 = XRayRenderer.Style.Bone;
						V3 a3 = body.Get(item2);
						string key = "humerus" + item7;
						this.Add(kind3, style3, a3, default(V3), 0.018f, key, 1);
					}
					if (!flag2 && body.Has(item2) && body.Has(item3))
					{
						V3 v13 = body.Get(item2);
						V3 a4 = body.Get(item3);
						V3 v14 = (a4 - v13).Normalized();
						V3 v15 = XRayRenderer.Perp(v14, right * item6, fwd);
						V3 a5 = V3.Cross(v14, v15).Normalized();
						if (V3.Dot(a5, fwd) > 0f)
						{
							a5 *= -1f;
						}
						XRayRenderer.Kind kind4 = XRayRenderer.Kind.Dot;
						XRayRenderer.Style style4 = XRayRenderer.Style.Bone;
						V3 a6 = a4 + v15 * 0.024f + v14 * 0.005f;
						string key = "humerus" + item7;
						this.Add(kind4, style4, a6, default(V3), 0.01f, key, 3);
						XRayRenderer.Kind kind5 = XRayRenderer.Kind.Dot;
						XRayRenderer.Style style5 = XRayRenderer.Style.Bone;
						V3 a7 = a4 - v15 * 0.026f + v14 * 0.005f;
						key = "humerus" + item7;
						this.Add(kind5, style5, a7, default(V3), 0.011f, key, 3);
						XRayRenderer.Kind kind6 = XRayRenderer.Kind.Dot;
						XRayRenderer.Style style6 = XRayRenderer.Style.Bone;
						V3 a8 = a4 + a5 * 0.018f;
						key = "forearm" + item7;
						this.Add(kind6, style6, a8, default(V3), 0.01f, key, 3);
						this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, a4 + v15 * 0.024f, a4 - v15 * 0.026f, 0f, "humerus" + item7, 3);
						XRayRenderer.Kind kind7 = XRayRenderer.Kind.Dot;
						XRayRenderer.Style style7 = XRayRenderer.Style.Bone;
						V3 a9 = v13 + v15 * 0.02f - v14 * 0.004f;
						key = "humerus" + item7;
						this.Add(kind7, style7, a9, default(V3), 0.01f, key, 3);
					}
					if (body.Has(item3) && body.Has(item4))
					{
						V3 v16 = body.Get(item3);
						V3 v17 = body.Get(item4);
						if (!flag2)
						{
							V3 v18 = (v17 - v16).Normalized();
							V3 a10 = XRayRenderer.Perp(v18, V3.Cross(v18, up), right);
							V3 a11 = V3.Lerp(v16, v17, 0.94f);
							XRayRenderer.Kind kind8 = XRayRenderer.Kind.Dot;
							XRayRenderer.Style style8 = XRayRenderer.Style.Bone;
							V3 a12 = a11 + a10 * 0.016f;
							string key = "forearm" + item7;
							this.Add(kind8, style8, a12, default(V3), 0.008f, key, 3);
							XRayRenderer.Kind kind9 = XRayRenderer.Kind.Dot;
							XRayRenderer.Style style9 = XRayRenderer.Style.Bone;
							V3 a13 = a11 - a10 * 0.014f;
							key = "forearm" + item7;
							this.Add(kind9, style9, a13, default(V3), 0.008f, key, 3);
						}
						V3 v19 = V3.Cross((v17 - v16).Normalized(), up).Normalized() * 0.011f;
						if (v19.Length() < 0.005f)
						{
							v19 = right * 0.011f;
						}
						this.Add(XRayRenderer.Kind.Bone, XRayRenderer.Style.Bone, v16 + v19, v17 + v19 * 0.8f, 0.007f, "forearm" + item7, 0);
						this.Add(XRayRenderer.Kind.Bone, XRayRenderer.Style.Bone, v16 - v19, v17 - v19 * 0.6f, 0.007f, "forearm" + item7, 1);
					}
					if (body.Has(item4))
					{
						V3 v20 = body.Get(item4);
						XRayRenderer.Kind kind10 = XRayRenderer.Kind.Dot;
						XRayRenderer.Style style10 = XRayRenderer.Style.Bone;
						V3 a14 = v20;
						string key = "hand" + item7;
						this.Add(kind10, style10, a14, default(V3), 0.016f, key, 1);
						if (!flag2)
						{
							IEnumerable<int[]> source = item5;
							Func<int[], bool> predicate;
							if ((predicate = <>9__4) == null)
							{
								predicate = (<>9__4 = ((int[] f) => body.Has(f[0])));
							}
							foreach (int[] source2 in source.Where(predicate))
							{
								List<V3> list3 = source2.Where(new Func<int, bool>(body.Has)).Select(new Func<int, V3>(body.Get)).ToList<V3>();
								this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v20, list3[0], 0f, "hand" + item7, 2);
								XRayRenderer.Kind kind11 = XRayRenderer.Kind.Dot;
								XRayRenderer.Style style11 = XRayRenderer.Style.Bone;
								V3 a15 = V3.Lerp(v20, list3[0], 0.2f);
								key = "hand" + item7;
								this.Add(kind11, style11, a15, default(V3), 0.006f, key, 3);
								XRayRenderer.Kind kind12 = XRayRenderer.Kind.Dot;
								XRayRenderer.Style style12 = XRayRenderer.Style.Bone;
								V3 a16 = list3[0];
								key = "hand" + item7;
								this.Add(kind12, style12, a16, default(V3), 0.0065f, key, 3);
								int num13 = 0;
								while (num13 + 1 < list3.Count)
								{
									this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, list3[num13], list3[num13 + 1], 0f, "hand" + item7, 3);
									if (flag)
									{
										XRayRenderer.Kind kind13 = XRayRenderer.Kind.Dot;
										XRayRenderer.Style style13 = XRayRenderer.Style.Bone;
										V3 a17 = list3[num13 + 1];
										key = "hand" + item7;
										this.Add(kind13, style13, a17, default(V3), 0.005f, key, 3);
									}
									num13++;
								}
							}
						}
					}
				}
				V3 pelvisPos = body.PelvisPos;
				foreach (float num14 in new float[]
				{
					-1f,
					1f
				})
				{
					int id = (num14 < 0f) ? 65478 : 6884;
					V3 v21 = body.Has(id) ? body.Get(id) : (pelvisPos + right * (num14 * 0.1f));
					V3 a18 = pelvisPos + right * (num14 * 0.04f) - fwd * 0.06f + up * 0.07f;
					V3 v22 = pelvisPos + right * (num14 * 0.13f) - fwd * 0.01f + up * 0.08f;
					V3 v23 = pelvisPos + right * (num14 * 0.13f) + fwd * 0.05f + up * 0.04f;
					V3 b = pelvisPos + right * (num14 * 0.09f) + fwd * 0.07f;
					V3 b2 = pelvisPos + fwd * 0.07f - up * 0.06f;
					V3 v24 = pelvisPos + right * (num14 * 0.05f) - fwd * 0.02f - up * 0.09f;
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, a18, v22, 0f, "pelvis", 0);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, v22, v23, 0f, "pelvis", 0);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, v23, v21, 0f, "pelvis", 0);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, v21, b2, 0f, "pelvis", 0);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, a18, v21, 0f, "pelvis", 2);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v23, b, 0f, "pelvis", 2);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v21, v24, 0f, "pelvis", 1);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v24, b2, 0f, "pelvis", 2);
				}
				V3 a19 = pelvisPos - fwd * 0.07f + up * 0.06f;
				V3 b3 = pelvisPos - fwd * 0.06f - up * 0.05f;
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, a19 + right * 0.04f, a19 - right * 0.04f, 0f, "pelvis", 2);
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, a19 + right * 0.04f, b3, 0f, "pelvis", 2);
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, a19 - right * 0.04f, b3, 0f, "pelvis", 2);
				foreach (ValueTuple<int, int, int, int, int, float, string> valueTuple2 in new ValueTuple<int, int, int, int, int, float, string>[]
				{
					new ValueTuple<int, int, int, int, int, float, string>(65478, 55120, 45454, 53081, 53082, -1f, "_l"),
					new ValueTuple<int, int, int, int, int, float, string>(6884, 43312, 33646, 41273, 41274, 1f, "_r")
				})
				{
					int item8 = valueTuple2.Item1;
					int item9 = valueTuple2.Item2;
					int item10 = valueTuple2.Item3;
					int item11 = valueTuple2.Item4;
					int item12 = valueTuple2.Item5;
					float item13 = valueTuple2.Item6;
					string item14 = valueTuple2.Item7;
					if (body.Has(item8) && body.Has(item9))
					{
						V3 v25 = body.Get(item8);
						V3 v26 = body.Get(item9);
						XRayRenderer.Kind kind14 = XRayRenderer.Kind.Dot;
						XRayRenderer.Style style14 = XRayRenderer.Style.Bone;
						V3 a20 = v25;
						string key = "femur" + item14;
						this.Add(kind14, style14, a20, default(V3), 0.02f, key, 1);
						this.Add(XRayRenderer.Kind.Bone, XRayRenderer.Style.Bone, v25, v26, 0.015f, "femur" + item14, 0);
						XRayRenderer.Kind kind15 = XRayRenderer.Kind.Dot;
						XRayRenderer.Style style15 = XRayRenderer.Style.Bone;
						V3 a21 = v26 + fwd * 0.045f;
						key = "knee" + item14;
						this.Add(kind15, style15, a21, default(V3), 0.018f, key, 2);
						XRayRenderer.Kind kind16 = XRayRenderer.Kind.Dot;
						XRayRenderer.Style style16 = XRayRenderer.Style.Bone;
						V3 a22 = v26;
						key = "knee" + item14;
						this.Add(kind16, style16, a22, default(V3), 0.017f, key, 2);
						if (!flag2)
						{
							V3 v27 = (v26 - v25).Normalized();
							V3 a23 = XRayRenderer.Perp(v27, right * item13, fwd);
							V3 v28 = v25 + a23 * 0.05f + v27 * 0.035f;
							this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, v25, v28, 0f, "femur" + item14, 3);
							XRayRenderer.Kind kind17 = XRayRenderer.Kind.Dot;
							XRayRenderer.Style style17 = XRayRenderer.Style.Bone;
							V3 a24 = v28;
							key = "femur" + item14;
							this.Add(kind17, style17, a24, default(V3), 0.016f, key, 3);
							XRayRenderer.Kind kind18 = XRayRenderer.Kind.Dot;
							XRayRenderer.Style style18 = XRayRenderer.Style.Bone;
							V3 a25 = v25 - a23 * 0.005f + v27 * 0.075f - fwd * 0.012f;
							key = "femur" + item14;
							this.Add(kind18, style18, a25, default(V3), 0.009f, key, 3);
							XRayRenderer.Kind kind19 = XRayRenderer.Kind.Dot;
							XRayRenderer.Style style19 = XRayRenderer.Style.Bone;
							V3 a26 = v26 + a23 * 0.026f - v27 * 0.012f - fwd * 0.006f;
							key = "femur" + item14;
							this.Add(kind19, style19, a26, default(V3), 0.014f, key, 3);
							XRayRenderer.Kind kind20 = XRayRenderer.Kind.Dot;
							XRayRenderer.Style style20 = XRayRenderer.Style.Bone;
							V3 a27 = v26 - a23 * 0.026f - v27 * 0.012f - fwd * 0.006f;
							key = "femur" + item14;
							this.Add(kind20, style20, a27, default(V3), 0.014f, key, 3);
							this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v26 + a23 * 0.03f + v27 * 0.028f, v26 - a23 * 0.03f + v27 * 0.028f, 0f, "tibia" + item14, 3);
						}
						if (!flag2)
						{
							this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Vessel, v25 + fwd * 0.02f, V3.Lerp(v25, v26, 0.5f) + fwd * 0.02f, 0f, "femoral" + item14, 3);
						}
					}
					if (body.Has(item9) && body.Has(item10))
					{
						V3 v29 = body.Get(item9);
						V3 v30 = body.Get(item10);
						V3 a28 = V3.Cross((v30 - v29).Normalized(), fwd).Normalized() * -item13;
						if (a28.Length() < 0.5f)
						{
							a28 = right * item13;
						}
						this.Add(XRayRenderer.Kind.Bone, XRayRenderer.Style.Bone, v29 - a28 * 0.006f, v30 - a28 * 0.004f, 0.011f, "tibia" + item14, 0);
						this.Add(XRayRenderer.Kind.Bone, XRayRenderer.Style.Bone, v29 + a28 * 0.03f, v30 + a28 * 0.02f, 0.006f, "tibia" + item14, 1);
						if (!flag2)
						{
							V3 a29 = (v29 - v30).Normalized();
							XRayRenderer.Kind kind21 = XRayRenderer.Kind.Dot;
							XRayRenderer.Style style21 = XRayRenderer.Style.Bone;
							V3 a30 = v30 + a29 * 0.02f + a28 * 0.026f;
							string key = "tibia" + item14;
							this.Add(kind21, style21, a30, default(V3), 0.01f, key, 3);
							XRayRenderer.Kind kind22 = XRayRenderer.Kind.Dot;
							XRayRenderer.Style style22 = XRayRenderer.Style.Bone;
							V3 a31 = v30 + a29 * 0.03f - a28 * 0.022f;
							key = "tibia" + item14;
							this.Add(kind22, style22, a31, default(V3), 0.01f, key, 3);
							XRayRenderer.Kind kind23 = XRayRenderer.Kind.Dot;
							XRayRenderer.Style style23 = XRayRenderer.Style.Bone;
							V3 a32 = v29 - a29 * 0.012f + fwd * 0.02f;
							key = "tibia" + item14;
							this.Add(kind23, style23, a32, default(V3), 0.01f, key, 3);
						}
					}
					if (body.Has(item10))
					{
						V3 v31 = body.Get(item10);
						V3 v32 = body.Has(item11) ? body.Get(item11) : (v31 + fwd * 0.12f - up * 0.06f);
						V3 v33 = v31 - (v32 - v31).Normalized() * 0.06f - up * 0.04f;
						this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, v31, v32, 0f, "foot" + item14, 0);
						this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v33, v32, 0f, "foot" + item14, 2);
						this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v31, v33, 0f, "foot" + item14, 1);
						if (!flag2)
						{
							V3 v34 = (v32 - v31).Normalized();
							V3 a33 = XRayRenderer.Perp(v34, right * item13, up);
							XRayRenderer.Kind kind24 = XRayRenderer.Kind.Dot;
							XRayRenderer.Style style24 = XRayRenderer.Style.Bone;
							V3 a34 = v33;
							string key = "foot" + item14;
							this.Add(kind24, style24, a34, default(V3), 0.016f, key, 3);
							XRayRenderer.Kind kind25 = XRayRenderer.Kind.Dot;
							XRayRenderer.Style style25 = XRayRenderer.Style.Bone;
							V3 a35 = V3.Lerp(v31, v32, 0.25f) - up * 0.01f;
							key = "foot" + item14;
							this.Add(kind25, style25, a35, default(V3), 0.012f, key, 3);
							V3 a36 = V3.Lerp(v31, v32, 0.38f) - up * 0.015f;
							float[] array5;
							if (!flag)
							{
								float[] array4 = new float[2];
								array4[0] = -0.025f;
								array5 = array4;
								array4[1] = 0.025f;
							}
							else
							{
								RuntimeHelpers.InitializeArray(array5 = new float[4], fieldof(<PrivateImplementationDetails>.C4C213B09534D98C56838913FE1AE9D6E12F8CFF08BF8EAA22F1B4542B246F6A).FieldHandle);
							}
							foreach (float num15 in array5)
							{
								V3 v35 = v32 + a33 * num15 - v34 * Math.Abs(num15) * 0.6f;
								this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, a36 + a33 * (num15 * 0.4f), v35, 0f, "foot" + item14, 3);
								XRayRenderer.Kind kind26 = XRayRenderer.Kind.Dot;
								XRayRenderer.Style style26 = XRayRenderer.Style.Bone;
								V3 a37 = v35;
								key = "foot" + item14;
								this.Add(kind26, style26, a37, default(V3), 0.005f, key, 3);
							}
						}
						if (body.Has(item12))
						{
							this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v32, body.Get(item12), 0f, "foot" + item14, 3);
						}
					}
				}
			}
			foreach (ValueTuple<PathResult, bool> valueTuple3 in paths)
			{
				PathResult item15 = valueTuple3.Item1;
				bool item16 = valueTuple3.Item2;
				V3 a38 = ((item15.End - item15.Entry).Length() > 0.01f) ? (item15.End - item15.Entry).Normalized() : (item15.Entry - item15.Start).Normalized();
				V3 a39 = (item15.Entry - item15.Start).Normalized();
				if (item15.Blunt)
				{
					this.Add(XRayRenderer.Kind.Line, item16 ? XRayRenderer.Style.PelletIn : XRayRenderer.Style.Incoming, item15.Entry - a39 * (item16 ? 0.1f : 0.2f), item15.Entry, 0f, null, item16 ? 2 : 0);
					XRayRenderer.Kind kind27 = XRayRenderer.Kind.Dot;
					XRayRenderer.Style style27 = (item15.Blow == BlowKind.Kick) ? XRayRenderer.Style.KickMark : ((item15.Blow == BlowKind.Gun) ? XRayRenderer.Style.GunMark : ((item15.Blow == BlowKind.Choke) ? XRayRenderer.Style.ChokeMark : XRayRenderer.Style.BruiseMark));
					V3 entry = item15.Entry;
					float r = item16 ? 0.016f : 0.024f;
					this.Add(kind27, style27, entry, default(V3), r, null, 0);
				}
				else
				{
					this.Add(XRayRenderer.Kind.Line, item16 ? XRayRenderer.Style.PelletIn : XRayRenderer.Style.Incoming, item15.Entry - a39 * (item16 ? 0.12f : 0.3f), item15.Entry, 0f, null, item16 ? 2 : 0);
					this.Add(XRayRenderer.Kind.Line, item16 ? XRayRenderer.Style.PelletInside : XRayRenderer.Style.Inside, item15.Entry, item15.End, 0f, null, (item16 > false) ? 1 : 0);
					if (item15.Exited)
					{
						this.Add(XRayRenderer.Kind.Line, item16 ? XRayRenderer.Style.PelletExit : XRayRenderer.Style.Exit, item15.End, item15.End + a38 * (item16 ? 0.1f : 0.22f), 0f, null, item16 ? 2 : 0);
					}
					if (!item16)
					{
						this.Add(XRayRenderer.Kind.Dot, XRayRenderer.Style.EntryMark, item15.Entry, default(V3), 0.012f, null, 0);
						if (!item15.Exited)
						{
							this.Add(XRayRenderer.Kind.Dot, XRayRenderer.Style.StopMark, item15.End, default(V3), 0.014f, null, 0);
						}
					}
				}
			}
			this.MarkHits(paths, hitKeys);
			foreach (XRayRenderer.Prim prim in this._prims)
			{
				if (prim.Hit)
				{
					prim.Priority = 0;
				}
			}
			HashSet<string> hashSet = new HashSet<string>();
			foreach (XRayRenderer.Prim prim2 in from p in this._prims
			where p.Fracture
			orderby (p.Kind == XRayRenderer.Kind.Dot) ? 1 : 0
			select p)
			{
				string item17 = string.Concat(new string[]
				{
					prim2.Key,
					"|",
					Math.Round((double)prim2.HitAt.X, 1).ToString(),
					",",
					Math.Round((double)prim2.HitAt.Y, 1).ToString(),
					",",
					Math.Round((double)prim2.HitAt.Z, 1).ToString()
				});
				if (!hashSet.Add(item17))
				{
					prim2.Fracture = false;
				}
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0014C51C File Offset: 0x0014A71C
		private void BuildAnimal(Anatomy.Body body, XRayQuality q)
		{
			XRayRenderer.<>c__DisplayClass50_0 CS$<>8__locals1 = new XRayRenderer.<>c__DisplayClass50_0();
			CS$<>8__locals1.body = body;
			bool flag = q == XRayQuality.High;
			bool flag2 = q == XRayQuality.Low;
			V3 up = CS$<>8__locals1.body.Up;
			V3 fwd = CS$<>8__locals1.body.Fwd;
			V3 right = CS$<>8__locals1.body.Right;
			float scale = CS$<>8__locals1.body.Scale;
			float depth = CS$<>8__locals1.body.Depth;
			float halfWidth = CS$<>8__locals1.body.HalfWidth;
			if (!flag2)
			{
				foreach (Anatomy.Sphere sphere in CS$<>8__locals1.body.Organs)
				{
					XRayRenderer.Kind kind = XRayRenderer.Kind.Dot;
					XRayRenderer.Style style = XRayRenderer.Style.Organ;
					V3 c2 = sphere.C;
					float r = sphere.R * 0.8f;
					string key = sphere.Key;
					this.Add(kind, style, c2, default(V3), r, key, 2).Color = XRayRenderer.OrganColor(sphere.Key);
				}
			}
			List<ValueTuple<int, V3>> list = (from id in new int[]
			{
				56200,
				14410,
				14411,
				14412,
				14413,
				14283,
				14284,
				14285,
				14286,
				21030
			}.Where(new Func<int, bool>(CS$<>8__locals1.body.Has))
			select new ValueTuple<int, V3>(id, base.<BuildAnimal>g__Get|0(id))).ToList<ValueTuple<int, V3>>();
			int num = 0;
			while (num + 1 < list.Count)
			{
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, list[num].Item2, list[num + 1].Item2, 0f, XRayRenderer.<BuildAnimal>g__KeyOf|50_2(list[num].Item1), 0);
				num++;
			}
			foreach (V3 v in XRayRenderer.Resample((from c in list
			select c.Item2).ToList<V3>(), (flag2 ? 0.1f : 0.06f) * scale))
			{
				float r;
				float num2;
				float r2;
				CS$<>8__locals1.body.TorsoCoords(v, out num2, out r, out r2);
				string text = (num2 > 1.02f) ? "cervical" : ((num2 > 0.55f) ? "spine_t" : "spine_l");
				XRayRenderer.Kind kind2 = XRayRenderer.Kind.Dot;
				XRayRenderer.Style style2 = XRayRenderer.Style.Bone;
				V3 a = v;
				r2 = 0.022f * scale;
				string key = text;
				this.Add(kind2, style2, a, default(V3), r2, key, 1);
				if (!flag2 && num2 <= 1.05f)
				{
					float num3 = (num2 > 0.55f) ? (0.05f + 0.1f * Math.Max(0f, 1f - Math.Abs(num2 - 0.85f) / 0.35f)) : 0.045f;
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v, v - fwd * (num3 * scale) - up * (0.02f * scale), 0f, text, 3);
				}
			}
			List<V3> list2 = AnimalAnatomy.TailIds.Where(new Func<int, bool>(CS$<>8__locals1.body.Has)).Select(new Func<int, V3>(CS$<>8__locals1.<BuildAnimal>g__Get|0)).ToList<V3>();
			if (list2.Count > 0 && CS$<>8__locals1.body.Has(56200))
			{
				List<V3> list3 = new List<V3>
				{
					CS$<>8__locals1.<BuildAnimal>g__Get|0(56200)
				};
				list3.AddRange(list2);
				int num4 = 0;
				while (num4 + 1 < list3.Count)
				{
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, list3[num4], list3[num4 + 1], 0f, "tail", 1);
					num4++;
				}
				if (!flag2)
				{
					foreach (V3 v2 in XRayRenderer.Resample(list3, 0.05f * scale))
					{
						XRayRenderer.Kind kind3 = XRayRenderer.Kind.Dot;
						XRayRenderer.Style style3 = XRayRenderer.Style.Bone;
						V3 a2 = v2;
						float r2 = 0.012f * scale;
						this.Add(kind3, style3, a2, default(V3), r2, "tail", 3);
					}
				}
			}
			if (CS$<>8__locals1.body.Has(21030))
			{
				XRayRenderer.<>c__DisplayClass50_1 CS$<>8__locals2;
				CS$<>8__locals2.head = CS$<>8__locals1.<BuildAnimal>g__Get|0(21030);
				CS$<>8__locals2.hf = CS$<>8__locals1.body.Snout;
				CS$<>8__locals2.hu = (fwd * -1f - CS$<>8__locals2.hf * V3.Dot(fwd * -1f, CS$<>8__locals2.hf)).Normalized();
				if (CS$<>8__locals2.hu.Length() < 0.5f)
				{
					CS$<>8__locals2.hu = new V3(0f, 0f, 1f);
				}
				CS$<>8__locals2.hr = V3.Cross(CS$<>8__locals2.hf, CS$<>8__locals2.hu).Normalized();
				CS$<>8__locals2.sl = 0.42f * scale;
				int segs = flag ? 16 : (flag2 ? 10 : 12);
				this.AddEllipse(XRayRenderer.<BuildAnimal>g__P|50_4(0.12f, 0f, 0.05f, ref CS$<>8__locals2), CS$<>8__locals2.hf, 0.2f * CS$<>8__locals2.sl, CS$<>8__locals2.hu, 0.17f * CS$<>8__locals2.sl, segs, 0.0, 6.283185307179586, XRayRenderer.Style.Bone, "skull", 0);
				if (!flag2)
				{
					this.AddEllipse(XRayRenderer.<BuildAnimal>g__P|50_4(0.12f, 0f, 0.05f, ref CS$<>8__locals2), CS$<>8__locals2.hf, 0.2f * CS$<>8__locals2.sl, CS$<>8__locals2.hr, 0.15f * CS$<>8__locals2.sl, segs, 0.0, 6.283185307179586, XRayRenderer.Style.Fine, "skull", 2);
				}
				foreach (float num5 in new float[]
				{
					-1f,
					1f
				})
				{
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, XRayRenderer.<BuildAnimal>g__P|50_4(0.28f, num5 * 0.1f, 0.14f, ref CS$<>8__locals2), XRayRenderer.<BuildAnimal>g__P|50_4(1f, num5 * 0.05f, 0.06f, ref CS$<>8__locals2), 0f, "skull", 0);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, XRayRenderer.<BuildAnimal>g__P|50_4(0.28f, num5 * 0.12f, -0.06f, ref CS$<>8__locals2), XRayRenderer.<BuildAnimal>g__P|50_4(1f, num5 * 0.05f, -0.02f, ref CS$<>8__locals2), 0f, "skull", 1);
					if (!flag2)
					{
						this.AddEllipse(XRayRenderer.<BuildAnimal>g__P|50_4(0.36f, num5 * 0.13f, 0.08f, ref CS$<>8__locals2), CS$<>8__locals2.hf, 0.07f * CS$<>8__locals2.sl, CS$<>8__locals2.hu, 0.06f * CS$<>8__locals2.sl, 8, 0.0, 6.283185307179586, XRayRenderer.Style.Fine, "skull", 2);
					}
				}
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, XRayRenderer.<BuildAnimal>g__P|50_4(1f, -0.05f, 0.06f, ref CS$<>8__locals2), XRayRenderer.<BuildAnimal>g__P|50_4(1f, 0.05f, 0.06f, ref CS$<>8__locals2), 0f, "skull", 2);
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, XRayRenderer.<BuildAnimal>g__P|50_4(1f, -0.05f, -0.02f, ref CS$<>8__locals2), XRayRenderer.<BuildAnimal>g__P|50_4(1f, 0.05f, -0.02f, ref CS$<>8__locals2), 0f, "skull", 2);
				foreach (float num6 in new float[]
				{
					-1f,
					1f
				})
				{
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, XRayRenderer.<BuildAnimal>g__P|50_4(0.06f, num6 * 0.12f, -0.1f, ref CS$<>8__locals2), XRayRenderer.<BuildAnimal>g__P|50_4(0.3f, num6 * 0.11f, -0.24f, ref CS$<>8__locals2), 0f, "jaw", 0);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, XRayRenderer.<BuildAnimal>g__P|50_4(0.3f, num6 * 0.11f, -0.24f, ref CS$<>8__locals2), XRayRenderer.<BuildAnimal>g__P|50_4(0.95f, num6 * 0.04f, -0.1f, ref CS$<>8__locals2), 0f, "jaw", 0);
				}
				if (!flag2)
				{
					int num7 = flag ? 7 : 4;
					for (int j = 0; j < num7; j++)
					{
						float num8 = 0.45f + (float)j * (0.48f / (float)Math.Max(1, num7 - 1));
						foreach (float num9 in new float[]
						{
							-1f,
							1f
						})
						{
							XRayRenderer.Kind kind4 = XRayRenderer.Kind.Dot;
							XRayRenderer.Style style4 = XRayRenderer.Style.Bone;
							V3 a3 = XRayRenderer.<BuildAnimal>g__P|50_4(num8, num9 * (0.1f - num8 * 0.05f), -0.07f, ref CS$<>8__locals2);
							float r2 = 0.009f * scale;
							this.Add(kind4, style4, a3, default(V3), r2, "jaw", 3);
						}
					}
				}
				this._framePoints.Add(XRayRenderer.<BuildAnimal>g__P|50_4(1.05f, 0f, 0f, ref CS$<>8__locals2));
				this._framePoints.Add(XRayRenderer.<BuildAnimal>g__P|50_4(0.1f, 0f, 0.25f, ref CS$<>8__locals2));
			}
			int num10 = flag ? 13 : ((q == XRayQuality.Medium) ? 9 : 6);
			for (int k = 0; k < num10; k++)
			{
				float num11 = 0.98f - (float)k * (0.46f / (float)Math.Max(1, num10 - 1));
				float num12 = Math.Max(0.55f, 1f - (float)Math.Pow((double)((num11 - 0.72f) / 0.4f), 2.0));
				float num13 = (num11 > 0.68f) ? 1f : (0.62f + (num11 - 0.52f) * 2.3f);
				foreach (float num14 in new float[]
				{
					-1f,
					1f
				})
				{
					List<V3> list4 = new List<V3>();
					int num15 = flag ? 6 : 4;
					for (int l = 0; l <= num15; l++)
					{
						double num16 = -1.4207963267948966 + (double)l * (1.4207963267948966 + 1.5707963267948966 * (double)num13 - 0.05) / (double)num15;
						float right2 = num14 * halfWidth * num12 * (float)Math.Cos(num16);
						float fwd2 = depth * (float)Math.Sin(num16);
						float s = -0.05f * scale * (float)((num16 + 1.5707963267948966) / 3.141592653589793);
						list4.Add(CS$<>8__locals1.body.TorsoPoint(num11, right2, fwd2) + up * s);
					}
					string key2 = (num14 < 0f) ? "ribs_l" : "ribs_r";
					int num17 = 0;
					while (num17 + 1 < list4.Count)
					{
						this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, list4[num17], list4[num17 + 1], 0f, key2, 1);
						num17++;
					}
				}
			}
			this.Add(XRayRenderer.Kind.Bone, XRayRenderer.Style.Bone, CS$<>8__locals1.body.TorsoPoint(1f, 0f, depth * 0.98f), CS$<>8__locals1.body.TorsoPoint(0.66f, 0f, depth * 0.98f), 0.018f * scale, "sternum", 1);
			foreach (ValueTuple<float, string, int, int, int, int, int, ValueTuple<int, int, int, int, int, int>> valueTuple in new ValueTuple<float, string, int, int, int, int, int, ValueTuple<int, int, int, int, int, int>>[]
			{
				new ValueTuple<float, string, int, int, int, int, int, ValueTuple<int, int, int, int, int, int>>(-1f, "_l", 30226, 37873, 53675, 34606, 41403, new ValueTuple<int, int, int, int, int, int>(41404, 65478, 55120, 45454, 53081, 53082)),
				new ValueTuple<float, string, int, int, int, int, int, ValueTuple<int, int, int, int, int, int>>(1f, "_r", 54802, 46065, 54187, 22798, 16827, new ValueTuple<int, int, int, int, int, int>(16828, 6884, 43312, 33646, 41273, 41274))
			})
			{
				float item = valueTuple.Item1;
				string item2 = valueTuple.Item2;
				int item3 = valueTuple.Item3;
				int item4 = valueTuple.Item4;
				int item5 = valueTuple.Item5;
				int item6 = valueTuple.Item6;
				int item7 = valueTuple.Item7;
				int item8 = valueTuple.Rest.Item1;
				int item9 = valueTuple.Rest.Item2;
				int item10 = valueTuple.Rest.Item3;
				int item11 = valueTuple.Rest.Item4;
				int item12 = valueTuple.Rest.Item5;
				int item13 = valueTuple.Rest.Item6;
				if (CS$<>8__locals1.body.Has(item3) && CS$<>8__locals1.body.Has(item4))
				{
					V3 a4 = CS$<>8__locals1.<BuildAnimal>g__Get|0(item3);
					V3 b = CS$<>8__locals1.<BuildAnimal>g__Get|0(item4);
					V3 a5 = a4 - fwd * (0.1f * scale) + up * (0.06f * scale);
					V3 v3 = a4 - fwd * (0.1f * scale) - up * (0.07f * scale);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, a5, b, 0f, "scapula" + item2, 0);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, v3, b, 0f, "scapula" + item2, 0);
					if (!flag2)
					{
						this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, a5, v3, 0f, "scapula" + item2, 2);
					}
					if (!flag2)
					{
						this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, V3.Lerp(a5, v3, 0.5f), b, 0f, "scapula" + item2, 3);
					}
				}
				this.LegBone(CS$<>8__locals1.body, item4, item5, "humerus" + item2, 0.022f * scale, 0);
				this.LegBone(CS$<>8__locals1.body, item5, item6, "forearm" + item2, 0.017f * scale, 0);
				this.LegBone(CS$<>8__locals1.body, item6, item7, "cannon_f" + item2, 0.013f * scale, 0);
				this.LegBone(CS$<>8__locals1.body, item7, item8, "foot_f" + item2, 0.012f * scale, 1);
				foreach (int num18 in new int[]
				{
					item4,
					item5,
					item6,
					item7
				}.Where(new Func<int, bool>(CS$<>8__locals1.body.Has)))
				{
					XRayRenderer.Kind kind5 = XRayRenderer.Kind.Dot;
					XRayRenderer.Style style5 = XRayRenderer.Style.Bone;
					V3 a6 = CS$<>8__locals1.<BuildAnimal>g__Get|0(num18);
					float r2 = 0.022f * scale;
					string key = (num18 == item4) ? ("humerus" + item2) : ((num18 == item5) ? ("forearm" + item2) : ((num18 == item6) ? ("cannon_f" + item2) : ("foot_f" + item2)));
					this.Add(kind5, style5, a6, default(V3), r2, key, 1);
				}
				if (CS$<>8__locals1.body.Has(item8))
				{
					XRayRenderer.Kind kind6 = XRayRenderer.Kind.Dot;
					XRayRenderer.Style style6 = XRayRenderer.Style.Bone;
					V3 a7 = CS$<>8__locals1.<BuildAnimal>g__Get|0(item8);
					float r2 = 0.02f * scale;
					string key = "foot_f" + item2;
					this.Add(kind6, style6, a7, default(V3), r2, key, 1);
				}
				if (!flag2 && CS$<>8__locals1.body.Has(item5) && CS$<>8__locals1.body.Has(item4))
				{
					XRayRenderer.Kind kind7 = XRayRenderer.Kind.Dot;
					XRayRenderer.Style style7 = XRayRenderer.Style.Bone;
					V3 a8 = CS$<>8__locals1.<BuildAnimal>g__Get|0(item5) - up * (0.035f * scale);
					float r2 = 0.016f * scale;
					string key = "forearm" + item2;
					this.Add(kind7, style7, a8, default(V3), r2, key, 3);
				}
				this.LegBone(CS$<>8__locals1.body, item9, item10, "femur" + item2, 0.025f * scale, 0);
				this.LegBone(CS$<>8__locals1.body, item10, item11, "tibia" + item2, 0.019f * scale, 0);
				this.LegBone(CS$<>8__locals1.body, item11, item12, "cannon_h" + item2, 0.013f * scale, 0);
				this.LegBone(CS$<>8__locals1.body, item12, item13, "foot_h" + item2, 0.012f * scale, 1);
				foreach (int num19 in new int[]
				{
					item9,
					item10,
					item11,
					item12
				}.Where(new Func<int, bool>(CS$<>8__locals1.body.Has)))
				{
					XRayRenderer.Kind kind8 = XRayRenderer.Kind.Dot;
					XRayRenderer.Style style8 = XRayRenderer.Style.Bone;
					V3 a9 = CS$<>8__locals1.<BuildAnimal>g__Get|0(num19);
					float r2 = 0.024f * scale;
					string key = (num19 == item9) ? ("femur" + item2) : ((num19 == item10) ? ("tibia" + item2) : ((num19 == item11) ? ("cannon_h" + item2) : ("foot_h" + item2)));
					this.Add(kind8, style8, a9, default(V3), r2, key, 1);
				}
				if (CS$<>8__locals1.body.Has(item13))
				{
					XRayRenderer.Kind kind9 = XRayRenderer.Kind.Dot;
					XRayRenderer.Style style9 = XRayRenderer.Style.Bone;
					V3 a10 = CS$<>8__locals1.<BuildAnimal>g__Get|0(item13);
					float r2 = 0.02f * scale;
					string key = "foot_h" + item2;
					this.Add(kind9, style9, a10, default(V3), r2, key, 1);
				}
				if (!flag2)
				{
					if (CS$<>8__locals1.body.Has(item10))
					{
						XRayRenderer.Kind kind10 = XRayRenderer.Kind.Dot;
						XRayRenderer.Style style10 = XRayRenderer.Style.Bone;
						V3 a11 = CS$<>8__locals1.<BuildAnimal>g__Get|0(item10) + up * (0.03f * scale);
						float r2 = 0.015f * scale;
						string key = "tibia" + item2;
						this.Add(kind10, style10, a11, default(V3), r2, key, 3);
					}
					if (CS$<>8__locals1.body.Has(item11))
					{
						XRayRenderer.Kind kind11 = XRayRenderer.Kind.Dot;
						XRayRenderer.Style style11 = XRayRenderer.Style.Bone;
						V3 a12 = CS$<>8__locals1.<BuildAnimal>g__Get|0(item11) - up * (0.045f * scale);
						float r2 = 0.016f * scale;
						string key = "tibia" + item2;
						this.Add(kind11, style11, a12, default(V3), r2, key, 3);
					}
				}
				if (!flag2 && CS$<>8__locals1.body.Has(item9) && CS$<>8__locals1.body.Has(item10))
				{
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Vessel, CS$<>8__locals1.<BuildAnimal>g__Get|0(item9) - right * (item * 0.04f * scale), V3.Lerp(CS$<>8__locals1.<BuildAnimal>g__Get|0(item9), CS$<>8__locals1.<BuildAnimal>g__Get|0(item10), 0.6f) - right * (item * 0.04f * scale), 0f, "femoral" + item2, 3);
				}
			}
			if (CS$<>8__locals1.body.Has(56200))
			{
				V3 v4 = CS$<>8__locals1.<BuildAnimal>g__Get|0(56200);
				foreach (float num20 in new float[]
				{
					-1f,
					1f
				})
				{
					int id2 = (num20 < 0f) ? 65478 : 6884;
					V3 v5 = CS$<>8__locals1.body.Has(id2) ? CS$<>8__locals1.<BuildAnimal>g__Get|0(id2) : (v4 + right * (num20 * 0.1f * scale) + fwd * (0.12f * scale));
					V3 a13 = v4 + up * (0.16f * scale) + right * (num20 * 0.14f * scale) - fwd * (0.02f * scale);
					V3 v6 = v4 - up * (0.12f * scale) + right * (num20 * 0.07f * scale) + fwd * (0.1f * scale);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, a13, v5, 0f, "pelvis", 0);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, v5, v6, 0f, "pelvis", 0);
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, a13, v4, 0f, "pelvis", 2);
					if (!flag2)
					{
						this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v6, v4 - up * (0.08f * scale), 0f, "pelvis", 2);
					}
				}
			}
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0014DA18 File Offset: 0x0014BC18
		private void LegBone(Anatomy.Body body, int a, int b, string key, float r, int pri)
		{
			if (body.Has(a) && body.Has(b))
			{
				this.Add(XRayRenderer.Kind.Bone, XRayRenderer.Style.Bone, body.Get(a), body.Get(b), r, key, pri);
			}
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0014DA4C File Offset: 0x0014BC4C
		private void BuildSkull(V3 hc, V3 hf, V3 hr, V3 hu, XRayQuality q)
		{
			XRayRenderer.<>c__DisplayClass52_0 CS$<>8__locals1;
			CS$<>8__locals1.hc = hc;
			CS$<>8__locals1.hf = hf;
			CS$<>8__locals1.hr = hr;
			CS$<>8__locals1.hu = hu;
			bool flag = q == XRayQuality.High;
			bool flag2 = q == XRayQuality.Low;
			V3 v = CS$<>8__locals1.hc - CS$<>8__locals1.hf * 0.012f;
			int segs = flag ? 20 : (flag2 ? 12 : 16);
			foreach (ValueTuple<float, XRayRenderer.Style, int> valueTuple in new ValueTuple<float, XRayRenderer.Style, int>[]
			{
				new ValueTuple<float, XRayRenderer.Style, int>(-0.03f, XRayRenderer.Style.Fine, 3),
				new ValueTuple<float, XRayRenderer.Style, int>(0f, XRayRenderer.Style.Bone, 0),
				new ValueTuple<float, XRayRenderer.Style, int>(0.045f, XRayRenderer.Style.Fine, 1),
				new ValueTuple<float, XRayRenderer.Style, int>(0.072f, XRayRenderer.Style.Fine, 2)
			})
			{
				float item = valueTuple.Item1;
				XRayRenderer.Style item2 = valueTuple.Item2;
				int item3 = valueTuple.Item3;
				if (!flag2 || item == 0f)
				{
					float num = (float)Math.Sqrt((double)Math.Max(0f, 1f - item / 0.088f * (item / 0.088f)));
					this.AddEllipse(v + CS$<>8__locals1.hu * item, CS$<>8__locals1.hf, 0.097f * num, CS$<>8__locals1.hr, 0.076f * num, segs, 0.0, 6.283185307179586, item2, "skull", item3);
				}
			}
			this.AddEllipse(v, CS$<>8__locals1.hf, 0.097f, CS$<>8__locals1.hu, 0.088f, segs, -0.3, 3.891592653589793, XRayRenderer.Style.Bone, "skull", 0);
			this.AddEllipse(v, CS$<>8__locals1.hr, 0.076f, CS$<>8__locals1.hu, 0.088f, segs, -0.55, 3.691592653589793, flag2 ? XRayRenderer.Style.Fine : XRayRenderer.Style.Bone, "skull", 1);
			if (!flag2)
			{
				foreach (float num2 in new float[]
				{
					-1f,
					1f
				})
				{
					V3 axisA = (CS$<>8__locals1.hf * 0.7071f + CS$<>8__locals1.hr * (0.7071f * num2)).Normalized();
					float ra = (float)Math.Sqrt(0.007592500187456608);
					this.AddEllipse(v, axisA, ra, CS$<>8__locals1.hu, 0.088f, flag ? 14 : 10, 0.1, 3.041592653589793, XRayRenderer.Style.Fine, "skull", 3);
				}
				foreach (float num3 in new float[]
				{
					-1f,
					1f
				})
				{
					this.AddEllipse(v + CS$<>8__locals1.hu * 0.02f, CS$<>8__locals1.hf, 0.07566f, CS$<>8__locals1.hr, num3 * 0.076f * 0.92f, flag ? 8 : 6, -0.2, 1.7278759594743864, XRayRenderer.Style.Fine, "skull", 3);
				}
				this.AddEllipse(v - CS$<>8__locals1.hu * 0.035f, CS$<>8__locals1.hr, 0.0418f, CS$<>8__locals1.hf, -0.08924001f, 8, 0.25, 2.891592653589793, XRayRenderer.Style.Fine, "skull", 3);
				this.Add(XRayRenderer.Kind.Dot, XRayRenderer.Style.Bone, v - CS$<>8__locals1.hf * 0.095060006f - CS$<>8__locals1.hu * 0.03f, default(V3), 0.008f, "skull", 3);
			}
			this.AddEllipse(XRayRenderer.<BuildSkull>g__P|52_0(0.08f, 0f, 0.005f, ref CS$<>8__locals1), CS$<>8__locals1.hr, 0.058f, CS$<>8__locals1.hf, 0.02f, 8, 0.15, 2.991592653589793, XRayRenderer.Style.Fine, "skull", 1);
			foreach (float num4 in new float[]
			{
				-1f,
				1f
			})
			{
				this.AddEllipse(XRayRenderer.<BuildSkull>g__P|52_0(0.084f, num4 * 0.033f, -0.022f, ref CS$<>8__locals1), CS$<>8__locals1.hr, 0.019f, CS$<>8__locals1.hu, 0.017f, flag ? 10 : 8, 0.0, 6.283185307179586, XRayRenderer.Style.Fine, "skull", 1);
			}
			V3 v2 = XRayRenderer.<BuildSkull>g__P|52_0(0.097f, 0f, -0.035f, ref CS$<>8__locals1);
			V3 v3 = XRayRenderer.<BuildSkull>g__P|52_0(0.094f, -0.013f, -0.066f, ref CS$<>8__locals1);
			V3 v4 = XRayRenderer.<BuildSkull>g__P|52_0(0.094f, 0.013f, -0.066f, ref CS$<>8__locals1);
			V3 b = XRayRenderer.<BuildSkull>g__P|52_0(0.098f, 0f, -0.072f, ref CS$<>8__locals1);
			this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v2, v3, 0f, "skull", 2);
			this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v2, v4, 0f, "skull", 2);
			this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v3, b, 0f, "skull", 2);
			this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v4, b, 0f, "skull", 2);
			this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, XRayRenderer.<BuildSkull>g__P|52_0(0.092f, 0f, -0.008f, ref CS$<>8__locals1), v2, 0f, "skull", 2);
			foreach (float num5 in new float[]
			{
				-1f,
				1f
			})
			{
				V3 v5 = XRayRenderer.<BuildSkull>g__P|52_0(0.072f, num5 * 0.05f, -0.048f, ref CS$<>8__locals1);
				V3 b2 = XRayRenderer.<BuildSkull>g__P|52_0(-0.004f, num5 * 0.064f, -0.052f, ref CS$<>8__locals1);
				V3 v6 = XRayRenderer.<BuildSkull>g__P|52_0(0.082f, num5 * 0.04f, -0.078f, ref CS$<>8__locals1);
				if (!flag2)
				{
					this.Add(XRayRenderer.Kind.Dot, XRayRenderer.Style.Bone, XRayRenderer.<BuildSkull>g__P|52_0(-0.012f, num5 * 0.058f, -0.075f, ref CS$<>8__locals1), default(V3), 0.009f, "skull", 3);
					this.Add(XRayRenderer.Kind.Dot, XRayRenderer.Style.Bone, v5, default(V3), 0.009f, "skull", 3);
				}
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, XRayRenderer.<BuildSkull>g__P|52_0(0.08f, num5 * 0.05f, -0.03f, ref CS$<>8__locals1), v5, 0f, "skull", 2);
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v5, b2, 0f, "skull", 2);
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v5, v6, 0f, "skull", 2);
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, v6, XRayRenderer.<BuildSkull>g__P|52_0(0.096f, num5 * 0.012f, -0.084f, ref CS$<>8__locals1), 0f, "skull", 2);
				V3 a = XRayRenderer.<BuildSkull>g__P|52_0(-0.002f, num5 * 0.062f, -0.06f, ref CS$<>8__locals1);
				V3 v7 = XRayRenderer.<BuildSkull>g__P|52_0(0.004f, num5 * 0.054f, -0.128f, ref CS$<>8__locals1);
				V3 v8 = XRayRenderer.<BuildSkull>g__P|52_0(0.06f, num5 * 0.04f, -0.14f, ref CS$<>8__locals1);
				V3 b3 = XRayRenderer.<BuildSkull>g__P|52_0(0.09f, num5 * 0.012f, -0.15f, ref CS$<>8__locals1);
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, a, v7, 0f, "jaw", 0);
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, v7, v8, 0f, "jaw", 0);
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, v8, b3, 0f, "jaw", 0);
				if (!flag2)
				{
					this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, XRayRenderer.<BuildSkull>g__P|52_0(0.01f, num5 * 0.058f, -0.075f, ref CS$<>8__locals1), XRayRenderer.<BuildSkull>g__P|52_0(0.03f, num5 * 0.05f, -0.098f, ref CS$<>8__locals1), 0f, "jaw", 2);
				}
			}
			this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Bone, XRayRenderer.<BuildSkull>g__P|52_0(0.09f, -0.012f, -0.15f, ref CS$<>8__locals1), XRayRenderer.<BuildSkull>g__P|52_0(0.09f, 0.012f, -0.15f, ref CS$<>8__locals1), 0f, "jaw", 0);
			if (!flag2)
			{
				int num6 = flag ? 5 : 3;
				for (int j = -num6; j <= num6; j++)
				{
					float num7 = (float)j * (0.032f / (float)num6);
					float num8 = 0.093f - num7 * num7 * 18f;
					XRayRenderer.Kind kind = XRayRenderer.Kind.Dot;
					XRayRenderer.Style style = XRayRenderer.Style.Bone;
					V3 a2 = XRayRenderer.<BuildSkull>g__P|52_0(num8, num7, -0.092f, ref CS$<>8__locals1);
					int i = flag ? 2 : 3;
					this.Add(kind, style, a2, default(V3), 0.0042f, "jaw", i);
					XRayRenderer.Kind kind2 = XRayRenderer.Kind.Dot;
					XRayRenderer.Style style2 = XRayRenderer.Style.Bone;
					V3 a3 = XRayRenderer.<BuildSkull>g__P|52_0(num8 - 0.003f, num7, -0.11f, ref CS$<>8__locals1);
					i = (flag ? 2 : 3);
					this.Add(kind2, style2, a3, default(V3), 0.0042f, "jaw", i);
				}
				this.Add(XRayRenderer.Kind.Line, XRayRenderer.Style.Fine, XRayRenderer.<BuildSkull>g__P|52_0(0.075f, -0.034f, -0.101f, ref CS$<>8__locals1), XRayRenderer.<BuildSkull>g__P|52_0(0.075f, 0.034f, -0.101f, ref CS$<>8__locals1), 0f, "jaw", 3);
			}
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0014E38C File Offset: 0x0014C58C
		private void AddEllipse(V3 center, V3 axisA, float ra, V3 axisB, float rb, int segs, double t0, double t1, XRayRenderer.Style style, string key, int pri)
		{
			bool flag = t1 - t0 >= 6.282185307179586;
			int num = flag ? segs : Math.Max(2, (int)Math.Ceiling((double)segs * (t1 - t0) / 6.283185307179586) + 1);
			V3[] array = new V3[num];
			for (int i = 0; i < num; i++)
			{
				double num2 = flag ? (t0 + (double)i * (t1 - t0) / (double)num) : (t0 + (double)i * (t1 - t0) / (double)(num - 1));
				array[i] = center + axisA * (float)(Math.Cos(num2) * (double)ra) + axisB * (float)(Math.Sin(num2) * (double)rb);
			}
			this._prims.Add(new XRayRenderer.Prim
			{
				Kind = XRayRenderer.Kind.Ring,
				Style = style,
				A = center,
				B = center,
				R = Math.Max(ra, rb),
				Pts = array,
				Key = key,
				Priority = pri,
				Open = !flag
			});
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0014E4A0 File Offset: 0x0014C6A0
		private static V3 Perp(V3 axis, V3 prefer, V3 fallback)
		{
			V3 v = prefer - axis * V3.Dot(prefer, axis);
			if (v.Length() < 0.2f)
			{
				v = fallback - axis * V3.Dot(fallback, axis);
			}
			return v.Normalized();
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0014E4EA File Offset: 0x0014C6EA
		private static string SpineKey(float frac)
		{
			if (frac > 1f)
			{
				return "cervical";
			}
			if (frac <= 0.45f)
			{
				return "spine_l";
			}
			return "spine_t";
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0014E510 File Offset: 0x0014C710
		private void MarkHits([TupleElementNames(new string[]
		{
			"path",
			"thin"
		})] IList<ValueTuple<PathResult, bool>> paths, ISet<string> keys)
		{
			List<ValueTuple<V3, V3>> list = (from p in paths
			where !p.Item1.Blunt
			select new ValueTuple<V3, V3>(p.Item1.Entry, p.Item1.End)).ToList<ValueTuple<V3, V3>>();
			List<ValueTuple<V3, V3>> list2 = (from p in paths
			select new ValueTuple<V3, V3>(p.Item1.Entry, p.Item1.End)).ToList<ValueTuple<V3, V3>>();
			foreach (XRayRenderer.Prim prim in this._prims)
			{
				if (prim.Key != null)
				{
					if (prim.Style == XRayRenderer.Style.Organ || prim.Style == XRayRenderer.Style.Vessel || prim.Kind == XRayRenderer.Kind.Sphere || prim.Kind == XRayRenderer.Kind.Ring)
					{
						prim.Hit = keys.Contains(prim.Key);
					}
					else if (XRayRenderer.<MarkHits>g__Skeletal|56_3(prim))
					{
						foreach (ValueTuple<V3, V3> valueTuple in list)
						{
							V3 item = valueTuple.Item1;
							V3 item2 = valueTuple.Item2;
							V3 a = prim.A;
							float num = (prim.Kind == XRayRenderer.Kind.Dot) ? Anatomy.DistToSegment(prim.A, item, item2) : Anatomy.SegmentDistance(prim.A, prim.B, item, item2, out a);
							float num2 = ((prim.Kind == XRayRenderer.Kind.Line) ? 0.004f : prim.R) + 0.014f;
							if (num <= num2)
							{
								prim.Hit = true;
								prim.Fracture = true;
								prim.HitAt = a;
								break;
							}
						}
					}
				}
			}
			using (IEnumerator<string> enumerator3 = keys.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					string key = enumerator3.Current;
					List<XRayRenderer.Prim> list3 = (from p in this._prims
					where p.Key == key && XRayRenderer.<MarkHits>g__Skeletal|56_3(p)
					select p).ToList<XRayRenderer.Prim>();
					if (list3.Count != 0)
					{
						if (!list3.Any((XRayRenderer.Prim p) => p.Hit))
						{
							XRayRenderer.Prim prim2 = null;
							float num3 = float.MaxValue;
							V3 hitAt = default(V3);
							foreach (XRayRenderer.Prim prim3 in list3)
							{
								foreach (ValueTuple<V3, V3> valueTuple2 in list2)
								{
									V3 item3 = valueTuple2.Item1;
									V3 item4 = valueTuple2.Item2;
									V3 a2 = prim3.A;
									float num4 = (prim3.Kind == XRayRenderer.Kind.Dot) ? Anatomy.DistToSegment(prim3.A, item3, item4) : Anatomy.SegmentDistance(prim3.A, prim3.B, item3, item4, out a2);
									if (num4 < num3)
									{
										num3 = num4;
										prim2 = prim3;
										hitAt = a2;
									}
								}
							}
							if (prim2 != null)
							{
								prim2.Hit = true;
								prim2.Fracture = true;
								prim2.HitAt = hitAt;
							}
						}
					}
				}
			}
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0014E8C8 File Offset: 0x0014CAC8
		private XRayRenderer.Prim Add(XRayRenderer.Kind kind, XRayRenderer.Style style, V3 a, V3 b = default(V3), float r = 0f, string key = null, int pri = 2)
		{
			XRayRenderer.Prim prim = new XRayRenderer.Prim
			{
				Kind = kind,
				Style = style,
				A = a,
				B = ((kind == XRayRenderer.Kind.Line || kind == XRayRenderer.Kind.Bone) ? b : a),
				R = r,
				Key = key,
				Priority = pri
			};
			this._prims.Add(prim);
			return prim;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0014E928 File Offset: 0x0014CB28
		private static List<V3> Resample(List<V3> poly, float spacing)
		{
			List<V3> list = new List<V3>();
			if (poly.Count == 0)
			{
				return list;
			}
			list.Add(poly[0]);
			float num = 0f;
			int num2 = 0;
			while (num2 + 1 < poly.Count)
			{
				V3 v = poly[num2];
				V3 v2 = poly[num2 + 1];
				float num3 = (v2 - v).Length();
				if (num3 >= 1E-05f)
				{
					float num4;
					for (num4 = spacing - num; num4 <= num3; num4 += spacing)
					{
						list.Add(V3.Lerp(v, v2, num4 / num3));
					}
					num = num3 - (num4 - spacing);
				}
				num2++;
			}
			return list;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0014E9CC File Offset: 0x0014CBCC
		public int Draw(float vx, float vy, float vw, float vh, float dt, int timeMs, int budget)
		{
			int rectsThisFrame = UiDraw.RectsThisFrame;
			this._start = rectsThisFrame;
			this._budget = budget;
			this.LastCut = 0;
			this.StepCamera(dt);
			double num = (double)(this._baseYaw + this.Yaw) * 3.141592653589793 / 180.0;
			double num2 = (double)this.Pitch * 3.141592653589793 / 180.0;
			V3 v = new V3((float)Math.Sin(num), (float)Math.Cos(num), 0f);
			V3 v2 = v * (float)Math.Cos(num2) + new V3(0f, 0f, -(float)Math.Sin(num2));
			V3 v3 = new V3(v.Y, -v.X, 0f);
			V3 v4 = V3.Cross(v3, v2);
			V3 center = this._center;
			float num3 = 0.05f;
			float num4 = 0.05f;
			foreach (V3 a in this._framePoints)
			{
				V3 a2 = a - center;
				num3 = Math.Max(num3, Math.Abs(V3.Dot(a2, v3)));
				num4 = Math.Max(num4, Math.Abs(V3.Dot(a2, v4)));
			}
			float num5 = Math.Min(vh * 0.85f / (2f * num4 + 0.12f), vw * UiDraw.Aspect * 0.85f / (2f * num3 + 0.12f));
			this._fit = ((this._fit < 0f) ? num5 : (this._fit + (num5 - this._fit) * Math.Min(1f, dt * 6f)));
			XRayRenderer.Cam cam = new XRayRenderer.Cam
			{
				C = center,
				S = v3,
				U = v4,
				F = v2,
				Scale = this._fit * this.Zoom,
				Cx = vx + vw / 2f + this.PanX,
				Cy = vy + vh / 2f + this.PanY
			};
			this._lastCam = cam;
			this._clip = new ValueTuple<float, float, float, float>(vx, vy, vx + vw, vy + vh);
			bool flag = timeMs / 220 % 2 == 0;
			ValueTuple<int, int, int> hitColor = flag ? new ValueTuple<int, int, int>(255, 70, 55) : new ValueTuple<int, int, int>(190, 30, 30);
			int num6 = Math.Max(100, budget / 25 * 25);
			float num7 = (float)Math.Ceiling((double)(this.Zoom * 4f)) / 4f;
			XRayRenderer._zoomThick = Math.Min(1.8f, (float)Math.Sqrt((double)Math.Max(1f, num7 - 0.25f)));
			string text = string.Format("{0}|{1}|{2}|{3}|{4:0.000}|{5:0.000}", new object[]
			{
				this._buildId,
				this._preference,
				num6,
				num7,
				vw,
				vh
			});
			if (text != this._planKey)
			{
				int budget2 = num6 - (int)((float)num6 * (6f + Math.Max(0f, num7 - 3f) * 3f) / 100f);
				if (this._planCacheBuild != this._buildId)
				{
					this._planCache.Clear();
					this._planCacheBuild = this._buildId;
				}
				ValueTuple<XRayQuality, int, bool, HashSet<XRayRenderer.Prim>, int, int> valueTuple;
				if (this._planCache.TryGetValue(text, out valueTuple))
				{
					this.ActiveLevel = valueTuple.Item1;
					if (this._preference == XRayQuality.Auto)
					{
						this._prims = this._levels[this.ActiveLevel];
					}
					this._planMp = valueTuple.Item2;
					this._planRich = valueTuple.Item3;
					this._planned = valueTuple.Item4;
					this.LastSpentEstimate = valueTuple.Item5;
					this.LastRichCost = valueTuple.Item6;
				}
				else
				{
					if (this._preference == XRayQuality.Auto)
					{
						this.ChooseAutoLevel(budget2, num7, vw, vh);
					}
					this.Plan(budget2, num7, vw, vh);
					int planCount = this.PlanCount;
					this.PlanCount = planCount + 1;
					if (this._planCache.Count > 40)
					{
						this._planCache.Clear();
					}
					this._planCache[text] = new ValueTuple<XRayQuality, int, bool, HashSet<XRayRenderer.Prim>, int, int>(this.ActiveLevel, this._planMp, this._planRich, this._planned, this.LastSpentEstimate, this.LastRichCost);
				}
				if (this._planKey != null && this._planKey.StartsWith(this._buildId.ToString() + "|") && UiDraw.RealMs - this._replanLogAt > 500 && (this._replanLogAt = UiDraw.RealMs) >= 0)
				{
					Action<string> diag = XRayRenderer.Diag;
					if (diag != null)
					{
						diag(string.Format("XRAY re-plan (same shot): {0} -> {1}  [build|detail|budget|zoom step|w|h] - now {2}, smoothness {3}", new object[]
						{
							this._planKey,
							text,
							this.ActiveLevel,
							this._planMp
						}));
					}
				}
				XRayRenderer._costClip = null;
				this._planKey = text;
			}
			this._mp = this._planMp;
			this._rich = this._planRich;
			foreach (XRayRenderer.Prim prim in this._prims)
			{
				prim.Depth = V3.Dot(V3.Lerp(prim.A, prim.B, 0.5f) - cam.C, cam.F);
				prim.Drawn = this._planned.Contains(prim);
			}
			this.LastDropped = this._prims.Count - this._planned.Count;
			foreach (XRayRenderer.Prim p4 in from p in this._prims
			where p.Drawn && p.Style == XRayRenderer.Style.Organ
			orderby p.Depth descending
			select p)
			{
				this.DrawPrim(p4, cam, hitColor, flag);
			}
			foreach (XRayRenderer.Prim p2 in from p in this._prims
			where p.Drawn && p.Style != XRayRenderer.Style.Organ
			orderby p.Priority, p.Depth descending
			select p)
			{
				this.DrawPrim(p2, cam, hitColor, flag);
			}
			foreach (XRayRenderer.Prim p3 in from p in this._prims
			where p.Drawn && p.Fracture
			select p)
			{
				ValueTuple<float, float, float, float, float, float, float, ValueTuple<float>> valueTuple2 = XRayRenderer.FractureMark(cam, p3);
				float item = valueTuple2.Item1;
				float item2 = valueTuple2.Item2;
				float item3 = valueTuple2.Item3;
				float item4 = valueTuple2.Item4;
				float item5 = valueTuple2.Item5;
				float item6 = valueTuple2.Item6;
				float item7 = valueTuple2.Item7;
				float item8 = valueTuple2.Rest.Item1;
				this.Seg(item, item2, item3, item4, 0.003f, new ValueTuple<int, int, int>(255, 220, 120), 255, 0);
				this.Seg(item5, item6, item7, item8, 0.003f, new ValueTuple<int, int, int>(255, 220, 120), 255, 0);
			}
			this.LastRects = UiDraw.RectsThisFrame - rectsThisFrame;
			return this.LastRects;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0014F24C File Offset: 0x0014D44C
		private static ValueTuple<float, float, float, float, float, float, float, ValueTuple<float>> FractureMark(XRayRenderer.Cam cam, XRayRenderer.Prim p)
		{
			ValueTuple<float, float, float> valueTuple = XRayRenderer.Project(cam, p.HitAt);
			float item = valueTuple.Item1;
			float item2 = valueTuple.Item2;
			float item3 = valueTuple.Item3;
			float num = 0.007f * item3;
			float aspect = UiDraw.Aspect;
			return new ValueTuple<float, float, float, float, float, float, float, ValueTuple<float>>(item - num / aspect, item2 - num, item + num / aspect, item2 + num, item - num / aspect, item2 + num, item + num / aspect, new ValueTuple<float>(item2 - num));
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0014F2B8 File Offset: 0x0014D4B8
		private static int FractureCost(XRayRenderer.Prim p, XRayRenderer.Cam cam, int mp)
		{
			if (!p.Fracture)
			{
				return 0;
			}
			ValueTuple<float, float, float, float, float, float, float, ValueTuple<float>> valueTuple = XRayRenderer.FractureMark(cam, p);
			float item = valueTuple.Item1;
			float item2 = valueTuple.Item2;
			float item3 = valueTuple.Item3;
			float item4 = valueTuple.Item4;
			float item5 = valueTuple.Item5;
			float item6 = valueTuple.Item6;
			float item7 = valueTuple.Item7;
			float item8 = valueTuple.Rest.Item1;
			return XRayRenderer.PlanCost(item, item2, item3, item4, 0.003f, mp) + XRayRenderer.PlanCost(item5, item6, item7, item8, 0.003f, mp);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0014F33C File Offset: 0x0014D53C
		[return: TupleElementNames(new string[]
		{
			"x",
			"y",
			"k"
		})]
		private static ValueTuple<float, float, float> Project(XRayRenderer.Cam c, V3 p)
		{
			V3 a = p - c.C;
			float num = V3.Dot(a, c.F);
			float num2 = 3.2f / Math.Max(0.5f, 3.2f + num);
			return new ValueTuple<float, float, float>(c.Cx + V3.Dot(a, c.S) * c.Scale * num2 / UiDraw.Aspect, c.Cy - V3.Dot(a, c.U) * c.Scale * num2, num2);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0014F3BF File Offset: 0x0014D5BF
		private static float Thick(XRayRenderer.Prim p, float k, float scale)
		{
			return XRayRenderer.ThickRaw(p, k, scale) * XRayRenderer._zoomThick;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0014F3D0 File Offset: 0x0014D5D0
		private static float ThickRaw(XRayRenderer.Prim p, float k, float scale)
		{
			XRayRenderer.Kind kind = p.Kind;
			if (kind == XRayRenderer.Kind.Bone)
			{
				return Math.Max(0.0042f, Math.Min(0.009f, 1.6f * p.R * scale * k));
			}
			if (kind != XRayRenderer.Kind.Sphere)
			{
				switch (p.Style)
				{
				case XRayRenderer.Style.Bone:
					return Math.Max(0.0036f, 0.004f * k);
				case XRayRenderer.Style.Fine:
					return Math.Max(0.0033f, 0.0036f * k);
				case XRayRenderer.Style.Cartilage:
				case XRayRenderer.Style.Vessel:
					return 0.003f;
				case XRayRenderer.Style.Incoming:
					return Math.Max(0.003f, 0.0034f * k);
				case XRayRenderer.Style.Inside:
					return Math.Max(0.004f, 0.0048f * k);
				case XRayRenderer.Style.Exit:
					return 0.003f;
				}
				return 0.0028f;
			}
			return Math.Max(0.003f, 0.0034f * k);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0014F4B0 File Offset: 0x0014D6B0
		private static int Cost(XRayRenderer.Prim p, XRayRenderer.Cam cam, int mp)
		{
			switch (p.Kind)
			{
			case XRayRenderer.Kind.Dot:
				if (p.Style != XRayRenderer.Style.Organ)
				{
					return 1;
				}
				return 2;
			case XRayRenderer.Kind.Ring:
			{
				int num = 0;
				for (int i = 0; i < (p.Open ? (p.Pts.Length - 1) : p.Pts.Length); i++)
				{
					ValueTuple<float, float, float> valueTuple = XRayRenderer.Project(cam, p.Pts[i]);
					float item = valueTuple.Item1;
					float item2 = valueTuple.Item2;
					float item3 = valueTuple.Item3;
					ValueTuple<float, float, float> valueTuple2 = XRayRenderer.Project(cam, p.Pts[(i + 1) % p.Pts.Length]);
					float item4 = valueTuple2.Item1;
					float item5 = valueTuple2.Item2;
					num += XRayRenderer.PlanCost(item, item2, item4, item5, XRayRenderer.Thick(p, item3, cam.Scale), XRayRenderer.Steps(p, mp));
				}
				return num;
			}
			case XRayRenderer.Kind.Sphere:
			{
				ValueTuple<float, float, float> valueTuple3 = XRayRenderer.Project(cam, p.A);
				float item6 = valueTuple3.Item1;
				float item7 = valueTuple3.Item2;
				float item8 = valueTuple3.Item3;
				float num2 = p.R * cam.Scale * item8;
				float t = XRayRenderer.Thick(p, item8, cam.Scale);
				int num3 = 0;
				for (int j = 0; j < 18; j++)
				{
					double num4 = (double)(j * 2) * 3.141592653589793 / 18.0;
					double num5 = (double)((j + 1) * 2) * 3.141592653589793 / 18.0;
					num3 += XRayRenderer.PlanCost(item6 + (float)Math.Cos(num4) * num2 / UiDraw.Aspect, item7 + (float)Math.Sin(num4) * num2 * 1.08f, item6 + (float)Math.Cos(num5) * num2 / UiDraw.Aspect, item7 + (float)Math.Sin(num5) * num2 * 1.08f, t, mp);
				}
				return num3;
			}
			default:
			{
				ValueTuple<float, float, float> valueTuple4 = XRayRenderer.Project(cam, p.A);
				float item9 = valueTuple4.Item1;
				float item10 = valueTuple4.Item2;
				float item11 = valueTuple4.Item3;
				ValueTuple<float, float, float> valueTuple5 = XRayRenderer.Project(cam, p.B);
				float item12 = valueTuple5.Item1;
				float item13 = valueTuple5.Item2;
				return XRayRenderer.PlanCost(item9, item10, item12, item13, XRayRenderer.Thick(p, item11, cam.Scale), XRayRenderer.Steps(p, mp));
			}
			}
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0014F6F0 File Offset: 0x0014D8F0
		private static int HighlightCost(XRayRenderer.Prim p, XRayRenderer.Cam cam, int mp)
		{
			ValueTuple<float, float, float> valueTuple = XRayRenderer.Project(cam, p.A);
			float item = valueTuple.Item1;
			float item2 = valueTuple.Item2;
			float item3 = valueTuple.Item3;
			ValueTuple<float, float, float> valueTuple2 = XRayRenderer.Project(cam, p.B);
			float item4 = valueTuple2.Item1;
			float item5 = valueTuple2.Item2;
			return XRayRenderer.PlanCost(item, item2, item4, item5, XRayRenderer.Thick(p, item3, cam.Scale) * 0.5f, XRayRenderer.Steps(p, mp));
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0014F75C File Offset: 0x0014D95C
		private static int PlanCost(float x0, float y0, float x1, float y1, float t, int mp)
		{
			ValueTuple<float, float, float, float>? costClip = XRayRenderer._costClip;
			if (costClip != null)
			{
				ValueTuple<float, float, float, float> valueOrDefault = costClip.GetValueOrDefault();
				float item = valueOrDefault.Item1;
				float item2 = valueOrDefault.Item2;
				float item3 = valueOrDefault.Item3;
				float item4 = valueOrDefault.Item4;
				if (!UiDraw.Clip(ref x0, ref y0, ref x1, ref y1, item, item2, item3, item4))
				{
					return 0;
				}
			}
			return UiDraw.LineCost(x0, y0, x1, y1, t, mp);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0014F7BD File Offset: 0x0014D9BD
		private static float Shade(float depth)
		{
			return 1f - Math.Max(0f, Math.Min(1f, (depth + 0.3f) / 0.6f)) * 0.45f;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0014F7EC File Offset: 0x0014D9EC
		private static ValueTuple<int, int, int> Mul([TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> c, float k)
		{
			return new ValueTuple<int, int, int>(Math.Min(255, (int)((float)c.Item1 * k)), Math.Min(255, (int)((float)c.Item2 * k)), Math.Min(255, (int)((float)c.Item3 * k)));
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0014F83C File Offset: 0x0014DA3C
		private void DrawPrim(XRayRenderer.Prim p, XRayRenderer.Cam cam, ValueTuple<int, int, int> hitColor, bool pulse)
		{
			float num = XRayRenderer.Shade(p.Depth);
			ValueTuple<float, float, float> valueTuple = XRayRenderer.Project(cam, p.A);
			float item = valueTuple.Item1;
			float item2 = valueTuple.Item2;
			float item3 = valueTuple.Item3;
			switch (p.Kind)
			{
			case XRayRenderer.Kind.Dot:
			{
				if (item < this._clip.Item1 || item > this._clip.Item3 || item2 < this._clip.Item2 || item2 > this._clip.Item4)
				{
					return;
				}
				float num2 = Math.Max(0.0025f, p.R * cam.Scale * item3);
				float aspect = UiDraw.Aspect;
				XRayRenderer.Style style = p.Style;
				ValueTuple<int, int, int> valueTuple2;
				int a;
				if (style != XRayRenderer.Style.Organ)
				{
					switch (style)
					{
					case XRayRenderer.Style.EntryMark:
						valueTuple2 = new ValueTuple<int, int, int>(255, 255, 255);
						a = 255;
						num2 = Math.Max(0.006f, num2);
						break;
					case XRayRenderer.Style.StopMark:
						valueTuple2 = new ValueTuple<int, int, int>(255, 215, 70);
						a = 255;
						num2 = Math.Max(0.007f, num2);
						break;
					case XRayRenderer.Style.BruiseMark:
						valueTuple2 = XRayViewer.PunchColor;
						a = 230;
						num2 = Math.Max(0.006f, num2);
						break;
					case XRayRenderer.Style.KickMark:
						valueTuple2 = XRayViewer.KickColor;
						a = 230;
						num2 = Math.Max(0.006f, num2);
						break;
					case XRayRenderer.Style.GunMark:
						valueTuple2 = XRayViewer.GunColor;
						a = 230;
						num2 = Math.Max(0.006f, num2);
						break;
					case XRayRenderer.Style.ChokeMark:
						valueTuple2 = XRayViewer.ChokeColor;
						a = 230;
						num2 = Math.Max(0.006f, num2);
						break;
					default:
						valueTuple2 = (p.Hit ? hitColor : XRayRenderer.Mul(XRayRenderer.BoneColor, num));
						a = (p.Hit ? 255 : 200);
						break;
					}
				}
				else
				{
					valueTuple2 = (p.Hit ? hitColor : p.Color);
					a = (p.Hit ? (pulse ? 170 : 110) : 38);
				}
				if (p.Style == XRayRenderer.Style.Organ)
				{
					if (!this.Spend(2))
					{
						return;
					}
					UiDraw.Blob(item, item2, num2 * (p.Hit ? 0.75f : 0.9f), valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, a);
					return;
				}
				else if (this._rich && p.Style == XRayRenderer.Style.Bone && num2 > 0.0032f)
				{
					if (!this.Spend(2))
					{
						return;
					}
					UiDraw.Blob(item, item2, num2, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255);
					return;
				}
				else
				{
					if (!this.Spend(1))
					{
						return;
					}
					UiDraw.Rect(item - num2 / aspect, item2 - num2, 2f * num2 / aspect, 2f * num2, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, a);
					return;
				}
				break;
			}
			case XRayRenderer.Kind.Ring:
			{
				ValueTuple<int, int, int> c = p.Hit ? hitColor : XRayRenderer.Mul(XRayRenderer.StyleColor(p.Style), num);
				for (int i = 0; i < (p.Open ? (p.Pts.Length - 1) : p.Pts.Length); i++)
				{
					ValueTuple<float, float, float> valueTuple3 = XRayRenderer.Project(cam, p.Pts[i]);
					float item4 = valueTuple3.Item1;
					float item5 = valueTuple3.Item2;
					float item6 = valueTuple3.Item3;
					ValueTuple<float, float, float> valueTuple4 = XRayRenderer.Project(cam, p.Pts[(i + 1) % p.Pts.Length]);
					float item7 = valueTuple4.Item1;
					float item8 = valueTuple4.Item2;
					this.Seg(item4, item5, item7, item8, XRayRenderer.Thick(p, item6, cam.Scale), c, 255, XRayRenderer.Steps(p, this._mp));
				}
				return;
			}
			case XRayRenderer.Kind.Sphere:
			{
				float num3 = p.R * cam.Scale * item3;
				float aspect2 = UiDraw.Aspect;
				float t = XRayRenderer.Thick(p, item3, cam.Scale);
				ValueTuple<int, int, int> c2 = p.Hit ? hitColor : XRayRenderer.Mul(XRayRenderer.BoneColor, num);
				for (int j = 0; j < 18; j++)
				{
					double num4 = (double)(j * 2) * 3.141592653589793 / 18.0;
					double num5 = (double)((j + 1) * 2) * 3.141592653589793 / 18.0;
					this.Seg(item + (float)Math.Cos(num4) * num3 / aspect2, item2 + (float)Math.Sin(num4) * num3 * 1.08f, item + (float)Math.Cos(num5) * num3 / aspect2, item2 + (float)Math.Sin(num5) * num3 * 1.08f, t, c2, 255, 0);
				}
				return;
			}
			default:
			{
				ValueTuple<float, float, float> valueTuple5 = XRayRenderer.Project(cam, p.B);
				float item9 = valueTuple5.Item1;
				float item10 = valueTuple5.Item2;
				float num6 = XRayRenderer.Thick(p, item3, cam.Scale);
				int steps = XRayRenderer.Steps(p, this._mp);
				if (p.Style == XRayRenderer.Style.Inside)
				{
					this.Seg(item, item2, item9, item10, num6, new ValueTuple<int, int, int>(255, 85, 60), 255, steps);
					return;
				}
				ValueTuple<int, int, int> c3 = p.Hit ? hitColor : XRayRenderer.Mul(XRayRenderer.StyleColor(p.Style), (p.Style >= XRayRenderer.Style.Incoming) ? 1f : num);
				if (this._rich && !p.Hit && p.Kind == XRayRenderer.Kind.Bone)
				{
					this.Seg(item, item2, item9, item10, num6, XRayRenderer.Mul(c3, 0.62f), 255, steps);
					float aspect3 = UiDraw.Aspect;
					float num7 = (item9 - item) * aspect3;
					float num8 = item10 - item2;
					float num9 = (float)Math.Sqrt((double)(num7 * num7 + num8 * num8));
					if (num9 > 0.0001f)
					{
						float num10 = -num8 / num9;
						float num11 = num7 / num9;
						if (num11 > 0f || (Math.Abs(num11) < 0.001f && num10 > 0f))
						{
							num10 = -num10;
							num11 = -num11;
						}
						float num12 = num6 * 0.2f;
						float num13 = num10 * num12 / aspect3;
						float num14 = num11 * num12;
						this.Seg(item + num13, item2 + num14, item9 + num13, item10 + num14, num6 * 0.5f, XRayRenderer.Mul(c3, 1.12f), 255, steps);
					}
					return;
				}
				this.Seg(item, item2, item9, item10, p.Hit ? (num6 * 1.25f) : num6, c3, 255, steps);
				return;
			}
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x0014FEAC File Offset: 0x0014E0AC
		public bool Rich
		{
			get
			{
				return this._rich;
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0014FEB4 File Offset: 0x0014E0B4
		private XRayRenderer.Cam CamAt(float yaw, float pitch, float zq, float vw, float vh)
		{
			double num = (double)(this._baseYaw + yaw) * 3.141592653589793 / 180.0;
			double num2 = (double)pitch * 3.141592653589793 / 180.0;
			V3 v = new V3((float)Math.Sin(num), (float)Math.Cos(num), 0f);
			V3 v2 = v * (float)Math.Cos(num2) + new V3(0f, 0f, -(float)Math.Sin(num2));
			V3 v3 = new V3(v.Y, -v.X, 0f);
			V3 v4 = V3.Cross(v3, v2);
			V3 center = this._center;
			float num3 = 0.05f;
			float num4 = 0.05f;
			foreach (V3 a in this._framePoints)
			{
				V3 a2 = a - center;
				num3 = Math.Max(num3, Math.Abs(V3.Dot(a2, v3)));
				num4 = Math.Max(num4, Math.Abs(V3.Dot(a2, v4)));
			}
			float num5 = Math.Min(vh * 0.85f / (2f * num4 + 0.12f), vw * UiDraw.Aspect * 0.85f / (2f * num3 + 0.12f));
			return new XRayRenderer.Cam
			{
				C = center,
				S = v3,
				U = v4,
				F = v2,
				Scale = num5 * zq,
				Cx = 0.5f,
				Cy = 0.5f
			};
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00150074 File Offset: 0x0014E274
		private List<XRayRenderer.Cam> PlanCams(float zq, float vw, float vh)
		{
			XRayRenderer._costClip = new ValueTuple<float, float, float, float>?(new ValueTuple<float, float, float, float>(0.5f - vw / 2f, 0.5f - vh / 2f, 0.5f + vw / 2f, 0.5f + vh / 2f));
			List<XRayRenderer.Cam> list = new List<XRayRenderer.Cam>();
			float[] array;
			if (zq <= 1f)
			{
				(array = new float[1])[0] = zq;
			}
			else
			{
				float[] array2 = new float[2];
				array2[0] = zq;
				array = array2;
				array2[1] = Math.Max(0.6f, zq - 0.25f);
			}
			foreach (float zq2 in array)
			{
				for (int j = 0; j < 360; j += 30)
				{
					foreach (float pitch in new float[]
					{
						-80f,
						-45f,
						-15f,
						10f,
						35f,
						60f,
						80f
					})
					{
						list.Add(this.CamAt((float)j, pitch, zq2, vw, vh));
					}
				}
			}
			return list;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00150168 File Offset: 0x0014E368
		private static int CoreWorstOf(List<XRayRenderer.Prim> prims, List<XRayRenderer.Cam> cams, int mp)
		{
			List<XRayRenderer.Prim> core = (from p in prims
			where p.Priority <= 2
			select p).ToList<XRayRenderer.Prim>();
			return cams.Max((XRayRenderer.Cam cam) => core.Sum((XRayRenderer.Prim p) => XRayRenderer.Cost(p, cam, mp) + XRayRenderer.FractureCost(p, cam, mp)));
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x001501C4 File Offset: 0x0014E3C4
		private void ChooseAutoLevel(int budget, float zq, float vw, float vh)
		{
			List<XRayRenderer.Cam> cams = this.PlanCams(zq, vw, vh);
			this.ActiveLevel = XRayQuality.Low;
			foreach (XRayQuality xrayQuality in new XRayQuality[]
			{
				XRayQuality.High,
				XRayQuality.Medium
			})
			{
				if (XRayRenderer.CoreWorstOf(this._levels[xrayQuality], cams, 8) <= budget)
				{
					this.ActiveLevel = xrayQuality;
					break;
				}
			}
			this._prims = this._levels[this.ActiveLevel];
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0015023C File Offset: 0x0014E43C
		private void Plan(int budget, float zq, float vw, float vh)
		{
			XRayRenderer.<>c__DisplayClass92_0 CS$<>8__locals1 = new XRayRenderer.<>c__DisplayClass92_0();
			CS$<>8__locals1.budget = budget;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cams = this.PlanCams(zq, vw, vh);
			CS$<>8__locals1.core = (from p in this._prims
			where p.Priority <= 2
			select p).ToList<XRayRenderer.Prim>();
			int l = 2;
			int num = 16;
			CS$<>8__locals1.best = 2;
			while (l <= num)
			{
				int num2 = (l + num) / 2;
				if (CS$<>8__locals1.<Plan>g__CoreWorst|1(num2) <= CS$<>8__locals1.budget)
				{
					CS$<>8__locals1.best = num2;
					l = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			this._planMp = CS$<>8__locals1.best;
			int[] array = (from cam in CS$<>8__locals1.cams
			select CS$<>8__locals1.core.Sum((XRayRenderer.Prim p) => XRayRenderer.Cost(p, cam, CS$<>8__locals1.best) + XRayRenderer.FractureCost(p, cam, CS$<>8__locals1.best))).ToArray<int>();
			CS$<>8__locals1.richCost = (from cam in CS$<>8__locals1.cams
			select (from p in CS$<>8__locals1.core
			where !p.Hit && p.Kind == XRayRenderer.Kind.Bone
			select p).Sum((XRayRenderer.Prim p) => XRayRenderer.HighlightCost(p, cam, CS$<>8__locals1.best)) + CS$<>8__locals1.core.Count(new Func<XRayRenderer.Prim, bool>(XRayRenderer.<Plan>g__BoneDot|92_3))).ToArray<int>();
			this._planRich = (CS$<>8__locals1.best >= 4 && array.Select((int t, int i) => t + CS$<>8__locals1.richCost[i]).All((int t) => t <= CS$<>8__locals1.budget));
			if (this._planRich)
			{
				for (int j = 0; j < array.Length; j++)
				{
					array[j] += CS$<>8__locals1.richCost[j];
				}
			}
			this._planned = new HashSet<XRayRenderer.Prim>(CS$<>8__locals1.core);
			using (IEnumerator<XRayRenderer.Prim> enumerator = (from p in this._prims
			where p.Priority > 2
			orderby p.Priority
			select p).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					XRayRenderer.Prim p = enumerator.Current;
					int[] array2 = (from cam in CS$<>8__locals1.cams
					select XRayRenderer.Cost(p, cam, CS$<>8__locals1.best) + XRayRenderer.FractureCost(p, cam, CS$<>8__locals1.best) + ((CS$<>8__locals1.<>4__this._planRich && XRayRenderer.<Plan>g__BoneDot|92_3(p)) ? 1 : 0)).ToArray<int>();
					bool flag = true;
					int num3 = 0;
					while (num3 < array.Length && flag)
					{
						flag = (array[num3] + array2[num3] <= CS$<>8__locals1.budget);
						num3++;
					}
					if (flag)
					{
						for (int k = 0; k < array.Length; k++)
						{
							array[k] += array2[k];
						}
						this._planned.Add(p);
					}
				}
			}
			this.LastSpentEstimate = array.Max();
			this.LastRichCost = CS$<>8__locals1.richCost.Max();
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x001504F4 File Offset: 0x0014E6F4
		private static bool IsPath(XRayRenderer.Prim p)
		{
			return p.Style >= XRayRenderer.Style.Incoming && p.Style <= XRayRenderer.Style.PelletExit;
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0015050E File Offset: 0x0014E70E
		private static int Steps(XRayRenderer.Prim p, int mp)
		{
			if (!XRayRenderer.IsPath(p))
			{
				if (p.Kind != XRayRenderer.Kind.Bone && p.Style != XRayRenderer.Style.Bone)
				{
					return mp;
				}
				return Math.Min(24, mp * 3);
			}
			else
			{
				if (p.Style != XRayRenderer.Style.Inside)
				{
					return 12;
				}
				return 24;
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00150544 File Offset: 0x0014E744
		private bool Spend(int n)
		{
			if (UiDraw.RectsThisFrame - this._start + n <= this._budget)
			{
				return true;
			}
			int lastCut = this.LastCut;
			this.LastCut = lastCut + 1;
			return false;
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x0015057A File Offset: 0x0014E77A
		// (set) Token: 0x060014CB RID: 5323 RVA: 0x00150582 File Offset: 0x0014E782
		public int LastCut { get; private set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x0015058B File Offset: 0x0014E78B
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x00150593 File Offset: 0x0014E793
		public int PlanCount { get; private set; }

		// Token: 0x060014CE RID: 5326 RVA: 0x0015059C File Offset: 0x0014E79C
		private void Seg(float x0, float y0, float x1, float y1, float t, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> c, int a, int steps = 0)
		{
			if (!UiDraw.Clip(ref x0, ref y0, ref x1, ref y1, this._clip.Item1, this._clip.Item2, this._clip.Item3, this._clip.Item4))
			{
				return;
			}
			int maxPieces = (steps > 0) ? steps : this._mp;
			if (!this.Spend(UiDraw.LineCost(x0, y0, x1, y1, t, maxPieces)))
			{
				return;
			}
			UiDraw.Line(x0, y0, x1, y1, t, c.Item1, c.Item2, c.Item3, a, maxPieces);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00150630 File Offset: 0x0014E830
		private void StepCamera(float dt)
		{
			dt = Math.Min(0.1f, Math.Max(0f, dt));
			this.Yaw += ((this._right > false) - (this._left > false)) * 100f * dt + (this.AutoSpin ? (25f * dt) : 0f);
			this.Pitch = Math.Max(-80f, Math.Min(80f, this.Pitch + ((this._up > false) - (this._down > false)) * 70f * dt));
			this.Zoom = Math.Max(0.6f, Math.Min(5f, this.Zoom * (1f + ((this._zoomIn > false) - (this._zoomOut > false)) * 1.3f * dt)));
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00150714 File Offset: 0x0014E914
		[return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static ValueTuple<int, int, int> StyleColor(XRayRenderer.Style s)
		{
			switch (s)
			{
			case XRayRenderer.Style.Bone:
				return XRayRenderer.BoneColor;
			case XRayRenderer.Style.Fine:
				return XRayRenderer.FineColor;
			case XRayRenderer.Style.Cartilage:
				return XRayRenderer.CartilageColor;
			case XRayRenderer.Style.Vessel:
				return new ValueTuple<int, int, int>(120, 55, 55);
			case XRayRenderer.Style.Incoming:
				return new ValueTuple<int, int, int>(245, 205, 80);
			case XRayRenderer.Style.Exit:
			case XRayRenderer.Style.PelletExit:
				return new ValueTuple<int, int, int>(240, 140, 50);
			case XRayRenderer.Style.PelletIn:
				return new ValueTuple<int, int, int>(215, 175, 70);
			case XRayRenderer.Style.PelletInside:
				return new ValueTuple<int, int, int>(230, 70, 50);
			}
			return XRayRenderer.BoneColor;
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x001507C0 File Offset: 0x0014E9C0
		[return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static ValueTuple<int, int, int> OrganColor(string key)
		{
			if (key == "heart")
			{
				return new ValueTuple<int, int, int>(205, 50, 60);
			}
			if (key.StartsWith("lung"))
			{
				return new ValueTuple<int, int, int>(195, 125, 145);
			}
			if (key == "liver")
			{
				return new ValueTuple<int, int, int>(160, 75, 50);
			}
			if (key == "stomach")
			{
				return new ValueTuple<int, int, int>(195, 145, 85);
			}
			if (key.StartsWith("kidney"))
			{
				return new ValueTuple<int, int, int>(165, 75, 75);
			}
			if (key == "intestines")
			{
				return new ValueTuple<int, int, int>(185, 145, 115);
			}
			if (key == "brain")
			{
				return new ValueTuple<int, int, int>(205, 175, 185);
			}
			if (key == "bladder")
			{
				return new ValueTuple<int, int, int>(175, 175, 95);
			}
			return new ValueTuple<int, int, int>(205, 85, 85);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x001509C4 File Offset: 0x0014EBC4
		[CompilerGenerated]
		internal static string <BuildAnimal>g__KeyOf|50_2(int id)
		{
			if (id == 14283 || id == 14284 || id == 14285 || id == 14286 || id == 21030)
			{
				return "cervical";
			}
			if (id != 14412 && id != 14413)
			{
				return "spine_l";
			}
			return "spine_t";
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00150A1C File Offset: 0x0014EC1C
		[CompilerGenerated]
		internal static V3 <BuildAnimal>g__P|50_4(float f, float r, float u, ref XRayRenderer.<>c__DisplayClass50_1 A_3)
		{
			return A_3.head + A_3.hf * (f * A_3.sl) + A_3.hr * (r * A_3.sl) + A_3.hu * (u * A_3.sl);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00150A77 File Offset: 0x0014EC77
		[CompilerGenerated]
		internal static V3 <BuildSkull>g__P|52_0(float f, float r, float u, ref XRayRenderer.<>c__DisplayClass52_0 A_3)
		{
			return A_3.hc + A_3.hf * f + A_3.hr * r + A_3.hu * u;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00150AB2 File Offset: 0x0014ECB2
		[CompilerGenerated]
		internal static bool <MarkHits>g__Skeletal|56_3(XRayRenderer.Prim p)
		{
			return p.Key != null && (p.Style == XRayRenderer.Style.Bone || p.Style == XRayRenderer.Style.Fine);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00150AD1 File Offset: 0x0014ECD1
		[CompilerGenerated]
		internal static bool <Plan>g__BoneDot|92_3(XRayRenderer.Prim p)
		{
			return p.Kind == XRayRenderer.Kind.Dot && p.Style == XRayRenderer.Style.Bone;
		}

		// Token: 0x04000E94 RID: 3732
		private const int SphereSegs = 18;

		// Token: 0x04000E95 RID: 3733
		private int _mp = 6;

		// Token: 0x04000E96 RID: 3734
		private List<XRayRenderer.Prim> _prims = new List<XRayRenderer.Prim>();

		// Token: 0x04000E97 RID: 3735
		private readonly Dictionary<XRayQuality, List<XRayRenderer.Prim>> _levels = new Dictionary<XRayQuality, List<XRayRenderer.Prim>>();

		// Token: 0x04000E98 RID: 3736
		private XRayQuality _preference = XRayQuality.Auto;

		// Token: 0x04000E9A RID: 3738
		private List<V3> _framePoints = new List<V3>();

		// Token: 0x04000E9B RID: 3739
		private V3 _center;

		// Token: 0x04000E9C RID: 3740
		private V3 _focus;

		// Token: 0x04000E9D RID: 3741
		private float _baseYaw;

		// Token: 0x04000E9E RID: 3742
		private float _fit = -1f;

		// Token: 0x04000E9F RID: 3743
		public float Yaw;

		// Token: 0x04000EA0 RID: 3744
		public float Pitch = 8f;

		// Token: 0x04000EA1 RID: 3745
		public float Zoom = 1f;

		// Token: 0x04000EA2 RID: 3746
		public float PanX;

		// Token: 0x04000EA3 RID: 3747
		public float PanY;

		// Token: 0x04000EA4 RID: 3748
		public bool AutoSpin;

		// Token: 0x04000EA7 RID: 3751
		private bool _left;

		// Token: 0x04000EA8 RID: 3752
		private bool _right;

		// Token: 0x04000EA9 RID: 3753
		private bool _up;

		// Token: 0x04000EAA RID: 3754
		private bool _down;

		// Token: 0x04000EAB RID: 3755
		private bool _zoomIn;

		// Token: 0x04000EAC RID: 3756
		private bool _zoomOut;

		// Token: 0x04000EAD RID: 3757
		private const float CameraDistance = 3.2f;

		// Token: 0x04000EAE RID: 3758
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static readonly ValueTuple<int, int, int> BoneColor = new ValueTuple<int, int, int>(222, 230, 234);

		// Token: 0x04000EAF RID: 3759
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static readonly ValueTuple<int, int, int> FineColor = new ValueTuple<int, int, int>(185, 205, 214);

		// Token: 0x04000EB0 RID: 3760
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static readonly ValueTuple<int, int, int> CartilageColor = new ValueTuple<int, int, int>(105, 135, 146);

		// Token: 0x04000EB1 RID: 3761
		[TupleElementNames(new string[]
		{
			"x0",
			"y0",
			"x1",
			"y1"
		})]
		private ValueTuple<float, float, float, float> _clip;

		// Token: 0x04000EB2 RID: 3762
		private XRayRenderer.Cam _lastCam;

		// Token: 0x04000EB3 RID: 3763
		private static float _zoomThick = 1f;

		// Token: 0x04000EB4 RID: 3764
		[TupleElementNames(new string[]
		{
			"x0",
			"y0",
			"x1",
			"y1"
		})]
		private static ValueTuple<float, float, float, float>? _costClip;

		// Token: 0x04000EB5 RID: 3765
		[TupleElementNames(new string[]
		{
			"level",
			"mp",
			"rich",
			"planned",
			"est",
			"richCost"
		})]
		private readonly Dictionary<string, ValueTuple<XRayQuality, int, bool, HashSet<XRayRenderer.Prim>, int, int>> _planCache = new Dictionary<string, ValueTuple<XRayQuality, int, bool, HashSet<XRayRenderer.Prim>, int, int>>();

		// Token: 0x04000EB6 RID: 3766
		private int _planCacheBuild = -1;

		// Token: 0x04000EB7 RID: 3767
		private bool _rich;

		// Token: 0x04000EB8 RID: 3768
		public int LastSpentEstimate;

		// Token: 0x04000EB9 RID: 3769
		public int LastRichCost;

		// Token: 0x04000EBA RID: 3770
		private int _buildId;

		// Token: 0x04000EBB RID: 3771
		private int _planMp;

		// Token: 0x04000EBC RID: 3772
		private bool _planRich;

		// Token: 0x04000EBD RID: 3773
		private string _planKey;

		// Token: 0x04000EBE RID: 3774
		private HashSet<XRayRenderer.Prim> _planned = new HashSet<XRayRenderer.Prim>();

		// Token: 0x04000EBF RID: 3775
		private const int PathSteps = 12;

		// Token: 0x04000EC0 RID: 3776
		private int _start;

		// Token: 0x04000EC1 RID: 3777
		private int _budget;

		// Token: 0x04000EC4 RID: 3780
		public static Action<string> Diag;

		// Token: 0x04000EC5 RID: 3781
		private int _replanLogAt = -100000;

		// Token: 0x020004AA RID: 1194
		private enum Kind
		{
			// Token: 0x040020EC RID: 8428
			Line,
			// Token: 0x040020ED RID: 8429
			Bone,
			// Token: 0x040020EE RID: 8430
			Dot,
			// Token: 0x040020EF RID: 8431
			Ring,
			// Token: 0x040020F0 RID: 8432
			Sphere
		}

		// Token: 0x020004AB RID: 1195
		private enum Style
		{
			// Token: 0x040020F2 RID: 8434
			Bone,
			// Token: 0x040020F3 RID: 8435
			Fine,
			// Token: 0x040020F4 RID: 8436
			Cartilage,
			// Token: 0x040020F5 RID: 8437
			Organ,
			// Token: 0x040020F6 RID: 8438
			Vessel,
			// Token: 0x040020F7 RID: 8439
			Incoming,
			// Token: 0x040020F8 RID: 8440
			Inside,
			// Token: 0x040020F9 RID: 8441
			Exit,
			// Token: 0x040020FA RID: 8442
			PelletIn,
			// Token: 0x040020FB RID: 8443
			PelletInside,
			// Token: 0x040020FC RID: 8444
			PelletExit,
			// Token: 0x040020FD RID: 8445
			EntryMark,
			// Token: 0x040020FE RID: 8446
			StopMark,
			// Token: 0x040020FF RID: 8447
			BruiseMark,
			// Token: 0x04002100 RID: 8448
			KickMark,
			// Token: 0x04002101 RID: 8449
			GunMark,
			// Token: 0x04002102 RID: 8450
			ChokeMark
		}

		// Token: 0x020004AC RID: 1196
		private sealed class Prim
		{
			// Token: 0x04002103 RID: 8451
			public XRayRenderer.Kind Kind;

			// Token: 0x04002104 RID: 8452
			public XRayRenderer.Style Style;

			// Token: 0x04002105 RID: 8453
			public V3 A;

			// Token: 0x04002106 RID: 8454
			public V3 B;

			// Token: 0x04002107 RID: 8455
			public float R;

			// Token: 0x04002108 RID: 8456
			public V3[] Pts;

			// Token: 0x04002109 RID: 8457
			public bool Open;

			// Token: 0x0400210A RID: 8458
			public string Key;

			// Token: 0x0400210B RID: 8459
			public int Priority;

			// Token: 0x0400210C RID: 8460
			public bool Hit;

			// Token: 0x0400210D RID: 8461
			public bool Fracture;

			// Token: 0x0400210E RID: 8462
			public V3 HitAt;

			// Token: 0x0400210F RID: 8463
			[TupleElementNames(new string[]
			{
				"r",
				"g",
				"b"
			})]
			public ValueTuple<int, int, int> Color;

			// Token: 0x04002110 RID: 8464
			public float Depth;

			// Token: 0x04002111 RID: 8465
			public bool Drawn;
		}

		// Token: 0x020004AD RID: 1197
		private struct Cam
		{
			// Token: 0x04002112 RID: 8466
			public V3 C;

			// Token: 0x04002113 RID: 8467
			public V3 S;

			// Token: 0x04002114 RID: 8468
			public V3 U;

			// Token: 0x04002115 RID: 8469
			public V3 F;

			// Token: 0x04002116 RID: 8470
			public float Scale;

			// Token: 0x04002117 RID: 8471
			public float Cx;

			// Token: 0x04002118 RID: 8472
			public float Cy;
		}
	}
}
