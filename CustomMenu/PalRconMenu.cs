using CliToolkit.AutoConfig;
using CliToolkit.Menus;
using CliToolkit.Tools;
using Spectre.Console;

namespace CliToolkit.CustomMenu;

[MenuEnter("帕鲁")]
public class PalRconEnter : AMenuEnter
{
    protected override async Task _WhenEnter(CancellationToken token)
    {
        // 检测链接
        using var client = RconUtils.GetClient();

        await Task.CompletedTask;
    }
}

[Menu("帕鲁/查看玩家")]
public class PalShowPlayers : AMenuExecute
{
    protected override async Task _Execute(CancellationToken token)
    {
        var players = RconUtils.ShowPlayers();

        var table = new Table();
        table.AddColumn("序号");
        table.AddColumn("玩家");
        table.AddColumn("uid");
        table.AddColumn("Steam ID");

        for(var i = 0; i < players.Count; i++)
        {
            var player = players[i];
            var color  = i % 2 == 0 ? CustomColor.Blue : CustomColor.Pink;

            table.AddRow(
                $"{(i + 1).ToString().WithColor(color)}",
                player.name.WithColor(color),
                player.uid.WithColor(color),
                player.steam_id.WithColor(color)
            );
        }

        AnsiConsole.Write(table);

        await Task.CompletedTask;
    }
}

[Menu("帕鲁/传送到好友")]
public class PalTeleport2Player : AMenuExecute
{
    protected override async Task _Execute(CancellationToken token)
    {
        if(!RconUtils.SelectPlayer(out var id))
        {
            return;
        }

        var result = RconUtils.SendMsg($"teleporttoplayer {id}");

        AnsiConsole.WriteLine($"执行结果: {result}");

        await Task.CompletedTask;
    }
}

[Menu("帕鲁/好友传送到我")]
public class PalPlayer2Me : AMenuExecute
{
    protected override async Task _Execute(CancellationToken token)
    {
        if(!RconUtils.SelectPlayer(out var me))
        {
            return;
        }

        if(!RconUtils.SelectPlayer(out var player))
        {
            return;
        }

        var result = RconUtils.SendMsg($"teleporttome {me} {player}");

        AnsiConsole.WriteLine($"执行结果: {result}");

        await Task.CompletedTask;
    }
}

[Menu("帕鲁/服务器信息")]
public class PalInfo : AMenuExecute
{
    protected override async Task _Execute(CancellationToken token)
    {
        var info = RconUtils.SendMsg("info");

        AnsiConsole.WriteLine(info);

        await Task.CompletedTask;
    }
}

[Menu("帕鲁/存档")]
public class PalSave : AMenuExecute
{
    protected override async Task _Execute(CancellationToken token)
    {
        var info = RconUtils.SendMsg("save");

        AnsiConsole.WriteLine(info);

        await Task.CompletedTask;
    }
}

[Menu("帕鲁/广播")]
public class PalBroadcast : AMenuExecute
{
    protected override async Task _Execute(CancellationToken token)
    {
        var msg = AnsiConsole.Prompt(new TextPrompt<string>("请输入广播内容: "));

        var info = RconUtils.SendMsg($"broadcast {msg}");

        AnsiConsole.WriteLine(info);

        await Task.CompletedTask;
    }
}

[Menu("帕鲁/踢人")]
public class PalKickPlayer : AMenuExecute
{
    protected override async Task _Execute(CancellationToken token)
    {
        if(!RconUtils.SelectPlayer(out var id))
        {
            return;
        }

        var result = RconUtils.SendMsg($"kickplayer {id}");

        AnsiConsole.WriteLine($"执行结果: {result}");

        await Task.CompletedTask;
    }
}

[Menu("帕鲁/计划停服")]
public class PalShutdown : AMenuExecute
{
    protected override async Task _Execute(CancellationToken token)
    {
        var seconds = AnsiConsole.Prompt(new TextPrompt<int>("请输入停服时间(秒): "));
        var msg     = AnsiConsole.Prompt(new TextPrompt<string>("请输入停服提示: "));

        var result = RconUtils.SendMsg($"shutdown {seconds} {msg}");

        AnsiConsole.WriteLine($"执行结果: {result}");

        await Task.CompletedTask;
    }
}

[Menu("帕鲁/停服")]
public class PalExit : AMenuExecute
{
    protected override async Task _Execute(CancellationToken token)
    {
        var result = RconUtils.SendMsg("doexit");

        AnsiConsole.WriteLine($"执行结果: {result}");

        await Task.CompletedTask;
    }
}

[Menu("帕鲁/Ban")]
public class PalBanPlayer : AMenuExecute
{
    protected override async Task _Execute(CancellationToken token)
    {
        if(!RconUtils.SelectPlayer(out var id))
        {
            return;
        }

        var result = RconUtils.SendMsg($"banplayer {id}");

        AnsiConsole.WriteLine($"执行结果: {result}");

        await Task.CompletedTask;
    }
}