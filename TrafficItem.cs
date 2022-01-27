using System;

namespace keeema.aquapcetraffic
{
    class TrafficItem
    {
        public string Place { get; set; }
        public DateTime TimeStamp { get; set; }

        public int Count { get; set; }

        public override string ToString()
        {
            return $"Place: {this.Place}, Count: {this.Count}, Timestamp (UTC):{this.TimeStamp}";
        }

    }
}