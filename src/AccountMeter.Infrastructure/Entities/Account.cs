﻿namespace AccountMeter.API.Entities;

public partial class Account
{
    public int AccountId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<MeterReading> MeterReadings { get; set; } = new List<MeterReading>();
}
