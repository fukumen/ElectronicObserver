﻿using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ElectronicObserver.Window.Tools.EventLockPlanner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ElectronicObserver.Database;

public class ElectronicObserverContext : DbContext
{
	public DbSet<EventLockPlannerModel> EventLockPlans { get; set; } = null!;

	private string DbPath { get; }

	public ElectronicObserverContext()
	{
		DbPath = Path.Join("Record", "ElectronicObserver.sqlite");
	}

	protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlite($"Data Source={DbPath}");

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<EventLockPlannerModel>()
			.HasKey(s => s.Id);

		builder.Entity<EventLockPlannerModel>()
			.Property(s => s.Locks)
			.HasConversion(JsonConverter<List<EventLockModel>>());

		builder.Entity<EventLockPlannerModel>()
			.Property(s => s.ShipLocks)
			.HasConversion(JsonConverter<List<ShipLockModel>>());

		builder.Entity<EventLockPlannerModel>()
			.Property(s => s.Phases)
			.HasConversion(JsonConverter<List<EventPhaseModel>>());
	}

	private static ValueConverter<T, string> JsonConverter<T>() where T : new() => new
	(
		list => JsonSerializer.Serialize(list, (JsonSerializerOptions?)null),
		s => FromJson<T>(s)
	);

	private static T FromJson<T>(string s) where T : new() => s switch
	{
		null or "" => new T(),
		_ => JsonSerializer.Deserialize<T>(s)!
	};
}