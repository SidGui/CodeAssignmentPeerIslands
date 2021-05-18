namespace CodeAssignmentPeerIslands.Domain
{
    public class Column
    {
        public string Operator { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string TableOrigin { get; set; }
        public string Alias { get; set; }
        public string StartValue { get; set; }
        public string EndValue { get; set; }
        public bool IsTableFieldValue { get; set; }
    }
}
