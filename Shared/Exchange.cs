using System;

namespace StonksApplication.Shared
{
	public class Exchange
	{
		public string exchange_id { get; set; }
		public string website { get; set; }
		public string name { get; set; }
		public string data_start { get; set; }
		public string data_end { get; set; }
		public DateTime data_quote_start { get; set; }
		public DateTime data_quote_end { get; set; }
		public DateTime data_orderbook_start { get; set; }
		public DateTime data_orderbook_end { get; set; }
		public DateTime data_trade_start { get; set; }
		public DateTime data_trade_end { get; set; }
		public int data_symbols_count { get; set; }
		public double volume_1hrs_usd { get; set; }
		public double volume_1day_usd { get; set; }
		public double volume_1mth_usd { get; set; }
	}

}
