namespace Librum.Settings
{
    public class LibrumSettings
    {
        public string BlogName { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Icon { get; set; }
        public string CanonicalUri { get; set; }
        public object Author { get; set; }
        public object Configuration { get; set; }
        public object UsersStore { get; set; }
        public string DisqusHostname { get; set; }
    }
}