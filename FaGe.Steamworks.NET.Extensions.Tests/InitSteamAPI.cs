using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaGe.Steamworks.NET.Extensions.Tests
{
	[SetUpFixture]
	public class InitSteamAPI
	{
		[OneTimeSetUp]
		public void SetupSteamAPI()
		{
			ESteamAPIInitResult clInit = SteamAPI.InitEx(out string? clErr);
			ESteamAPIInitResult gsInit = GameServer.InitEx(0, 12450, 12451, EServerMode.eServerModeNoAuthentication, "S.NET Automated Test", out string gsErr);

			Assert.Multiple(() =>
			{
				Assert.That(clInit, Is.EqualTo(ESteamAPIInitResult.k_ESteamAPIInitResult_OK), clErr);
				Assert.That(gsInit, Is.EqualTo(ESteamAPIInitResult.k_ESteamAPIInitResult_OK), gsErr);
			});
		}

		[OneTimeTearDown]
		public void Shutdown()
		{
			SteamAPI.Shutdown();
			GameServer.Shutdown();
		}
	}
}
