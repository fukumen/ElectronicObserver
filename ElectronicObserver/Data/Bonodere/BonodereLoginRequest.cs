﻿using System.Text.Json.Serialization;

namespace ElectronicObserver.Data.Bonodere;

public class BonodereLoginRequest
{
	[JsonPropertyName("key")]
	public required string Key { get; set; }

	[JsonPropertyName("password")]
	public required string Password { get; set; }

	[JsonPropertyName("duration")]
	public required int Duration { get; set; }
}
