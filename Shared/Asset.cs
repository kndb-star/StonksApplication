namespace StonksApplication.Shared
{
	public class Asset
	{
		public string asset_id { get; set; }
		public string name { get; set; }
		public int type_is_crypto { get; set; }
		public double volume_1hrs_usd { get; set; }
		public double volume_1day_usd { get; set; }
		public double volume_1mth_usd { get; set; }
		public double price_usd { get; set; }
		public string url { get; set; }
	}
}
