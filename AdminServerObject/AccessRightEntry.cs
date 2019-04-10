namespace AdminServerObject
{
    public class AccessRightEntry
    {
        public string entryId { get; set; } = "";
        public string permission { get; set; } = "";
        public string virtualDir { get; set; } = "/";
        public string physicalDir { get; set; }= "/";
    }
}