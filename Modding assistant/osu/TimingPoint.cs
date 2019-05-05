using System;
using System.Collections.Generic;
using Modding_assistant.Utility;

namespace osu
{
	// Token: 0x02000037 RID: 55
	public class TimingPoint
	{
		// Token: 0x06000260 RID: 608 RVA: 0x0001EFB4 File Offset: 0x0001D1B4
		public TimingPoint(string str)
		{
			List<string> list = new List<string>(str.Split(new char[]
			{
				','
			}));
			if (list.Count >= 7)
			{
				float num = 0f;
				int value = 0;
				float.TryParse(list[0].Trim(), out num);
				this.time = Convert.ToInt32(Math.Floor((double)num));
				double.TryParse(list[1].Trim(), out this.msPerBeat);
				int.TryParse(list[2].Trim(), out this.measure);
				int.TryParse(list[3].Trim(), out value);
				this.HSsetType = (HitsoundSetType)Enum.ToObject(typeof(HitsoundSetType), value);
				int.TryParse(list[4].Trim(), out this.HSsetNumber);
				float.TryParse(list[5].Trim(), out num);
				this.volume = Convert.ToInt32(num);
				int.TryParse(list[6].Trim(), out value);
				this.unInherited = Convert.ToBoolean(value);
				if (list.Count >= 8)
				{
					Enum.TryParse<KiaiType>(list[7].Trim(), true, out this.kiai);
				}
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0001F0F8 File Offset: 0x0001D2F8
		public TimingPoint()
		{
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0001F107 File Offset: 0x0001D307
		public float BpmMultiplier
		{
			get
			{
				if (this.msPerBeat >= 0.0)
				{
					return 1f;
				}
				return ut_Math.Clamp((float)(-(float)this.msPerBeat), 10f, 1000f) / 100f;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0001F140 File Offset: 0x0001D340
		public override string ToString()
		{
			TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)this.time);
			return string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0001F188 File Offset: 0x0001D388
		public float GetBpm()
		{
			return Convert.ToSingle(Math.Round(60000.0 / this.msPerBeat, 3));
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0001F1A5 File Offset: 0x0001D3A5
		public float GetSV()
		{
			return Convert.ToSingle(Math.Round(100.0 / Math.Abs(this.msPerBeat), 2));
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0001F1C7 File Offset: 0x0001D3C7
		public string GetTypeStr()
		{
			if (this.unInherited)
			{
				return "Uninherited (red)";
			}
			return "Inherited (green)";
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0001F1DC File Offset: 0x0001D3DC
		public string GetSettingsStr()
		{
			string text = string.Empty;
			if (this.kiai == KiaiType.None)
			{
				text = "None";
			}
			if (this.kiai.HasFlag(KiaiType.Kiai))
			{
				text = "Kiai";
			}
			if (this.kiai.HasFlag(KiaiType.OmitFirstBarLine))
			{
				if (text != string.Empty)
				{
					text += " | ";
				}
				text += "Omit first bar line";
			}
			return text;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0001F259 File Offset: 0x0001D459
		public string GetHSSetStr()
		{
			if (this.HSsetType == HitsoundSetType.Normal)
			{
				return "Normal";
			}
			if (this.HSsetType == HitsoundSetType.Drum)
			{
				return "Drum";
			}
			if (this.HSsetType == HitsoundSetType.Soft)
			{
				return "Soft";
			}
			return "Auto";
		}

		// Token: 0x040001CB RID: 459
		public int time;

		// Token: 0x040001CC RID: 460
		public double msPerBeat;

		// Token: 0x040001CD RID: 461
		public int measure;

		// Token: 0x040001CE RID: 462
		public HitsoundSetType HSsetType;

		// Token: 0x040001CF RID: 463
		public int HSsetNumber;

		// Token: 0x040001D0 RID: 464
		public int volume;

		// Token: 0x040001D1 RID: 465
		public bool unInherited = true;

		// Token: 0x040001D2 RID: 466
		public KiaiType kiai;
	}
}
