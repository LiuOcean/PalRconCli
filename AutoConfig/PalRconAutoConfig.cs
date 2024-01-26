using Newtonsoft.Json;
using Spectre.Console;

namespace CliToolkit.AutoConfig;

[Serializable]
[AutoConfig(false, false)]
public class PalRconAutoConfig : IAutoConfig
{
    [JsonProperty] public string server_addr { get; private set; }
    [JsonProperty] public int    server_port { get; private set; }

    [JsonProperty] public string password { get; private set; }

    public bool CheckPrompt()
    {
        bool changed = false;
        if(string.IsNullOrEmpty(server_addr))
        {
            server_addr = AnsiConsole.Ask<string>("请输入服务器地址: ");
            changed     = true;
        }

        if(server_port == 0)
        {
            server_port = AnsiConsole.Ask("请输入端口号: ", 25575);
            changed     = true;
        }

        if(string.IsNullOrEmpty(password))
        {
            password = AnsiConsole.Ask<string>("请输入服务器密码: ");
            changed  = true;
        }

        return changed;
    }
}