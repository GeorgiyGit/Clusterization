using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL;
using static OpenAI.ObjectModels.SharedModels.IOpenAiModels;

namespace Domain.Services.DataSources.Telegram
{
    public class WTelegramService : BackgroundService
    {
        public readonly WTelegram.Client Client;
        public User User => Client.User;
        public string ConfigNeeded = "connecting";

        private readonly IConfiguration _config;

        public WTelegramService(IConfiguration config, ILogger<WTelegramService> logger)
        {
            _config = config;
            WTelegram.Helpers.Log = (lvl, msg) => logger.Log((LogLevel)lvl, msg);
            Client = new WTelegram.Client(int.Parse(_config["TelegramOptions:ApiId"]), _config["TelegramOptions:ApiHash"]);
        }

        public override void Dispose()
        {
            Client.Dispose();
            base.Dispose();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ConfigNeeded = await DoLogin(_config["TelegramOptions:PhoneNumber"]);
        }

        public async Task<string> DoLogin(string loginInfo)
        {
            return ConfigNeeded = await Client.Login(loginInfo);
        }
    }
}
