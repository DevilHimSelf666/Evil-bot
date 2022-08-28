namespace IkEvil
{
    public class BotSetting
    {
        public string ApplicationId { get; set; }
        public int Permissions { get; set; }
        public string Scope { get; set; }
        public string Token { get; set; }
        public BotSetting()
        {
            ApplicationId = "";
            Scope = "";
            Token = "";

        }
    }
}
