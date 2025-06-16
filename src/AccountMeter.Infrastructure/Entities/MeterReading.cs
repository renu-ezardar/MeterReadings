﻿namespace AccountMeter.API.Entities;

public partial class MeterReading
{
    public int ReadingId { get; set; }

    public int AccountId { get; set; }

    public DateTime MeterReadingDateTime { get; set; }

    public int MeterReadValue { get; set; }

    public virtual Account Account { get; set; } = null!;
}
