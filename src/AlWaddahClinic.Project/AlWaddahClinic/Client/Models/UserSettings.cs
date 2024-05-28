using System;
using MudBlazor;

namespace AlWaddahClinic.Client.Models;

public class UserSettings
{
	public string? MainMessage { get; set; } = "Hello Doctor!";
	public string? BackgroundColor { get; set; } = "white";
	public string? NavbarColor { get; set; } = "#7F71FF";
}


