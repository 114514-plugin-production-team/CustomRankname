# CustomRankname
简述: 给玩家自定义的称号/彩称
## 安装教程
将"CustomRankname.dll"下载并复制到<ins>"%appdata%\SCP Secret Laboratory\LabAPI\plugins\{服务器端口号}"</ins>,然后重启服务器,控制台会反馈玩家称号信息文件目录,如" [CustomRank]文件已创建,目录[C:\Users\Administrator\AppData\Roaming\SCP Secret Laboratory\LabAPI\CustRank\7777\player.json]"\
**注意:插件需要"Newtonsoft.Json.dll"作为依赖，请将"Newtonsoft.Json.dll"放在"%appdata%\SCP Secret Laboratory\LabAPI\plugins\global"里**
### 使用教程
打开控制台反馈的目录文件,发现里面的内容\
```
[{"UserId":"114514@steam","Badge":"这是一个单色称号","BadgeColor":"red"},{"UserId":"114514@steam","Badge":"这是一个彩色称号","BadgeColor":"rainbow"}]
```\
学过Json的入都知道\
不知道的先打开[Json解压](https://www.json.cn/),左边输入内容，回车得\
```
[
    {
        "UserId": "114514@steam",
        "Badge": "这是一个单色称号",
        "BadgeColor": "red"
    },
    {
        "UserId": "114514@steam",
        "Badge": "这是一个彩色称号",
        "BadgeColor": "rainbow"
    }
]
```\
看懂了吗,把"UserId":"114514@steam"中的114514@steam改成你的/他人的,"Badge": "这是一个单色称号"中的这是一个单色称号改成你/他想要的称号，"BadgeColor": "red"这里如果填的是rainbow那这个人的称号就是彩色的,相反填一个颜色单词则为普通颜色\
然后把改好的复制到文件里，玩家重进服务器即可\
有多个就复制
```
{
    "UserId": "114514@steam",
    "Badge": "这是一个单色称号",
    "BadgeColor": "red"
},
```多个\
**注意:最后一个Json组末尾不能有逗号，然后每个Json组不能删去双引号(英文双引号和英文逗号)**
#### 联系
QQ群:807254783\
插件作者QQ:3145186196
