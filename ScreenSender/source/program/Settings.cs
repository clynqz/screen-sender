using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSender;

public static class Settings
{
    public static readonly string BotToken;

    static Settings()
    {
        BotToken = Secret.BotToken;
    }
}
