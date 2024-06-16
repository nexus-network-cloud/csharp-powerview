// See https://aka.ms/new-console-template for more information

using NexusNetworkCloud.CsharpPowerview;
using NexusNetworkCloud.CsharpPowerView.Api.Shades;
using System.Net;

IPowerViewApiClient powerViewApiClient = new PowerViewApiClient(IPAddress.Parse("192.168.255.110"), new HttpClient());

var shadeResult = await powerViewApiClient.Api.Shades.SetPositionAsync(new(51133, new() { PosKind1 = PositionKind.PrimaryRail, PosKind2 = PositionKind.SecondaryRail, PositionOne = 0, PositionTwo = 15000 }));

Console.WriteLine("Done!");
