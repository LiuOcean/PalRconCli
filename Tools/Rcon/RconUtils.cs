using CliToolkit.AutoConfig;
using Spectre.Console;

namespace CliToolkit.Tools;

public class PalUserInfo
{
    public string name;
    public string uid;
    public string steam_id;

    public PalUserInfo(string row)
    {
        var splits = row.Split(',');
        name     = splits[0];
        uid      = splits[1];
        steam_id = splits[2];
    }
}

public static class RconUtils
{
    public static RconClient GetClient()
    {
        var config = AutoConfigUtils.GetAutoConfig<PalRconAutoConfig>();
        var result = new RconClient(config.server_addr, config.server_port);

        if(!result.Authenticate(config.password))
        {
            throw new Exception("password is incorrect");
        }

        return result;
    }

    public static bool SelectPlayer(out string steam_id)
    {
        steam_id = string.Empty;

        var players = ShowPlayers();

        if(players.Count <= 0)
        {
            AnsiConsole.WriteLine("没有玩家在线".WithColor(CustomColor.Red));
            return false;
        }

        var player = AnsiConsole.Prompt(
            new SelectionPrompt<PalUserInfo>().AddChoices(players).UseConverter(p => p.name)
        );

        steam_id = player.steam_id;
        return true;
    }

    public static List<PalUserInfo> ShowPlayers()
    {
        var result = new List<PalUserInfo>();

        using var client = GetClient();

        client.SendCommand("showplayers", out var resp);

        if(string.IsNullOrEmpty(resp.Body))
        {
            return result;
        }

        var lines = resp.Body.Split('\n');

        if(lines.Length <= 1)
        {
            return result;
        }

        for(int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];

            if(string.IsNullOrEmpty(line))
            {
                continue;
            }

            result.Add(new PalUserInfo(line));
        }

        return result;
    }

    public static string SendMsg(string command)
    {
        using var client = GetClient();

        client.SendCommand(command, out var resp);

        return resp.Body;
    }
}