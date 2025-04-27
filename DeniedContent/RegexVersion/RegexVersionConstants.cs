using System.Text.RegularExpressions;

namespace Benchmarks.DeniedContent.RegexVersion
{
    public static class RegexVersionConstants
    {
        public const string GroupPrefix = "g_";
        public static string CharReplacementsStr = "{\"a\":\"(4|e|@|&|a)\",\"b\":\"(6|8|d|p|b)\",\"c\":\"(6|g|c)\",\"d\":\"(b|0|o|q|d)\",\"e\":\"(a|3|£|€|e)\",\"f\":\"(t|e|f)\",\"g\":\"(6|9|q|c|g)\",\"h\":\"(4|a|h)\",\"i\":\"(1|j|l|!|\\\\||i)\",\"j\":\"(1|i|l|!|\\\\||j)\",\"k\":\"(x|k)\",\"l\":\"(1|i|j|!|\\\\||l)\",\"m\":\"(n|nn|rn|m)\",\"n\":\"(m|n)\",\"o\":\"(0|d|q|o)\",\"p\":\"(q|p)\",\"q\":\"(0|9|o|g|q)\",\"r\":\"(n|r)\",\"s\":\"(5|\\\\$|s)\",\"t\":\"(7|f|t)\",\"u\":\"(v|u)\",\"v\":\"(u|v)\",\"w\":\"(vv|w)\",\"x\":\"(k|x)\",\"y\":\"(y)\",\"z\":\"(2|7|z)\",\"0\":\"(o|d|q|0)\",\"1\":\"(i|j|l|!|\\\\||1)\",\"2\":\"(z|2)\",\"3\":\"(e|£|€|3)\",\"4\":\"(a|h|4)\",\"5\":\"(s|\\\\$|5)\",\"6\":\"(9|b|g|6)\",\"7\":\"(t|z|7)\",\"8\":\"(b|8)\",\"9\":\"(g|q|9)\",\"!\":\"(1|i|j|l|\\\\||!)\",\"|\":\"(1|i|j|l|!|\\\\|)\",\"@\":\"(a|@)\",\"£\":\"(3|e|€|£)\",\"$\":\"(5|s|\\\\$)\",\"€\":\"(3|e|£|€)\",\"\\\"\":\"('|\\\")\",\"'\":\"(\\\"|')\",\"<\":\"(>|<)\",\">\":\"(<|>)\"}";

        public static string BaseDeniedSendersStr = "\r\n[\"admin\",\"otpsms\",\"mobile\",\"bank\",\"app\",\"validate\",\"confirm\",\"allowance\",\"help\",\"post\",\"isa\",\"check\",\"courier\",\"smscode\",\"deliver\",\"bill\",\"notify\",\"verifysms\",\"parcel\",\"verify\",\"save\",\"secure\",\"tracking\",\"payment\",\"pay\",\"advise\",\"collect\",\"update\",\"trust\",\"pin\",\"auth\",\"service\",\"package\",\"scam\",\"ratify\",\"message\",\"logon\",\"shipping\",\"login\",\"control\",\"infosms\",\"contact\",\"key\",\"reminder\",\"banking\",\"order\",\"approve\",\"support\",\"energy\",\"updates\",\"network\",\"collection\",\"account\",\"reply\",\"pincode\",\"onetimepin\",\"code\",\"sms\",\"certify\",\"warning\",\"msg\",\"cloudotp\",\"aware\",\"mortgage\",\"info\",\"msgauth\",\"authorise\",\"loan\",\"active\",\"winner\",\"rebate\",\"smsinfo\",\"smsverfiy\",\"malware\",\"trace\",\"access\",\"card\",\"noreply\",\"call\",\"delivery\",\"alert\",\"saving\",\"signon\",\"verifyme\",\"track\",\"sign\",\"virus\",\"allow\",\"otp\",\"appointment\",\"security\",\"repayment\",\"purchase\",\"delay\",\"caution\",\"protocol\",\"authmsg\",\"approved\",\"system\",\"accept\",\"refund\",\"1timepin\",\"warn\",\"signin\",\"smsotp\",\"2fa\",\"receipt\",\"respond\",\"logistics\",\"savings\",\"discount\",\"text\",\"authsms\",\"billing\",\"schedule\",\"logmein\",\"fraud\",\"smsauth\",\"txt\",\"belen\"]";


        public static readonly List<string> SpacerCharacters = new()
        {
            " ",
            ",",
            ".",
            "_",
            "-"
        };

        public static readonly string SpacerPattern = $"({string.Join("|", SpacerCharacters.Select(Regex.Escape))})*";

    }
}
